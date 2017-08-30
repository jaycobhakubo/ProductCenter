using System;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Data;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class MenuDetailForm : GradientForm
    {
        protected DisplayMode DisplayMode = new NormalDisplayMode();

        #region Properties
        /// <summary>
        /// GTI supports only POS menus, so force the display to only POS
        /// </summary>
        public MenuTypeListItem SetConstantMenuType
        {
            set
            {
                m_menuTypeList.Text = value.MenuTypeName;
                m_menuTypeList.Enabled = false;
            }
        }
        /// <summary>
        /// Set the Menu Type List
        /// </summary>
        public Array PopulateMenuTypeList
        {
            set
            {
                // Clear the Menu Type List
                m_menuTypeList.Items.Clear();

                // Populate the Menu Type List.
                foreach (MenuTypeListItem menuTypeListItem in value)
                {
                    m_menuTypeList.Items.Add(new ListItem(menuTypeListItem.MenuTypeName, menuTypeListItem.MenuTypeId.ToString()));
                }
            }
        }

        /// <summary>
        /// Get or Set the Menu Type.
        /// </summary>
        public string MenuTypeName
        {
            get
            {
                ListItem li = (ListItem)m_menuTypeList.SelectedItem;
                return li.Text;
            }
            set
            {
                m_menuTypeList.Text = value;
            }
        }

        /// <summary>
        /// Get the Menu Type Id.
        /// </summary>
        public string MenuTypeId
        {
            get
            {
                ListItem li = (ListItem)m_menuTypeList.SelectedItem;
                return li.Value;
            }
            set
            {
                // Set the deafult text to be edited using the id value.
                ListItem li;
                string strText = "";
                for (int i = 0; i < m_menuTypeList.Items.Count; i++)
                {
                    li = (ListItem)m_menuTypeList.Items[i];
                    if (li.Value == value)
                    {
                        strText = li.Text;
                    }
                }
                m_menuTypeList.Text = strText;
            }
        }

        /// <summary>
        /// Gets or Sets the Menu Name.
        /// </summary>
        public string MenuName
        {
            get
            {
                return m_menuName.Text;
            }
            set
            {
                m_menuName.Text = value;
            }
        }
        #endregion 

        #region Constructors
        public MenuDetailForm()
        {
            InitializeComponent();

                        //Set new flat background
            //System.Drawing.Color defaultBackground = System.Drawing.ColorTranslator.FromHtml("#44658D");
            //this.BackColor = defaultBackground;
            //this.ForeColor = System.Drawing.Color.White;

            AcceptButton = m_accept;
            CancelButton = m_cancel;
            EnableButtons();
        }
        #endregion Constructors

        #region Member Methods
        private void EnableButtons()
        {
            m_accept.Enabled = m_menuName.Text != "" &&
                m_menuTypeList.SelectedIndex != -1;
        }

        private void m_accept_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void m_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion Member Methods

        private void m_menuName_TextChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }

    }
}