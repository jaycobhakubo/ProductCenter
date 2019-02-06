// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © FortuNet dba GameTech
// International, Inc.
//
// US3692 Adding support for whole points
// US4460: (US4428) Product Center: Set primary validation
//DE12846: Error found in US4460: (US4428) Product Center: Set primary validation > Close button
//US4695: Product Center: Move validations setup into Validations

using System;
using System.Linq;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Data;
using GTI.Modules.ProductCenter.Business;
using GTI.Modules.ProductCenter.Properties;
using GTI.Modules.ProductCenter.UI.Discounts;
using GTI.Modules.Shared.Data;
using System.Collections.Generic;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class ProductCenterMdiForm : GradientForm 
    {
        #region Declarations
        private static int m_operatorId;
        private const string ModuleName = "ProductCenterMDIForm.";
        private static ProductsForm m_productsForm;
        private static PackagesForm m_packagesForm;
        private static MenusForm m_menusForm; 
        private static CardLevelForm m_cardLevelForm;
        private static CardColorSetManagementForm m_cardColorSetForm;
        private static CouponManagementForm m_couponManagementForm;
        private static PositionMaps.CardPositionMapManagementForm m_cardPositionMapManagementForm;
        private static CouponMangementAddForm m_cmAddForm;
        private static ElementHost m_elementhost;
        private static DiscountView m_discountView;
        private static ValidationForm m_validationForm;
        protected DisplayMode m_displayMode = new NormalDisplayMode();
        private readonly ProductCenterSettings m_productCenterSettings;
        private bool m_isCouponManagement = false;//US3979
        private List<int> m_staffPermissions; // the staff's permission tokens
   
        #endregion
        
        #region Constructor Destructor
        public ProductCenterMdiForm(ProductCenterSettings objSetting)
        {
            m_productCenterSettings = objSetting;
            InitProdCtrMdiForm();
        }

        /// <summary>
        /// Close this module.
        /// </summary>
        public ProductCenterMdiForm()
        {
            Application.Exit();
        }


        private void InitProdCtrMdiForm()
        {
            InitializeComponent();
            Cursor = Cursors.WaitCursor;
            ModuleComm moduleCom = new ModuleComm();
            int staffid = moduleCom.GetStaffId();
            m_staffPermissions = new List<int>();
            try
            {
                GetStaffModuleFeaturesMessage featuresMessage = new GetStaffModuleFeaturesMessage(staffid, (int)EliteModule.ProductCenter, 0);//Get all the permission for this staff on product center.
                featuresMessage.Send();
                if (featuresMessage.ModuleFeatureList != null)
                {
                    foreach (int token in featuresMessage.ModuleFeatureList)
                    {
                        if(!m_staffPermissions.Any(x=>x == token))
                            m_staffPermissions.Add(token);
                    }
                }
            }
            catch (ServerException ex)
            {
                string err = String.Format("Unable to get staff's permissions. Reason: {0}",
                    GameTech.Elite.Client.ServerErrorTranslator.GetReturnCodeMessage((GameTech.Elite.Client.ServerReturnCode)ex.ReturnCode));
                ProductCenter.Business.ProductCenter.Log(err, LoggerLevel.Severe);
                MessageForm.Show(err);
            }
            catch (Exception ex)
            {
                m_menusForm.PopulateDailyMenuList = null;
                string err = "Unable to get staff's permissions. Reason: " + ex.ToString();
                ProductCenter.Business.ProductCenter.Log(err, LoggerLevel.Severe);
                MessageForm.Show(err);
            }

            //Set the Operator Id
            var modComm = new ModuleComm();
            try
            {
                m_operatorId = modComm.GetOperatorId();
            }
            catch (ServerCommException ex)
            {
                MessageForm.Show( ModuleName + "Unable to obtain Operator Id " + ex.Message, Resources.GetOperatorIDTitle, MessageFormTypes.OK, 0);
            }

            // DE14108 display coupon management if the setting is enabled and they have permission to
            couponManagementsToolStripMenuItem.Visible = m_productCenterSettings.AllowCouponManagement && m_staffPermissions.Any(x => x == (int)ModuleFeature.CouponManagement);

            cardPositionMapsTSMI.Visible = m_staffPermissions.Any(p => p == (int)ModuleFeature.CardPositionMapManagement);

            StartPosition = FormStartPosition.CenterScreen;
            productsToolStripMenuItem_Click(null, null);
            m_discountView = null; m_elementhost = null; m_validationForm = null;
        }

        #endregion
        
        #region Events
        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ProductCenterMdiForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(m_cardColorSetForm != null && m_cardColorSetForm.EditMode)
            {
                var dr = MessageForm.Show(this, "Card color set form has unsaved changes." + Environment.NewLine
                                            + "Closing now will discard any unsaved changes." + Environment.NewLine
                                            + Environment.NewLine
                                            + "Do you want to discard changes and close?"
                                            , "Unsaved Changes", MessageFormTypes.YesNo);
                if(dr != System.Windows.Forms.DialogResult.Yes)
                    e.Cancel = true;
            }
        }

        private void ProductCenterMDIForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            OnIdleHandlerSetup("");
        }

        private string m_formWithIdleActive = String.Empty;

        private void OnIdleHandlerSetup(string formName)
        {
            
            // Unhook Idle event
            switch (m_formWithIdleActive)
            {
                case "Products":
                    m_productsForm.UnHookIdle();                  
                    break;
                case "Packages":
                    m_packagesForm.UnHookIdle();                  
                    break;
                case "Menus":
                    m_menusForm.UnHookIdle();                
                    break;
                case "CardLevels":
                    m_cardLevelForm.UnHookIdle();                  
                    break;
                case "Coupon":
                    m_couponManagementForm.UnHookIdle();
                    break;
                case "Discounts":
                    m_discountView.UnHookIdle();
                    m_elementhost.SendToBack();
                    break;
                case "Validation":

                    break;
            }

            //START RALLY US1796          
            // Hook Idle event to new form
            switch (formName)
            {
                case "Products":
                    m_productsForm.HookIdle();
                    m_searchMenuItem.Text = "&Search Products...";
                    toolStripSeparator1.Visible = true;
                    m_searchMenuItem.Visible = true;
                    break;
                case "Packages":
                    m_packagesForm.HookIdle();
                    m_searchMenuItem.Text = "&Search Packages...";
                    toolStripSeparator1.Visible = true;
                    m_searchMenuItem.Visible = true;
                    break;
                case "Menus":
                    m_menusForm.HookIdle();
                    m_searchMenuItem.Text = "&Search...";
                    toolStripSeparator1.Visible = false;
                    m_searchMenuItem.Visible = false;
                    break;
                case "CardLevels":
                    m_cardLevelForm.HookIdle();
                    m_searchMenuItem.Text = "&Search...";
                    toolStripSeparator1.Visible = false;
                    m_searchMenuItem.Visible = false;
                    break;
                case "Coupon":
                    m_couponManagementForm.HookIdle();
                    m_searchMenuItem.Text = "&Search...";
                    toolStripSeparator1.Visible = false;
                    m_searchMenuItem.Visible = false;
                    break;
                case "Discounts":
                    m_discountView.HookIdle();
                    m_searchMenuItem.Text = "&Search Discounts...";
                    toolStripSeparator1.Visible = true;
                    m_searchMenuItem.Visible = true;
                    break;
                case "CardColorSets":
                    m_searchMenuItem.Text = "&Search...";
                    toolStripSeparator1.Visible = false;
                    m_searchMenuItem.Visible = false;
                    break;
                case "Validation":
                    toolStripSeparator1.Visible = false;
                    m_searchMenuItem.Visible = false;
                    break;
            }
            //END RALLY US1796
            m_formWithIdleActive = formName;
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            Cursor = Cursors.WaitCursor;

            if (m_productsForm == null || m_productsForm.IsDisposed)            //To prevent multiple instances of the same form
                // FIX TA5873
                m_productsForm = new ProductsForm(m_productCenterSettings);
                // END TA5873

            // Populate the Product Item List.
            m_productsForm.OperatorId = m_operatorId;
            
            //START RALLY US1796 
            m_productsForm.PopulateProductList = ProductItems.SearchProducts(m_operatorId, m_productsForm.lastProductName, 
                m_productsForm.lastProductTypeId, 
                m_productCenterSettings.CreditEnabled,m_productsForm.ShowInactive).ToArray();
            //END RALLY US1796

            m_productsForm.MdiParent = this;
            m_productsForm.Dock = DockStyle.Fill;
            OnIdleHandlerSetup("Products");
            SetIsCouponManagementToFalse();
            Cursor = Cursors.Default;
            m_productsForm.Show();
            m_productsForm.BringToFront();
        }

        private void packagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            // To prevent multiple instances of the same form
            if (m_packagesForm == null || m_packagesForm.IsDisposed)
                m_packagesForm = new PackagesForm();

            // Set the form's current Operator Id
            m_packagesForm.OperatorId = m_operatorId;

            Debug.WriteLine("packagesToolStripMenuItem ");
            m_packagesForm.LoadPackageTreeView(-1, m_packagesForm.lastPackageName);//RALLY US1796

            // Pass in the Product Center Global Settings.
            m_packagesForm.ProdCenterSettings = m_productCenterSettings;

            m_packagesForm.MdiParent = this;
            m_packagesForm.Dock = DockStyle.Fill;
            OnIdleHandlerSetup("Packages");
            SetIsCouponManagementToFalse();
            Cursor = Cursors.Default;
            m_packagesForm.Show();
            m_packagesForm.BringToFront();
        }

        private void menusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            // To prevent multiple instances of the same form
            if (m_menusForm == null || m_menusForm.IsDisposed)
                m_menusForm = new MenusForm(m_operatorId, m_staffPermissions);

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
                ProductCenter.Business.ProductCenter.Log("Error getting gaming date: " + ex.ToString(), LoggerLevel.Severe);
            }

            // Pass in the Product Center Global Settings.
            m_menusForm.ProdCenterSettings = m_productCenterSettings;

            // Populate the Menu List.
            m_menusForm.PopulateMenuList = MenuItems.NameSorted(m_operatorId).ToArray();

            m_menusForm.MdiParent = this;
            m_menusForm.Dock = DockStyle.Fill;
            OnIdleHandlerSetup("Menus");
            SetIsCouponManagementToFalse();
            Cursor = Cursors.Default;
            m_menusForm.Show();
            m_menusForm.BringToFront();
        }

        private void cardLevelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            // To prevent multiple instances of the same form
            if (m_cardLevelForm == null || m_cardLevelForm.IsDisposed)
                m_cardLevelForm = new CardLevelForm();

            // Populate the Menu List.
            m_cardLevelForm.PopulateCardLevels = GetCardLevelMessage.CardLevels(0);

            m_cardLevelForm.MdiParent = this;
            m_cardLevelForm.Dock = DockStyle.Fill;
            OnIdleHandlerSetup("CardLevels");
            SetIsCouponManagementToFalse();
            Cursor = Cursors.Default;
            m_cardLevelForm.Show();
            m_cardLevelForm.BringToFront();
        }

        private void cardColorSetTSMI_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            if(m_cardColorSetForm == null || m_cardColorSetForm.IsDisposed)
            {
                m_cardColorSetForm = new CardColorSetManagementForm();
                m_cardColorSetForm.EditModeChanged += cardColorSetForm_EditModeChanged;
            }

            m_cardColorSetForm.MdiParent = this;
            m_cardColorSetForm.Dock = DockStyle.Fill;
            OnIdleHandlerSetup("CardColorSets");
            SetIsCouponManagementToFalse();
            Cursor = Cursors.Default;
            m_cardColorSetForm.Show();
            m_cardColorSetForm.BringToFront();
        }

        void cardColorSetForm_EditModeChanged(object sender, EventArgs e)
        {
            setupToolStripMenuItem.Enabled = !m_cardColorSetForm.EditMode;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var assemblyTitle = string.Empty;
            var assemblyProduct = string.Empty;
            var assemblyDescription = string.Empty;

            // Get the Assembly Information
            var asm = Assembly.GetExecutingAssembly();

            // Get the Title attribute on this assembly.
            var attributes = asm.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
                var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                assemblyTitle = titleAttribute.Title;
            }

            // Get the Product attribute on this assembly.
            attributes = asm.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            if (attributes.Length > 0)
            {
                var productAttribute = (AssemblyProductAttribute)attributes[0];
                assemblyProduct = productAttribute.Product;
            }

            // Get the Description attribute on this assembly
            attributes = asm.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                var descriptionAtribute = (AssemblyDescriptionAttribute)attributes[0];
                assemblyDescription = descriptionAtribute.Description;
            }

            var ab = new AboutBox
                     {
                         AssemblyTitle = assemblyTitle,
                         AssemblyProduct = assemblyProduct,
                         AssemblyVersion = asm.GetName().Version.ToString(),
                         AssemblyDescription = assemblyDescription,                    
                     };
            ab.isProductCenter = true;
            SetIsCouponManagementToFalse();
            ab.ShowDialog(this);
        }

        //US4460
        /// <summary>
        /// Handles the Click event of the validationToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void validationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            // To prevent multiple instances of the same form
            if (m_validationForm == null || m_validationForm.IsDisposed)
                m_validationForm = new ValidationForm();

            m_validationForm.Initialize(m_productCenterSettings);

            m_validationForm.MdiParent = this;
            m_validationForm.Dock = DockStyle.Fill;
            OnIdleHandlerSetup("Validation");
            SetIsCouponManagementToFalse();
            Cursor = Cursors.Default;
            m_validationForm.Show();
            m_validationForm.BringToFront();

            //Cursor = Cursors.WaitCursor;
            //if (m_validationView == null)
            //{
            //    m_validationView = new ValidationForm();

            //    //DE12846
            //    //m_validationView.CloseButton.Click += (o, args) =>
            //    //{
            //    //    Close();
            //    //};
            //}

            //if (m_elementhost == null || m_elementhost.IsDisposed)
            //{
            //    m_elementhost = new ElementHost();
            //    Controls.Add(m_elementhost);
            //}

            //m_validationView.Load(m_productCenterSettings.CardCountValidation, m_productCenterSettings.MaxValidationsPerTransaction);

            //OnIdleHandlerSetup("Validation");
            //HideAllContainedUIs(); // do this here to minimize flicker
            //Cursor = Cursors.Default;
            //m_elementhost.BringToFront();
            //m_elementhost.Dock = DockStyle.Fill;
            ////m_elementhost.Child = m_validationView;
            //m_elementhost.Show();
        }

        private void cardPositionMapsTSMI_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            // To prevent multiple instances of the same form
            if(m_cardPositionMapManagementForm == null || m_cardPositionMapManagementForm.IsDisposed)
                m_cardPositionMapManagementForm = new PositionMaps.CardPositionMapManagementForm();

            m_cardPositionMapManagementForm.MdiParent = this;
            m_cardPositionMapManagementForm.Dock = DockStyle.Fill;
            SetIsCouponManagementToFalse();
            Cursor = Cursors.Default;
            m_cardPositionMapManagementForm.Show();
            m_cardPositionMapManagementForm.BringToFront();
        }

        //Open coupon management UI.
        private void couponManagementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (m_couponManagementForm == null || m_couponManagementForm.IsDisposed)
            { m_couponManagementForm = new CouponManagementForm(m_productCenterSettings); }
            m_couponManagementForm.MdiParent = this;
            m_couponManagementForm.Dock = DockStyle.Fill;
            m_couponManagementForm.OperatorID = m_operatorId;
            OnIdleHandlerSetup("Coupon");
            m_isCouponManagement = true;
            Cursor = Cursors.Default;
            m_couponManagementForm.Show();
            m_couponManagementForm.BringToFront();
        }

        private void mainMenuStrip_Click(object sender, EventArgs e)
        {
            if (m_isCouponManagement == true)
            {
                m_couponManagementForm.clearAnyDisplayMessage();
            }
        }

        /// <summary>
        /// Actions that occur when the new discount menu tool stip item is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DiscountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            // To prevent multiple instances of the same UI
            if (m_discountView == null)
            {
                m_discountView = new DiscountView(m_productCenterSettings)
                {
                    OperatorId = m_operatorId // Set the UI's current Operator Id
                };
            }

            if (m_elementhost == null || m_elementhost.IsDisposed)
            {
                m_elementhost = new ElementHost();
                this.Controls.Add(m_elementhost);
            }
            else
            {
                m_discountView.CloseTransitionControl();
            }
            m_discountView.InitializeData();
            m_elementhost.Child = m_discountView;
            m_elementhost.Dock = DockStyle.Fill;
            OnIdleHandlerSetup("Discounts");
            SetIsCouponManagementToFalse();
            HideAllContainedUIs(); // do this here to minimize flicker
            Cursor = Cursors.Default;
            m_elementhost.Show();
            m_elementhost.BringToFront();
        }

        //START RALLY US1796 
        private void m_searchMenuItem_Click(object sender, EventArgs e)
        {
            if (m_formWithIdleActive == "Products")
            {
                if (m_productsForm != null)
                {
                    m_productsForm.ContextMenuFilterProduct_Click(null, null);
                }
            }
            else if (m_formWithIdleActive == "Packages")
            {
                if (m_packagesForm != null)
                {
                    m_packagesForm.PackageSearchClick(null, null);
                }
            }
            else if (String.Equals(m_formWithIdleActive, "Discounts"))
            {
                if (m_discountView != null)
                {
                    m_discountView.ShowSearchBox(null, null);
                }
            }
        }

        private void SetIsCouponManagementToFalse()
        {
            if (m_isCouponManagement != false) { m_isCouponManagement = false; }
        }
        //END RALLY US1796 

        /// <summary>
        /// Hides all the other UIs
        /// </summary>
        private void HideAllContainedUIs()
        {
            if (m_packagesForm != null) // note: removes edit menu
                m_packagesForm.Hide();
            if (m_productsForm != null)
                m_productsForm.Hide();
            if (m_menusForm != null)
                m_menusForm.Hide();
            if (m_cardLevelForm != null)
                m_cardLevelForm.Hide();
            if (m_couponManagementForm != null)
                m_couponManagementForm.Hide();
            if (m_cmAddForm != null)
                m_cmAddForm.Hide();
            if(m_cardColorSetForm != null)
                m_cardColorSetForm.Hide();
        }
        #endregion


    }
}