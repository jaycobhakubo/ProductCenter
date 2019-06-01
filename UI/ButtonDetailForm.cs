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
using System.Web.UI.WebControls;
using System.Windows.Forms;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Data;
using GTI.Modules.Shared.Business;
using RadioButton=System.Windows.Forms.RadioButton;
using System.Collections.Generic;
using GTI.Modules.Shared.Data;
using GTI.Modules.ProductCenter.Properties;
using GTI.Modules.ProductCenter.Business;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class ButtonDetailForm : GradientForm
    {
        #region Private Members
        protected DisplayMode DisplayMode = new NormalDisplayMode();
        protected readonly int packageWidth;
        protected readonly int nonPackageWidth;
        protected readonly int operatorId;
        protected static ButtonGraphicSelection buttonGraphicSelection;
        protected bool _isDailyButton;
        protected DailyMenuButton m_dailyMenuButton = null;
        protected Array _productList; // the list of products available for the packages
        private static List<ProductTypeListItem> _productTypes;
        private static string DAILY_EDIT_WARNING = "Are you sure you want to edit this daily button?"
                    + Environment.NewLine + "The change will take place immediately and can have hazardous consequences";
        #endregion

        public ButtonDetailForm(int opId, ProductCenterSettings settings, bool isDailyButton = false)
        {
            Canceled = true;
            operatorId = opId;
            InitializeComponent();
            Application.Idle += OnIdle;
            AcceptButton = btnAccept;
            CancelButton = btnCancel;
            packageWidth = Width;
            nonPackageWidth = listViewProducts.Left;

            ProdCenterSettings = settings;
            _isDailyButton = isDailyButton; // US1772
            if(_productTypes == null)
                _productTypes = new List<ProductTypeListItem>(GetProductTypesMessage.GetArray());
        }

        private void ButtonDetailForm_Load(object sender, EventArgs e)
        {
            if (IsPackage)
            {
                if (PackageComboBox.Items.Count > 0 && PackageComboBox.SelectedIndex == -1)
                    PackageComboBox.SelectedIndex = 0;
            }
            else if (IsDiscount)
            {
                if (DiscountComboBox.Items.Count > 0 && DiscountComboBox.SelectedIndex == -1)
                    DiscountComboBox.SelectedIndex = 0;
            }
            else if (IsFunction)
            {
                if (FunctionComboBox.Items.Count > 0 && FunctionComboBox.SelectedIndex == -1)
                    FunctionComboBox.SelectedIndex = 0;
            }
            else
            {
                IsPackage = true;
            }

            //if (WholePoints)
            //{
            //    DiscountRB.Visible = false;
            //}

            if (_isDailyButton) // US1772
            {
                this.Text = "Today's Button Details";
                btnAddProduct.Visible = btnDeleteProduct.Visible = false; // disabled for first release
                btnEditProduct.Visible = true;
                btnDelete.Enabled = PackageComboBox.Enabled = DiscountComboBox.Enabled = FunctionComboBox.Enabled = false;
                PackageRB.Enabled = DiscountRB.Enabled = FunctionRB.Enabled = false;
            }
            else
            {
                btnAddProduct.Visible = btnDeleteProduct.Visible = btnEditProduct.Visible = false;
            }

            DisplayMode btnDisplayMode = new NormalDisplayMode();

            imgButtonGraphic.Stretch = isStretch;
            imgButtonGraphic.ImageNormal = curImage;
            imgButtonGraphic.Font = btnDisplayMode.MenuButtonFont;
            imgButtonGraphic.Size = btnDisplayMode.MenuButtonSize;
        }

        private void OnIdle(object sender, EventArgs e)
        {
            btnAccept.Enabled = (PackageComboBox.SelectedIndex != -1 ||
                DiscountComboBox.SelectedIndex != -1 ||
                FunctionComboBox.SelectedIndex != -1) &&
                KeyTextBox.Text != string.Empty;
        }

        #region Public Properties
        public ProductCenterSettings ProdCenterSettings { get; set; }
        public bool IsCreateMode { get; set; }
        public bool Canceled { get; set; }
        public bool Cleared { get; protected set; }
        public int PageNumber { get; set; }
        public int KeyNumber { get; set; }
        public bool WholePoints { get; set; } //US3692
        public System.Drawing.Image curImage { get; set; } //US4935
        public bool isStretch { get; set; }//US4935
        public DailyMenuButton OurDailyMenuButton
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or Sets the Key Text
        /// </summary>
        public string KeyText
        {
            get { return KeyTextBox.Text; }
            set { KeyTextBox.Text = value.Trim(); } //RALLY TA 5744
        }

        public bool IsPackage
        {
            get { return PackageRB.Checked; }
            set { PackageRB.Checked = value; }
        }

        /// <summary>
        /// Gets or Sets the Is Discount Flag
        /// </summary>
        public bool IsDiscount
        {
            get { return DiscountRB.Checked; }
            set { DiscountRB.Checked = value; }
        }

        /// <summary>
        /// Gets or Sets the Is Function Flag
        /// </summary>
        public bool IsFunction
        {
            get { return FunctionRB.Checked; }
            set { FunctionRB.Checked = value; }
        }

        #region Populate ComboBoxes

        /// <summary>
        /// Populates the Package List
        /// </summary>
        public Array PopulatePackageList
        {
            set
            {
                // Clear the list
                PackageComboBox.Items.Clear();
                
                // Populate the List
                foreach (PackageItem packageItemList in value)
                {
                    string packName = null;
                 
                    if(_isDailyButton)
                        packName = String.Format("{0,-35}", packageItemList.PackageName);
                    else
                        packName = String.Format("{0,-35}{1,6:C}", packageItemList.PackageName, Convert.ToDecimal(packageItemList.PackagePrice));

                    //packageItemList.IsValidationPackage
                    PackageComboBox.Items.Add(new ListItem(packName, packageItemList.PackageId.ToString()));
                }
            }
        }

        /// <summary>
        /// Populates the Discount List.
        /// </summary>
        public Array PopulateDiscountList
        {
            set
            {
                // Clear the List
                DiscountComboBox.Items.Clear();
                _discountList = value.OfType<DiscountItem>().ToList();

                // Populate the List
                foreach (DiscountItem item in value)
                {
                    if (item.DiscountAwardType == DiscountItem.AwardTypes.Manual && item.IsActive)//Show only active manual discount. US4628
                    {
                        DiscountComboBox.Items.Add(new ListItem(item.DiscountName, item.DiscountId.ToString())); // US4642
                    }
                }
            }
        }
        private List<DiscountItem> _discountList; // RALLY DE12882

        /// <summary>
        /// Populates the Function List
        /// </summary>
        public Array PopulateFunctionList
        {
            set
            {
                // Clear the list
                FunctionComboBox.Items.Clear();

                // Populate the List
                foreach (FunctionList functionList in value)
                {
                    FunctionComboBox.Items.Add(new ListItem(functionList.FunctionName, functionList.FunctionId.ToString()));
                }
            }
        }
                
        #endregion

        #region Get/Set Mode ComboBoxes
        /// <summary>
        /// Gets the Discount Id
        /// </summary>
        public int DiscountId
        {
            get
            {
                ListItem li = (ListItem)DiscountComboBox.SelectedItem;
                int num = 0;
                if (li != null)
                    int.TryParse(li.Value, out num);
                return num;
            }
            set
            {
                for (var i = 0; i < DiscountComboBox.Items.Count; i++)
                {
                    ListItem li = (ListItem)DiscountComboBox.Items[i];
                    if (li.Value == value.ToString())
                        DiscountComboBox.SelectedIndex = i;
                }
            }
        }

        /// <summary>
        /// Gets the Selected Package Id
        /// </summary>
        public int PackageId
        {
            get
            {
                ListItem li = (ListItem)PackageComboBox.SelectedItem;
                int num = 0;
                if (li != null)
                    int.TryParse(li.Value, out num);
                return num;
            }
            set
            {
                for (var i = 0; i < PackageComboBox.Items.Count; i++)
                {
                    ListItem li = (ListItem)PackageComboBox.Items[i];
                    if (li.Value == value.ToString())
                        PackageComboBox.SelectedIndex = i;
                }
            }
        }

        /// <summary>
        /// Gets the Selected Function Id
        /// </summary>
        public int FunctionId
        {
            get
            {
                var li = (ListItem)FunctionComboBox.SelectedItem;
                int num = 0;
                if (li != null)
                    int.TryParse(li.Value, out num);
                return num;
            }
            set
            {
                for (var i = 0; i < FunctionComboBox.Items.Count; i++)
                {
                    var li = (ListItem)FunctionComboBox.Items[i];
                    if (li.Value == value.ToString())
                        FunctionComboBox.SelectedIndex = i;
                }
            }
        }

        /// <summary>
        /// Gets the Selected Button Graphic Id
        /// </summary>
        public int ButtonGraphicId //US4935 changed
        {
            get;
            set;
        }
        #endregion

        /// <summary>
        /// Gets or Sets the Key Locked
        /// </summary>
        public bool KeyLocked
        {
            get { return checkBoxKeyLocked.Checked; }
            set { checkBoxKeyLocked.Checked = value; }
        }

        /// <summary>
        /// Gets or Sets the Player Required Flag
        /// </summary>
        public bool PlayerRequired
        {
            get { return checkBoxPlayerRequired.Checked; }
            set { checkBoxPlayerRequired.Checked = value; }
        }

        /// <summary>
        /// Gets or sets if the button's package is to be used as the default validation package for the menu.
        /// </summary>
        public bool DefaultValidation
        {
            get
            {
                return checkBoxDefaultValidation.Checked;
            }

            set
            {
                checkBoxDefaultValidation.Checked = value;
            }
        }

        public bool RequiresAuthorization
        {
            get
            {
                return checkBoxRequiresAuthorization.Checked;
            }

            set
            {
                checkBoxRequiresAuthorization.Checked = value;
            }
        }

        public List<PackageProduct> ProductList
        {
            set
            {
                if (value != null)
                {
                    _productList = value.ToArray();
                    int sortColumn = listViewProducts.SortColumn;
                    if (sortColumn > 0) listViewProducts.PreLoadAdjustment();

                    ListItem li = (ListItem)PackageComboBox.Items[PackageComboBox.SelectedIndex];
                    if (li != null)
                    {
                        KeyText = li.Text.Length > 25 ? li.Text.Substring(0, 25) : li.Text;//RALLY DE 6659
                        int packageId = int.Parse(li.Value);
                        listViewProducts.Items.Clear();
                        foreach (PackageProduct packageProductListItem in _productList)
                        {
                            AddProductToListView(packageProductListItem);
                        }
                    }

                    if (sortColumn > 0) listViewProducts.ForceClickOnColumn(sortColumn);
                }
                else
                {
                    _productList = null;
                    listViewProducts.Items.Clear();
                }
            }
        }

        public List<DailyProductPackageItem> DailyProductList
        {
            set
            {
                if (value != null && value.Count > 0)
                {
                    _productList = value.ToArray();
                    int sortColumn = listViewProducts.SortColumn;
                    if (sortColumn > 0) listViewProducts.PreLoadAdjustment();

                    ListItem li = (ListItem)PackageComboBox.Items[PackageComboBox.SelectedIndex];
                    if (li != null)
                    {
                        KeyText = li.Text.Length > 25 ? li.Text.Substring(0, 25) : li.Text;//RALLY DE 6659
                        int packageId = int.Parse(li.Value);
                        listViewProducts.Items.Clear();
                        foreach (DailyProductPackageItem packageProductListItem in _productList)
                        {
                            AddProductToListView(packageProductListItem);
                        }
                    }

                    if (sortColumn > 0) listViewProducts.ForceClickOnColumn(sortColumn);
                }
                else
                {
                    _productList = null;
                    listViewProducts.Items.Clear();
                }
            }
            get
            {
                List<DailyProductPackageItem> temp = new List<DailyProductPackageItem>();

                foreach (ListViewItem prod in listViewProducts.Items)
                {
                    if (prod.Tag is DailyProductPackageItem)
                        temp.Add((DailyProductPackageItem)prod.Tag);
                    else
                        break;
                }

                return temp;
            }
        }
        #endregion

        #region Event Handlers
        // One of the Mode Radio buttons were clicked (Package, Discount, Function)
        private void ModeChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;
            
            if (rb != null && rb.Checked)
            {
                PackageComboBox.Visible = false;
                DiscountComboBox.Visible = false;
                FunctionComboBox.Visible = false;
                
                ListItem li;

                switch (rb.Name)
                {
                    case "PackageRB":
                        PackageComboBox.Visible = true;
                        
                        if (PackageComboBox.Items.Count > 0)
                        {
                            if (PackageComboBox.SelectedIndex == -1)
                                PackageComboBox.SelectedIndex = 0;
                            
                            li = (ListItem)PackageComboBox.Items[PackageComboBox.SelectedIndex];
                            KeyText = li.Text.Length > 35 ? li.Text.Substring(0, 35) : li.Text;//RALLY DE 6659
                        }
                        
                        Width = packageWidth;
                        checkBoxPlayerRequired.Enabled = true; // RALLY DE12882
                        PlayerRequired = false;

                        checkBoxDefaultValidation.Checked = false;                   
                        checkBoxDefaultValidation.Visible = true;
                        break;
                    case "DiscountRB":
                        DiscountComboBox.Visible = true;
                        if (DiscountComboBox.Items.Count > 0)
                        {
                            if (DiscountComboBox.SelectedIndex == -1) DiscountComboBox.SelectedIndex = 0;

                            DiscountComboBox_SelectedIndexChanged(this, null); // RALLY DE12882 Note: don't need to do the same "KeyText" as the other cases since it's the same logic as in this method
                        }

                        Width = nonPackageWidth;
                        checkBoxDefaultValidation.Checked = false;                   
                        checkBoxDefaultValidation.Visible = false;

                        break;
                    case "FunctionRB":
                        FunctionComboBox.Visible = true;
                        if (FunctionComboBox.Items.Count > 0)
                        {
                            if (FunctionComboBox.SelectedIndex == -1) FunctionComboBox.SelectedIndex = 0;
                            li = (ListItem)FunctionComboBox.Items[FunctionComboBox.SelectedIndex];
                            KeyText = li.Text; //RALLY DE 6659
                        }
 
                        Width = nonPackageWidth;
                        checkBoxPlayerRequired.Enabled = true; // RALLY DE12882
                        PlayerRequired = false;
                        checkBoxDefaultValidation.Checked = false;                   
                        checkBoxDefaultValidation.Visible = false;
                        break;
                }
            }
        }

        private void AcceptClick(object sender, EventArgs e)
        {
            if (_isDailyButton)
            {
                DialogResult res = MessageForm.Show(DAILY_EDIT_WARNING, 
                    "Edit Daily Button Warning", MessageFormTypes.YesNo);
                if (res == System.Windows.Forms.DialogResult.No)
                    return;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Application.Idle -= OnIdle;
            Canceled = false;
            Cleared = false;
            Close();
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            if (_isDailyButton)
            {
                DialogResult res = MessageForm.Show(DAILY_EDIT_WARNING,
                    "Edit Daily Button Warning", MessageFormTypes.YesNo);
                if (res == System.Windows.Forms.DialogResult.No)
                    return;
            }
            Application.Idle -= OnIdle;
            Canceled = false;
            Cleared = true;
            Close();
        }

        private void CancelClick(object sender, EventArgs e)
        {
            Application.Idle -= OnIdle;
            Canceled = true;
            Cleared = false;
            Close();
        }

        private void SelectClick(object sender, EventArgs e) //US4935
        {
            buttonGraphicSelection = new ButtonGraphicSelection();
            buttonGraphicSelection.ShowDialog(this);

            if (buttonGraphicSelection.Selected == true)
            {
                ButtonGraphicId = buttonGraphicSelection.ButtonGraphicId;
                imgButtonGraphic.ImageNormal = buttonGraphicSelection.imgButton.ImageNormal;
                imgButtonGraphic.Stretch = buttonGraphicSelection.imgButton.Stretch;
            }
        }

        private void FunctionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem li = (ListItem)FunctionComboBox.Items[FunctionComboBox.SelectedIndex];
            KeyText = li.Text; //RALLY DE 6659
        }

        private void DiscountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem li = (ListItem)DiscountComboBox.Items[DiscountComboBox.SelectedIndex];
            //START RALLY DE 6659
            KeyText = li.Text.Length > 14 ?  li.Text.Substring(0,14) : li.Text; 
            if (KeyText.Length > 3 && KeyText.Substring(KeyText.Length - 3, 3) == "---")
            {
                KeyText = KeyText.Substring(0, KeyText.Length - 4);
            }
            //END RALLY DE 6659

            // START RALLY DE12882
            int discountID = Int32.Parse(li.Value); // do this here so it's only done once. Don't need to "TryParse" since we know it's an int by it's definition
            DiscountItem match = _discountList.FirstOrDefault(x => x.DiscountId == discountID);
            if (match != null)
            {
                PlayerRequired = match.IsPlayerRequired;
                checkBoxPlayerRequired.Enabled = false;
            }
            // END RALLY DE12882
        }

        private void PackageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PackageComboBox.SelectedIndex >= 0)
            {
                int sortColumn = listViewProducts.SortColumn;
                if (sortColumn > 0) listViewProducts.PreLoadAdjustment();

                ListItem li = (ListItem)PackageComboBox.Items[PackageComboBox.SelectedIndex];
                if (li != null)
                {
                    KeyText = li.Text.Length > 25 ? li.Text.Substring(0, 25) : li.Text;//RALLY DE 6659

                    bool daily = ((ButtonDetailForm)this)._isDailyButton;
                    int packageId = int.Parse(li.Value);
                    List<DailyProductPackageItem> dailyPackageProducts = null;
                    List<PackageProduct> packageProducts = null;                    
                    
                    if (daily)
                        dailyPackageProducts = ((ButtonDetailForm)this).OurDailyMenuButton.ProductItems;
                    else
                        packageProducts = GetPackageProductMessage.GetPackageProducts(packageId, operatorId);
                    
                    listViewProducts.Items.Clear();

                    if (daily)
                    {
                        if (dailyPackageProducts != null)
                        {
                            foreach (var dailyPackageProductListItem in dailyPackageProducts)
                                AddProductToListView(dailyPackageProductListItem);
                        }
                    }
                    else
                    {
                        if (packageProducts != null)
                        {
                            foreach (var packageProductListItem in packageProducts)
                                AddProductToListView(packageProductListItem);
                        }
                    }
                }

                if (sortColumn > 0) listViewProducts.ForceClickOnColumn(sortColumn);
            }
        }

        /// US1772
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if(_productList == null) // only grab it once if they keep going into the UI
                    _productList = GetProductItemMessage.GetProductItems(operatorId).ToArray();
                SelectProductForm selectProductForm = new SelectProductForm { ProductList = _productList };
                if (selectProductForm.ShowDialog(this) == DialogResult.OK)
                {
                    DisplayProductDetails(selectProductForm.SelectedProductItem, null, false, false);
                }
            }
            catch(Exception ex)
            {
                Logger.LogSevere(Resources.ButtonDetailAddProductError + ex.ToString(), "ButtonDetailForm.cs", 0);
                MessageForm.Show(Resources.ButtonDetailAddProductError, Resources.ButtonDetailErrorTitle, MessageFormTypes.OK);
            }
        }

        /// <summary>
        /// Actions that occur when the user edits the product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (!btnEditProduct.Enabled || !btnEditProduct.Visible) // also called through double-clicks
                return;

            lock (this)
            {
                try
                {
                    if (listViewProducts.SelectedIndices.Count > 0)
                    {
                        Cursor = Cursors.WaitCursor;

                        if (listViewProducts.SelectedItems[0].Tag is PackageProduct)
                        {
                            // Get the package product info from the selected list item.
                            var packageProduct = (PackageProduct)listViewProducts.SelectedItems[0].Tag;

                            // Set the product info
                            var productItem = new ProductItemList
                            {
                                IsActive = true,
                                ProductItemId = packageProduct.ProductId,
                                ProductItemName = packageProduct.ProductName,
                                ProductSalesSourceName = packageProduct.SalesSourceName,
                                ProductTypeId = packageProduct.ProductTypeId,
                                ProductTypeName = packageProduct.ProductTypeName,
                                SalesSourceId = packageProduct.SalesSourceId
                            };

                            DisplayProductDetails(productItem, packageProduct, true, false);
                        }
                        else if (listViewProducts.SelectedItems[0].Tag is DailyProductPackageItem)
                        {

                            // Get the package product info from the selected list item.
                            var packageProduct = ((DailyProductPackageItem)listViewProducts.SelectedItems[0].Tag).ToPackageProduct();
                            if (String.IsNullOrWhiteSpace(packageProduct.ProductTypeName) && _productTypes != null && _productTypes.Any(x=>x.ProductTypeId == packageProduct.ProductTypeId))
                            {
                                packageProduct.ProductTypeName = _productTypes.First(x => x.ProductTypeId == packageProduct.ProductTypeId).ProductType;
                            }

                            // Set the product info
                            var productItem = new ProductItemList
                            {
                                IsActive = true,
                                ProductItemId = packageProduct.ProductId,
                                ProductItemName = packageProduct.ProductName,
                                ProductSalesSourceName = packageProduct.SalesSourceName,
                                ProductTypeId = packageProduct.ProductTypeId,
                                ProductTypeName = packageProduct.ProductTypeName,
                                SalesSourceId = packageProduct.SalesSourceId,
                                Validate = ((DailyProductPackageItem)listViewProducts.SelectedItems[0].Tag).IsValidated,
                                BarcodedPaper = ((DailyProductPackageItem)listViewProducts.SelectedItems[0].Tag).IsBarcodedPaper,
                            };

                            DisplayProductDetails(productItem, packageProduct, true, false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogSevere("Error trying to edit the package product: " + ex.ToString(), "ButtonDetailForm.cs", 0);
                    MessageForm.Show("Error trying to edit the package product: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Actions that occur when the user removes a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (listViewProducts.SelectedIndices.Count > 0)
            {
                if (MessageForm.Show(Resources.ConfirmDelete, Resources.DeleteProductTitle, MessageFormTypes.YesNo, 0) == DialogResult.Yes)
                {
                    // Remove the selected product from the list.
                    listViewProducts.SelectedItems[0].Remove();
                }
            }
        }

        /// <summary>
        /// Actions that occur when the selected index on the listview changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewProducts.SelectedIndices.Count > 0) // multi-select is turned off, but it shouldn't break this functionality
            {
                btnDeleteProduct.Enabled = true;
                btnEditProduct.Enabled = true;
            }
            else
            {
                btnDeleteProduct.Enabled = false;
                btnEditProduct.Enabled = false;
            }
        }
        #endregion

        #region Display Product Forms
        private bool DisplayProductDetails(ProductItemList productItem, object packageProduct, bool isEdit, bool isPaste)
        {
            bool getMore = false;
            // Select the form to display depending on the product type
            switch ((ProductType)productItem.ProductTypeId)
            {
                case ProductType.CrystalBallQuickPick:
                case ProductType.CrystalBallScan:
                case ProductType.CrystalBallHandPick:
                case ProductType.CrystalBallPrompt:
                case ProductType.CrystalBallFavorites:
                    // FIX : TA6092 Support CBB license file flag
                    // FIX : TA7890
                    if (ProdCenterSettings.CrystalBallEnabled)
                    {
                        getMore = DisplayCrystalBallProductDetailForm(productItem, packageProduct, isEdit, isPaste);
                    }
                    else
                    {
                        MessageForm.Show(Resources.ProductTypeNotSupported, Resources.ProductTypeErrorTitle, MessageFormTypes.OK);//RALLY DE 6657
                    }
                    // END : TA6092 Support CBB license file flag
                    break;
                case ProductType.Electronic:
                    getMore = DisplayBingoProductDetailForm(productItem, packageProduct, isEdit, isPaste);
                    break;
                //START RALLY DE 6644
                case ProductType.Concessions:
                    getMore = DisplayBasicProductDetailForm("Concession", productItem, packageProduct, isEdit, isPaste);
                    break;
                //END RALLY DE 6644
                case ProductType.Merchandise:
                    getMore = DisplayBasicProductDetailForm("Merchandise", productItem, packageProduct, isEdit, isPaste);
                    break;
                case ProductType.CreditRefundableFixed:
                case ProductType.CreditNonRefundableFixed:
                    getMore = DisplayBasicProductDetailForm("Credit Fixed", productItem, packageProduct, isEdit, isPaste);
                    break;
                case ProductType.CreditRefundableOpen:
                case ProductType.CreditNonRefundableOpen:
                    getMore = DisplayBasicProductDetailForm("Credit Open", productItem, packageProduct, isEdit, isPaste);
                    break;
                case ProductType.BingoOther:
                    getMore = DisplayBasicProductDetailForm("Other Products", productItem, packageProduct, isEdit, isPaste);
                    break;
                //RALLY DE 6644 support for paper (16) and pulltab (17)
                case ProductType.Paper:
                    getMore = DisplayPaperProductDetailForm("Paper", productItem, packageProduct, isEdit, isPaste);
                    break;
                case ProductType.PullTab:
                    getMore = DisplayBasicProductDetailForm("Pull Tab", productItem, packageProduct, isEdit, isPaste);
                    break;
                //END RALLY DE 6644
                case ProductType.Validation:
                    getMore = DisplayBasicProductDetailForm("Validation", productItem, packageProduct, isEdit, isPaste, true);
                    break;
                case ProductType.BonusValidation://US5361
                    getMore = DisplayBasicProductDetailForm("Bonus Validation", productItem, packageProduct, isEdit, isPaste, true);
                    break;
                default:
                    ProductCenter.Business.ProductCenter.Log(String.Format("Unknown product type {0} being added to a package", productItem.ProductTypeId), LoggerLevel.Warning);
                    getMore = DisplayBasicProductDetailForm(productItem.ProductTypeName, productItem, packageProduct, isEdit, isPaste);
                    //MessageForm.Show(Resources.ProductTypeNotSupported, Resources.ProductTypeErrorTitle, MessageFormTypes.OK);
                    break;
            }
            return getMore;
        }

        private bool IsProductADuplicate(PackageProduct testProduct)
        {
            foreach (ListViewItem prod in listViewProducts.Items)
            {
                if (prod.Tag is PackageProduct)
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
                else if (prod.Tag is DailyProductPackageItem)
                {
                    DailyProductPackageItem curProd = (DailyProductPackageItem)prod.Tag;
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
            DialogResult diagRes = DialogResult.OK;
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
                    CardTypeListItem[] cardTypes = GetCardTypeMessage.GetArray();

                    BingoProductDetailForm bingoProductDetailForm = new BingoProductDetailForm
                    {
                        // FIX DE4125
                        Settings = ProdCenterSettings,
                        //UsePrePrintedPacks = ProdCenterSettings.UsePrePrintedPacks,
                        // END DE4125
                        ProductItem = productItem,
                        PackageProduct = packageProduct,
                        CardLevelList = cardLevelList,
                        CardTypeList = cardTypes,

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
                        bingoProductDetailForm.CardTypeId = packageProduct.CardTypeId; // DE13858
                        bingoProductDetailForm.GameCategoryName = packageProduct.GameCategoryName;
                        bingoProductDetailForm.GameTypeName = GameTypeNameFromId(packageProduct.GameTypeId, gameTypeList);
                        bingoProductDetailForm.Quantity = packageProduct.Quantity.ToString();
                        bingoProductDetailForm.IsTaxed = packageProduct.IsTaxed;
                        bingoProductDetailForm.CardCount = packageProduct.CardCount.ToString();
                        bingoProductDetailForm.Price = packageProduct.Price;
                        bingoProductDetailForm.AltPrice = packageProduct.AltPrice;// US4543
                        bingoProductDetailForm.CountsTowardsQualifyingSpend = packageProduct.CountsTowardsQualifyingSpend; // US4587
                        bingoProductDetailForm.Prepaid = packageProduct.Prepaid;
                        bingoProductDetailForm.PointsPerQuantity = packageProduct.PointsPerQuantity;
                        bingoProductDetailForm.PointsPerDollar = packageProduct.PointsPerDollar;
                        bingoProductDetailForm.PointsToRedeem = packageProduct.PointsToRedeem;
                        bingoProductDetailForm.CardPositionsMapId = packageProduct.CardPositionsMapId;
                        DailyProductPackageItem.CopyPositionStarCodes(packageProduct.m_positionStarCodes, ref bingoProductDetailForm.m_positionStarCodes);
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
                    diagRes = bingoProductDetailForm.ShowDialog(this);
                    
                    if (diagRes != DialogResult.Cancel)
                    {
                        Cursor = Cursors.WaitCursor;

                        if (_isDailyButton)
                        {
                            DailyProductPackageItem selectedItem = null;
                            if (isEdit && listViewProducts.SelectedIndices.Count > 0)
                                selectedItem = (DailyProductPackageItem)listViewProducts.SelectedItems[0].Tag;

                            // Get the product Info from the form.
                            var packageProductListItem = new DailyProductPackageItem
                            {
                                DailyProductId = selectedItem != null? selectedItem.DailyProductId : 0,
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
                                Prepaid = bingoProductDetailForm.Prepaid,
                                PointsPerQuantity = bingoProductDetailForm.PointsPerQuantity,
                                PointsPerDollar = bingoProductDetailForm.PointsPerDollar,
                                PointsToRedeem = bingoProductDetailForm.PointsToRedeem,
                                ProgramGameName = string.Empty,
                                // FIX : TA5759
                                NumbersRequired = (ushort)((bingoProductDetailForm.GameTypeName.Contains("Pick Yur Platter")) ? 12 : 0),
                                // END : TA5759
                                ProgramCBBGameId = 0,
                                CountsTowardsQualifyingSpend = bingoProductDetailForm.CountsTowardsQualifyingSpend, //DE12974
                                CardPositionsMapId = bingoProductDetailForm.CardPositionsMapId,
                            };

                            DailyProductPackageItem.CopyPositionStarCodes(bingoProductDetailForm.m_positionStarCodes, ref packageProductListItem.m_positionStarCodes);

                            if (isEdit)
                            {
                                // Remove the old selected product.
                                listViewProducts.SelectedItems[0].Remove();
                            }

                            // Fix : DE3495 Invalid index after sorting by header
                            AddProductToListView(packageProductListItem, true);
                        }
                        else
                        {
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
                                CountsTowardsQualifyingSpend = bingoProductDetailForm.CountsTowardsQualifyingSpend, //DE12974
                                Prepaid = bingoProductDetailForm.Prepaid
                            };

                            if (IsProductADuplicate(packageProductListItem))
                                return diagRes == DialogResult.OK ? false : true;

                            if (isEdit)
                            {
                                // Remove the old selected product.
                                listViewProducts.SelectedItems[0].Remove();
                            }

                            // Fix : DE3495 Invalid index after sorting by header
                            AddProductToListView(packageProductListItem, true);
                        }

                        Cursor = Cursors.Default;
                    }
                }
                catch (Exception ex)
                {
                    MessageForm.Show("DisplayBingoProductDetailForm(): " + ex.Message);
                }
            }
            return diagRes == DialogResult.Retry ? true : false;
        }

        private bool DisplayCrystalBallProductDetailForm(ProductItemList productItem, object objPackageProduct, bool isEdit, bool isPaste)
        {
            DialogResult diagRes = DialogResult.OK;
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
                    GameCategory[] gameCategoryList = GetGameCategoriesMessage.GetArray();
                    //packageProduct.GameTypeName = "Crystal Ball";

                    // Initialize the form.
                    CrystalBallProductDetailForm crystalBallProductDetailForm = new CrystalBallProductDetailForm
                    {
                        ProductItem = productItem,
                        PackageProduct = packageProduct,
                        CardMediaList = GetCardMediaMessage.GetArray(0),
                        GameCategoryList = gameCategoryList,
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
                        crystalBallProductDetailForm.Prepaid = packageProduct.Prepaid;
                        crystalBallProductDetailForm.PointsPerQuantity = packageProduct.PointsPerQuantity;
                        crystalBallProductDetailForm.PointsPerDollar = packageProduct.PointsPerDollar;
                        crystalBallProductDetailForm.PointsToRedeem = packageProduct.PointsToRedeem;
                        crystalBallProductDetailForm.NumbersRequired = packageProduct.NumbersRequired;
                        crystalBallProductDetailForm.GameTypeName = packageProduct.GameTypeName;                     
                    }
                    Cursor = Cursors.Default;
                    // END TA5759

                    // Display the form
                    diagRes = crystalBallProductDetailForm.ShowDialog(this);
                    if (diagRes != DialogResult.Cancel)
                    {
                        Cursor = Cursors.WaitCursor;

                        if (_isDailyButton)
                        {
                            DailyProductPackageItem selectedItem = null;
                            if (isEdit && listViewProducts.SelectedIndices.Count > 0)
                                selectedItem = (DailyProductPackageItem)listViewProducts.SelectedItems[0].Tag;

                            // Get the product Info from the form.
                            var packageProductListItem = new DailyProductPackageItem
                            {
                                DailyProductId = selectedItem != null ? selectedItem.DailyProductId : 0,
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

                            if (isEdit)
                            {
                                // Remove the old selected product.
                                listViewProducts.SelectedItems[0].Remove();
                            }

                            // Fix : DE3495 Invalid index after sorting by header
                            AddProductToListView(packageProductListItem, true);
                        }
                        else
                        {
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
                                Prepaid = crystalBallProductDetailForm.Prepaid,
                                PointsPerQuantity = crystalBallProductDetailForm.PointsPerQuantity,
                                PointsPerDollar = crystalBallProductDetailForm.PointsPerDollar,
                                PointsToRedeem = crystalBallProductDetailForm.PointsToRedeem,
                                NumbersRequired = crystalBallProductDetailForm.NumbersRequired,
                                ProgramGameName = string.Empty,
                                ProgramCBBGameId = 0
                            };

                            if (IsProductADuplicate(packageProductListItem))
                                return diagRes == DialogResult.OK ? false : true;

                            if (isEdit)
                            {
                                // Remove the old selected product.
                                listViewProducts.SelectedItems[0].Remove();
                            }

                            // Fix : DE3495 Invalid index after sorting by header
                            AddProductToListView(packageProductListItem, true);
                        }
                        Cursor = Cursors.Default;
                    }
                }
                catch (Exception ex)
                {
                    MessageForm.Show("DisplayCrystalBallProductDetailForm(): " + ex.Message);
                }
            }
            return diagRes == DialogResult.Retry ? true : false;
        }

        private bool DisplayBasicProductDetailForm(string formName, ProductItemList productItem, object objPackageProduct, bool isEdit, bool isPaste, bool hideAddAnotherProductButton = false)
        {
            DialogResult diagRes = DialogResult.OK;
            lock (this)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    var packageProduct = (objPackageProduct == null)
                                             ? new PackageProduct()
                                             : (PackageProduct)objPackageProduct;

                    // Initialize the form.
                    BasicProductDetailForm basicProductDetailForm = new BasicProductDetailForm
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
                        basicProductDetailForm.Prepaid = packageProduct.Prepaid;
                        basicProductDetailForm.PointsPerQuantity = packageProduct.PointsPerQuantity;
                        basicProductDetailForm.PointsPerDollar = packageProduct.PointsPerDollar;
                        basicProductDetailForm.PointsToRedeem = packageProduct.PointsToRedeem;
                        basicProductDetailForm.Prepaid = packageProduct.Prepaid;
                    }

                    //US4459: (US4428) Product Center: Create validation package
                    if (hideAddAnotherProductButton)
                    {
                        basicProductDetailForm.HideAllowAnotherProductButton = true;
                    }

                    Cursor = Cursors.Default;

                    basicProductDetailForm.SetProductType(productItem.ProductTypeId); //RALLY DE 6644

                    // Display the form
                    diagRes = basicProductDetailForm.ShowDialog(this);
                    if (diagRes != DialogResult.Cancel)
                    {
                        Cursor = Cursors.WaitCursor;
                        // Get the product Info from the form.
                        if (_isDailyButton)
                        {
                            DailyProductPackageItem selectedItem = null;
                            if (isEdit && listViewProducts.SelectedIndices.Count > 0)
                                selectedItem = (DailyProductPackageItem)listViewProducts.SelectedItems[0].Tag;

                            var packageProductListItem = new DailyProductPackageItem
                            {
                                DailyProductId = selectedItem != null ? selectedItem.DailyProductId : 0,
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
                                Prepaid = basicProductDetailForm.Prepaid,
                                CountsTowardsQualifyingSpend = basicProductDetailForm.CountsTowardsQualifyingSpend, // US4587
                                PointsPerQuantity = basicProductDetailForm.PointsPerQuantity,
                                PointsPerDollar = basicProductDetailForm.PointsPerDollar,
                                PointsToRedeem = basicProductDetailForm.PointsToRedeem,
                                ProgramGameName = string.Empty,
                                NumbersRequired = 0,
                                ProgramCBBGameId = 0
                            };

                            if (isEdit)
                            {
                                // Remove the old selected product.
                                listViewProducts.SelectedItems[0].Remove();
                            }

                            // Fix : DE3495 Invalid index after sorting by header
                            AddProductToListView(packageProductListItem, true);
                        }
                        else
                        {
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
                                Prepaid = basicProductDetailForm.Prepaid,
                                PointsPerQuantity = basicProductDetailForm.PointsPerQuantity,
                                PointsPerDollar = basicProductDetailForm.PointsPerDollar,
                                PointsToRedeem = basicProductDetailForm.PointsToRedeem,
                                ProgramGameName = string.Empty,
                                NumbersRequired = 0,
                                ProgramCBBGameId = 0
                            };

                            if (IsProductADuplicate(packageProductListItem))
                                return diagRes == DialogResult.OK ? false : true;

                            if (isEdit)
                            {
                                // Remove the old selected product.
                                listViewProducts.SelectedItems[0].Remove();
                            }

                            // Fix : DE3495 Invalid index after sorting by header
                            AddProductToListView(packageProductListItem, true);
                        }
                        Cursor = Cursors.Default;
                    }
                }
                catch (Exception ex)
                {
                    MessageForm.Show("DisplayBasicProductDetailForm(): " + ex.Message);
                }
            }
            return diagRes == DialogResult.Retry ? true : false;
        }

        private bool DisplayPaperProductDetailForm(string formName, ProductItemList productItem, object objPackageProduct, bool isEdit, bool isPaste)
        {
            DialogResult diagRes = DialogResult.OK;
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
                    CardTypeListItem[] cardTypes = GetCardTypeMessage.GetArray();

                    // Initialize the form.
                    PaperProductDetailForm paperProductDetailForm = new PaperProductDetailForm
                    {
                        Title = formName,
                        ProductItem = productItem,
                        PackageProduct = packageProduct,
                        CardTypeList = cardTypes, // DE14015
                        CardLevelList = cardLevelList, // US4516
                        CardLevelName = CardLevelName1Multiple(cardLevelList),
                        CardTypeName = CardType.Standard.ToString(),
                        PointsPerDollar = "0",
                        PointsPerQuantity = "0",
                        PointsToRedeem = "0",
                        Price = "0.00",
                        AltPrice = "0.00",
                        CountsTowardsQualifyingSpend = true,
                        Quantity = "1",
                        GameCategoryList = gameCategoryList,
                        AllowAddAnother = !(isEdit || isPaste),
                        WholePoints = ProdCenterSettings.WholeProductPoints,
                        Prepaid = packageProduct.Prepaid
                    };

                    if (isEdit || isPaste)
                    {
                        // Set the form's value for editing.
                        paperProductDetailForm.CardLevelName = packageProduct.CardLevelName;
                        if(packageProduct.CardTypeId != 0)
                            paperProductDetailForm.CardTypeId = packageProduct.CardTypeId;
                        paperProductDetailForm.Quantity = packageProduct.Quantity.ToString();
                        paperProductDetailForm.IsTaxed = packageProduct.IsTaxed;
                        paperProductDetailForm.Price = packageProduct.Price;
                        paperProductDetailForm.AltPrice = packageProduct.AltPrice;
                        paperProductDetailForm.CountsTowardsQualifyingSpend = packageProduct.CountsTowardsQualifyingSpend; // US4587
                        paperProductDetailForm.Prepaid = packageProduct.Prepaid;
                        paperProductDetailForm.PointsPerQuantity = packageProduct.PointsPerQuantity;
                        paperProductDetailForm.PointsPerDollar = packageProduct.PointsPerDollar;
                        paperProductDetailForm.PointsToRedeem = packageProduct.PointsToRedeem;
                        paperProductDetailForm.GameCategoryName = packageProduct.GameCategoryName;
                    }

                    Cursor = Cursors.Default;

                    // Display the form
                    diagRes = paperProductDetailForm.ShowDialog(this);
                    
                    if (diagRes != DialogResult.Cancel)
                    {
                        Cursor = Cursors.WaitCursor;
                        // Get the product Info from the form.
                        if (_isDailyButton)
                        {
                            DailyProductPackageItem selectedItem = null;
                            if (isEdit && listViewProducts.SelectedIndices.Count > 0)
                                selectedItem = (DailyProductPackageItem)listViewProducts.SelectedItems[0].Tag;

                            var packageProductListItem = new DailyProductPackageItem
                            {
                                DailyProductId = selectedItem != null ? selectedItem.DailyProductId : 0,
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
                                CardTypeId = paperProductDetailForm.CardTypeId,
                                CardTypeName = string.Empty,
                                GameCategoryId = paperProductDetailForm.GameCategoryId,
                                GameCategoryName = paperProductDetailForm.GameCategoryName,
                                Quantity = byte.Parse(paperProductDetailForm.Quantity),
                                IsTaxed = paperProductDetailForm.IsTaxed,
                                CardCount = 0,
                                Price = paperProductDetailForm.Price,
                                AltPrice = paperProductDetailForm.AltPrice,
                                Prepaid = paperProductDetailForm.Prepaid,
                                CountsTowardsQualifyingSpend = paperProductDetailForm.CountsTowardsQualifyingSpend, // US4587
                                PointsPerQuantity = paperProductDetailForm.PointsPerQuantity,
                                PointsPerDollar = paperProductDetailForm.PointsPerDollar,
                                PointsToRedeem = paperProductDetailForm.PointsToRedeem,
                                ProgramGameName = string.Empty,
                                NumbersRequired = 0,
                                ProgramCBBGameId = 0
                            };

                            if (isEdit)
                            {
                                // Remove the old selected product.
                                listViewProducts.SelectedItems[0].Remove();
                            }

                            // Fix : DE3495 Invalid index after sorting by header
                            AddProductToListView(packageProductListItem, true);
                            //SavePackageProducts(packageProductListItem);
                        }
                        else
                        {
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
                                Prepaid = paperProductDetailForm.Prepaid,
                                PointsPerQuantity = paperProductDetailForm.PointsPerQuantity,
                                PointsPerDollar = paperProductDetailForm.PointsPerDollar,
                                PointsToRedeem = paperProductDetailForm.PointsToRedeem,
                                ProgramGameName = string.Empty,
                                NumbersRequired = 0,
                                ProgramCBBGameId = 0
                            };

                            if (IsProductADuplicate(packageProductListItem))
                                return diagRes == DialogResult.OK ? false : true;

                            if (isEdit)
                            {
                                // Remove the old selected product.
                                listViewProducts.SelectedItems[0].Remove();
                            }

                            // Fix : DE3495 Invalid index after sorting by header
                            AddProductToListView(packageProductListItem, true);
                        }
                        Cursor = Cursors.Default;
                    }
                }
                catch (Exception ex)
                {
                    MessageForm.Show("DisplayPaperProductDetailForm(): " + ex.Message);
                }
            }
            return diagRes == DialogResult.Retry ? true : false;
        }
        
        /// <summary>
        /// Adds the selected product to the list
        /// </summary>
        /// <param name="packProd">the product to add</param>
        /// <param name="restoreSort">
        /// whether or not to enforce the sort for the item being added (from the user pressing a column header)
        ///   Note: this is somewhat resource intensive, so if you're adding multiple items, send false and then sort outside
        /// </param>
        private void AddProductToListView(PackageProduct packProd, bool restoreSort = false)
        {
            int sortColumn = listViewProducts.SortColumn;
            if (restoreSort && sortColumn > 0)
                listViewProducts.PreLoadAdjustment();

            var lvi = listViewProducts.Items.Add(packProd.ProductName);
            lvi.Tag = packProd;
            lvi.SubItems.Add(packProd.CardMediaName);
            lvi.SubItems.Add(packProd.Quantity.ToString());
            
            lvi.SubItems.Add(Helper.DecimalStringToMoneyString(packProd.Price));

            if (restoreSort && sortColumn > 0)
                listViewProducts.ForceClickOnColumn(sortColumn);
        }

        /// <summary>
        /// Adds the selected daily product to the list
        /// </summary>
        /// <param name="packProd">the product to add</param>
        /// <param name="restoreSort">
        /// whether or not to enforce the sort for the item being added (from the user pressing a column header)
        ///   Note: this is somewhat resource intensive, so if you're adding multiple items, send false and then sort outside
        /// </param>
        private void AddProductToListView(DailyProductPackageItem packProd, bool restoreSort = false)
        {
            int sortColumn = listViewProducts.SortColumn;
            if (restoreSort && sortColumn > 0)
                listViewProducts.PreLoadAdjustment();

            var lvi = listViewProducts.Items.Add(packProd.ProductName);
            lvi.Tag = packProd;
            lvi.SubItems.Add(packProd.CardMediaName);
            lvi.SubItems.Add(packProd.Quantity.ToString());
            
            lvi.SubItems.Add(Helper.DecimalStringToMoneyString(packProd.Price));

            if (restoreSort && sortColumn > 0)
                listViewProducts.ForceClickOnColumn(sortColumn);
        }        
        #endregion Display Product Forms
    }
}