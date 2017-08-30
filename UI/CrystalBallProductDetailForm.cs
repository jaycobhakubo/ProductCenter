// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © FortuNet dba GameTech
// International, Inc.
//
// US3692 Adding support for whole points
using System;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Data;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class CrystalBallProductDetailForm : GradientForm
    {
        #region Constructors
        public CrystalBallProductDetailForm()
        {
            InitializeComponent();

            //Set new flat background
            //System.Drawing.Color defaultBackground = System.Drawing.ColorTranslator.FromHtml("#44658D");
            //this.BackColor = defaultBackground;
            //this.ForeColor = System.Drawing.Color.White;

            // Create and assign the form's idle event
            Application.Idle += OnIdle;
            AcceptButton = btnDone;
            CancelButton = btnCancel;
        }

        private void CrystalBallProductDetailForm_Load(object sender, EventArgs e)
        {
            txtCardCount.Focus();

            CheckForWholePoints();
        }

        private void CheckForWholePoints()
        {
            if (WholePoints)
            {
                lblPointsPerDollarLabel.Visible = false;
                txtPointsPerDollar.Visible = false;

                txtPointsPerQuantity.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Integer;
                txtPointsToRedeem.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Integer;
            }
        }

        // FIX TA5759
        private void OnIdle(object sender, EventArgs e)
        {
            if (ProductItem.ProductTypeId > 0)
            {
                int cardcount;
                int.TryParse(CardCount, out cardcount);
                int qty;
                int.TryParse(Quantity, out qty);
                decimal price1;
                decimal.TryParse(Price, out price1);
                decimal price2;
                decimal.TryParse(PackageProduct.Price, out price2);
                decimal altPrice1; // US4543
                decimal.TryParse(AltPrice, out altPrice1);
                decimal altPrice2;
                decimal.TryParse(PackageProduct.AltPrice, out altPrice2);
                bool doEnable = (
                    CardCount != PackageProduct.CardCount.ToString() ||
                    CardMediaName != PackageProduct.CardMediaName ||
                    PointsPerQuantity != PackageProduct.PointsPerQuantity ||
                    PointsPerDollar != PackageProduct.PointsPerDollar ||
                    PointsToRedeem != PackageProduct.PointsToRedeem ||
                    price1 != price2 || altPrice1 != altPrice2 ||
                    CountsTowardsQualifyingSpend != PackageProduct.CountsTowardsQualifyingSpend || // US4587
                    IsTaxed != PackageProduct.IsTaxed ||
                    Quantity != PackageProduct.Quantity.ToString() ||
                    GameCategoryName != PackageProduct.GameCategoryName ||
                    GameTypeName != PackageProduct.GameTypeName ||
                    NumbersRequired != PackageProduct.NumbersRequired)
                    &&
                    (cardcount > 0 && qty > 0 && NumbersRequired > 0 && NumbersRequired < 17);
                btnDone.Enabled = doEnable;
                btnAdd.Enabled = doEnable && AllowAddAnother;
                if (!doEnable && NumbersRequired == 0)
                    txtNumbersRequired_TextChanged(null, null);
            }
        }
        // END TA5759
        #endregion Constructors
        #region Add, Done and Cancel Click event handlers
        private void AddClick(object sender, EventArgs e)
        {
            Application.Idle -= OnIdle;
            DialogResult = DialogResult.Retry;
            Close();
        }

        private void DoneClick(object sender, EventArgs e)
        {
            Application.Idle -= OnIdle;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelClick(object sender, EventArgs e)
        {
            Application.Idle -= OnIdle;
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
        public PackageProduct PackageProduct { private get; set; }
        public bool AllowAddAnother { get; set; }
        #region Product Item
        private ProductItemList productItem;
        /// <summary>
        /// Sets the product item object.
        /// </summary>
        public ProductItemList ProductItem
        {
            private get { return productItem; } 
            set
            {
                productItem = value;
                txtProductName.Text = value.ProductItemName;
                txtProductType.Text = value.ProductTypeName;
             
            }
        }
        #endregion
        #region Card Level
        /// <summary>
        /// Gets the Selected Card Level Id.
        /// </summary>
        public int CardLevelId { get; set; }
        /// <summary>
        /// Gets or Sets the Card Level Name.
        /// </summary>
        public string CardLevelName { get; set; }
        #endregion
        #region Card Type
        /// <summary>
        /// Gets the Selected Card Type Id.
        /// </summary>
        public int CardTypeId { get; set; }
        /// <summary>
        /// Gets or Sets the Card Type Name.
        /// </summary>
        public string CardTypeName { get; set; }
        #endregion
        #region Card Count
        /// <summary>
        /// Get/Set the Card Count
        /// </summary>
        public string CardCount
        {
            get { return txtCardCount.Text; }
            set { txtCardCount.Text = string.IsNullOrEmpty(value) ? "1" : value; }
        }
        #endregion
        #region Card Media
        /// <summary>
        /// Sets the Card Media List.
        /// </summary>
        public Array CardMediaList
        {
            set
            {
                cboCardMediaList.Items.Clear();
                foreach (CardMediaListItem cardMediaListItem in value)
                {
                    cboCardMediaList.Items.Add(new ListItem(cardMediaListItem.CardMediaName, 
                                                            cardMediaListItem.CardMediaId.ToString()));
                }
            }
        }
        /// <summary>
        /// Gets the Selected Card Media Id.
        /// </summary>
        public int CardMediaId
        {
            get
            {
                ListItem li = (ListItem)cboCardMediaList.SelectedItem;
                int num = 0;
                if (li != null)
                    int.TryParse(li.Value, out num);
                return num;
            }
        }

        /// <summary>
        /// Gets or Sets the Card Media Name.
        /// </summary>
        public string CardMediaName
        {
            get
            {
                ListItem li = (ListItem)cboCardMediaList.SelectedItem;
                return li != null ? li.Text : string.Empty;
            }
            set { cboCardMediaList.Text = value; }
        }
        #endregion
        #region Game Category
        /// <summary>
        /// Sets the Game Category List.
        /// </summary>
        public Array GameCategoryList
        {
            set
            {
                cboGameCategoryList.Items.Clear();
                foreach (GameCategory gameCategoryListItem in value)
                {
                    cboGameCategoryList.Items.Add(new ListItem(gameCategoryListItem.Name,
                                                               gameCategoryListItem.Id.ToString()));
                }
            }
        }
        /// <summary>
        /// Gets the Selected Game Category Id.
        /// </summary>
        public int GameCategoryId
        {
            get
            {
                ListItem li = (ListItem)cboGameCategoryList.SelectedItem;
                int num = 0;
                if (li != null)
                    int.TryParse(li.Value, out num);
                return num;
            }
        }

        /// <summary>
        /// Gets or Sets the Game Category Name.
        /// </summary>
        public string GameCategoryName
        {
            get
            {
                var li = (ListItem)cboGameCategoryList.SelectedItem;
                return li != null ? li.Text : string.Empty;
            }
            set { cboGameCategoryList.Text = value; }
        }
        #endregion
        // FIX TA5759
        #region Game Type
        /// <summary>
        /// Sets the Game Type List.
        /// </summary>
        public Array GameTypeList
        {
            set
            {
                // Save the current game type in use
                var strTemp = "";
                if (!string.IsNullOrEmpty(cboGameTypeList.Text))
                {
                    strTemp = cboGameTypeList.Text;
                }

                cboGameTypeList.Items.Clear();

                // Populate the List.
                foreach (GameTypeListItem gameTypeListItem in value)
                {
                    if (gameTypeListItem.GameTypeId == (int)GameType.CrystalBall ||
                        gameTypeListItem.GameTypeId == (int)GameType.PickYurPlatter)
                    {
                        cboGameTypeList.Items.Add(new ListItem(gameTypeListItem.GameTypeName, gameTypeListItem.GameTypeId.ToString()));
                    }
                }

                // Restore the current game type in use
                if (!string.IsNullOrEmpty(strTemp))
                {
                    cboGameTypeList.Text = strTemp;
                }
            }
        }
        /// <summary>
        /// Gets the Selected Game Type Id.
        /// </summary>
        public int GameTypeId
        {
            get
            {
                var li = (ListItem)cboGameTypeList.SelectedItem;
                int num = 0;
                if (li != null)
                    int.TryParse(li.Value, out num);
                return num;
            }
        }

        /// <summary>
        /// Gets or Sets the Game Type Name.
        /// </summary>
        public string GameTypeName
        {
            get
            {
                var li = (ListItem)cboGameTypeList.SelectedItem;
                return li != null ? li.Text : string.Empty;
            }
            set
            {
                cboGameTypeList.Text = value;
            }
        }
        private void GameTypeChanged(object sender, EventArgs e)
        {
            if (productItem.PaperLayoutCount == 0)
            {
                if (GameTypeName.ToLower().Contains("pick yur platter"))
                {
                    NumbersRequired = 12;
                    txtNumbersRequired.Enabled = false;
                    return;
                }
            }
            txtNumbersRequired.Enabled = true;
        }
        #endregion
        // END TA5759
        #region Points Per Quantity
        /// <summary>
        /// Get/Set the PointsPerQuantity
        /// </summary>
        public string PointsPerQuantity
        {
            get { return txtPointsPerQuantity.Text; }
            set { txtPointsPerQuantity.Text = string.IsNullOrEmpty(value) ? "0.0" : value; }
        }
        #endregion
        #region Points Per Dollar
        /// <summary>
        /// Get/Set the PointsPerDollar
        /// </summary>
        public string PointsPerDollar
        {
            get { return txtPointsPerDollar.Text; }
            set { txtPointsPerDollar.Text = string.IsNullOrEmpty(value) ? "0.0" : value; }
        }
        #endregion
        #region Points To Redeem
        /// <summary>
        /// Get/Set the PointsToRedeem
        /// </summary>
        public string PointsToRedeem
        {
            get { return txtPointsToRedeem.Text; }
            set { txtPointsToRedeem.Text = string.IsNullOrEmpty(value) ? "0.0" : value; }
        }
        #endregion
        #region Counts Towards Qualifying Spend
        /// US4587
        /// <summary>
        /// Get/Set whether or not the product applies towards the qualifying points for the sale
        /// </summary>
        public bool CountsTowardsQualifyingSpend
        {
            get { return checkBoxPointQualify.Checked; }
            set { checkBoxPointQualify.Checked = value; }
        }

        #endregion
        #region Price
        public string Price
        {
            get { return txtPrice.Text; }
            set
            {
                decimal price = 0;
                Decimal.TryParse(value, out price);
                txtPrice.Text = price.ToString("F2"); // if it's invalid, just set it to zero instead of crashing
            }
        }
        private void Money_Enter(object sender, EventArgs e)
        {
            Helper.Money_Enter(sender, e);
        }
        private void Money_Validated(object sender, EventArgs e)
        {
            Helper.Money_Validated(sender, e);
        }
        #endregion
        #region Alt Price
        // US4543
        public string AltPrice
        {
            get { return txtAltPrice.Text; }
            set
            {
                decimal altPrice = 0;
                Decimal.TryParse(value, out altPrice);
                txtAltPrice.Text = altPrice.ToString("F2"); // if it's invalid, just set it to zero instead of crashing
            }
        }
        #endregion
        #region Taxed
        /// <summary>
        /// Gets or Sets the Is Taxed flag.
        /// </summary>
        public bool IsTaxed
        {
            get { return checkBoxTaxed.Checked; }
            set { checkBoxTaxed.Checked = value; }
        }
        #endregion
        #region Quantity
        /// <summary>
        /// Get/Set the Quantity
        /// </summary>
        public string Quantity
        {
            get { return txtQuantity.Text; }
            set { txtQuantity.Text = string.IsNullOrEmpty(value) ? "1" : value; }
        }
        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            int result;
            int.TryParse(txtQuantity.Text, out result);
            bool isOk = result > 0;
            txtQuantity.BackColor = !isOk ? Color.LightPink : Color.FromArgb(215, 251, 193);
        }
        #endregion
        #region Numbers Required
        /// <summary>
        /// Get/Set the NumbersRequired
        /// </summary>
        public ushort NumbersRequired
        {
            get 
            {
                ushort nr;
                ushort.TryParse(txtNumbersRequired.Text, out nr); 
                return nr;
            }
            set
            {
                txtNumbersRequired.Text = value.ToString();
            }
        }
        private void txtNumbersRequired_TextChanged(object sender, EventArgs e)
        {
            ushort numReq = NumbersRequired;
            txtNumbersRequired.BackColor = (numReq > 0 && numReq < 17)
                                               ? Color.FromArgb(215, 251, 193)
                                               : Color.LightPink;
        }
        #endregion
        #region Field event support
        private void Number_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Helper.Number_Validating(sender, e);
        }
        private void Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            Helper.Number_KeyPress(sender, e);
        }
        private void Decimal_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Helper.Decimal_Validating(sender, e);
        }
        private void Decimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            // FIX : DE3115 prevent negative values on some price fields
            Helper.PositiveDecimal_KeyPress(sender, e);
            // END : DE3115
        }
        #endregion

        // US3692
        public bool WholePoints { get; set; }
       
    }
}
