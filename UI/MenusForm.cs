#region Copyright
// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © FortuNet dba GameTech
// International, Inc.
//
// US3692 Adding support for whole points
#endregion

using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTI.Controls;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Properties;
using GTI.Modules.ProductCenter.Data;
using GTI.Modules.Shared.Business;
using GTI.Modules.Shared.Data;
using GTI.Modules.ProductCenter.Business;
using System.ComponentModel;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class MenusForm : GradientForm
    {
        #region Constants and Data Types
        protected DisplayMode DisplayMode = new NormalDisplayMode();
        protected const string ALL_DEVICES_SELECTED = "[All]";
        protected const string NO_DEVICES_SELECTED = "[None]";
        protected const string MANY_DEVICES_SELECTED = "Multiple";
        protected const int MENUS_BASE_NODE_IDX = 0; // the base node index for the future menus
        protected const int DAILY_MENUS_BASE_NODE_IDX = 1; // the base node index for the daily menus
        #endregion Constants and Data types

        #region Member Variables
        private static MenuDetailForm menuDetailForm;
        private static ButtonDetailForm buttonDetailForm;
        private static MenuButtonList draggedButton;
        private static bool isDragging;
        private static POSMenuItem copiedMenu;
        private static PageItem copiedPage;
        private static ImageButton copiedButton = new ImageButton();
        private int m_operatorId;
        private List<ButtonGraphic> graphics;
        private List<Device> m_devicesForPage; // DE13556 NOTE: This should only be set in the UpdateDevicesUI(), but can be read any time after.
        private Dictionary<POSMenuItem, List<DailyMenuButton>> m_dailyMenus;
        private TreeNode dailyRootNode;
        private List<CardMediaListItem> m_cardMediaList;    // US1772
        private List<GameCategory> m_gameCategoryList;      // US1772
        private List<GameTypeListItem> m_gameTypeList;      // US1772
        private List<CardLevelItem> m_cardLevelList;        // US1772
        private WaitForm m_waitForm;
        private BackgroundWorker saveAndLoadWorker;         // US1772
        private BackgroundWorker loadWorker;                // US1772
        private bool m_canEditDaily = false;                // US1772
        #endregion Member Variables

        #region Member Properties

        /// <summary>
        /// The settings for this application
        /// </summary>
        public ProductCenterSettings ProdCenterSettings
        {
            get;
            set;
        }

        /// <summary>
        /// Populates the Menu LIst.
        /// </summary>
        public Array PopulateMenuList
        {
            set
            {
                // Clear the Menu Item List.
                MenuTreeView.SelectedNode = MenuTreeView.Nodes[MENUS_BASE_NODE_IDX];
                MenuTreeView.SelectedNode.Nodes.Clear();

                // Populate the Menu Item List;
                foreach (POSMenuItem menuItemList in value)
                {
                    // Add child node to root node
                    var treeNode = new TreeNode(menuItemList.MenuName) { Tag = menuItemList };
                    MenuTreeView.SelectedNode = MenuTreeView.Nodes[MENUS_BASE_NODE_IDX];
                    MenuTreeView.SelectedNode.Nodes.Add(treeNode);
                }
                // Set the root node as default node and expand it.
                MenuTreeView.SelectedNode = MenuTreeView.Nodes[MENUS_BASE_NODE_IDX];
                MenuTreeView.Focus();
                MenuTreeView.SelectedNode.Expand();
            }
        }

        /// <summary>
        /// Populates the Daily Menu List.
        /// </summary>
        public Dictionary<POSMenuItem, List<DailyMenuButton>> PopulateDailyMenuList
        {
            set
            {
                if (this.InvokeRequired)
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        PopulateDailyMenuList = value;
                    }));
                }
                else
                {
                    if (dailyRootNode == null)  // that node was removed. We can't reference it.
                        return;
                    // Clear the Daily Menu Item List.
                    dailyRootNode.Nodes.Clear();
                    m_dailyMenus = value;

                    // Populate the Daily Menu Item List;
                    if (value != null)
                    {
                        if (!MenuTreeView.Nodes.Contains(dailyRootNode))
                            MenuTreeView.Nodes.Add(dailyRootNode);

                        foreach (KeyValuePair<POSMenuItem, List<DailyMenuButton>> pair in value)
                        {
                            POSMenuItem menuItem = pair.Key;
                            // Add child node to root node
                            POSMenuItem temp = new POSMenuItem() { IsDailyMenu = true, MenuId = menuItem.MenuId, MenuName = menuItem.MenuName, MenuTypeId = menuItem.MenuTypeId };
                            var treeNode = new TreeNode(menuItem.MenuName) { Tag = temp };
                            dailyRootNode.Nodes.Add(treeNode);
                        }
                        dailyRootNode.Expand();
                    }
                    else
                    {
                        MenuTreeView.Nodes.Remove(dailyRootNode);
                    }
                }
            }
        }
        #endregion Member Properties

        #region Constructors
        public MenusForm(int operatorId = 0, List<int> staffPermissions = null)
        {
            InitializeComponent();

            #region Daily Setup
            dailyRootNode = MenuTreeView.Nodes[DAILY_MENUS_BASE_NODE_IDX];
            Font temp = new System.Drawing.Font(MenuTreeView.Font, FontStyle.Bold);
            dailyRootNode.NodeFont = temp;
            MenuTreeView.Nodes.Remove(dailyRootNode);

            saveAndLoadWorker = new BackgroundWorker();
            saveAndLoadWorker.DoWork += new DoWorkEventHandler(SaveDaily);
            saveAndLoadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(SaveCompleted);

            loadWorker = new BackgroundWorker();
            loadWorker.DoWork += new DoWorkEventHandler(ReloadDaily);
            loadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(LoadDailyCompleted);

            m_waitForm = new WaitForm();
            m_waitForm.WaitImage = Resources.PleaseWait;
            m_waitForm.Cursor = Cursors.WaitCursor;
            m_waitForm.ProgressBarVisible = false;
            m_waitForm.CancelButtonVisible = false;
            if (staffPermissions != null && staffPermissions.Any(x => x == (int)ModuleFeature.EditDailyMenu))
                m_canEditDaily = true;

            if(!m_canEditDaily)
                m_chkShowDaily.Visible = false;
            #endregion

            m_operatorId = operatorId;

            MenuTreeView.ExpandAll();
            System.Threading.Thread t = new System.Threading.Thread(LoadData);
            t.Start();
        }

        public void HookIdle()
        {
            Application.Idle += OnIdle;
        }
        public void UnHookIdle()
        {
            Application.Idle -= OnIdle;
        }

        #region On Idle
        private void OnIdle(object sender, EventArgs e)
        {
            //When form is in idle state will execute this.
            //Enable or Disable controls here.

            bool selectedDailyMenu = false; // US1772
            if (MenuTreeView.SelectedNode.Level == 0) // base node 
            {
                if (MenuTreeView.SelectedNode == dailyRootNode)
                    selectedDailyMenu = true;
            }
            else if (MenuTreeView.SelectedNode.Level == 1) // menus
            {
                if (MenuTreeView.SelectedNode.Tag is POSMenuItem)
                    selectedDailyMenu = ((POSMenuItem)MenuTreeView.SelectedNode.Tag).IsDailyMenu;
            }
            else if (MenuTreeView.SelectedNode.Level == 2) // menu pages
            {
                if (MenuTreeView.SelectedNode.Parent.Tag is POSMenuItem)
                    selectedDailyMenu = ((POSMenuItem)MenuTreeView.SelectedNode.Parent.Tag).IsDailyMenu;
            }

            // Context Menu Stuff
            addMenuToolStripMenuItem.Enabled = !selectedDailyMenu;
            editMenuToolStripMenuItem.Enabled = MenuTreeView.SelectedNode.Level == 1 && !selectedDailyMenu;
            copyMenuToolStripMenuItem.Enabled = MenuTreeView.SelectedNode.Level == 1 && !selectedDailyMenu;
            pasteMenuToolStripMenuItem.Enabled = !string.IsNullOrEmpty(copiedMenu.MenuName) && !selectedDailyMenu;
            deleteMenuToolStripMenuItem.Enabled = MenuTreeView.SelectedNode.Level == 1 && !selectedDailyMenu;

            addPageToolStripMenuItem.Enabled = MenuTreeView.SelectedNode.Level == 1 && !selectedDailyMenu;
            assignPageToDeviceToolStripMenuItem.Enabled = MenuTreeView.SelectedNode.Level == 2 && !selectedDailyMenu;
            copyPageToolStripMenuItem.Enabled = MenuTreeView.SelectedNode.Level == 2 && !selectedDailyMenu;
            pastePageToolStripMenuItem.Enabled = (copiedPage.Menu.MenuId != 0) && (MenuTreeView.SelectedNode.Level == 1)
                                                  && !selectedDailyMenu;
            deletePageToolStripMenuItem.Enabled = MenuTreeView.SelectedNode.Level == 2
                                                  && MenuTreeView.SelectedNode.Parent.Nodes.Count
                                                  == MenuTreeView.SelectedNode.Index + 1
                                                  && !selectedDailyMenu;

            // Top Menu Stuff
            addMenuToolStripMenuItem1.Enabled = !selectedDailyMenu;
            editMenuToolStripMenuItem1.Enabled = MenuTreeView.SelectedNode.Level == 1 && !selectedDailyMenu;
            copyMenuToolStripMenuItem1.Enabled = MenuTreeView.SelectedNode.Level == 1 && !selectedDailyMenu;
            pasteMenuToolStripMenuItem1.Enabled = !string.IsNullOrEmpty(copiedMenu.MenuName) && !selectedDailyMenu;
            deleteMenuToolStripMenuItem1.Enabled = MenuTreeView.SelectedNode.Level == 1 && !selectedDailyMenu;

            addPageToolStripMenuItem1.Enabled = MenuTreeView.SelectedNode.Level == 1 && !selectedDailyMenu;
            assignPageToDeviceToolStripMenuItem1.Enabled = MenuTreeView.SelectedNode.Level == 2 && !selectedDailyMenu;
            copyPageToolStripMenuItem1.Enabled = MenuTreeView.SelectedNode.Level == 2 && !selectedDailyMenu;
            pastePageToolStripMenuItem1.Enabled = (copiedPage.Menu.MenuId != 0) && (MenuTreeView.SelectedNode.Level == 1)
                                                   && !selectedDailyMenu;
            deletePageToolStripMenuItem1.Enabled = MenuTreeView.SelectedNode.Level == 2
                                                   && MenuTreeView.SelectedNode.Parent.Nodes.Count
                                                   == MenuTreeView.SelectedNode.Index + 1
                                                   && !selectedDailyMenu;

            // Menu List Stuff
            if (MenuTreeView.SelectedNode.Level == 2)
            {
                // Show the buttons to the user
                ButtonPanel.Show();

                if(selectedDailyMenu)
                    DragInfoLabel.Hide();
                else
                    DragInfoLabel.Show();
            }
            else
            {
                // Hide the control to the user.
                ButtonPanel.Hide();
                DragInfoLabel.Hide();
                DeviceInfoLabel.Visible = DeviceTypeLinkLabel.Visible = false; // don't need to set visible in the other check, the "idle" event handler does that
            }
        }
        #endregion On Idle

        #endregion Constructors

        #region Private Misc Methods

        /// <summary>
        /// allows the developer to load some data that doesn't change often on a background thread
        /// </summary>
        private void LoadData()
        {
            if (graphics == null)
            {
                try
                {
                    // Note: this should probably be first in case the user tries to click on a menu page very quickly.
                    //     Since one message is sent at a time, this will be processed before the pages are loaded, so everything should be fine
                    graphics = GetButtonGraphicsMessage.GetButtonGraphics(-1);
                }
                catch (Exception ex)
                {
                    ProductCenter.Business.ProductCenter.Log("Unable to get button graphics: " + ex.ToString(), LoggerLevel.Severe);
                    graphics = new List<ButtonGraphic>(); // keep other things from throwing exceptions
                }
            }

            // JAN - note that the user can change some of these things while we're running, but since we're loading these for the 
            //     "daily" menu items; the data will be valid until the user starts editing them (and the edit window loads its own copies when it starts)
            if (m_cardMediaList == null)
            {
                m_cardMediaList = new List<CardMediaListItem>();
                try
                {
                    m_cardMediaList.AddRange(GetCardMediaMessage.GetArray(0));
                }
                catch (Exception ex)
                {
                    ProductCenter.Business.ProductCenter.Log("Unable to get card media list: " + ex.ToString(), LoggerLevel.Severe);
                }
            }
            if (m_gameCategoryList == null)
            {
                m_gameCategoryList = new List<GameCategory>();
                try
                {
                    m_gameCategoryList.AddRange(GetGameCategoriesMessage.GetArray());
                }
                catch (Exception ex)
                {
                    ProductCenter.Business.ProductCenter.Log("Unable to get game category list: " + ex.ToString(), LoggerLevel.Severe);
                }
            }
            if (m_gameTypeList == null)
            {
                m_gameTypeList = new List<GameTypeListItem>();
                try
                {
                    m_gameTypeList.AddRange(GetGameTypeMessage.GetArray());
                }
                catch (Exception ex)
                {
                    ProductCenter.Business.ProductCenter.Log("Unable to get game type list: " + ex.ToString(), LoggerLevel.Severe);
                }
            }
            if (m_cardLevelList == null)
            {
                m_cardLevelList = new List<CardLevelItem>();
                try
                {
                    m_cardLevelList.AddRange(GetCardLevelMessage.CardLevels(0).ToArray());
                }
                catch (Exception ex)
                {
                    ProductCenter.Business.ProductCenter.Log("Unable to get level list: " + ex.ToString(), LoggerLevel.Severe);
                }
            }
        }

        /// US1772
        /// <summary>
        /// Fills in the list of daily menus
        /// </summary>
        private void GetDailyMenus()
        {
            if (m_canEditDaily)
            {
                Dictionary<POSMenuItem, List<DailyMenuButton>> temp = new Dictionary<POSMenuItem, List<DailyMenuButton>>();
                POSMenuItem loadingMenu = new POSMenuItem();
                loadingMenu.IsDailyMenu = true;
                loadingMenu.MenuName = "(loading...)";
                temp.Add(loadingMenu, new List<DailyMenuButton>());
                PopulateDailyMenuList = temp;

                DateTime gamingDate = DateTime.Now;
                try
                {
                    GetGamingDateMessage dateMessage = new GetGamingDateMessage(m_operatorId);
                    dateMessage.Send();
                    if (dateMessage.ServerReturnCode == GTIServerReturnCode.Success)
                        gamingDate = dateMessage.GamingDate;
                }
                catch (Exception ex)
                {
                    ProductCenter.Business.ProductCenter.Log("Unable to get gaming date: " + ex.ToString(), LoggerLevel.Severe);
                }

                //Populate the Daily Menu List
                try
                {
                    PopulateDailyMenuList = GetDailyStaffMenusMessage.GetDailyStaffMenus(gamingDate);
                }
                catch (ServerException ex)
                {
                    string err = String.Format("Unable to get daily menu. Reason: {0}",
                        GameTech.Elite.Client.ServerErrorTranslator.GetReturnCodeMessage((GameTech.Elite.Client.ServerReturnCode)ex.ReturnCode));
                    ProductCenter.Business.ProductCenter.Log(err, LoggerLevel.Severe);
                    MessageForm.Show(err);
                    PopulateDailyMenuList = null;
                }
                catch (Exception ex)
                {
                    string err = "Unable to get daily menu: " + ex.ToString();
                    ProductCenter.Business.ProductCenter.Log(err, LoggerLevel.Severe);
                    MessageForm.Show(err);
                    PopulateDailyMenuList = null;
                }
            }
            else
            {
                PopulateDailyMenuList = null;
            }
        }

        /// US1772
        /// <summary>
        /// Saves the daily menu button edit and reloads the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveDaily(object sender, DoWorkEventArgs e)
        {
            Tuple<POSMenuItem, DailyMenuButton> tup = (Tuple<POSMenuItem, DailyMenuButton>)e.Argument;
            if (tup != null)
            {
                POSMenuItem menu = tup.Item1;
                DailyMenuButton button = tup.Item2;

                try
                {
                    // Save the button info to the database
                    UpdateDailyMenuDataMessage.UpdateDailyMenuData(menu, button);
                }
                catch (ServerException ex)
                {
                    string err = String.Format("Unable to save daily menu. Reason: {0}",
                        GameTech.Elite.Client.ServerErrorTranslator.GetReturnCodeMessage((GameTech.Elite.Client.ServerReturnCode)ex.ReturnCode));
                    ProductCenter.Business.ProductCenter.Log(err, LoggerLevel.Severe);
                    MessageForm.Show(err);
                }
                catch (Exception ex)
                {
                    string err = "Unable to save daily menu: " + ex.ToString();
                    ProductCenter.Business.ProductCenter.Log(err, LoggerLevel.Severe);
                    MessageForm.Show(err);
                }
            }
        }

        /// US1772
        /// <summary>
        /// Runs the load worker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (loadWorker != null && !loadWorker.IsBusy)
            {
                loadWorker.RunWorkerAsync();
                m_waitForm.Message = "Loading daily menu";
            }
        }

        /// US1772
        /// <summary>
        /// Saves the daily menu button edit and reloads the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReloadDaily(object sender, DoWorkEventArgs e)
        {
            GetDailyMenus();
        }

        /// US1772
        /// <summary>
        /// Closes the wait form and resets the cursor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadDailyCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (m_waitForm != null && !m_waitForm.IsDisposed)
            {
                m_waitForm.CloseForm();
            }
            Cursor = Cursors.Default;
        }
        #endregion

        #region Menu Events

        #region Add Menu
        private void AddMenuClick(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Initialize the form.
            List<MenuTypeListItem> menuTypes = GetMenuTypesMessage.GetList();
            menuDetailForm = new MenuDetailForm { PopulateMenuTypeList = menuTypes.ToArray(), SetConstantMenuType = menuTypes[0] };

            Cursor = Cursors.Default;

            // Add to the Menu List
            if (menuDetailForm.ShowDialog(this) == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;

                SetMenuMessage.Save(0, m_operatorId, menuDetailForm.MenuName, int.Parse(menuDetailForm.MenuTypeId));

                // Set the root node as default node
                MenuTreeView.SelectedNode = MenuTreeView.Nodes[0];
                MenuTreeView.Focus();
                MenuTreeView.SelectedNode.Expand();

                // Populate the Menu List.
                PopulateMenuList = MenuItems.NameSorted(m_operatorId).ToArray();

                Application.DoEvents();

                // Again... Set the root node as default node
                MenuTreeView.SelectedNode = MenuTreeView.Nodes[0];
                MenuTreeView.Focus();
                MenuTreeView.SelectedNode.Expand();

                Cursor = Cursors.Default;
            }
        }
        #endregion Add Menu

        #region Edit Menu
        private void EditMenuClick(object sender, EventArgs e)
        {
            if (MenuTreeView.SelectedNode.Level == 1 && MenuTreeView.SelectedNode.Tag is POSMenuItem && !((POSMenuItem)MenuTreeView.SelectedNode.Tag).IsDailyMenu)
            {
                Cursor = Cursors.WaitCursor;

                // Initialize the form.
                List<MenuTypeListItem> menuTypes = GetMenuTypesMessage.GetList();
                menuDetailForm = new MenuDetailForm { PopulateMenuTypeList = menuTypes.ToArray(), SetConstantMenuType = menuTypes[0] };
                //menuDetailForm = new MenuDetailForm { PopulateMenuTypeList = GetMenuTypesMessage.GetList() };

                // Set the values to be edited in the detail form.
                var menuItem = (POSMenuItem)MenuTreeView.SelectedNode.Tag;
                menuDetailForm.MenuName = menuItem.MenuName;
                menuDetailForm.MenuTypeId = menuItem.MenuTypeId.ToString();

                Cursor = Cursors.Default;

                // Update the Menu List.
                if (menuDetailForm.ShowDialog(this) == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    // Update the Menu Treeview and the tag's MenuItem object.
                    MenuTreeView.SelectedNode.Text = menuDetailForm.MenuName;
                    menuItem.MenuName = menuDetailForm.MenuName;
                    menuItem.MenuTypeId = int.Parse(menuDetailForm.MenuTypeId);
                    MenuTreeView.SelectedNode.Tag = menuItem;

                    SetMenuMessage.Save(menuItem.MenuId, m_operatorId, menuItem.MenuName, menuItem.MenuTypeId);

                    Cursor = Cursors.Default;
                }
            }
        }
        #endregion Edit Menu

        #region Copy Menu
        private void CopyMenuClick(object sender, EventArgs e)
        {
            if (MenuTreeView.SelectedNode.Level == 1)
            {
                copiedMenu = (POSMenuItem)MenuTreeView.SelectedNode.Tag;
            }
        }
        #endregion Copy Menu

        #region Paste Menu
        private void PasteMenuClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(copiedMenu.MenuName))
            {
                Cursor = Cursors.WaitCursor;

                // Initialize the form.
                List<MenuTypeListItem> menuTypes = GetMenuTypesMessage.GetList();
                menuDetailForm = new MenuDetailForm
                                 {
                                     PopulateMenuTypeList = menuTypes.ToArray(),
                                     SetConstantMenuType = menuTypes[0],
                                     MenuName = copiedMenu.MenuName,
                                     //MenuTypeId = copiedMenu.MenuTypeId.ToString()
                                 };

                Cursor = Cursors.Default;

                // Add to the Menu List
                if (menuDetailForm.ShowDialog(this) == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    // Add the menu to the database
                    var menuId = SetMenuMessage.Save(0, m_operatorId, menuDetailForm.MenuName,
                                                     int.Parse(menuDetailForm.MenuTypeId));

                    // Create a new Menu Item object to store it in the node's tag
                    var menuItem = new POSMenuItem
                                   {
                                       MenuId = menuId,
                                       MenuName = menuDetailForm.MenuName,
                                       MenuTypeId = int.Parse(menuDetailForm.MenuTypeId)
                                   };

                    // Set the root node as default node
                    MenuTreeView.SelectedNode = MenuTreeView.Nodes[0];
                    MenuTreeView.Focus();
                    MenuTreeView.SelectedNode.Expand();

                    // Add Child node to root node
                    var treeNode = new TreeNode(menuDetailForm.MenuName) { Tag = menuItem };
                    MenuTreeView.SelectedNode.Nodes.Add(treeNode);

                    // Set the last node as default node.
                    MenuTreeView.SelectedNode = MenuTreeView.Nodes[0].LastNode;
                    MenuTreeView.Focus();
                    MenuTreeView.SelectedNode.Expand();

                    // Get the Page List from the Database
                    int totalPages;
                    var buttons = GetMenuButtonMessage.GetButtons(copiedMenu.MenuId, 0, out totalPages);

                    // Add pages to the menu item in the treeview
                    MenuTreeView.SelectedNode.Nodes.Clear();
                    for (var i = 1; i <= totalPages; i++)
                    {
                        var treeNodePage = new TreeNode("Page " + i);
                        var pageItem = new PageItem { MenuPage = (byte)i };
                        pageItem.Menu.MenuId = menuId;
                        treeNodePage.Tag = pageItem;
                        MenuTreeView.SelectedNode.Nodes.Add(treeNodePage);
                    }

                    // Save buttons if any
                    if (buttons.Count > 0)
                    {
                        // Save the button info to the database
                        var setMenuButtonMsg = new SetMenuButtonMessage
                                               {
                                                   MenuId = menuId,
                                                   MenuButtonList = buttons.ToArray()
                                               };
                        setMenuButtonMsg.Send();
                    }

                    MenuTreeView.Focus();
                    MenuTreeView.SelectedNode.Expand();

                    Cursor = Cursors.Default;
                }
            }
        }
        #endregion Paste Menu

        #region Delete Menu
        private void DeleteMenuClick(object sender, EventArgs e)
        {
            if (MenuTreeView.SelectedNode.Level == 1)
            {
                if (MessageForm.Show(Resources.ConfirmDelete, Resources.DeleteMenuTitle, MessageFormTypes.YesNo, 0)//RALLY DE 6657
                    == DialogResult.Yes)//RALLY DE 6657
                {
                    Cursor = Cursors.WaitCursor;

                    // Get the Menu info from the selected node.
                    var menuItem = (POSMenuItem)MenuTreeView.SelectedNode.Tag;

                    // Remove from the Treeview
                    MenuTreeView.SelectedNode.Remove();

                    //// Remove it from the database
                    DelMenuMessage.DeleteMenu(m_operatorId, menuItem.MenuId);

                    Cursor = Cursors.Default;
                }
            }
        }
        #endregion Delete Menu

        #region Menu List After Select
        private void AfterSelectMenuList(object sender, TreeViewEventArgs e)
        {
            // Just in case a page was deleted
            if (e.Action == TreeViewAction.Unknown && MenuTreeView.SelectedNode.Level == 1)
            {
                return;
            }

            if (MenuTreeView.SelectedNode.Level > 0)
            {
                // Get the menu pages
                if (MenuTreeView.SelectedNode.Level == 1 && MenuTreeView.SelectedNode.Tag != null)
                {
                    Cursor = Cursors.WaitCursor;

                    // Get the Menu info from the selected node.
                    var menuItem = (POSMenuItem)MenuTreeView.SelectedNode.Tag;

                    // Get the Page List from the database.
                    int totalPages = 0;
                    if (menuItem.IsDailyMenu) // US1772 if this is a daily menu, we already have the info stored
                    {
                        if (m_dailyMenus != null && m_dailyMenus.ContainsKey(menuItem))
                        {
                            foreach (var button in m_dailyMenus[menuItem])
                            {
                                if (button.PageNumber > totalPages)
                                    totalPages = button.PageNumber;
                            }
                        }
                    }
                    else
                    {
                        GetMenuButtonMessage.GetButtons(menuItem.MenuId, 0, out totalPages);
                    }

                    // Add pages to the menu item in the treeview
                    MenuTreeView.SelectedNode.Nodes.Clear();
                    for (var i = 1; i <= totalPages; i++)
                    {
                        var treeNode = new TreeNode("Page " + i);
                        var pageItem = new PageItem { Menu = menuItem, MenuPage = (byte)i };
                        treeNode.Tag = pageItem;
                        MenuTreeView.SelectedNode.Nodes.Add(treeNode);
                    }
                    MenuTreeView.Focus();
                    MenuTreeView.SelectedNode.Expand();
                }

                //Get the Menu Buttons Panel
                if (MenuTreeView.SelectedNode.Level == 2)
                {
                    Cursor = Cursors.WaitCursor;

                    // Get the Page info from the selected node
                    var pageItem = (PageItem)MenuTreeView.SelectedNode.Tag;

                    //// Populate the buttons
                    if (pageItem.Menu.IsDailyMenu) // US1772
                    {
                        if (m_dailyMenus != null && m_dailyMenus.ContainsKey(pageItem.Menu))
                        {
                            IEnumerable<DailyMenuButton> pageButtons = m_dailyMenus[pageItem.Menu].Where(x => x.PageNumber == pageItem.MenuPage);
                            CreateMenuButtons(pageButtons.ToArray());
                        }

                        DeviceInfoLabel.Visible = DeviceTypeLinkLabel.Visible = false; // don't display the device for daily menus
                    }
                    else
                    {
                        int totalPages;
                        List<MenuButtonList> menuButtons = GetMenuButtonMessage.GetButtons(pageItem.Menu.MenuId, pageItem.MenuPage, out totalPages);
                        CreateMenuButtons(menuButtons.ToArray());

                        List<Device> devicesForPage = null;

                        if (menuButtons != null && menuButtons.Count > 0)
                            devicesForPage = menuButtons.First().ValidDevices; // devices are technically assigned per button, but UI only supports per-page

                        UpdateDevicesUI(devicesForPage);
                    }
                }
            }
            Cursor = Cursors.Default;
        }
        #endregion Menu List After Select

        #region Show Daily Checkbox

        /// <summary>
        /// Actions that occur when the "show daily" checkbox changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDaily_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                if (loadWorker != null && !loadWorker.IsBusy)
                {
                    loadWorker.RunWorkerAsync();
                    m_waitForm.Message = "Loading daily menu";
                    m_waitForm.ShowDialog(this);
                }
            }
            else
            {
                PopulateDailyMenuList = null;
            }

        }

        #endregion

        #endregion

        #region Page Events

        #region Add Page
        private void AddPageClick(object sender, EventArgs e)
        {
            // Get the Menu info from the selected node
            POSMenuItem menuItem = (POSMenuItem)MenuTreeView.SelectedNode.Tag;

            // Calculate the next page based on all the existing pages
            int nextPage = MenuTreeView.SelectedNode.GetNodeCount(false) + 1;

            // Add the new empty page
            TreeNode treeNode = new TreeNode("Page " + nextPage);
            PageItem pageItem = new PageItem { Menu = menuItem, MenuPage = (byte)nextPage };
            treeNode.Tag = pageItem;
            // Get a list of all buttons for all pages
            int totalPages;
            List<MenuButtonList> buttonList = GetMenuButtonMessage.GetButtons(pageItem.Menu.MenuId, 0, out totalPages);
            // FIX : TA5013 adding menu with out packages causes index exception
            if (PackageItems.Sorted.Count == 0)
            {
                MessageForm.Show(Resources.NoPackagesFound, Resources.GetPackagesTitle, MessageFormTypes.OK, 0);//RALLY DE 6657
                return;
            }
            // END : TA5013
            PackageItem pkg = PackageItems.Sorted[0];
            MenuTreeView.SelectedNode.Nodes.Add(treeNode);
            MenuTreeView.Focus();
            MenuTreeView.SelectedNode.Expand();
            // Add a dummy button for the new page.
            buttonList.Add(new MenuButtonList
                           {
                               PageNumber = pageItem.MenuPage,
                               KeyNum = 0,
                               KeyText = "<name>",
                               ReceiptText = pkg.ReceiptText,
                               PackageId = pkg.PackageId
                           });
            // Save the new button list.
            SetMenuButtonMessage.Save(pageItem.Menu.MenuId, buttonList.ToArray());
        }
        #endregion Add Page

        #region Copy Page
        private void CopyPageClick(object sender, EventArgs e)
        {
            if (MenuTreeView.SelectedNode.Level == 2)
            {
                copiedPage = (PageItem)MenuTreeView.SelectedNode.Tag;
            }
        }
        #endregion Copy Page

        #region Paste Page
        private void PastePageClick(object sender, EventArgs e)
        {
            if ((copiedPage.Menu.MenuId != 0) && (MenuTreeView.SelectedNode.Level == 1))
            {
                Cursor = Cursors.WaitCursor;

                // Get the Menu Buttons for the copied page
                int totalPages;
                var buttons = GetMenuButtonMessage.GetButtons(copiedPage.Menu.MenuId, copiedPage.MenuPage, out totalPages);

                // Get the button list
                var menuButtonList = buttons.ToArray();

                // Calculate the next page based on all the existing pages
                var nextPage = MenuTreeView.SelectedNode.GetNodeCount(false) + 1;

                // Change the page number in all buttons
                for (var i = 0; i < menuButtonList.Length; i++)
                {
                    menuButtonList[i].PageNumber = (byte)nextPage;
                }

                // Get the Menu info from the selected node
                var menuItem = (POSMenuItem)MenuTreeView.SelectedNode.Tag;

                // Save the menu buttons if any
                if (menuButtonList.Length > 0)
                {
                    // Copy the button info to the database
                    SetMenuButtonMessage.Save(menuItem.MenuId, menuButtonList);
                }

                // Add the new copied page
                var treeNode = new TreeNode("Page " + nextPage);
                var pageItem = new PageItem { Menu = menuItem, MenuPage = (byte)nextPage };
                treeNode.Tag = pageItem;
                MenuTreeView.SelectedNode.Nodes.Add(treeNode);
                MenuTreeView.Focus();
                MenuTreeView.SelectedNode.Expand();

                Cursor = Cursors.Default;
            }
        }
        #endregion Paste Page

        #region Delete Page
        private void DeletePageClick(object sender, EventArgs e)
        {
            bool deletePage = true;
            bool dataInPage = false;

            // Get the Page's info from the selected node
            PageItem pageItem = (PageItem)MenuTreeView.SelectedNode.Tag;

            // Verify that there's no associated buttons to this page
            for (var i = 0; i < ButtonPanel.Controls.Count; i++)
            {
                // Get the Button's info
                var buttonItem = (MenuButtonList)ButtonPanel.Controls[i].Tag;

                if (!string.IsNullOrEmpty(buttonItem.KeyText))
                {
                    dataInPage = true;
                }
            }

            if (dataInPage)
            {
                if (MessageForm.Show(Resources.ConfirmDeletePage, Resources.DeletePageTitle, MessageFormTypes.YesNo, 0)//RALLY DE 6657
                    == DialogResult.No)//RALLY DE 6657
                {
                    deletePage = false;
                }
            }

            if (deletePage)
            {
                if (dataInPage)
                {
                    Cursor = Cursors.WaitCursor;

                    var buttonCount = 0;
                    var buttonIndex = 0;

                    // Get the Button count
                    for (var j = 0; j < ButtonPanel.Controls.Count; j++)
                    {
                        if (!string.IsNullOrEmpty(ButtonPanel.Controls[j].Text))
                        {
                            buttonCount++;
                        }
                    }

                    // Create a new Menu Button instance to send to the server
                    MenuButtonList[] menuButtonList = new MenuButtonList[buttonCount];

                    // Build the Buttons List to be removed
                    for (var j = 0; j < ButtonPanel.Controls.Count; j++)
                    {
                        MenuButtonList mbl = (MenuButtonList)ButtonPanel.Controls[j].Tag;
                        if (!string.IsNullOrEmpty(ButtonPanel.Controls[j].Text))
                        {
                            menuButtonList[buttonIndex] = mbl;
                            menuButtonList[buttonIndex].RemoveButton = 1; // To remove it...
                            buttonIndex++;
                        }
                    }

                    // Set the Menu Buttons for this page
                    SetMenuButtonMessage.Save(pageItem.Menu.MenuId, menuButtonList);

                    Cursor = Cursors.Default;
                }

                MenuTreeView.SelectedNode.Remove();

                if (MenuTreeView.SelectedNode.Nodes.Count > 0)
                {
                    MenuTreeView.SelectedNode = MenuTreeView.SelectedNode.LastNode;
                }
                else if (MenuTreeView.SelectedNode.Level == 1)
                {
                    ButtonPanel.Hide();
                    DragInfoLabel.Hide();
                }
                MenuTreeView.Focus();
            }
        }
        #endregion Delete Page

        #region Assign Page to Device

        /// <summary>
        /// Displays a UI for assigning the selected page to a device type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssignPageToDevice(object sender, EventArgs e)
        {
            List<Device> devicesForPage = m_devicesForPage;

            MenuPageDeviceTypeForm devicePageForm = new MenuPageDeviceTypeForm(null, devicesForPage);

            if (devicePageForm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                // Get the Page's info from the selected node
                PageItem pageItem = (PageItem)MenuTreeView.SelectedNode.Tag;

                // Create a new Menu Button instance to send to the server
                List<MenuButtonList> menuButtonList = new List<MenuButtonList>();

                // Build the Buttons List to be removed
                for (var j = 0; j < ButtonPanel.Controls.Count; j++)
                {
                    MenuButtonList mbl = (MenuButtonList)ButtonPanel.Controls[j].Tag;
                    if (!string.IsNullOrEmpty(ButtonPanel.Controls[j].Text))
                    {
                        mbl.ValidDevices = devicePageForm.SelectedDevices;
                        menuButtonList.Add(mbl);
                    }
                }

                // Save the menu buttons if any
                if (menuButtonList.Count > 0)
                {
                    // Copy the button info to the database
                    SetMenuButtonMessage.Save(pageItem.Menu.MenuId, menuButtonList.ToArray());

                    Application.DoEvents();
                    //Get the Menu Buttons Panel
                    if (MenuTreeView.SelectedNode.Level == 2)
                    {
                        Cursor = Cursors.WaitCursor;

                        // Get the Page info from the selected node
                        pageItem = (PageItem)MenuTreeView.SelectedNode.Tag;

                        // Populate the buttons
                        int totalPages;
                        List<MenuButtonList> menuButtons = GetMenuButtonMessage.GetButtons(pageItem.Menu.MenuId, pageItem.MenuPage, out totalPages);
                        CreateMenuButtons(menuButtons.ToArray());
                        devicesForPage = null;

                        if (menuButtons != null && menuButtons.Count > 0)
                            devicesForPage = menuButtons.First().ValidDevices;

                        UpdateDevicesUI(devicesForPage);
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AssignPageToDevice(this, null);
        }

        /// US4756
        /// <summary>
        /// Updates the displayed UI for the device list
        /// </summary>
        /// <param name="devicesForPage"></param>
        private void UpdateDevicesUI(List<Device> devicesForPage)
        {
            m_devicesForPage = devicesForPage;

            if (devicesForPage == null || devicesForPage.Count == 0) // check device type of first button.
            {
                DeviceInfoLabel.Visible = DeviceTypeLinkLabel.Visible = true;
                DeviceTypeLinkLabel.Text = ALL_DEVICES_SELECTED;
            }
            else
            {
                DeviceInfoLabel.Visible = DeviceTypeLinkLabel.Visible = true;
                if (devicesForPage.Count == 1)
                {
                    if (String.IsNullOrWhiteSpace(devicesForPage.First().Name))
                        DeviceTypeLinkLabel.Text = NO_DEVICES_SELECTED; // if name is null, then probably got a disabled device
                    else
                        DeviceTypeLinkLabel.Text = devicesForPage.First().Name;
                }
                else
                    DeviceTypeLinkLabel.Text = MANY_DEVICES_SELECTED;
            }
        }
        #endregion

        #endregion

        #region Create Menu Buttons

        /// <summary>
        /// Fills in the UI with a blank button page
        /// </summary>
        private void CreateBlankPage()
        {
            // Remove all the old buttons.
            ButtonPanel.Controls.Clear();
            var currentColumn = 0;
            var currentRow = 0;

            DisplayMode btnDisplayMode = new NormalDisplayMode();

            for (var x = 0; x < btnDisplayMode.MenuButtonsPerPage; x++)
            {
                var button = new ImageButton
                {
                    Name = "MenuButton" + x,
                    Font = btnDisplayMode.MenuButtonFont,
                    Size = btnDisplayMode.MenuButtonSize,
                    ImageNormal = Resources.GrayButtonUp,
                    ImagePressed = Resources.GrayButtonDown,
                    UseMnemonic = false,
                    ShowFocus = false,
                    TabIndex = 0,
                    TabStop = false
                };

                // Add Button Double Click Event
                button.MouseClick += ButtonClick;

                // Add Button Drag and Drop Event
                button.MouseDown += ButtonMouseDown;
                button.DragEnter += ButtonDragEnter;
                button.DragDrop += ButtonDragDrop;
                button.DragLeave += ButtonDragLeave;
                button.AllowDrop = true;

                // Add Button context menu
                button.ContextMenuStrip = buttonContextMenu;

                // Determine where the button is going to be.
                var buttonLoc = new Point(currentColumn * button.Width, currentRow * button.Height);

                if (currentColumn != 0)
                {
                    buttonLoc.X += (btnDisplayMode.MenuButtonXSpacing * currentColumn);
                }

                if (currentRow != 0)
                {
                    buttonLoc.Y += (btnDisplayMode.MenuButtonYSpacing * currentRow);
                }

                button.Location = buttonLoc;

                // Set an empty button object
                var buttonItem = new MenuButtonList();
                button.Tag = buttonItem;

                //this.toolTip1.SetToolTip(button, "Left = Edit; Right = Drag & Drop");
                ButtonPanel.Controls.Add(button);

                // Check to see if a new column is needed.
                if (++currentRow >= btnDisplayMode.MenuButtonsPerColumn)
                {
                    currentRow = 0;
                    currentColumn++;
                }
            }
        }

        /// <summary>
        /// Creates and adds MenuButtons to the panel.
        /// </summary>
        private void CreateMenuButtons(MenuButtonList[] menuButtonList)
        {
            ButtonPanel.SuspendLayout();

            CreateBlankPage();

            // Set Button's data
            for (var j = 0; j < menuButtonList.Length; j++)
            {
                // FIX: DE2406 TA3217 Button text shows package price on discounts and functions
                int intId = menuButtonList[j].PackageId;
                if (intId > 0)
                {
                    PackageItem pkg = PackageItems.GetPackageItem(intId);
                    decimal dec = Convert.ToDecimal(pkg.PackagePrice);
                    string strAmt = String.Format("{0:C}", dec);
                    ButtonPanel.Controls[menuButtonList[j].KeyNum].Text = menuButtonList[j].KeyText + Environment.NewLine + strAmt;
                }
                else
                {
                    ButtonPanel.Controls[menuButtonList[j].KeyNum].Text = menuButtonList[j].KeyText;
                }
                // END: DE2406 TA3217 Button text shows package price on discounts and functions
                ButtonPanel.Controls[menuButtonList[j].KeyNum].Tag = menuButtonList[j];

                // Disable drag and drop on these...
                ButtonPanel.Controls[menuButtonList[j].KeyNum].AllowDrop = false;
                SetButtonGraphic(menuButtonList[j].ButtonGraphicId, menuButtonList[j].KeyNum);
            }

            ButtonPanel.ResumeLayout();
        }

        /// <summary>
        /// Creates and adds DailyMenuButtons to the panel
        /// </summary>
        /// <param name="menuButtonList"></param>
        private void CreateMenuButtons(DailyMenuButton[] menuButtonList)
        {
            ButtonPanel.SuspendLayout();

            CreateBlankPage();

            // Set Button's data
            for (var j = 0; j < menuButtonList.Length; j++)
            {
                if (menuButtonList[j].PackageId != 0)
                {
                    decimal dec = 0;
                    if (menuButtonList[j].ProductItems != null)
                    {
                        foreach (var item in menuButtonList[j].ProductItems)
                            dec += Convert.ToDecimal(item.Price) * (decimal)item.Quantity;
                    }

                    string strAmt = String.Format("{0:C}", dec);
                    ButtonPanel.Controls[menuButtonList[j].KeyNum].Text = menuButtonList[j].KeyText + Environment.NewLine + strAmt;
                }
                else
                {
                    ButtonPanel.Controls[menuButtonList[j].KeyNum].Text = menuButtonList[j].KeyText;
                }
                // END: DE2406 TA3217 Button text shows package price on discounts and functions
                ButtonPanel.Controls[menuButtonList[j].KeyNum].Tag = menuButtonList[j];

                // Disable drag and drop on these...
                ButtonPanel.Controls[menuButtonList[j].KeyNum].AllowDrop = false;
                SetButtonGraphic(menuButtonList[j].ButtonGraphicId, menuButtonList[j].KeyNum);
            }

            ButtonPanel.ResumeLayout();
        }

        /// <summary>
        /// Applies the graphic to the sent-in button
        /// </summary>
        /// <param name="buttonGraphicId"></param>
        /// <param name="keyNum"></param>
        private void SetButtonGraphic(int buttonGraphicId, int keyNum)
        {
            Image normalImage = Resources.GrayButtonUp;
            Image pressedImage = Resources.GrayButtonDown;
            bool isStretch = true;
            string graphicName = "None";

            foreach (var bg in graphics)
            {
                if (bg.ButtonGraphicId == buttonGraphicId)
                {
                    graphicName = bg.ButtonGraphicDescription;
                    break;
                }
            }

            switch (graphicName)
            {
                case "Set":
                    normalImage = Resources.SetButtonUp;
                    pressedImage = Resources.SetButtonDown;
                    isStretch = false;
                    break;
                case "Book":
                    normalImage = Resources.BookButtonUp;
                    pressedImage = Resources.BookButtonDown;
                    isStretch = false;
                    break;
                case "Paper":
                    normalImage = Resources.PaperButtonUp;
                    pressedImage = Resources.PaperButtonDown;
                    isStretch = false;
                    break;
                case "Credit":
                    normalImage = Resources.CreditButtonUp;
                    pressedImage = Resources.CreditButtonDown;
                    isStretch = false;
                    break;
                case "Discount":
                    normalImage = Resources.DiscountButtonUp;
                    pressedImage = Resources.DiscountButtonDown;
                    isStretch = false;
                    break;
                case "Merchandise":
                    normalImage = Resources.MerchandiseButtonUp;
                    pressedImage = Resources.MerchandiseButtonDown;
                    isStretch = false;
                    break;
                case "Concession":
                    normalImage = Resources.ConcessionButtonUp;
                    pressedImage = Resources.ConcessionButtonDown;
                    isStretch = false;
                    break;
                case "Electronic":
                    normalImage = Resources.ElectronicButtonUp;
                    pressedImage = Resources.ElectronicButtonDown;
                    isStretch = false;
                    break;
                // US2098
                case "Brown":
                    normalImage = Resources.BrownButtonUp;
                    pressedImage = Resources.BrownButtonDown;
                    isStretch = true;
                    break;

                case "Green":
                    normalImage = Resources.GreenButtonUp;
                    pressedImage = Resources.GreenButtonDown;
                    isStretch = true;
                    break;

                case "Orange":
                    normalImage = Resources.OrangeButtonUp;
                    pressedImage = Resources.OrangeButtonDown;
                    isStretch = true;
                    break;

                case "Purple":
                    normalImage = Resources.PurpleButtonUp;
                    pressedImage = Resources.PurpleButtonDown;
                    isStretch = true;
                    break;

                case "Rainbow":
                    normalImage = Resources.RainbowButtonUp;
                    pressedImage = Resources.RainbowButtonDown;
                    isStretch = true;
                    break;

                case "Red":
                    normalImage = Resources.RedButtonUp;
                    pressedImage = Resources.RedButtonDown;
                    isStretch = true;
                    break;

                case "White":
                    normalImage = Resources.WhiteButtonUp;
                    pressedImage = Resources.WhiteButtonDown;
                    isStretch = true;
                    break;

                case "Yellow":
                    normalImage = Resources.YellowButtonUp; // DE13505 resource name was incorrect
                    pressedImage = Resources.YellowButtonDown;
                    isStretch = true;
                    break;

                // END: US2098

                //US4885
                case "Black3D":
                    normalImage = Resources.BlackGelButtonUp;
                    pressedImage = Resources.BlackGelButtonDown;
                    isStretch = false;
                    break;

                case "BlackFlat":
                    normalImage = Resources.BlackFlatButtonUp;
                    pressedImage = Resources.BlackFlatButtonDown;
                    isStretch = false;
                    break;

                case "Blue3D":
                    normalImage = Resources.BlueGelButtonUp;
                    pressedImage = Resources.BlueGelButtonDown;
                    isStretch = false;
                    break;

                case "BlueFlat":
                    normalImage = Resources.BlueFlatButtonUp;
                    pressedImage = Resources.BlueFlatButtonDown;
                    isStretch = false;
                    break;

                case "Brown3D":
                    normalImage = Resources.BrownGelButtonUp;
                    pressedImage = Resources.BrownGelButtonDown;
                    isStretch = false;
                    break;

                case "BrownFlat":
                    normalImage = Resources.BrownFlatButtonUp;
                    pressedImage = Resources.BrownFlatButtonDown;
                    isStretch = false;
                    break;

                case "Gold3D":
                    normalImage = Resources.GoldGelButtonUp;
                    pressedImage = Resources.GoldGelButtonDown;
                    isStretch = false;
                    break;

                case "GoldFlat":
                    normalImage = Resources.GoldFlatButtonUp;
                    pressedImage = Resources.GoldFlatButtonDown;
                    isStretch = false;
                    break;

                case "Gray3D":
                    normalImage = Resources.GrayGelButtonUp;
                    pressedImage = Resources.GrayGelButtonDown;
                    isStretch = false;
                    break;

                case "GrayFlat":
                    normalImage = Resources.GrayFlatButtonUp;
                    pressedImage = Resources.GrayFlatButtonDown;
                    isStretch = false;
                    break;

                case "Green3D":
                    normalImage = Resources.GreenGelButtonUp;
                    pressedImage = Resources.GreenGelButtonDown;
                    isStretch = false;
                    break;

                case "GreenFlat":
                    normalImage = Resources.GreenFlatButtonUp;
                    pressedImage = Resources.GreenFlatButtonDown;
                    isStretch = false;
                    break;

                case "Lavender3D":
                    normalImage = Resources.LavenderGelButtonUp;
                    pressedImage = Resources.LavenderGelButtonDown;
                    isStretch = false;
                    break;

                case "LavenderFlat":
                    normalImage = Resources.LavenderFlatButtonUp;
                    pressedImage = Resources.LavenderFlatButtonDown;
                    isStretch = false;
                    break;

                case "Orange3D":
                    normalImage = Resources.OrangeGelButtonUp;
                    pressedImage = Resources.OrangeGelButtonDown;
                    isStretch = false;
                    break;

                case "OrangeFlat":
                    normalImage = Resources.OrangeFlatButtonUp;
                    pressedImage = Resources.OrangeFlatButtonDown;
                    isStretch = false;
                    break;

                case "Orchid3D":
                    normalImage = Resources.OrchidGelButtonUp;
                    pressedImage = Resources.OrchidGelButtonDown;
                    isStretch = false;
                    break;

                case "OrchidFlat":
                    normalImage = Resources.OrchidFlatButtonUp;
                    pressedImage = Resources.OrchidFlatButtonDown;
                    isStretch = false;
                    break;

                case "Pink3D":
                    normalImage = Resources.PinkGelButtonUp;
                    pressedImage = Resources.PinkGelButtonDown;
                    isStretch = false;
                    break;

                case "PinkFlat":
                    normalImage = Resources.PinkFlatButtonUp;
                    pressedImage = Resources.PinkFlatButtonDown;
                    isStretch = false;
                    break;

                case "Rainbow3D":
                    normalImage = Resources.RainbowGelButtonUp;
                    pressedImage = Resources.RainbowGelButtonDown;
                    isStretch = false;
                    break;

                case "RainbowFlat":
                    normalImage = Resources.RainbowFlatButtonUp;
                    pressedImage = Resources.RainbowFlatButtonDown;
                    isStretch = false;
                    break;

                case "Red3D":
                    normalImage = Resources.RedGelButtonUp;
                    pressedImage = Resources.RedGelButtonDown;
                    isStretch = false;
                    break;

                case "RedFlat":
                    normalImage = Resources.RedFlatButtonUp;
                    pressedImage = Resources.RedFlatButtonDown;
                    isStretch = false;
                    break;

                case "Tan3D":
                    normalImage = Resources.TanGelButtonUp;
                    pressedImage = Resources.TanGelButtonDown;
                    isStretch = false;
                    break;

                case "TanFlat":
                    normalImage = Resources.TanFlatButtonUp;
                    pressedImage = Resources.TanFlatButtonDown;
                    isStretch = false;
                    break;

                case "White3D":
                    normalImage = Resources.WhiteGelButtonUp;
                    pressedImage = Resources.WhiteGelButtonDown;
                    isStretch = false;
                    break;

                case "WhiteFlat":
                    normalImage = Resources.WhiteFlatButtonUp;
                    pressedImage = Resources.WhiteFlatButtonDown;
                    isStretch = false;
                    break;

                case "Yellow3D":
                    normalImage = Resources.YellowGelButtonUp;
                    pressedImage = Resources.YellowGelButtonDown;
                    isStretch = false;
                    break;

                case "YellowFlat":
                    normalImage = Resources.YellowFlatButtonUp;
                    pressedImage = Resources.YellowFlatButtonDown;
                    isStretch = false;
                    break;
                //END US4885

                case "PaperBrown":
                    normalImage = Resources.PaperBrownButtonUp;
                    pressedImage = Resources.PaperBrownButtonDown;
                    isStretch = false;
                    break;
                case "PaperOrange":
                    normalImage = Resources.PaperOrangeButtonUp;
                    pressedImage = Resources.PaperOrangeButtonDown;
                    isStretch = false;
                    break;
                case "PaperPurple":
                    normalImage = Resources.PaperPurpleButtonUp;
                    pressedImage = Resources.PaperPurpleButtonDown;
                    isStretch = false;
                    break;
                case "PaperGreen":
                    normalImage = Resources.PaperGreenButtonUp;
                    pressedImage = Resources.PaperGreenButtonDown;
                    isStretch = false;
                    break;
                case "PaperRed":
                    normalImage = Resources.PaperRedButtonUp;
                    pressedImage = Resources.PaperRedButtonDown;
                    isStretch = false;
                    break;
                case "PaperRainbow":
                    normalImage = Resources.PaperRainbowButtonUp;
                    pressedImage = Resources.PaperRainbowButtonDown;
                    isStretch = false;
                    break;
                case "PaperTan":
                    normalImage = Resources.PaperTanButtonUp;
                    pressedImage = Resources.PaperTanButtonDown;
                    isStretch = false;
                    break;
                case "PaperWhite":
                    normalImage = Resources.PaperWhiteButtonUp;
                    pressedImage = Resources.PaperWhiteButtonDown;
                    isStretch = false;
                    break;
            }
            ((ImageButton)ButtonPanel.Controls[keyNum]).ImageNormal = normalImage;
            ((ImageButton)ButtonPanel.Controls[keyNum]).ImagePressed = pressedImage;
            ((ImageButton)ButtonPanel.Controls[keyNum]).Stretch = isStretch;
        }
        #endregion Create Menu Buttons

        #region Button Click
        private void ButtonClick(object sender, MouseEventArgs e)
        {
            ButtonClick(sender, false);
        }

        private void ButtonClick(object sender, bool skipDialog)
        {
            // The user released the mouse quickly therefore is a button click event
            timer1.Enabled = false;

            var button = sender as ImageButton;
            if (button == null)
                return;

            Cursor = Cursors.WaitCursor;

            // Initialize the Form
            bool isDailyMenu = // US1772
                MenuTreeView.SelectedNode != null && MenuTreeView.SelectedNode.Parent != null // we should have a menu page selected, so get the parent (the menu)
                && MenuTreeView.SelectedNode.Parent.Tag is POSMenuItem
                && ((POSMenuItem)MenuTreeView.SelectedNode.Parent.Tag).IsDailyMenu; // if this is a daily menu, then we should edit it in a different way


            if (isDailyMenu)
            {
                ClickDailyButton(button, skipDialog);
            }
            else
            {
                ClickHistoryButton(button, skipDialog);
            }
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Actions that occur when the user clicks on a "history" POS button
        /// </summary>
        /// <param name="button"></param>
        /// <param name="skipDialog"></param>
        private void ClickHistoryButton(ImageButton button, bool skipDialog)
        {
            // US3692
            buttonDetailForm = new ButtonDetailForm(m_operatorId, ProdCenterSettings);

            if (string.IsNullOrEmpty(button.Text))
                buttonDetailForm.IsCreateMode = true;

            // Populate the Package List
            buttonDetailForm.PopulatePackageList = PackageItems.Sorted.ToArray();

            // Populate the Discount List
            var discountList = GetDiscountMessage.GetDiscountList();
            buttonDetailForm.PopulateDiscountList = discountList.ToArray();

            // Populate the Function List
            buttonDetailForm.PopulateFunctionList = GetFunctionMessage.GetFunctionList(0).ToArray();

            // Load the button graphic descriptions
            //buttonDetailForm.PopulateButtonGraphicsComboBox = GetButtonGraphicsMessage.GetButtonGraphics(-1).ToArray();

            // Load the Button info from the seleced button
            var menuButton = (MenuButtonList)button.Tag;

            // Set the form's values for editing.
            if (menuButton.PackageId > 0)
            {
                buttonDetailForm.IsPackage = true;
                buttonDetailForm.PackageId = menuButton.PackageId;
                buttonDetailForm.IsCreateMode = false;
            }
            else if (menuButton.DiscountId > 0)
            {
                buttonDetailForm.IsDiscount = true;
                buttonDetailForm.DiscountId = menuButton.DiscountId;
                buttonDetailForm.IsCreateMode = true;
            }
            else if (menuButton.FunctionId > 0)
            {
                buttonDetailForm.IsFunction = true;
                buttonDetailForm.FunctionId = menuButton.FunctionId;
                buttonDetailForm.IsCreateMode = true;
            }

            buttonDetailForm.ButtonGraphicId = menuButton.ButtonGraphicId;
            buttonDetailForm.curImage = button.ImageNormal; //US4935
            buttonDetailForm.isStretch = button.Stretch; //US4935
            buttonDetailForm.PageNumber = int.Parse(MenuTreeView.SelectedNode.Text.Substring(4));
            buttonDetailForm.KeyLocked = menuButton.KeyLocked;
            buttonDetailForm.KeyNumber = ButtonPanel.Controls.IndexOf(button);
            buttonDetailForm.PlayerRequired = menuButton.PlayerRequired;
            buttonDetailForm.KeyText = menuButton.KeyText ?? "";
            buttonDetailForm.DefaultValidation = menuButton.DefaultValidation;
            buttonDetailForm.RequiresAuthorization = menuButton.RequiresAuthorization;
            Cursor = Cursors.Default;

            // Display the form
            if (!skipDialog)
                buttonDetailForm.ShowDialog(this);
            else
                buttonDetailForm.Canceled = false;

            // Check if has not been canceled
            if (!buttonDetailForm.Canceled)
            {
                // Get the button info
                var menuButtonItem = new MenuButtonList[1];
                menuButtonItem[0].PackageId = 0;
                menuButtonItem[0].DiscountId = 0;
                menuButtonItem[0].FunctionId = 0;
                if (buttonDetailForm.IsPackage)
                    menuButtonItem[0].PackageId = buttonDetailForm.PackageId;
                else if (buttonDetailForm.IsDiscount)
                    menuButtonItem[0].DiscountId = buttonDetailForm.DiscountId;
                else if (buttonDetailForm.IsFunction)
                    menuButtonItem[0].FunctionId = buttonDetailForm.FunctionId;
                menuButtonItem[0].PageNumber = (byte)buttonDetailForm.PageNumber;
                menuButtonItem[0].KeyNum = (byte)buttonDetailForm.KeyNumber;
                menuButtonItem[0].KeyText = buttonDetailForm.KeyText.Length > 40 ? buttonDetailForm.KeyText.Substring(0, 40) : buttonDetailForm.KeyText;
                menuButtonItem[0].ButtonGraphicId = buttonDetailForm.ButtonGraphicId;
                menuButtonItem[0].KeyLocked = buttonDetailForm.KeyLocked;
                menuButtonItem[0].PlayerRequired = buttonDetailForm.PlayerRequired;
                menuButtonItem[0].RemoveButton = (byte)(buttonDetailForm.Cleared ? 1 : 0);
                menuButtonItem[0].ValidDevices = m_devicesForPage; // the devices this page is valid for
                menuButtonItem[0].DefaultValidation = buttonDetailForm.DefaultValidation;
                menuButtonItem[0].RequiresAuthorization = buttonDetailForm.RequiresAuthorization;

                // Get the Page's info from the selected node
                var pageItem = (PageItem)MenuTreeView.SelectedNode.Tag;

                if (buttonDetailForm.DiscountId > 0)
                {
                    foreach (var dis in discountList)
                    {
                        if (dis.DiscountId == buttonDetailForm.DiscountId)
                        {
                            menuButtonItem[0].DiscountAmount = Convert.ToDecimal(dis.DiscountAmount);
                            menuButtonItem[0].DiscountPointsPerDollar = Convert.ToDecimal(dis.PointsPerDollar);
                            menuButtonItem[0].DiscountTypeId = (int)dis.Type;
                            break;
                        }
                    }
                }
                
                // Save the button info to the database
                SetMenuButtonMessage.Save(pageItem.Menu.MenuId, menuButtonItem);

                //!! NOTE: When we start allowing for editing the package-products from this form, we should save that here !!

                // Update the button's info tag
                button.Text = menuButtonItem[0].KeyText;
                button.Tag = menuButtonItem[0];
                button.AllowDrop = false;

                if (buttonDetailForm.Cleared)
                {
                    button.Text = string.Empty;
                    button.Tag = new MenuButtonList();
                    button.AllowDrop = true;
                }
            }

            //button.ImageNormal = Resources.GrayButtonUp;

            Application.DoEvents();
            //Get the Menu Buttons Panel
            if (MenuTreeView.SelectedNode.Level == 2 && !buttonDetailForm.Canceled)
            {
                Cursor = Cursors.WaitCursor;

                // Get the Page info from the selected node
                var pageItem = (PageItem)MenuTreeView.SelectedNode.Tag;

                // Populate the buttons
                int totalPages;
                CreateMenuButtons(
                    GetMenuButtonMessage.GetButtons(pageItem.Menu.MenuId, pageItem.MenuPage, out totalPages).ToArray());

                Cursor = Cursors.Default;
            }
        }

        /// US1772
        /// <summary>
        /// Actions that occur when the user clicks on a "daily" POS button
        /// </summary>
        /// <param name="button"></param>
        /// <param name="skipDialog"></param>
        private void ClickDailyButton(ImageButton button, bool skipDialog)
        {
            if (string.IsNullOrEmpty(button.Text))
                return; // we can't currently add new buttons

            // US3692
            buttonDetailForm = new ButtonDetailForm(m_operatorId, ProdCenterSettings, true);

            buttonDetailForm.OurDailyMenuButton = (DailyMenuButton)button.Tag;

            // Populate the Package List
            buttonDetailForm.PopulatePackageList = PackageItems.Sorted.ToArray();

            // Populate the Discount List
            var discountList = GetDiscountMessage.GetDiscountList();
            buttonDetailForm.PopulateDiscountList = discountList.ToArray();

            // Populate the Function List
            buttonDetailForm.PopulateFunctionList = GetFunctionMessage.GetFunctionList(0).ToArray();

            // Load the Button info from the seleced button
            var menuButton = buttonDetailForm.OurDailyMenuButton;

            if (menuButton != null)
            {
                foreach (var packageProduct in menuButton.ProductItems)
                {
                    if (packageProduct.CardMediaId != 0 && String.IsNullOrWhiteSpace(packageProduct.CardMediaName)) // we don't get them from the server with the media name; fill it in.
                    {
                        if (m_cardMediaList.Any(x => x.CardMediaId == packageProduct.CardMediaId))
                            packageProduct.CardMediaName = m_cardMediaList.First(x => x.CardMediaId == packageProduct.CardMediaId).CardMediaName;
                    }
                    if (packageProduct.GameCategoryId != 0 && String.IsNullOrWhiteSpace(packageProduct.GameCategoryName))
                    {
                        if (m_gameCategoryList != null && m_gameCategoryList.Any(x => x.Id == packageProduct.GameCategoryId))
                            packageProduct.GameCategoryName = m_gameCategoryList.First(x => x.Id == packageProduct.GameCategoryId).Name;
                    }
                    if (packageProduct.GameTypeId != 0 && String.IsNullOrWhiteSpace(packageProduct.GameTypeName))
                    {
                        if (m_gameTypeList != null && m_gameTypeList.Any(x => x.GameTypeId == packageProduct.GameTypeId))
                            packageProduct.GameTypeName = m_gameTypeList.First(x => x.GameTypeId == packageProduct.GameTypeId).GameTypeName;
                    }
                    if (packageProduct.CardLevelId != 0 && String.IsNullOrWhiteSpace(packageProduct.CardLevelName))
                    {
                        if (m_cardLevelList != null && m_cardLevelList.Any(x => x.CardLevelId == packageProduct.CardLevelId))
                            packageProduct.CardLevelName = m_cardLevelList.First(x => x.CardLevelId == packageProduct.CardLevelId).CardLevelName;
                    }
                }
            }

            // Set the form's values for editing.
            if (menuButton.PackageId > 0)
            {
                buttonDetailForm.IsPackage = true;
                buttonDetailForm.PackageId = menuButton.PackageId;
                buttonDetailForm.IsCreateMode = false;
            }
            else if (menuButton.DiscountId > 0)
            {
                buttonDetailForm.IsDiscount = true;
                buttonDetailForm.DiscountId = menuButton.DiscountId;
                buttonDetailForm.IsCreateMode = true;
            }
            else if (menuButton.FunctionId > 0)
            {
                buttonDetailForm.IsFunction = true;
                buttonDetailForm.FunctionId = menuButton.FunctionId;
                buttonDetailForm.IsCreateMode = true;
            }

            buttonDetailForm.ButtonGraphicId = menuButton.ButtonGraphicId;
            buttonDetailForm.curImage = button.ImageNormal; //US4935
            buttonDetailForm.isStretch = button.Stretch; //US4935
            buttonDetailForm.PageNumber = int.Parse(MenuTreeView.SelectedNode.Text.Substring(4));
            buttonDetailForm.KeyLocked = menuButton.KeyLocked;
            buttonDetailForm.KeyNumber = ButtonPanel.Controls.IndexOf(button);
            buttonDetailForm.PlayerRequired = menuButton.PlayerRequired;
            buttonDetailForm.DailyProductList = menuButton.ProductItems;
            buttonDetailForm.DefaultValidation = menuButton.DefaultValidation;
            buttonDetailForm.RequiresAuthorization = menuButton.RequiresAuthorization;

            if (!String.IsNullOrWhiteSpace(menuButton.KeyText)) // DE13855 Note: Set the ProductItems first because it changes the KeyText 
                buttonDetailForm.KeyText = menuButton.KeyText;
            
            Cursor = Cursors.Default;

            // Display the form
            if (!skipDialog)
                buttonDetailForm.ShowDialog(this);
            else
                buttonDetailForm.Canceled = false;

            // Check if has not been canceled
            if (!buttonDetailForm.Canceled)
            {
                Cursor = Cursors.WaitCursor;

                // Get the button info
                var menuButtonItem = new DailyMenuButton();
                
                menuButtonItem.MenuButtonId = menuButton.MenuButtonId;

                if (buttonDetailForm.IsPackage)
                    menuButtonItem.PackageId = buttonDetailForm.PackageId;
                else if (buttonDetailForm.IsDiscount)
                    menuButtonItem.DiscountId = buttonDetailForm.DiscountId;
                else if (buttonDetailForm.IsFunction)
                    menuButtonItem.FunctionId = buttonDetailForm.FunctionId;
                
                menuButtonItem.PageNumber = (byte)buttonDetailForm.PageNumber;
                menuButtonItem.KeyNum = (byte)buttonDetailForm.KeyNumber;
                menuButtonItem.KeyText = buttonDetailForm.KeyText.Length > 40 ? buttonDetailForm.KeyText.Substring(0, 40) : buttonDetailForm.KeyText;
                menuButtonItem.ButtonGraphicId = buttonDetailForm.ButtonGraphicId;
                menuButtonItem.KeyLocked = buttonDetailForm.KeyLocked;
                menuButtonItem.PlayerRequired = buttonDetailForm.PlayerRequired;
                menuButtonItem.RemoveButton = buttonDetailForm.Cleared;
                menuButtonItem.ProductItems = buttonDetailForm.DailyProductList;
                menuButtonItem.DefaultValidation = buttonDetailForm.DefaultValidation;
                menuButtonItem.RequiresAuthorization = buttonDetailForm.RequiresAuthorization;

                // DE13827 things the button form doesn't know about that we need to grab from the original item
                menuButtonItem.PackageId = menuButton.PackageId;
                menuButtonItem.DiscountId = menuButton.DiscountId;
                menuButtonItem.FunctionId = menuButton.FunctionId;
                menuButtonItem.ChargeDeviceFee = menuButton.ChargeDeviceFee;
                menuButtonItem.ReceiptText = menuButton.ReceiptText;
                menuButtonItem.KeyColor = menuButton.KeyColor;

                // Get the Page's info from the selected node
                var pageItem = (PageItem)MenuTreeView.SelectedNode.Tag;

                if (buttonDetailForm.DiscountId > 0)
                {
                    foreach (var dis in discountList)
                    {
                        if (dis.DiscountId == buttonDetailForm.DiscountId)
                        {
                            menuButtonItem.DiscountAmount = Convert.ToDecimal(dis.DiscountAmount);
                            menuButtonItem.DiscountPointsPerDollar = Convert.ToDecimal(dis.PointsPerDollar);
                            menuButtonItem.DiscountTypeId = (int)dis.Type;
                            break;
                        }
                    }
                }
                
                Cursor = Cursors.WaitCursor;
                Tuple<POSMenuItem, DailyMenuButton> tup = new Tuple<POSMenuItem, DailyMenuButton>(pageItem.Menu, menuButtonItem);
                saveAndLoadWorker.RunWorkerAsync(tup);

                m_waitForm.Message = "Saving daily menu";
                m_waitForm.ShowDialog(this);
            }

        }
        #endregion Button Click

        #region Button Drag & Drop

        private void ButtonDragDrop(object sender, DragEventArgs e)
        {
            var button = sender as ImageButton;
            if (button != null)
            {
                button.Text = e.Data.GetData(DataFormats.Text).ToString();
                button.AllowDrop = false;
                button.ImageNormal = Resources.GrayButtonUp;
                button.ImagePressed = Resources.GrayButtonUp;
            }
            isDragging = true;

            Cursor = Cursors.WaitCursor;

            // Get the Page's info from the selected node
            var pageItem = (PageItem)MenuTreeView.SelectedNode.Tag;

            var menuButtonItem = new MenuButtonList[2];

            // First remove the old button
            draggedButton.RemoveButton = 1; // Removes the old button...
            menuButtonItem[0] = draggedButton;

            // Change the button's position
            draggedButton.KeyNum = (byte)ButtonPanel.Controls.IndexOf(button); // Change the button's position
            draggedButton.RemoveButton = 0; // To create the new button
            if (button != null)
            {
                button.Tag = draggedButton;
                menuButtonItem[1] = (MenuButtonList)button.Tag;
                if (draggedButton.ButtonGraphicId != 0)
                {
                    SetButtonGraphic(draggedButton.ButtonGraphicId, draggedButton.KeyNum);
                }
            }

            // Set the button list
            SetMenuButtonMessage.Save(pageItem.Menu.MenuId, menuButtonItem);
            Cursor = Cursors.Default;
        }

        #region Button Mouse Down
        private void ButtonMouseDown(object sender, MouseEventArgs e)
        {
            var button = sender as ImageButton;
            if (button != null)
            {
                bool isDailyMenu = // US1772. DE13856, DE13857
                    MenuTreeView.SelectedNode != null && MenuTreeView.SelectedNode.Parent != null // we should have a menu page selected, so get the parent (the menu)
                    && MenuTreeView.SelectedNode.Parent.Tag is POSMenuItem
                    && ((POSMenuItem)MenuTreeView.SelectedNode.Parent.Tag).IsDailyMenu;

                // If the user holds the button down for more than 200 ms then is a drag and drop event
                // otherwise is a button click event.
                if (button.Text != string.Empty && e.Button == MouseButtons.Left)
                {
                    timer1.Tag = button;
                    timer1.Enabled = true;
                }

                // Enable/Disable context menu options depending on the button's state
                if (e.Button == MouseButtons.Right)
                {
                    addButtonToolStripMenuItem.Enabled = button.Text == string.Empty && !isDailyMenu;
                    editButtonToolStripMenuItem.Enabled = button.Text != string.Empty;
                    copyButtonToolStripMenuItem.Enabled = button.Text != string.Empty && !isDailyMenu;
                    pasteButtonToolStripMenuItem.Enabled = button.Text == string.Empty
                                                           && !string.IsNullOrEmpty(copiedButton.Text) && !isDailyMenu;
                    deleteButtonToolStripMenuItem.Enabled = button.Text != string.Empty && !isDailyMenu;
                    buttonContextMenu.Tag = button; // Pass the button to the control tag
                }
            }
        }
        #endregion Button Mouse Down

        #region Button Context Menu
        private void addButtonToolStripMenuItem_Click(object sender, EventArgs e) { ButtonClick(buttonContextMenu.Tag, null); }

        private void editButtonToolStripMenuItem_Click(object sender, EventArgs e) { ButtonClick(buttonContextMenu.Tag, null); }

        private void copyButtonToolStripMenuItem_Click(object sender, EventArgs e) { copiedButton = (ImageButton)buttonContextMenu.Tag; }

        private void pasteButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get the copied button
            var sourceButton = copiedButton;

            // Get the button's attributes from the source button's tag
            var sourceMenuButtonList = (MenuButtonList)sourceButton.Tag;

            // Get the destination button
            var destButton = (ImageButton)buttonContextMenu.Tag;
            destButton.Text = string.Empty;

            // Get the button's attributes from the destination button's tag
            var destMenuButtonList = (MenuButtonList)destButton.Tag;

            // Save the new location
            var newKeyNum = destMenuButtonList.KeyNum;
            var newPageNumber = destMenuButtonList.PageNumber;

            // Copy the button's attributes
            destMenuButtonList = sourceMenuButtonList;

            // Change to the new location
            destMenuButtonList.KeyNum = newKeyNum;
            destMenuButtonList.PageNumber = newPageNumber;
            destMenuButtonList.KeyText = sourceMenuButtonList.KeyText;

            // Update the button's tag
            destButton.Tag = destMenuButtonList;

            ButtonClick(destButton, true);
        }

        private void deleteButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Get the button
            var button = (ImageButton)buttonContextMenu.Tag;

            // Get the button's attributes from the button's tag
            var menuButtonList = (MenuButtonList)button.Tag;

            // Get the button info
            var menuButtonItem = new MenuButtonList[1];
            menuButtonItem[0].PackageId = menuButtonList.PackageId;
            menuButtonItem[0].DiscountId = menuButtonList.DiscountId;
            menuButtonItem[0].FunctionId = menuButtonList.FunctionId;
            menuButtonItem[0].PageNumber = menuButtonList.PageNumber;
            menuButtonItem[0].KeyNum = menuButtonList.KeyNum;
            menuButtonItem[0].KeyText = menuButtonList.KeyText;
            menuButtonItem[0].ButtonGraphicId = menuButtonList.ButtonGraphicId;
            menuButtonItem[0].KeyLocked = menuButtonList.KeyLocked;
            menuButtonItem[0].PlayerRequired = menuButtonList.PlayerRequired;
            menuButtonItem[0].RemoveButton = 1;

            // Get the Page's info from the selected node
            var pageItem = (PageItem)MenuTreeView.SelectedNode.Tag;

            // Save the button info to the database
            SetMenuButtonMessage.Save(pageItem.Menu.MenuId, menuButtonItem);

            // Update the button's info tag
            button.Text = string.Empty;
            button.Tag = new MenuButtonList();
            button.AllowDrop = true;
            button.ImageNormal = Resources.GrayButtonUp;
            button.ImagePressed = Resources.GrayButtonDown;
            button.Stretch = true;

            Cursor = Cursors.Default;
        }
        #endregion Button Context Menu

        #region Button Drag Enter
        private void ButtonDragEnter(object sender, DragEventArgs e)
        {
            timer1.Enabled = false;

            var button = sender as ImageButton;
            if (button != null)
            {
                if (e.Data.GetDataPresent(DataFormats.Text))
                {
                    e.Effect = DragDropEffects.Move;
                    if (button.Stretch)
                    {
                        button.ImageNormal = Resources.GrayButtonDown;
                        button.ImagePressed = Resources.GrayButtonDown;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                    if (button.Stretch)
                    {
                        button.ImageNormal = Resources.GrayButtonUp;
                        button.ImagePressed = Resources.GrayButtonUp;
                    }
                }
            }
        }
        #endregion Button Drag Enter

        #region Button Drag Leave
        private void ButtonDragLeave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            // FIX: DE2407 TA3216 Eliminate button artifacts when dragging
            ImageButton button = sender as ImageButton;

            // Load the Button info from the seleced button
            if (button != null)
            {
                button.ImageNormal = Resources.GrayButtonUp;
                button.ImagePressed = Resources.GrayButtonUp;
            }
            // END: DE2407 TA3216 Eliminate button artifacts when dragging
        }
        #endregion Button Drag Leave

        #region Button Timer Tick
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            // Get the button's info from the sender's tag
            var ti = sender as Timer;
            if (ti != null)
            {
                var button = ti.Tag as ImageButton;

                // Load the Button info from the seleced button
                if (button != null && button.Tag is MenuButtonList) // DE14013 temporarily don't allow daily button dragging
                {
                    draggedButton = (MenuButtonList)button.Tag;

                    // Do the button drag and drop event...
                    button.DoDragDrop(button.Text, DragDropEffects.Copy | DragDropEffects.Move);

                    // since the Button is being dragged we can clear it
                    if (isDragging)
                    {
                        var buttonItem = new MenuButtonList();
                        button.Tag = buttonItem;
                        button.Text = string.Empty;
                        button.Stretch = true;
                        button.ImageNormal = Resources.GrayButtonUp;
                        button.ImagePressed = Resources.GrayButtonUp;
                        button.AllowDrop = true;
                        isDragging = false;
                    }
                }
            }
        }
        #endregion Button Timer Tick

        #endregion Button Drag & Drop

        #region Menu List Key Down
        private void m_menuList_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    EditMenuClick(this, e);
                    break;
                case Keys.Delete:
                    DeleteMenuClick(this, e);
                    break;
                case Keys.Insert:
                    AddMenuClick(this, e);
                    break;
            }
        }
        #endregion Menu List Key Down
    }
}