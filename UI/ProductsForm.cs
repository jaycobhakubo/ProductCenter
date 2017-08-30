// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2012 GameTech
// International, Inc.

//UserStories fixed - US2244 OAS 6/20/2012
// US2826 Adding support for barcoded paper
//US4059 adding ability to select perm file

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using GTI.Modules.ProductCenter.Business;//RALLY DE 4125
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Properties;
using GTI.Modules.ProductCenter.Data;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class ProductsForm : GradientForm
    {
        #region Declarations
        protected DisplayMode DisplayMode = new NormalDisplayMode();
        private static ProductDetailForm productDetailForm;
        private static ProductItemList copiedProduct;
        private int lastSelectedIndex = -1;
        //START RALLY US1796
        private int lastProductTypeIndex;
        public string lastProductTypeId;
        private string lastProductTypeName;
        public string lastProductName;
        private List<ProductItemList> m_productListItem;
        //END RALLY US1796
        private readonly ProductCenterSettings mobjSetting; //RALLY DE 4125
        #endregion

        public ProductsForm(ProductCenterSettings settings) //RALLY DE 4125
        {
            mobjSetting = settings; //RALLY DE 4125
            InitializeComponent();

            //START RALLY US1796
            lastProductTypeIndex = -1;
            lastProductTypeId = "0";
            lastProductTypeName = "(All)";
            lastProductName = string.Empty;
            //END RALLY US1796
            // FIX : DE4036
            if (!mobjSetting.PlayWithPaper)
            {
                productListView.Columns.Remove(paperLayoutHeader);
            }
            // END : DE4036
            m_chkShowInactive.Checked = false;

        }

        #region Member Methods

        public void HookIdle()
        {
            if (lastSelectedIndex > -1)
            {
                productListView.SelectedIndices.Add(lastSelectedIndex);
                productListView.EnsureVisible(lastSelectedIndex);
            }
            Application.Idle += OnIdle;
        }
        public void UnHookIdle()
        {
            Application.Idle -= OnIdle;
            lastSelectedIndex = productListView.SelectedIndices.Count > 0 ? productListView.SelectedIndices[0] : -1;
        }

        private void OnIdle(object sender, EventArgs e)
        {
            //When form is in idle state will execute this.
            //Enable or Disable controls here.

            // Context menu stuff
            contextMenuEditProduct.Enabled = productListView.SelectedIndices.Count > 0;
            contextMenuDeleteProduct.Enabled = productListView.SelectedIndices.Count > 0;
            contextMenuActivateProduct.Enabled = productListView.SelectedIndices.Count > 0;
            contextMenuCopyProduct.Enabled = productListView.SelectedIndices.Count > 0;
            contextMenuPasteProduct.Enabled = !string.IsNullOrEmpty(copiedProduct.ProductItemName);
            contextMenuFilterProduct.Enabled = true;

            // Top menu stuff
            editMenuEditProduct.Enabled = productListView.SelectedIndices.Count > 0;
            editMenuCopyProduct.Enabled = productListView.SelectedIndices.Count > 0;
            editMenuPasteProduct.Enabled = !string.IsNullOrEmpty(copiedProduct.ProductItemName);
            editMenuDeleteProduct.Enabled = productListView.SelectedIndices.Count > 0;
            editMenuActivateProduct.Enabled = productListView.SelectedIndices.Count > 0;
        }

        private void AddProduct_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            //Initialize the form.
            productDetailForm = new ProductDetailForm(false, mobjSetting) //RALLY DE 4125
                                  {
                                      PopulateProductTypesList = GetProductTypesMessage.GetArray(),
                                      PopulateSalesSourceList = GetSalesSourceMessage.GetArray(),
                                      PopulateProductGroupList = GetProductGroupMessage.GetList(),
                                      PopulatePaperLayout = GetPaperLayoutMessage.GetList(),//RALLY TA 5744
                                      PopulatePermFilesList = GetPermFilesMessage.GetList(), //US4059
                                      IsActive = true,
                                      SalesSource = "Register",
                                      AccrualAssociationsCount = 0
                                  };

            productDetailForm.HideValidateCheckBox();
            Cursor = Cursors.Default;
            
            if (productDetailForm.ShowDialog(this)  == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;

                ProductItemList newProductItem = new ProductItemList();
                newProductItem.ProductItemId = 0;
                newProductItem.ProductTypeId = int.Parse(productDetailForm.ProductTypeId);
                newProductItem.SalesSourceId = int.Parse(productDetailForm.SalesSourceId);
                newProductItem.ProductItemName = productDetailForm.ProductItemName;
                newProductItem.ProductGroupName = productDetailForm.ProductGroupName;  //DE8766
                newProductItem.ProductGroupId = int.Parse(productDetailForm.ProductGroupId); //DE8766
                newProductItem.PaperLayoutId = int.Parse(productDetailForm.PaperLayoutId);
                newProductItem.AccuralList = new List<Accrual>();
                newProductItem.IsActive = productDetailForm.IsActive;
                newProductItem.BarcodedPaper = productDetailForm.BarcodedPaper;
                newProductItem.PermFileId = productDetailForm.PermFileId; //US4059
                newProductItem.Validate = productDetailForm.Validate;
                //Add this product to the database.

                //START RALLY DE8439
                try
                {
                    SetProductItemMessage.Save(newProductItem); //RALLY TA 5744 RALLY US1796
                }
                
                catch (ServerException ex)
                {
                    MessageForm.Show(this, ex.Message, Resources.SaveProductItem);
                    newProductItem.IsActive = true;
                }
                //END RALLY 8439
                PopulateProductList = ProductItems.SearchProducts(OperatorId, lastProductName, lastProductTypeId, mobjSetting.CreditEnabled, ShowInactive).ToArray();//RALLY  US1796
                ListViewItem lvi = productListView.FindItemWithText(productDetailForm.ProductItemName);
                if (lvi != null)
                {
                    productListView.SelectedIndices.Add(lvi.Index);
                    productListView.EnsureVisible(lvi.Index);
                }
        
                Cursor = Cursors.Default;
                Application.DoEvents();
            }
        }

        private void ProductListDoubleClick(object sender, EventArgs e)
        {
            if (productListView.SelectedIndices.Count > 0)
            {
                EditProduct_Click(this, e);
            }
        }

        private void EditProduct_Click(object sender, EventArgs e)
        {
            if (productListView.SelectedIndices.Count > 0)
            {
                var lastItem = productListView.Items;
                Cursor = Cursors.WaitCursor;

                //Initialize the form.
                productDetailForm = new ProductDetailForm(false, mobjSetting) //RALLY DE 4125
                                      {
                                          PopulateProductTypesList = GetProductTypesMessage.GetArray(),
                                          PopulateSalesSourceList = GetSalesSourceMessage.GetArray(),
                                          PopulatePaperLayout = GetPaperLayoutMessage.GetList(), //RALLY TA 5744
                                          PopulateProductGroupList = GetProductGroupMessage.GetList(),//RALLY US1796
                                          PopulatePermFilesList = GetPermFilesMessage.GetList() //US4059
                                      };

                // Get the Product Info from the Listview tag
                var productItem = (ProductItemList)productListView.SelectedItems[0].Tag;
                var selectedIndex = productListView.SelectedIndices[0];
                var topItem = productListView.TopItem;
                
                //Set the values to be edited in the detail form
                productDetailForm.ProductItemName = productItem.ProductItemName;
                productDetailForm.ProductType = productItem.ProductTypeName;
                productDetailForm.SalesSource = productItem.ProductSalesSourceName;
                productDetailForm.ProductGroupName = productItem.ProductGroupName;
                productDetailForm.PaperlayoutName = productItem.PaperLayoutName; //RALLY TA 5744
                productDetailForm.IsActive = productItem.IsActive;
                productDetailForm.BarcodedPaper = productItem.BarcodedPaper; // US2826
                productDetailForm.PermFileId = productItem.PermFileId;//US4059
                productDetailForm.Validate = productItem.Validate;
                productDetailForm.AccrualAssociationsCount = productItem.AccuralList.Count;

                Cursor = Cursors.Default;

                if (productDetailForm.ShowDialog(this) == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    // Get the Product info from the form
                    productItem.ProductItemName = productDetailForm.ProductItemName;
                    productItem.ProductTypeId = int.Parse(productDetailForm.ProductTypeId);
                    productItem.ProductTypeName = productDetailForm.ProductType;
                    productItem.SalesSourceId = int.Parse(productDetailForm.SalesSourceId);
                    productItem.ProductSalesSourceName = productDetailForm.SalesSource;
                    productItem.ProductGroupId = int.Parse(productDetailForm.ProductGroupId);
                    productItem.ProductGroupName = productDetailForm.ProductGroupName;
                    productItem.PaperLayoutId = int.Parse(productDetailForm.PaperLayoutId);//RALLY TA 5744
                    productItem.PaperLayoutName = productDetailForm.PaperlayoutName;//RALLY TA 5744
                    productItem.AccuralList = productItem.AccuralList;
                    productItem.IsActive = productDetailForm.IsActive;
                    productItem.BarcodedPaper = productDetailForm.BarcodedPaper; //US2826
                    productItem.PermFileId = productDetailForm.PermFileId;//US4059
                    productItem.Validate = productDetailForm.Validate;
                    //START RALLY DE8439
                    try
                    {
                        SetProductItemMessage.Save(productItem); //RALLY TA 5744 RALLY US1796
                    }

                    catch (ServerException ex)
                    {
                        MessageForm.Show(this, ex.Message, Resources.SaveProductItem);
                        productItem.IsActive = true;
                    }
                    //END RALLY 8439
                    PopulateProductList = ProductItems.SearchProducts(OperatorId, lastProductName, lastProductTypeId, mobjSetting.CreditEnabled, ShowInactive).ToArray();//RALLY  US1796//RALLY DE 6809
                    productListView.TopItem = topItem;
                    if (!(selectedIndex == (lastItem.Count) && !productItem.IsActive))
                    {
                        productListView.EnsureVisible(selectedIndex);
                        productListView.SelectedIndices.Add(selectedIndex);
                    }
                    else if (lastItem.Count > 0)
                    {

                        selectedIndex = selectedIndex - 1;
                        productListView.EnsureVisible(selectedIndex);
                        productListView.SelectedIndices.Add(selectedIndex);

                    }


                    Cursor = Cursors.Default;
                }
            }
        }

        private void CopyProduct_Click(object sender, EventArgs e)
        {
            if (productListView.SelectedIndices.Count > 0)
            {
                copiedProduct = (ProductItemList)productListView.SelectedItems[0].Tag;
            }
        }

        private void PasteProduct_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(copiedProduct.ProductItemName))
            {
                Cursor = Cursors.WaitCursor;

                //Initialize the form.
                productDetailForm = new ProductDetailForm(false, mobjSetting)//RALLY TA 4125
                                      {
                                          PopulateProductTypesList = GetProductTypesMessage.GetArray(),
                                          PopulateSalesSourceList = GetSalesSourceMessage.GetArray(),
                                          PopulateProductGroupList = GetProductGroupMessage.GetList(),
                                          PopulatePaperLayout = GetPaperLayoutMessage.GetList(),
                                          ProductItemName = string.Empty,
                                          ProductType = copiedProduct.ProductTypeName,
                                          SalesSource = copiedProduct.ProductSalesSourceName,
                                          ProductGroupName = copiedProduct.ProductGroupName,//RALLY TA 5744
                                          PaperlayoutName = copiedProduct.PaperLayoutName,//RALLY TA 5744
                                          PopulatePermFilesList = GetPermFilesMessage.GetList(), //US4059
                                          IsActive = true,
                                          BarcodedPaper = copiedProduct.BarcodedPaper, //US2826
                                          PermFileId = copiedProduct.PermFileId, //US4059
                                          Validate = copiedProduct.Validate,
                                          AccrualAssociationsCount = copiedProduct.AccuralList.Count
                                      };
                Cursor = Cursors.Default;

                if (productDetailForm.ShowDialog(this) == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    ProductItemList newProductItem = new ProductItemList();
                    newProductItem.ProductItemId = 0;
                    newProductItem.ProductTypeId = int.Parse(productDetailForm.ProductTypeId);
                    newProductItem.SalesSourceId = int.Parse(productDetailForm.SalesSourceId);
                    newProductItem.ProductItemName = productDetailForm.ProductItemName;
                    newProductItem.PaperLayoutId = int.Parse(productDetailForm.PaperLayoutId);
                    //START RALLY DE8873
                    newProductItem.ProductGroupId = int.Parse(productDetailForm.ProductGroupId);
                    newProductItem.ProductGroupName = productDetailForm.ProductGroupName;
                    newProductItem.PaperLayoutId = int.Parse(productDetailForm.PaperLayoutId);
                    newProductItem.PaperLayoutName = productDetailForm.PaperlayoutName;
                    //END RALLY DE8873
                    newProductItem.AccuralList = copiedProduct.AccuralList;
                    newProductItem.IsActive = true;
                    newProductItem.BarcodedPaper = productDetailForm.BarcodedPaper; //US2826
                    newProductItem.PermFileId = productDetailForm.PermFileId; //US4059
                      newProductItem.Validate = productDetailForm.Validate;
                    //START RALLY DE8439
                    try
                    {
                        SetProductItemMessage.Save(newProductItem); //RALLY TA 5744 RALLY US1796
                    }

                    catch (ServerException ex)
                    {
                        MessageForm.Show(this, ex.Message, Resources.SaveProductItem);
                    }
                    //END RALLY 8439

                    PopulateProductList = ProductItems.SearchProducts(OperatorId, lastProductName, lastProductTypeId, mobjSetting.CreditEnabled,ShowInactive).ToArray();//RALLY  US1796//RALLY DE 6809
                    ListViewItem lvi = productListView.FindItemWithText(productDetailForm.ProductItemName);
                    if (lvi != null)
                    {
                        productListView.SelectedIndices.Add(lvi.Index);
                        productListView.EnsureVisible(lvi.Index);
                    }

                  

                    Cursor = Cursors.Default;
                }
            }
        }

        private void DeleteProduct_Click(object sender, EventArgs e)
        {
            if (productListView.SelectedIndices.Count > 0)
            {
                // Get the Product Item info.
                ProductItemList productItem = (ProductItemList)productListView.SelectedItems[0].Tag;
                var selectedIndex = productListView.SelectedIndices[0];
                var topItem = productListView.TopItem;
                var lastItem = productListView.Items;

                bool doDelete = MessageForm.Show(Resources.ConfirmInactivation, Resources.InactivateProductTitle, MessageFormTypes.YesNo, 0)//RALLY DE8938
                               == DialogResult.Yes;
                
                if (doDelete)
                {
                    // Update the product listview...              
                    productItem.IsActive = false;
                    //START RALLY DE8439
                    try
                    {
                        SetProductItemMessage.Save(productItem); //RALLY TA 5744 RALLY US1796
                    }

                    catch (ServerException ex)
                    {
                        MessageForm.Show(this, ex.Message, Resources.SaveProductItem);
                        productItem.IsActive = true;
                    }
                    //END RALLY 8439

                    //DE8769
                    PopulateProductList = ProductItems.SearchProducts(OperatorId, lastProductName, lastProductTypeId, mobjSetting.CreditEnabled, ShowInactive).ToArray();//RALLY  US1796//RALLY DE 6809
                   // productListView.TopItem = topItem;

                    if (!(selectedIndex == (lastItem.Count) && !productItem.IsActive))
                    {
                        productListView.EnsureVisible(selectedIndex);
                        productListView.SelectedIndices.Add(selectedIndex);
                    }
                    else if (lastItem.Count > 0)

                    {
                                                
                            selectedIndex = selectedIndex - 1;
                            productListView.EnsureVisible(selectedIndex);
                            productListView.SelectedIndices.Add(selectedIndex);                      
                    }
                
                    Cursor = Cursors.Default;
                    
                    
                }
            }
        }

        void ProductList_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    EditProduct_Click(this, e);
                    break;
                case Keys.Delete:
                    DeleteProduct_Click(this, e);
                    break;
                case Keys.Insert:
                    AddProduct_Click(this, e);
                    break;
            }
        }

        //START RALLY US1796
        /// <summary>
        /// Called when the search products context menu is clicked
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event arguments</param>
        public void ContextMenuFilterProduct_Click(object sender, EventArgs e)
        {
            lock (this)
            {              
                ProductSearchForm form = new ProductSearchForm(mobjSetting)
                {
                    PopulateProductTypesList = GetProductTypesMessage.GetArray()
                };
                
                form.SetLastProductTypeIndex(lastProductTypeIndex);
                form.SetLastProductName(lastProductName);

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    lastProductTypeIndex = form.GetLastProductTypeIndex();
                    lastProductName = form.ProductName;
                    lastProductTypeId = form.ProductTypeId;
                    lastProductTypeName = form.ProductTypeName;
                    List<ProductItemList>newList = ProductItems.SearchProducts(OperatorId, lastProductName, lastProductTypeId, mobjSetting.CreditEnabled, ShowInactive);
                        if (newList == null)
                        {
                            MessageForm.Show(Resources.NoProductsFound, Resources.FilterProducts, MessageFormTypes.Pause, 1000);
                        }
                        else
                        {
                            PopulateProductList = newList.ToArray();  
                        }
                }
            }
        }

        /// <summary>
        /// Redraws the product controls and sets the filtered text
        /// </summary>
        public void RedrawProductsList()
        {
            lock (this)
            {
                if (string.IsNullOrEmpty(lastProductName) &&
                    lastProductTypeId == "0")
                {
                    m_filteredByText.Visible = false;
                    productListView.Location = new System.Drawing.Point(6, 39);
                }
                else
                {
                    m_filteredByText.Visible = true;       
                    productListView.Location = new System.Drawing.Point(6, 39);
                    string filteredByTextString = "Filtered by: ";
                    if (!string.IsNullOrEmpty(lastProductName))
                    {
                        filteredByTextString += string.Format("Product name: {0}", lastProductName);
                    }
                    if (lastProductTypeId != "0")
                    {
                        if (string.IsNullOrEmpty(lastProductName))
                        {
                            filteredByTextString += string.Format("Product type: {0}", lastProductTypeName);
                        }
                        else
                        {
                            filteredByTextString += string.Format(", Product type: {0}", lastProductTypeName);
                        }
                    }
                    m_filteredByText.Text = filteredByTextString;
                }
                                
            }         
        }
        //END RALLY US1796
        #endregion Member Methods

        #region Member Properties

        public List<ProductItemList> ListOfProductItem
        {
            get { return m_productListItem; }
        }

        /// <summary>
        /// Set the Operator Id.
        /// </summary>
        public int OperatorId { private get; set; }

        /// <summary>
        /// Populates the form's Product List.
        /// </summary>
        public Array PopulateProductList
        {
            set
            {
                // Clear the Product Item List.
                productListView.Items.Clear();

                m_productListItem = new List<ProductItemList>();
                m_productListItem = value.Cast<ProductItemList>().ToList();
                
                // Populate the Product Item List.
                foreach (ProductItemList productItemList in value)
                {
                    //RALLY DE 6772 unknown products
                    if (productItemList.ProductItemId > 0)
                    {
                        var lvi = productListView.Items.Add(productItemList.ProductItemName);
                        lvi.SubItems.Add(productItemList.ProductTypeName);
                        lvi.SubItems.Add(productItemList.ProductGroupName);
                        lvi.SubItems.Add(productItemList.ProductSalesSourceName);
                        
                        // START RALLY DE 4036 Add column for paper layout
                        if (mobjSetting.PlayWithPaper)
                        {
                            lvi.SubItems.Add(productItemList.PaperLayoutName);
                        }
                       
                        //END RALLY DE 4036
                        lvi.SubItems.Add(productItemList.IsActive.ToString());

                        lvi.SubItems.Add(productItemList.AccuralList.Count != 0 ? String.Format("Yes ({0})", productItemList.AccuralList.Count) : "No");

                        lvi.Tag = productItemList;
                    }
                }
                RedrawProductsList();//RALLY US1796

                //productListView.SuspendLayout();
                productListView.BeginUpdate();
                try
                {
                    productListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    int totalMinColWidth = 0;
                    for(int i = 0; i < productListView.Columns.Count; i++)
                        totalMinColWidth += productListView.Columns[i].Width;
                    int availableColWidth = productListView.Width - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
                    if(totalMinColWidth < availableColWidth)
                    {
                        decimal ratio = (decimal)availableColWidth / (decimal)totalMinColWidth;
                        for(int i = 0; i < productListView.Columns.Count; i++)
                            productListView.Columns[i].Width = (int)(productListView.Columns[i].Width * ratio);
                    }
                }
                finally
                {
                    //productListView.ResumeLayout();
                    productListView.EndUpdate();
                }
            }
        }
        #endregion Member Properties

        private void editProductGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductGroups frm = new ProductGroups();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                int selectedIndex = productListView.SelectedIndices.Count > 0 ? productListView.SelectedIndices[0] : -1;
                var topItem = productListView.TopItem;
                PopulateProductList = ProductItems.SearchProducts(OperatorId, lastProductName, lastProductTypeId, mobjSetting.CreditEnabled,ShowInactive).ToArray();//RALLY  US1796//RALLY DE 6809
                if (selectedIndex > -1)
                {
                    productListView.TopItem = topItem;
                    productListView.EnsureVisible(selectedIndex);
                    productListView.SelectedIndices.Add(selectedIndex);
                }
            }
        }

        public bool ShowInactive
        {
            get
            {
                return m_chkShowInactive.Checked;
            }
        }

        private void m_chkShowInactive_CheckedChanged(object sender, EventArgs e)
        {
            int selectedIndex = productListView.SelectedIndices.Count > 0 ? productListView.SelectedIndices[0] : -1;
            var topItem = productListView.TopItem;
            PopulateProductList = ProductItems.SearchProducts(OperatorId, lastProductName, lastProductTypeId, mobjSetting.CreditEnabled, ShowInactive).ToArray();//RALLY  US1796//RALLY DE 6809
            if (selectedIndex > -1 && selectedIndex < productListView.Items.Count)
            {
                productListView.TopItem = topItem;
                productListView.EnsureVisible(selectedIndex);
                productListView.SelectedIndices.Add(selectedIndex);
                ProductItemList productItem = (ProductItemList)productListView.SelectedItems[0].Tag;
                editMenuActivateProduct.Visible = true;
                editMenuDeleteProduct.Visible = true;
                if (productItem.IsActive)
                {
                    editMenuActivateProduct.Visible = false;
                    
                }
                else
                    editMenuDeleteProduct.Visible = false;
                
            }
        }

        private void productListView_MouseClick(object sender, MouseEventArgs e)
        {
            contextMenuDeleteProduct.Visible = true;
            contextMenuActivateProduct.Visible = true;
            editMenuActivateProduct.Visible = true;
            editMenuDeleteProduct.Visible = true;
            ProductItemList productItem = (ProductItemList)productListView.SelectedItems[0].Tag;
            if (e.Button == MouseButtons.Right)
            {
              
                if (productItem.IsActive)
                {
                    contextMenuActivateProduct.Visible = false;
                    editMenuActivateProduct.Visible = false;

                }
                else
                {
                    contextMenuDeleteProduct.Visible = false;
                    editMenuDeleteProduct.Visible = false;
                }



            }
            else
            {
                if (productItem.IsActive)
                {
                    editMenuActivateProduct.Visible = false;
                }
                else
                    editMenuDeleteProduct.Visible = false;


            }

        }

        private void contextMenuActivateProduct_Click(object sender, EventArgs e)
        {
            if (productListView.SelectedIndices.Count > 0)
            {
                // Get the Product Item info.
                ProductItemList productItem = (ProductItemList)productListView.SelectedItems[0].Tag;
                var selectedIndex = productListView.SelectedIndices[0];
                var topItem = productListView.TopItem;

                bool doActivate = MessageForm.Show(Resources.ConfirmActivate, Resources.ActivateProductTitle, MessageFormTypes.YesNo, 0)
                               == DialogResult.Yes;

                if (doActivate)
                {
                    // Update the product listview...              
                    productItem.IsActive = true;
                    //START RALLY DE8439
                    try
                    {
                        SetProductItemMessage.Save(productItem); //RALLY TA 5744 RALLY US1796
                    }

                    catch (ServerException ex)
                    {
                        MessageForm.Show(this, ex.Message, Resources.SaveProductItem);
                        productItem.IsActive = false;
                    }
                    //END RALLY 8439

                    //DE8769
                    PopulateProductList = ProductItems.SearchProducts(OperatorId, lastProductName, lastProductTypeId, mobjSetting.CreditEnabled, ShowInactive).ToArray();//RALLY  US1796//RALLY DE 6809
                    productListView.TopItem = topItem;
                    productListView.EnsureVisible(selectedIndex);
                    productListView.SelectedIndices.Add(selectedIndex);
                    Cursor = Cursors.Default;
                }
            }

        }

       
             
    }
}