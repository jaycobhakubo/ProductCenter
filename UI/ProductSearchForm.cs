using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using GTI.Modules.ProductCenter.Business;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Properties;
using GTI.Modules.ProductCenter.Data;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class ProductSearchForm :  GradientForm
    {
        protected DisplayMode DisplayMode = new NormalDisplayMode();

        #region Product Properties
        /// <summary>
        /// Get or Set the Product Name
        /// </summary>
        public string ProductName
        {
            get { return m_productName.Text; }
            set { m_productName.Text = value; }
        }

        /// <summary>
        /// Set the Product Type List
        /// </summary>
        public Array PopulateProductTypesList
        {
            set
            {
                // Clear the Product Types List
                cboProductTypes.Items.Clear();
                cboProductTypes.Items.Add(new ListItem("(All)", "0"));
                // Populate the Product Types List
                foreach (ProductTypeListItem productTypeListItem in value)
                {

                    if (!CrystalBallEnabled)
                    {
                        if ((productTypeListItem.ProductTypeId > 0 && productTypeListItem.ProductTypeId < 5) || productTypeListItem.ProductTypeId == 21)
                            continue;
                    }

                    cboProductTypes.Items.Add(new ListItem(productTypeListItem.ProductType, productTypeListItem.ProductTypeId.ToString()));
                }               
            }
        }

        /// <summary>
        /// Get the Product Type Id.
        /// </summary>
        public string ProductTypeId
        {
            get { return ((ListItem)cboProductTypes.SelectedItem).Value; }
        }

        public string ProductTypeName
        {
            get { return ((ListItem)cboProductTypes.SelectedItem).ToString(); }
        }
        #endregion
        
        private bool CrystalBallEnabled 
        { 
            get; 
            set; 
        }
       

        #region Constructors

        public ProductSearchForm(ProductCenterSettings settings)
        {
            InitializeComponent();

            //Set new flat background
            //System.Drawing.Color defaultBackground = System.Drawing.ColorTranslator.FromHtml("#44658D");
            //this.BackColor = defaultBackground;
            //this.ForeColor = System.Drawing.Color.White;

            AcceptButton = m_accept;
            CancelButton = m_cancel;

            CrystalBallEnabled = settings.CrystalBallEnabled;

            m_productName.Text = "";
        }

        public int GetLastProductTypeIndex()
        {
            return cboProductTypes.SelectedIndex;
        }

        public void SetLastProductTypeIndex(int index)
        {
            if (cboProductTypes != null)
            {
                if (index > 0 && index < cboProductTypes.Items.Count)
                {
                    cboProductTypes.SelectedIndex = index;
                }
                else
                {
                    cboProductTypes.SelectedIndex = 0;
                }
            }
        }

        public void SetLastProductName(string productName)
        {           
            m_productName.Text = productName;     
        }
        #endregion

        #region Button event handlers

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
        #endregion
    }
}