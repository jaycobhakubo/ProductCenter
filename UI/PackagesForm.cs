// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © FortuNet dba GameTech
// International, Inc.
//
// US3692 Adding support for whole points
// US4460: (US4428) Product Center: Set primary validation
//DE12848: Error found in US4459: (US4428) Product Center: Create validation package > Can copy a validation product
//DE12974: Error found in US4587: Product Center: Set if a products spend qualifies for points.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Properties;
using GTI.Modules.ProductCenter.Data;
using GTI.Modules.ProductCenter.Business ;
using GTI.Modules.Shared.Business;
using GTI.Modules.Shared.Data;

//TA9926

namespace GTI.Modules.ProductCenter.UI
{
    public partial class PackagesForm : GradientForm
    {
        #region Declarations
        protected DisplayMode DisplayMode = new NormalDisplayMode();
        private static PackageDetailForm packageDetailForm;
        private static SelectProductForm selectProductForm; // This replaces the above form.
        private static bool hasOpenCredit;
        private static PackageProduct copiedProduct;
        private static PackageItem copiedPackage = new PackageItem();
        private static PackageProduct[] copiedPackageProduct;
        private static BingoProductDetailForm bingoProductDetailForm;
        private static BasicProductDetailForm basicProductDetailForm;
        private static PaperProductDetailForm paperProductDetailForm;
        private static CrystalBallProductDetailForm crystalBallProductDetailForm;
        //START RALLY US1796
        private int lastAccrualIndex;
        public string lastAccrualID;
        private string lastAccrualName;   
        public string lastPackageName;
        //END RALLY US1796
        #endregion

        #region Properties
        public int OperatorId { get; set; }
        public ProductCenterSettings ProdCenterSettings 
        { 
            get; 
            set; 
        }
        #endregion

        #region Packages TreeView loading and filtering
        /// <summary>
        /// Loads the treeview with optional selection and filtering.
        /// </summary>
        /// <param name="nodeIndex">-1 loads parent node, else load and select</param>
        /// <param name="searchString">used as filter</param>
        public void LoadPackageTreeView(int nodeIndex, string searchString)
        {
            var isSubNode = nodeIndex != -1;
            var node = isSubNode ? nodeIndex : 0;

            var srtlist = PackageItems.FilteredBy(OperatorId,searchString,"0"); //RALLY US1796
            RedrawFilterList();//RALLY US1796
            // Clear the Package Item List.
            if (isSubNode) 
                treeViewPackages.Nodes[0].Nodes.Clear();
            else
            {
                treeViewPackages.SelectedNode = treeViewPackages.Nodes[node];
                treeViewPackages.SelectedNode.Nodes.Clear();
            }

            // Populate the Package Item List.
            foreach (var packageItemList in srtlist)
            {
                // Create a new Package Item object to set in the node's tag.
                PackageItem packageItem = new PackageItem
                {
                    PackageId = packageItemList.PackageId,
                    PackageName = packageItemList.PackageName,
                    ChargeDeviceFee = packageItemList.ChargeDeviceFee,
                    ReceiptText = packageItemList.ReceiptText,
                    OverrideValidation =  packageItemList.OverrideValidation,
                    ValidationQuantity = packageItemList.ValidationQuantity
                };

                string strPackName = String.Format("{0,-25}{1,6}", packageItemList.PackageName,
                                                Helper.DecimalStringToMoneyString(packageItemList.PackagePrice));   // FIX: DE1858 TA2521 Cannot enter negative money value
                TreeNode treeNode = new TreeNode(strPackName) { Tag = packageItem, Name = packageItemList.PackageName };  // FIX: DE2369 TA3143 Add another product deselects package

                if (isSubNode) 
                    treeViewPackages.Nodes[0].Nodes.Add(treeNode);
                else
                {
                    // Fix : DE3495 Invalid index after sorting by header
                    treeViewPackages.Nodes[node].Nodes.Add(treeNode);
                    // End : DE3495 Invalid index after sorting by header
                }
            }
            // Fix : DE3495 Invalid index after sorting by header
            // Set the root node as default node and expand it.
            TreeNode curNode = isSubNode ? treeViewPackages.Nodes[0].Nodes[node] : treeViewPackages.Nodes[node];
            if (treeViewPackages.SelectedNode != curNode)
            {
                treeViewPackages.SelectedNode = isSubNode ? treeViewPackages.Nodes[0].Nodes[node] : treeViewPackages.Nodes[node];
            }
            // End : DE3495 Invalid index after sorting by header

            treeViewPackages.Focus();
            treeViewPackages.SelectedNode.Expand();
            if (isSubNode)
            {
                TreeViewPackagesAfterSelect(null, null);
            }
        }
        #endregion

        #region Constructors
        public PackagesForm()
        {
            OperatorId = 0;
            InitializeComponent();

            //Set new flat background
            //System.Drawing.Color defaultBackground = System.Drawing.ColorTranslator.FromHtml("#44658D");
            //this.BackColor = defaultBackground;
            //this.ForeColor = System.Drawing.Color.White;

            // Autoresize columns in the listview.
            for (var i = 0; i < listViewProducts.Columns.Count; i++)
            {
                listViewProducts.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            //START US RALLY1796
            lastAccrualID = "0";
            lastAccrualIndex = 0;
            lastAccrualName = "(All)";
            lastPackageName = string.Empty;
            //END US RALLY1796
        }

        public void HookIdle()
        {
            Application.Idle += OnIdle;
        }
        public void UnHookIdle()
        {
            Application.Idle -= OnIdle;
        }

        private void OnIdle(object sender, EventArgs e)
        {
            //When form is in idle state will execute this.
            //Enable or Disable controls here.

            // Context menu stuff
            contextMenuAddProduct.Enabled = treeViewPackages.SelectedNode.Level > 0;// && !IsValidationPackage;
            contextMenuEditProduct.Enabled = listViewProducts.SelectedIndices.Count > 0;
            contextMenuCopyProduct.Enabled = listViewProducts.SelectedIndices.Count > 0;// && !IsValidationPackage; //DE12848
            contextMenuPasteProduct.Enabled = !string.IsNullOrEmpty(copiedProduct.ProductName) && treeViewPackages.SelectedNode.Level > 0;// && !IsValidationPackage; // DE12848
            contextMenuDeleteProduct.Enabled = listViewProducts.SelectedIndices.Count > 0;
           
            contextMenuEditPackage.Enabled = treeViewPackages.SelectedNode.Level > 0;
            contextMenuCopyPackage.Enabled = treeViewPackages.SelectedNode.Level > 0;
            contextMenuPastePackage.Enabled = !string.IsNullOrEmpty(copiedPackage.PackageName);
            contextMenuDeletePackage.Enabled = treeViewPackages.SelectedNode.Level > 0;

            // Top menu stuff
            editMenuEditPackage.Enabled = treeViewPackages.SelectedNode.Level > 0;
            editMenuCopyPackage.Enabled = treeViewPackages.SelectedNode.Level > 0;
            editMenuPastePackage.Enabled = !string.IsNullOrEmpty(copiedPackage.PackageName);
            editMenuDeletePackage.Enabled = treeViewPackages.SelectedNode.Level > 0;


            editMenuAddProduct.Enabled = treeViewPackages.SelectedNode.Level > 0;// && !IsValidationPackage;
            editMenuEditProduct.Enabled = listViewProducts.SelectedIndices.Count > 0;
            editMenuCopyProduct.Enabled = listViewProducts.SelectedIndices.Count > 0;// && !IsValidationPackage;
            editMenuPasteProduct.Enabled = !string.IsNullOrEmpty(copiedProduct.ProductName) && treeViewPackages.SelectedNode.Level > 0;// && !IsValidationPackage;
            editMenuDeleteProduct.Enabled = listViewProducts.SelectedIndices.Count > 0;
        }
        #endregion

        #region Add Package
        private void AddPackageClick(object sender, EventArgs e)
        {
            try
            {
                //Initialize the form.
                packageDetailForm = new PackageDetailForm();

                Cursor = Cursors.Default;

                if (packageDetailForm.ShowDialog(this) == DialogResult.OK)
                {
                    // Add to the Packages List
                    Cursor = Cursors.WaitCursor;

                    int pkgId = SetPackageItemMessage.SetPackage(0,
                                                packageDetailForm.ChargeDeviceFee,
                                                packageDetailForm.PackageName,
                                                packageDetailForm.ReceiptText,
                                                packageDetailForm.OverrideValidation, 
                                                packageDetailForm.ValidationQuantity);

                    // Create a new Package Item object to set in the node's tag.
                    PackageItem packItm = new PackageItem
                                  {
                                      PackageId = pkgId,
                                      PackageName = packageDetailForm.PackageName,
                                      ChargeDeviceFee = packageDetailForm.ChargeDeviceFee,
                                      ReceiptText = packageDetailForm.ReceiptText,
                                      OverrideValidation = packageDetailForm.OverrideValidation,
                                      ValidationQuantity = packageDetailForm.ValidationQuantity
                                  };

                    // FIX: DE2369 TA3143 Add another product deselects package
                    // Add child node to root node.
                    TreeNode treeNode = new TreeNode(packageDetailForm.PackageName) { Tag = packItm, Name = packageDetailForm.PackageName };
                    // END: DE2369 TA3143 Add another product deselects package
                    treeViewPackages.Nodes[0].Nodes.Add(treeNode);

                    // Again... Set the root node as default node.
                    treeViewPackages.SelectedNode = treeNode;
                    treeViewPackages.Focus();

                    Cursor = Cursors.Default;
                    AddProductClick(null, null);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("m_addPackage_Click()....Exception: " + ex.Message);
            }
        }
        #endregion

        #region Edit Package
        private void EditPackageClick(object sender, EventArgs e)
        {
            if (treeViewPackages.SelectedNode.Level > 0)
            {
                Cursor = Cursors.WaitCursor;

                //Initialize the form.
                packageDetailForm = new PackageDetailForm();

                //Set the values to be edited in the detail form
                var packageItem = (PackageItem)treeViewPackages.SelectedNode.Tag;
                packageDetailForm.PackageName = packageItem.PackageName;
                packageDetailForm.ChargeDeviceFee = packageItem.ChargeDeviceFee;
                packageDetailForm.ReceiptText = packageItem.ReceiptText;
                packageDetailForm.OverrideValidation = packageItem.OverrideValidation;
                packageDetailForm.ValidationQuantity = packageItem.ValidationQuantity;

                Cursor = Cursors.Default;

                if (packageDetailForm.ShowDialog(this) == DialogResult.OK)
                {
                    // Update the Package List.
                    Cursor = Cursors.WaitCursor;

                    // Update the Package Treeview and the tag's PackageItem object.
                    treeViewPackages.SelectedNode.Text = packageDetailForm.PackageName;
                    packageItem.PackageName = packageDetailForm.PackageName;
                    packageItem.ChargeDeviceFee = packageDetailForm.ChargeDeviceFee;
                    packageItem.ReceiptText = packageDetailForm.ReceiptText;
                    packageItem.OverrideValidation = packageDetailForm.OverrideValidation;
                    packageItem.ValidationQuantity = packageDetailForm.ValidationQuantity;
                    treeViewPackages.SelectedNode.Tag = packageItem;

                    // Update the package in the database.
                    SetPackageItemMessage.SetPackage(packageItem.PackageId,
                                                     packageItem.ChargeDeviceFee,
                                                     packageItem.PackageName,
                                                     packageItem.ReceiptText,
                                                     packageItem.OverrideValidation,
                                                     packageItem.ValidationQuantity);
                    LoadPackageTreeView(treeViewPackages.SelectedNode.Index, lastPackageName);//RALLY US1796
                    Cursor = Cursors.Default;
                }
            }
        }
        #endregion

        #region Copy Package
        private void CopyPackageClick(object sender, EventArgs e)
        {
            if (treeViewPackages.SelectedNode.Level > 0)
            {
                copiedPackage = (PackageItem)treeViewPackages.SelectedNode.Tag;
                // Get the Product List.
                hasOpenCredit = false;
                copiedPackageProduct = new PackageProduct[listViewProducts.Items.Count];
                for (var i = 0; i < listViewProducts.Items.Count; i++)
                {
                    copiedPackageProduct[i] = (PackageProduct)listViewProducts.Items[i].Tag;

                    if (copiedPackageProduct[i].ProductTypeId == 11 || copiedPackageProduct[i].ProductTypeId == 13)
                        hasOpenCredit = true;
                }
            }
        }

        #endregion Copy Package

        #region Packages TreeView event handling
        private void TreeViewPackagesKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    EditPackageClick(sender, e);
                    break;
                case Keys.Delete:
                    DeletePackageClick(sender, e);
                    break;
                case Keys.Insert:
                    AddPackageClick(sender, e);
                    break;
            }
        }

        // Fix : DE3495 Invalid index after sorting by header
        private void FillListViewWithProduct(IEnumerable<PackageProduct> packageProducts)
        {
            int sortColumn = listViewProducts.SortColumn;
            if (sortColumn > 0) listViewProducts.PreLoadAdjustment();
            foreach (var packageProductListItem in packageProducts)
            {
                AddProductToListView(packageProductListItem, false);
            }
            if (sortColumn > 0) listViewProducts.ForceClickOnColumn(sortColumn);
        }
        // End : DE3495 Invalid index after sorting by header

        private void AddProductToListView(PackageProduct packageProductListItem, bool restoreSort) 
        {
            // Fix : DE3495 Invalid index after sorting by header
            int sortColumn = listViewProducts.SortColumn;
            if (restoreSort && sortColumn > 0)
            {
                listViewProducts.PreLoadAdjustment();
            }
            // End : DE3495 Invalid index after sorting by header
            ListViewItem lvi = listViewProducts.Items.Add(packageProductListItem.ProductName); // 0
            lvi.SubItems.Add(packageProductListItem.Quantity.ToString()); // 1
            lvi.SubItems.Add(Helper.DecimalStringToMoneyString(packageProductListItem.Price));   // 2 
            lvi.SubItems.Add(packageProductListItem.GameCategoryName); // 3
            lvi.SubItems.Add(packageProductListItem.CardTypeName); // 4
            lvi.SubItems.Add(packageProductListItem.CardLevelName); // 5
            lvi.SubItems.Add(packageProductListItem.CardCount.ToString()); // 6
            lvi.SubItems.Add(packageProductListItem.GameTypeName); // 7
            lvi.SubItems.Add(packageProductListItem.ProductTypeName); // 8
            lvi.SubItems.Add(packageProductListItem.CardMediaName); // 9
            lvi.SubItems.Add(packageProductListItem.ProgramGameName); // 10
            lvi.SubItems.Add(packageProductListItem.IsTaxed.ToString()); // 11
            lvi.SubItems.Add(packageProductListItem.SalesSourceName); // 12
            lvi.SubItems.Add(packageProductListItem.PointsPerQuantity); // 13
            lvi.SubItems.Add(packageProductListItem.PointsPerDollar); // 14
            lvi.SubItems.Add(packageProductListItem.PointsToRedeem); // 15
            lvi.SubItems.Add(packageProductListItem.NumbersRequired.ToString()); // 16

            // Set the ListViewItem tag with the Package Product Object.
            lvi.Tag = packageProductListItem;

            // Set the Has Open Credit flag.
            if (packageProductListItem.ProductTypeId == 11 || packageProductListItem.ProductTypeId == 13)
            {
                hasOpenCredit = true;
            }

            if (listViewProducts.Columns[0].Text.Length+1 >= packageProductListItem.ProductName.Length)
                listViewProducts.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            else
                listViewProducts.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
            // column 2 is the price column
            listViewProducts.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
            if (listViewProducts.Columns[9].Text.Length + 1 >= packageProductListItem.GameTypeName.Length)
                listViewProducts.AutoResizeColumn(9, ColumnHeaderAutoResizeStyle.HeaderSize);
            else
                listViewProducts.AutoResizeColumn(9, ColumnHeaderAutoResizeStyle.ColumnContent);
            // Fix : DE3495 Invalid index after sorting by header
            if (restoreSort && sortColumn > 0)
            {
                listViewProducts.ForceClickOnColumn(sortColumn);
            }
            // End : DE3495 Invalid index after sorting by header
        }

        private void TreeViewPackagesAfterSelect(object sender, TreeViewEventArgs e)
        {
            // Clear the ListView
            listViewProducts.Items.Clear();

            if (treeViewPackages.SelectedNode.Level > 0)
            {
                Cursor = Cursors.WaitCursor;

                // Get the Package Info from the selected node.
                PackageItem packageItem = (PackageItem)treeViewPackages.SelectedNode.Tag;

                // Reset the Has Open Credit flag to false.
                hasOpenCredit = false;
                List<PackageProduct> packageProducts = GetPackageProductMessage.GetPackageProducts(packageItem.PackageId, OperatorId);
                // Populate the products listview.
                FillListViewWithProduct(packageProducts.ToArray());
                Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Paste Package
        private void PastePackageClick(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(copiedPackage.PackageName))
            {
                Cursor = Cursors.WaitCursor;

                //Initialize the form.
                packageDetailForm = new PackageDetailForm();

                //Set the values to be edited in the detail form
                var packageItem = copiedPackage;
                packageDetailForm.PackageName = string.Empty;
                packageDetailForm.ChargeDeviceFee = packageItem.ChargeDeviceFee;
                packageDetailForm.ReceiptText = packageItem.ReceiptText;
                packageDetailForm.OverrideValidation = packageItem.OverrideValidation;
                packageDetailForm.ValidationQuantity = packageItem.ValidationQuantity;

                Cursor = Cursors.Default;

                if (packageDetailForm.ShowDialog(this) == DialogResult.OK)
                {
                // Add to the Packages List
                    Cursor = Cursors.WaitCursor;

                    var pkgId = SetPackageItemMessage.SetPackage(0,
                                                                 packageDetailForm.ChargeDeviceFee,
                                                                 packageDetailForm.PackageName,
                                                                 packageDetailForm.ReceiptText,
                                                                 packageDetailForm.OverrideValidation,
                                                                 packageDetailForm.ValidationQuantity);

                    // Create a new Package Item object to set in the node's tag.
                    var packageItemNew = new PackageItem
                                            {
                                                PackageId = pkgId,
                                                PackageName = packageDetailForm.PackageName,
                                                ChargeDeviceFee = packageDetailForm.ChargeDeviceFee,
                                                ReceiptText = packageDetailForm.ReceiptText,
                                                OverrideValidation = packageDetailForm.OverrideValidation,
                                                ValidationQuantity = packageDetailForm.ValidationQuantity
                                            };

                    // Set the root node as default node.
                    treeViewPackages.SelectedNode = treeViewPackages.Nodes[0];
                    treeViewPackages.Focus();
                    treeViewPackages.SelectedNode.Expand();
                    //START RALLY DE10092
                    decimal packageCost = 0.0M;
                    decimal packageProductPrice = 0.0M;

                    foreach (PackageProduct packageProduct in copiedPackageProduct)
                    {
                        if (decimal.TryParse(packageProduct.Price, out packageProductPrice))
                        {
                            packageCost += packageProductPrice * packageProduct.Quantity;
                        }
                    }

                    string strPackName;
     
                    strPackName = String.Format("{0,-25}{1,6}", packageDetailForm.PackageName,
                                                   packageCost.ToString("F2"));   // FIX: DE1858 TA2521 Cannot enter negative money value
                   
                    // Add child node to root node.
                    var treeNode = new TreeNode(strPackName) {Tag = packageItemNew};
                    //END RALLY DE 10092
                    treeViewPackages.SelectedNode.Nodes.Add(treeNode);

                    // Set the last node as default node.
                    treeViewPackages.SelectedNode = treeViewPackages.Nodes[0].LastNode;
                    treeViewPackages.Focus();
                    treeViewPackages.SelectedNode.Expand();

                    // Now add the copied products if any and populate the products listview.
                    FillListViewWithProduct(copiedPackageProduct);

                    // Save the Product List for the selected Package.
                    SavePackageProducts();

                    Cursor = Cursors.Default;
                }
            }
        }

        #endregion Paste Package

        #region Delete Package
        private void DeletePackageClick(object sender, EventArgs e)
        {
            if (treeViewPackages.SelectedNode.Level > 0)
            {
                if (MessageForm.Show(Resources.ConfirmDeletePackage, Resources.DeletePackageTitle, MessageFormTypes.YesNo, 0) == DialogResult.Yes)//RALLY DE 6657
                {
                    Cursor = Cursors.WaitCursor;

                    // Get the Package Info from the selected node.
                    var packageItem = (PackageItem)treeViewPackages.SelectedNode.Tag;

                    // Update the Package Treeview.
                    // FIX: DE8818 - Don't allow packages in use to be deleted.
                    try
                    {
                        DelPackageItemMessage.DeletePackage(packageItem.PackageId);                        
                        listViewProducts.Items.Clear();     //RALLY DE9674
                        SavePackageProducts();              //RALLY DE9674  
                        treeViewPackages.SelectedNode.Remove();
                    }
                    catch(ServerException ex)
                    {
                        MessageForm.Show(this, ex.Message, Resources.DeletePackageTitle);
                    }

                    Cursor = Cursors.Default;
                }
            }
        }

        #endregion

        #region Save Package Products
        private void SavePackageProducts()
        {
            lock (this)
            {
                try
                {
                    // Get the Package Info from the selected node.
                    var packageItem = (PackageItem)treeViewPackages.SelectedNode.Tag;

                    var intPkLstNx = treeViewPackages.SelectedImageIndex;

                    // Reset the Has Open Credit Flag.
                    hasOpenCredit = false;

                    // Get the Product List.
                    var packageProductList = new PackageProduct[listViewProducts.Items.Count];

                    for (var i = 0; i < listViewProducts.Items.Count; i++)
                    {
                        packageProductList[i] = (PackageProduct)listViewProducts.Items[i].Tag;

                        if (packageProductList[i].ProductTypeId == 11 || packageProductList[i].ProductTypeId == 13)
                            hasOpenCredit = true;
                    }

                    SetPackageProductMessage.SetPackageProduct(packageItem.PackageId, OperatorId, packageProductList);

                    treeViewPackages.SelectedImageIndex = intPkLstNx;
                    if (listViewProducts.Items.Count > 0)
                    {
                        listViewProducts.SelectedIndices.Add(listViewProducts.Items.Count - 1);
                    }
                }
                catch (Exception ex)
                {
                    MessageForm.Show("SavePackageProducts()...Exception: " + ex.Message);
                }
            }
        }
        #endregion Save Package Products

        #region Package Search
        public void PackageSearchClick(object sender, EventArgs e)
        {
            lock (this)
            {
                try
                {
                    //START RALLY US1796
                    PackageSearchForm form = new PackageSearchForm(ProdCenterSettings);
                    
                    form.SetLastPackageName(lastPackageName);
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {               
                        lastPackageName = form.PackageName;                     
                        LoadPackageTreeView(-1, lastPackageName);
                    }
                    //END RALLY US1796
                }
                catch (Exception ex)
                {
                    throw new Exception("packageSearchToolStripMenuItem_Click()...Exception: "+ex.Message );
                }
            }
        }

        /// <summary>
        /// Redraws based on the filters set
        /// </summary>
        private void RedrawFilterList()
        {
            if (!string.IsNullOrEmpty(lastPackageName) || string.Compare(lastAccrualID, "0") != 0)
            {
                listViewProducts.Location = new System.Drawing.Point(0, 17);
                treeViewPackages.Location = new System.Drawing.Point(0, 17);
               
                    if (!string.IsNullOrEmpty(lastPackageName))
                    {
                        m_filteredbyLabel.Text = string.Format(Resources.FilteredByPackageName, lastPackageName);
                    }          
            }


            else
            {
                m_filteredbyLabel.Text = string.Empty;
                listViewProducts.Location = new System.Drawing.Point(0, 0);
                treeViewPackages.Location = new System.Drawing.Point(0, 0);
            }
            
        }
        #endregion

        #region Is Validation Package
        

        public bool IsValidationPackage // US4460
        {
            get
            {
                if (listViewProducts.Items.Count != 1)
                {
                    return false;
                }

                var packageProduct = (PackageProduct)listViewProducts.Items[0].Tag;

                if (packageProduct.ProductTypeId == (int) ProductType.Validation)
                {
                    return true;
                }
                    
                return false;
            }
        }

        #endregion

        #region Add Product
        // FIX: DE2369 TA3143 Add another product deselects package
        private void AddProductClick(object sender, EventArgs e)
        {
            if (treeViewPackages.SelectedNode.Level > 0)
            {
                bool doContinue = true;
                string curNodeName = treeViewPackages.SelectedNode.Name;
                while (doContinue)
                {
                    // US4460
                    Array productList;
                    //if (listViewProducts.Items.Count > 0)
                    //{
                    //    productList = GetProductItemMessage.GetProductItems(OperatorId).Where(p => p.ProductTypeId != (int)ProductType.Validation).ToArray();
                    //}
                    //else
                    {
                        productList = GetProductItemMessage.GetProductItems(OperatorId).ToArray();
                    }

                    selectProductForm = new SelectProductForm { ProductList = productList };
                    doContinue = false;
                    if (selectProductForm.ShowDialog(this) == DialogResult.OK)
                    {
                        doContinue = DisplayProductDetails(selectProductForm.SelectedProductItem, null, false, false);
                        if (doContinue)
                        {
                            foreach (TreeNode tn in treeViewPackages.Nodes[0].Nodes)
                            {
                                if (tn.Name == curNodeName)
                                {
                                    treeViewPackages.SelectedNode = tn;
                                }
                            }
                        }
                    }
                }
            }
        }
        // END: DE2369 TA3143 Add another product deselects package

        #endregion Add Product

        #region Edit Product
        private void EditProductClick(object sender, EventArgs e)
        {
            lock (this)
            {
                try
                {
                    if (listViewProducts.SelectedIndices.Count > 0)
                    {
                        Cursor = Cursors.WaitCursor;

                        // Get the package product info from the selected list item.
                        var packageProductList = (PackageProduct)listViewProducts.SelectedItems[0].Tag;

                        // Set the product info
                        var productItem = new ProductItemList
                                                      {
                                                          IsActive = true,
                                                          ProductItemId = packageProductList.ProductId,
                                                          ProductItemName = packageProductList.ProductName,
                                                          ProductSalesSourceName = packageProductList.SalesSourceName,
                                                          ProductTypeId = packageProductList.ProductTypeId,
                                                          ProductTypeName = packageProductList.ProductTypeName,
                                                          SalesSourceId = packageProductList.SalesSourceId
                                                      };

                        DisplayProductDetails(productItem, packageProductList, true, false);
                    }
                }
                catch (Exception ex)
                {
                    MessageForm.Show("m_editProductPackage_Click(): " + ex.Message);
                }
            }
        }

        #endregion Edit Product

        #region Copy Product
        private void CopyProductClick(object sender, EventArgs e)
        {
            if (listViewProducts.SelectedIndices.Count > 0)
            {
                copiedProduct = (PackageProduct)listViewProducts.SelectedItems[0].Tag;
            }
        }

        #endregion Copy Product

        #region Paste Product
        private void PasteProductClick(object sender, EventArgs e)
        {
            lock (this)
            {
                try
                {
                    if (!string.IsNullOrEmpty(copiedProduct.ProductName))
                    {
                        Cursor = Cursors.WaitCursor;

                        // Get the package product info from the previously copied product.
                        var packageProductList = copiedProduct;

                        // Set the product info
                        var productItem = new ProductItemList
                                          {
                                              IsActive = true,
                                              ProductItemId = packageProductList.ProductId,
                                              ProductItemName = packageProductList.ProductName,
                                              ProductSalesSourceName = packageProductList.SalesSourceName,
                                              ProductTypeId = packageProductList.ProductTypeId,
                                              ProductTypeName = packageProductList.ProductTypeName,
                                              SalesSourceId = packageProductList.SalesSourceId
                                          };

                        DisplayProductDetails(productItem, packageProductList, false, true);
                    }
                }
                catch (Exception ex)
                {
                    MessageForm.Show("m_pasteProductPackage_Click(): " + ex.Message);
                }
            }
        }

        #endregion Paste Product

        #region Delete Product
        private void DeleteProductClick(object sender, EventArgs e)
        {
            if (listViewProducts.SelectedIndices.Count > 0)
            {
                if (MessageForm.Show(Resources.ConfirmDelete, Resources.DeleteProductTitle, /*MessageFormTypes.YesNo_FlatDesign*/MessageFormTypes.YesNo, 0) == DialogResult.Yes)//RALLY DE 6657
                {
                    // Remove the selected product from the list.
                    listViewProducts.SelectedItems[0].Remove();

                    // Save the Product List for the selected Package.
                    SavePackageProducts();
                    // FIX : DE3796
                    LoadPackageTreeView(treeViewPackages.SelectedNode.Index, lastPackageName);//RALLY US1796
                    // END : DE3796
                }
            }
        }

        #endregion

        #region Products ListView event handling
        private void ListViewProductsKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    EditProductClick(sender, e);
                    break;
                case Keys.Delete:
                    DeleteProductClick(sender, e);
                    break;
                case Keys.Insert:
                    AddProductClick(sender, e);
                    break;
            }
        }
        #endregion

        #region Display Product Forms
        private bool DisplayProductDetails(ProductItemList productItem, object products, bool isEdit, bool isPaste)
        {
            bool getMore = false;
            // Select the form to display depending on the product type
            switch ((ProductType)productItem.ProductTypeId)
            {
                case ProductType.CrystalBallQuickPick:
                case ProductType.CrystalBallScan:
                case ProductType.CrystalBallHandPick:
                case ProductType.CrystalBallPrompt:
                    // FIX : TA6092 Support CBB license file flag
                    // FIX : TA7890
                    if (ProdCenterSettings.CrystalBallEnabled)
                    {
                        getMore = DisplayCrystalBallProductDetailForm(productItem, products, isEdit, isPaste);
                    }
                    else
                    {
                        MessageForm.Show(Resources.ProductTypeNotSupported, Resources.ProductTypeErrorTitle, MessageFormTypes.OK);//RALLY DE 6657
                    }
                    // END : TA6092 Support CBB license file flag
                    break;
                case ProductType.Electronic:
                    getMore = DisplayBingoProductDetailForm(productItem, products, isEdit, isPaste);
                    break;
                //START RALLY DE 6644
                case ProductType.Concessions:
                    getMore = DisplayBasicProductDetailForm("Concession", productItem, products, isEdit, isPaste);
                    break;
                //END RALLY DE 6644
                case ProductType.Merchandise:
                    getMore = DisplayBasicProductDetailForm("Merchandise", productItem, products, isEdit, isPaste);
                    break;
                case ProductType.CreditRefundableFixed:
                case ProductType.CreditNonRefundableFixed:
                    getMore = DisplayBasicProductDetailForm("Credit Fixed", productItem, products, isEdit, isPaste);
                    break;
                case ProductType.CreditRefundableOpen:
                case ProductType.CreditNonRefundableOpen:
                    getMore = DisplayBasicProductDetailForm("Credit Open", productItem, products, isEdit, isPaste);
                    break;
                case ProductType.BingoOther:
                    getMore = DisplayBasicProductDetailForm("Other Products", productItem, products, isEdit, isPaste);
                    break;
                //RALLY DE 6644 support for paper (16) and pulltab (17)
                case ProductType.Paper:
                    getMore = DisplayPaperProductDetailForm("Paper", productItem, products, isEdit, isPaste);
                    break;
                case ProductType.PullTab:
                    getMore = DisplayBasicProductDetailForm("Pull Tab", productItem, products, isEdit, isPaste);
                    break;
                //END RALLY DE 6644
                case ProductType.Validation:
                    getMore = DisplayBasicProductDetailForm("Validation ", productItem, products, isEdit, isPaste, true);
                    break;
                default:
                    MessageForm.Show(Resources.ProductTypeNotSupported, Resources.ProductTypeErrorTitle, MessageFormTypes.OK);
                    break;
            }
            // FIX : DE3796
            LoadPackageTreeView(treeViewPackages.SelectedNode.Index, lastPackageName);//RALLY US1796
            // END : DE3796
            return getMore;
        }

        private bool IsProductADuplicate(PackageProduct testProduct)
        {
            foreach (ListViewItem prod in listViewProducts.Items)
            {
                PackageProduct curProd = (PackageProduct)prod.Tag;
                curProd.ProgramGameName = curProd.ProgramGameName ?? string.Empty;
                if (curProd.Equals(testProduct))
                {
                    MessageForm.Show(Resources.DuplicatedProduct, Resources.AddProductTitle, MessageFormTypes.OK);//RALLY DE 6657
                    Cursor = Cursors.Default;
                    return true;
                }
                // We can not have more than one Open Credit product
                if (curProd.ProductTypeId == 11 || curProd.ProductTypeId == 13)
                {
                    if (testProduct.ProductTypeId == 11 || testProduct.ProductTypeId == 13)
                    {
                        MessageForm.Show(Resources.HasOpenCredit, Resources.AddProductTitle, MessageFormTypes.OK);//RALLY DE 6657
                        Cursor = Cursors.Default;
                        return true;
                    }
                }
            }
            return false;
        }

        private static string GameTypeNameFromId(int id, GameTypeListItem[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].GameTypeId == id)
                    return list[i].GameTypeName;
            }
            return "";
        }

        private static string CardLevelName1Multiple(CardLevelItem[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].Multiplier == "1")
                    return list[i].CardLevelName;
            }
            return "";
        }

        private bool DisplayBingoProductDetailForm(ProductItemList productItem, object objPackageProduct, bool isEdit, bool isPaste)
        {
            DialogResult result = DialogResult.OK;
            lock (this)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    var packageProduct = (objPackageProduct == null)
                                             ? new PackageProduct()
                                             : (PackageProduct)objPackageProduct;

                    // Initialize the form.
                    GameTypeListItem[] gameTypeList = GetGameTypeMessage.GetArray();
                    CardLevelItem[] cardLevelList = GetCardLevelMessage.CardLevels(0).ToArray();
                    GameCategory[] gameCategoryList = GetGameCategoriesMessage.GetArray();

                    bingoProductDetailForm = new BingoProductDetailForm
                                             {
                                                 // FIX DE4125
                                                 Settings = ProdCenterSettings,
                                                 //UsePrePrintedPacks = ProdCenterSettings.UsePrePrintedPacks,
                                                 // END DE4125
                                                 ProductItem = productItem,
                                                 PackageProduct = packageProduct,
                                                 CardLevelList = cardLevelList,
                                                 CardTypeList = GetCardTypeMessage.GetArray(),
                                                 
                                                 GameCategoryList = gameCategoryList,
                                                 GameTypeList = gameTypeList,
                                                 CardTypeName = CardType.Standard.ToString(),
                                                 GameTypeName =
                                                     GameTypeNameFromId((int)GameType.SeventyFiveNumberBingo, gameTypeList),
                                                 CardLevelName = CardLevelName1Multiple(cardLevelList),
                                                 
                                                 GameCategoryName = "Regular",
                                                 PointsPerDollar = "0",
                                                 PointsPerQuantity = "0",
                                                 PointsToRedeem = "0",
                                                 Price = "0.00",
                                                 AltPrice = "0.00",// US4543
                                                 CountsTowardsQualifyingSpend = true, // US4587
                                                 Quantity = "1",
                                                 AllowAddAnother = !(isEdit || isPaste)
                                             };
                    if (isEdit || isPaste)
                    {
                        // Set the form's value for editing.
                        bingoProductDetailForm.CardLevelName = packageProduct.CardLevelName;
                        bingoProductDetailForm.CardTypeName = packageProduct.CardTypeName;
                        
                        bingoProductDetailForm.GameCategoryName = packageProduct.GameCategoryName;
                        bingoProductDetailForm.GameTypeName = packageProduct.GameTypeName;
                        bingoProductDetailForm.Quantity = packageProduct.Quantity.ToString();
                        bingoProductDetailForm.IsTaxed = packageProduct.IsTaxed;
                        bingoProductDetailForm.CardCount = packageProduct.CardCount.ToString();
                        bingoProductDetailForm.Price = packageProduct.Price;
                        bingoProductDetailForm.AltPrice = packageProduct.AltPrice;// US4543
                        bingoProductDetailForm.CountsTowardsQualifyingSpend = packageProduct.CountsTowardsQualifyingSpend; // US4587
                        bingoProductDetailForm.PointsPerQuantity = packageProduct.PointsPerQuantity;
                        bingoProductDetailForm.PointsPerDollar = packageProduct.PointsPerDollar;
                        bingoProductDetailForm.PointsToRedeem = packageProduct.PointsToRedeem;
                    }
                    // FIX TA5873
                    else
                    {
                        if (productItem.PaperLayoutCount != 0 && !string.IsNullOrEmpty(productItem.PaperLayoutName))
                        {
                            bingoProductDetailForm.CardCount = productItem.PaperLayoutCount.ToString();
                        }
                        else
                        {
                            bingoProductDetailForm.CardCount = "1";
                        }
                    }

                    bingoProductDetailForm.ValidateCardCount = ProdCenterSettings.CardCountValidation;

                    // END TA5873
                    Cursor = Cursors.Default;

                    // Display the form
                    result = bingoProductDetailForm.ShowDialog(this);
                    if (result != DialogResult.Cancel)
                    {
                        Cursor = Cursors.WaitCursor;
                        // Get the product Info from the form.
                        var packageProductListItem = new PackageProduct
                                                     {
                                                         ProductId = productItem.ProductItemId,
                                                         ProductName = productItem.ProductItemName,
                                                         ProductTypeId = productItem.ProductTypeId,
                                                         ProductTypeName = productItem.ProductTypeName,
                                                         SalesSourceId = productItem.SalesSourceId,
                                                         SalesSourceName = productItem.ProductSalesSourceName,
                                                         GameTypeId = bingoProductDetailForm.GameTypeId,
                                                         GameTypeName = bingoProductDetailForm.GameTypeName,
                                                         CardLevelId = bingoProductDetailForm.CardLevelId,
                                                         CardLevelName = bingoProductDetailForm.CardLevelName,
                                                         CardMediaId = bingoProductDetailForm.CardMediaId,
                                                         CardMediaName = bingoProductDetailForm.CardMediaName,
                                                         CardTypeId = bingoProductDetailForm.CardTypeId,
                                                         CardTypeName = bingoProductDetailForm.CardTypeName,
                                                         GameCategoryId = bingoProductDetailForm.GameCategoryId,
                                                         GameCategoryName = bingoProductDetailForm.GameCategoryName,
                                                         Quantity = byte.Parse(bingoProductDetailForm.Quantity),
                                                         IsTaxed = bingoProductDetailForm.IsTaxed,
                                                         CardCount = ushort.Parse(bingoProductDetailForm.CardCount),
                                                         Price = bingoProductDetailForm.Price,
                                                         AltPrice = bingoProductDetailForm.AltPrice,// US4543
                                                         PointsPerQuantity = bingoProductDetailForm.PointsPerQuantity,
                                                         PointsPerDollar = bingoProductDetailForm.PointsPerDollar,
                                                         PointsToRedeem = bingoProductDetailForm.PointsToRedeem,
                                                         ProgramGameName = string.Empty,
                                                         // FIX : TA5759
                                                         NumbersRequired = (ushort)((bingoProductDetailForm.GameTypeName.Contains("Pick Yur Platter")) ? 12 : 0),
                                                         // END : TA5759
                                                         ProgramCBBGameId = 0,
                                                         CountsTowardsQualifyingSpend = bingoProductDetailForm.CountsTowardsQualifyingSpend //DE12974

                                                     };

                        if (IsProductADuplicate(packageProductListItem))
                            return result == DialogResult.OK ? false : true;

                        if (isEdit)
                        {
                            // Remove the old selected product.
                            listViewProducts.SelectedItems[0].Remove();
                        }

                        // Fix : DE3495 Invalid index after sorting by header
                        AddProductToListView(packageProductListItem, true);

                        SavePackageProducts();
                        Cursor = Cursors.Default;
                    }
                }
                catch (Exception ex)
                {
                    MessageForm.Show("DisplayBingoProductDetailForm(): " + ex.Message);
                }
            }
            return result == DialogResult.Retry ? true : false;
        }

        private bool DisplayCrystalBallProductDetailForm(ProductItemList productItem, object objPackageProduct, bool isEdit, bool isPaste)
        {
            DialogResult result = DialogResult.OK;
            lock (this)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    var packageProduct = (objPackageProduct == null)
                                             ? new PackageProduct()
                                             : (PackageProduct)objPackageProduct;

                    // FIX TA5759
                    GameTypeListItem[] gameTypeList = GetGameTypeMessage.GetArray();
                    
                    // Force the GameType to CrystalBall and the CardType to Standard
                    //packageProduct.GameTypeId = (int)GameType.CrystalBall;
                    packageProduct.CardTypeId = (int)CardType.Standard;
                    packageProduct.CardTypeName = CardType.Standard.ToString();
                    //packageProduct.GameTypeName = "Crystal Ball";

                    // Initialize the form.
                    crystalBallProductDetailForm = new CrystalBallProductDetailForm
                                                   {
                                                       ProductItem = productItem,
                                                       PackageProduct = packageProduct,
                                                       CardMediaList = GetCardMediaMessage.GetArray(0),
                                                       GameCategoryList = GetGameCategoriesMessage.GetArray(),
                                                       GameTypeList = gameTypeList,
                                                       GameTypeName = GameTypeNameFromId((int)GameType.CrystalBall, gameTypeList),
                                                       //GameTypeId = packageProduct.GameTypeId,
                                                       CardTypeName = packageProduct.CardTypeName,
                                                       CardTypeId = packageProduct.CardTypeId,
                                                       GameCategoryName = "Regular",
                                                       CardMediaName = CardMedia.Electronic.ToString(),
                                                       CardCount = "1",
                                                       PointsPerDollar = "0",
                                                       PointsPerQuantity = "0",
                                                       PointsToRedeem = "0",
                                                       Price = "0.00",
                                                       AltPrice = "0.00",
                                                       Quantity = "1",
                                                       AllowAddAnother = !(isEdit || isPaste),
                                                       WholePoints = ProdCenterSettings.WholeProductPoints
                                                   };

                    if (isEdit || isPaste)
                    {
                        // Set the form's value for editing.
                        crystalBallProductDetailForm.CardMediaName = packageProduct.CardMediaName;
                        crystalBallProductDetailForm.Quantity = packageProduct.Quantity.ToString();
                        crystalBallProductDetailForm.IsTaxed = packageProduct.IsTaxed;
                        crystalBallProductDetailForm.CardCount = packageProduct.CardCount.ToString();
                        crystalBallProductDetailForm.CardLevelName = packageProduct.CardLevelName;
                        crystalBallProductDetailForm.CardLevelId = packageProduct.CardLevelId;
                        crystalBallProductDetailForm.GameCategoryName = packageProduct.GameCategoryName;
                        crystalBallProductDetailForm.Price = packageProduct.Price;
                        crystalBallProductDetailForm.AltPrice = packageProduct.AltPrice; // US4543
                        crystalBallProductDetailForm.CountsTowardsQualifyingSpend = packageProduct.CountsTowardsQualifyingSpend;// US4587
                        crystalBallProductDetailForm.PointsPerQuantity = packageProduct.PointsPerQuantity;
                        crystalBallProductDetailForm.PointsPerDollar = packageProduct.PointsPerDollar;
                        crystalBallProductDetailForm.PointsToRedeem = packageProduct.PointsToRedeem;
                        crystalBallProductDetailForm.NumbersRequired = packageProduct.NumbersRequired;
                        crystalBallProductDetailForm.GameTypeName = packageProduct.GameTypeName;
                    }
                    Cursor = Cursors.Default;
                    // END TA5759

                    // Display the form
                    result = crystalBallProductDetailForm.ShowDialog(this);
                    if (result != DialogResult.Cancel)
                    {
                        Cursor = Cursors.WaitCursor;
                        // Get the product Info from the form.
                        var packageProductListItem = new PackageProduct
                                                     {
                                                         ProductId = productItem.ProductItemId,
                                                         ProductName = productItem.ProductItemName,
                                                         ProductTypeId = productItem.ProductTypeId,
                                                         ProductTypeName = productItem.ProductTypeName,
                                                         SalesSourceId = productItem.SalesSourceId,
                                                         SalesSourceName = productItem.ProductSalesSourceName,
                                                         GameTypeId = crystalBallProductDetailForm.GameTypeId,
                                                         GameTypeName = crystalBallProductDetailForm.GameTypeName,
                                                         CardLevelId = crystalBallProductDetailForm.CardLevelId,
                                                         CardLevelName = crystalBallProductDetailForm.CardLevelName,
                                                         CardMediaId = crystalBallProductDetailForm.CardMediaId,
                                                         CardMediaName = crystalBallProductDetailForm.CardMediaName,
                                                         CardTypeId = crystalBallProductDetailForm.CardTypeId,
                                                         CardTypeName = crystalBallProductDetailForm.CardTypeName,
                                                         GameCategoryId = crystalBallProductDetailForm.GameCategoryId,
                                                         GameCategoryName = crystalBallProductDetailForm.GameCategoryName,
                                                         Quantity = byte.Parse(crystalBallProductDetailForm.Quantity),
                                                         IsTaxed = crystalBallProductDetailForm.IsTaxed,
                                                         CardCount = ushort.Parse(crystalBallProductDetailForm.CardCount),
                                                         Price = crystalBallProductDetailForm.Price,
                                                         AltPrice = crystalBallProductDetailForm.AltPrice, // US4543
                                                         CountsTowardsQualifyingSpend = crystalBallProductDetailForm.CountsTowardsQualifyingSpend, // US4587
                                                         PointsPerQuantity = crystalBallProductDetailForm.PointsPerQuantity,
                                                         PointsPerDollar = crystalBallProductDetailForm.PointsPerDollar,
                                                         PointsToRedeem = crystalBallProductDetailForm.PointsToRedeem,
                                                         NumbersRequired = crystalBallProductDetailForm.NumbersRequired,
                                                         ProgramGameName = string.Empty,
                                                         ProgramCBBGameId = 0
                        };

                        if (IsProductADuplicate(packageProductListItem)) 
                            return result == DialogResult.OK ? false : true;

                        if (isEdit)
                        {
                            // Remove the old selected product.
                            listViewProducts.SelectedItems[0].Remove();
                        }

                        // Fix : DE3495 Invalid index after sorting by header
                        AddProductToListView(packageProductListItem, true);

                        SavePackageProducts();
                        Cursor = Cursors.Default;
                    }
                }
                catch (Exception ex)
                {
                    MessageForm.Show("DisplayCrystalBallProductDetailForm(): " + ex.Message);
                }
            }
            return result == DialogResult.Retry ? true : false;
        }

        private bool DisplayBasicProductDetailForm(string formName, ProductItemList productItem, object objPackageProduct, bool isEdit, bool isPaste, bool hideAddAnotherProductButton = false)
        {
            DialogResult result = DialogResult.OK;
            lock (this)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    var packageProduct = (objPackageProduct == null)
                                             ? new PackageProduct()
                                             : (PackageProduct)objPackageProduct;

                    // Initialize the form.
                    basicProductDetailForm = new BasicProductDetailForm 
                                                {
                                                    Title = formName,
                                                    ProductItem = productItem,
                                                    PackageProduct = packageProduct,
                                                    PointsPerDollar = "0",
                                                    PointsPerQuantity = "0",
                                                    PointsToRedeem = "0",
                                                    Price = "0.00",
                                                    AltPrice = "0.00",// US4543
                                                    CountsTowardsQualifyingSpend = true, // US4587
                                                    Quantity = "1",
                                                    AllowAddAnother = !(isEdit || isPaste),
                                                    WholePoints = ProdCenterSettings.WholeProductPoints
                                                };
                    if (isEdit || isPaste)
                    {
                        // Set the form's value for editing.
                        basicProductDetailForm.Quantity = packageProduct.Quantity.ToString();
                        basicProductDetailForm.IsTaxed = packageProduct.IsTaxed;
                        basicProductDetailForm.Price = packageProduct.Price;
                        basicProductDetailForm.AltPrice = packageProduct.AltPrice;// US4543
                        basicProductDetailForm.CountsTowardsQualifyingSpend = packageProduct.CountsTowardsQualifyingSpend;// US4587
                        basicProductDetailForm.PointsPerQuantity = packageProduct.PointsPerQuantity;
                        basicProductDetailForm.PointsPerDollar = packageProduct.PointsPerDollar;
                        basicProductDetailForm.PointsToRedeem = packageProduct.PointsToRedeem;
                    }

                    //US4459: (US4428) Product Center: Create validation package
                    if (hideAddAnotherProductButton)
                    {
                        basicProductDetailForm.HideAllowAnotherProductButton = true;
                    }

                    Cursor = Cursors.Default;
                    
                    basicProductDetailForm.SetProductType(productItem.ProductTypeId); //RALLY DE 6644

                    // Display the form
                    result = basicProductDetailForm.ShowDialog(this);
                    if (result != DialogResult.Cancel)
                    {
                        Cursor = Cursors.WaitCursor;
                        // Get the product Info from the form.
                        var packageProductListItem = new PackageProduct
                        {
                            ProductId = productItem.ProductItemId,
                            ProductName = productItem.ProductItemName,
                            ProductTypeId = productItem.ProductTypeId,
                            ProductTypeName = productItem.ProductTypeName,
                            SalesSourceId = productItem.SalesSourceId,
                            SalesSourceName = productItem.ProductSalesSourceName,
                            CardCutId = 0,
                            CardCutName = string.Empty,
                            GameTypeId = 0,
                            GameTypeName = string.Empty,
                            CardLevelId = 0,
                            CardLevelName = string.Empty,
                            CardMediaId = 0,
                            CardMediaName = string.Empty,
                            CardTypeId = 0,
                            CardTypeName = string.Empty,
                            GameCategoryId = 0,
                            GameCategoryName = string.Empty,
                            Quantity = byte.Parse(basicProductDetailForm.Quantity),
                            IsTaxed = basicProductDetailForm.IsTaxed,
                            CardCount = 0,
                            Price = basicProductDetailForm.Price,
                            AltPrice = basicProductDetailForm.AltPrice,// US4543
                            CountsTowardsQualifyingSpend = basicProductDetailForm.CountsTowardsQualifyingSpend, // US4587
                            PointsPerQuantity = basicProductDetailForm.PointsPerQuantity,
                            PointsPerDollar = basicProductDetailForm.PointsPerDollar,
                            PointsToRedeem = basicProductDetailForm.PointsToRedeem,
                            ProgramGameName = string.Empty,
                            NumbersRequired = 0,
                            ProgramCBBGameId = 0
                        };

                        if (IsProductADuplicate(packageProductListItem))
                            return result == DialogResult.OK ? false : true;

                        if (isEdit)
                        {
                            // Remove the old selected product.
                            listViewProducts.SelectedItems[0].Remove();
                        }

                        // Fix : DE3495 Invalid index after sorting by header
                        AddProductToListView(packageProductListItem, true);
                        SavePackageProducts();
                        Cursor = Cursors.Default;
                    }
                }
                catch (Exception ex)
                {
                    MessageForm.Show("DisplayBasicProductDetailForm(): " + ex.Message);
                }
            }
            return result == DialogResult.Retry ? true : false;
        }

        private bool DisplayPaperProductDetailForm(string formName, ProductItemList productItem, object objPackageProduct, bool isEdit, bool isPaste)
        {
            DialogResult result = DialogResult.OK;
            lock (this)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    var packageProduct = (objPackageProduct == null)
                                             ? new PackageProduct()
                                             : (PackageProduct)objPackageProduct;

                    GameCategory[] gameCategoryList = GetGameCategoriesMessage.GetArray();
                    CardLevelItem[] cardLevelList = GetCardLevelMessage.CardLevels(0).ToArray();

                    // Initialize the form.
                    paperProductDetailForm = new PaperProductDetailForm
                    {
                        Title = formName,
                        ProductItem = productItem,
                        PackageProduct = packageProduct,
                        CardLevelList = cardLevelList, // US4516
                        CardLevelName = CardLevelName1Multiple(cardLevelList),
                        PointsPerDollar = "0",
                        PointsPerQuantity = "0",
                        PointsToRedeem = "0",
                        Price = "0.00",
                        AltPrice = "0.00",
                        CountsTowardsQualifyingSpend = true,
                        Quantity = "1",
                        GameCategoryList = gameCategoryList,
                        AllowAddAnother = !(isEdit || isPaste),
                        WholePoints = ProdCenterSettings.WholeProductPoints
                    };
                    if (isEdit || isPaste)
                    {
                        // Set the form's value for editing.
                        paperProductDetailForm.CardLevelName = packageProduct.CardLevelName; // US4516

                        paperProductDetailForm.Quantity = packageProduct.Quantity.ToString();
                        paperProductDetailForm.IsTaxed = packageProduct.IsTaxed;
                        paperProductDetailForm.Price = packageProduct.Price;
                        paperProductDetailForm.AltPrice = packageProduct.AltPrice;
                        paperProductDetailForm.CountsTowardsQualifyingSpend = packageProduct.CountsTowardsQualifyingSpend; // US4587
                        paperProductDetailForm.PointsPerQuantity = packageProduct.PointsPerQuantity;
                        paperProductDetailForm.PointsPerDollar = packageProduct.PointsPerDollar;
                        paperProductDetailForm.PointsToRedeem = packageProduct.PointsToRedeem;
                        paperProductDetailForm.GameCategoryName = packageProduct.GameCategoryName;

                    }
                    Cursor = Cursors.Default;

//                    paperProductDetailForm.SetProductType(productItem.ProductTypeId); //RALLY DE 6644
                    // Display the form
                    result = paperProductDetailForm.ShowDialog(this);
                    if (result != DialogResult.Cancel)
                    {
                        Cursor = Cursors.WaitCursor;
                        // Get the product Info from the form.
                        var packageProductListItem = new PackageProduct
                        {
                            ProductId = productItem.ProductItemId,
                            ProductName = productItem.ProductItemName,
                            ProductTypeId = productItem.ProductTypeId,
                            ProductTypeName = productItem.ProductTypeName,
                            SalesSourceId = productItem.SalesSourceId,
                            SalesSourceName = productItem.ProductSalesSourceName,
                            CardCutId = 0,
                            CardCutName = string.Empty,
                            GameTypeId = 0,
                            GameTypeName = string.Empty,
                            CardLevelId = paperProductDetailForm.CardLevelId, // US4516
                            CardLevelName = paperProductDetailForm.CardLevelName,
                            CardMediaId = 0,
                            CardMediaName = string.Empty,
                            CardTypeId = 0,
                            CardTypeName = string.Empty,
                            GameCategoryId = paperProductDetailForm.GameCategoryId,
                            GameCategoryName = paperProductDetailForm.GameCategoryName,
                            Quantity = byte.Parse(paperProductDetailForm.Quantity),
                            IsTaxed = paperProductDetailForm.IsTaxed,
                            CardCount = 0,
                            Price = paperProductDetailForm.Price,
                            AltPrice = paperProductDetailForm.AltPrice,
                            CountsTowardsQualifyingSpend = paperProductDetailForm.CountsTowardsQualifyingSpend, // US4587
                            PointsPerQuantity = paperProductDetailForm.PointsPerQuantity,
                            PointsPerDollar = paperProductDetailForm.PointsPerDollar,
                            PointsToRedeem = paperProductDetailForm.PointsToRedeem,
                            ProgramGameName = string.Empty,
                            NumbersRequired = 0,
                            ProgramCBBGameId = 0
                        };

                        if (IsProductADuplicate(packageProductListItem))
                            return result == DialogResult.OK ? false : true;

                        if (isEdit)
                        {
                            // Remove the old selected product.
                            listViewProducts.SelectedItems[0].Remove();
                        }

                        // Fix : DE3495 Invalid index after sorting by header
                        AddProductToListView(packageProductListItem, true);
                        SavePackageProducts();
                        Cursor = Cursors.Default;
                    }
                }
                catch (Exception ex)
                {
                    MessageForm.Show("DisplayPaperProductDetailForm(): " + ex.Message);
                }
            }
            return result == DialogResult.Retry ? true : false;
        }
        #endregion Display Product Forms

        
    }
}