using System;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Data;
using GTI.Modules.ProductCenter.Properties;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class SelectProductForm : GradientForm
    {
        #region Constants

        protected DisplayMode DisplayMode = new NormalDisplayMode();

        private Array productList;
        #endregion Constants

        #region Constructors

        public SelectProductForm()
        {
            InitializeComponent();

            // Create and assign the form's idle event
            Application.Idle += OnIdle;
            AcceptButton = m_next;
            CancelButton = m_cancel;
        }

        private void OnIdle(object sender, EventArgs e)
        {
            //When form is in idle state will execute this; Enable or Disable controls here.
            m_next.Enabled = m_productList.SelectedIndex != -1;

            if (m_productList.SelectedIndex != -1)
            {
                // Get the info from the selected item.
                var li = (ListItem)m_productList.SelectedItem;

                var iproductId = int.Parse(li.Value);

                // Find the selected items's object
                foreach (ProductItemList productItem in productList)
                {
                    if (productItem.ProductItemId == iproductId)
                    {
                        SelectedProductItem = productItem;
                    }
                }

                m_productType.Text = SelectedProductItem.ProductTypeName;              
                m_productGroup.Text = SelectedProductItem.ProductGroupName;
                m_salesSource.Text = SelectedProductItem.ProductSalesSourceName;//RALLY US1863
                //START RALLY DE 6642
                if(string.IsNullOrEmpty(m_productGroup.Text))
                {
                    m_productGroup.Text = Resources.ProductGroupNone;
                }
                //END RALLY DE 6642
            }
            else
            {
                
                m_productType.Text = string.Empty;
                m_productGroup.Text = string.Empty;
                m_salesSource.Text = string.Empty;//RALLY US1863
            }
        }

        #endregion Constructors

        #region Methods

        private void m_next_Click(object sender, EventArgs e)
        {
            Application.Idle -= OnIdle;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void m_cancel_Click(object sender, EventArgs e)
        {
            Application.Idle -= OnIdle;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion Methods

        #region Properties
        /// <summary>
        /// Gets the user's selected product item.
        /// </summary>
        public ProductItemList SelectedProductItem { get; private set; }

        /// <summary>
        /// Populates the Product List.
        /// </summary>
        public Array ProductList
        {
            set
            {
                // Save the current product list object.
                productList = value;

                // Clear the Product List.
                m_productList.Items.Clear();

                // Populate the Product List.
                foreach (ProductItemList productItem in value)
                {
                    //RALLY DE 6770
                    if (productItem.ProductItemId > 0 && productItem.IsActive) // DE8783 shouldn't be able to selet inactive products
                    {
                        m_productList.Items.Add(new ListItem(productItem.ProductItemName, productItem.ProductItemId.ToString()));
                    }
                }
            }
        }

        #endregion Properties
    }
}
