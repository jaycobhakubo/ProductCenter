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
using GTI.Modules.Shared.Data;

//TA9926

namespace GTI.Modules.ProductCenter.UI
{
    public partial class PaperProductDetailForm : GradientForm
    {
        #region Local variables
        public string Title { set { Text = value + " Product Details"; } }
        #endregion Constants
        #region Constructors
        public PaperProductDetailForm()
        {
            InitializeComponent();

            //Set new flat background
            //System.Drawing.Color defaultBackground = System.Drawing.ColorTranslator.FromHtml("#44658D");
            //this.BackColor = defaultBackground;
            //this.ForeColor = System.Drawing.Color.White;
            Application.Idle += OnIdle;
            //AcceptButton = btnDone;
            //CancelButton = btnCancel;
        }

        private void PaperProductDetailForm_Load(object sender, EventArgs e)
        {
            txtPointsPerQuantity.SelectionStart = 0;
            txtPointsPerQuantity.SelectionLength = txtPointsPerQuantity.Text.Length;
            txtPointsPerQuantity.Focus();

            // US3692 
            CheckForWholePoints();
        }

        /// <summary>
        /// US3692 Adding support for only allowing whole points
        /// </summary>
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
        // FIX : DE3072 Done button not disabled on entry
        private void OnIdle(object sender, EventArgs e)
        {
            if (ProductItem.ProductTypeId > 0)
            {
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
                    CardLevelName != PackageProduct.CardLevelName || 
                    PointsPerQuantity != PackageProduct.PointsPerQuantity ||
                    PointsPerDollar != PackageProduct.PointsPerDollar ||
                    PointsToRedeem != PackageProduct.PointsToRedeem ||
                    GameCategoryName != PackageProduct.GameCategoryName ||
                    price1 != price2 || altPrice1 != altPrice2 ||
                    IsTaxed != PackageProduct.IsTaxed ||
                    CardLevelId != PackageProduct.CardLevelId ||  // US4516
                    CardTypeId != PackageProduct.CardTypeId ||  // DE14016
                    CountsTowardsQualifyingSpend != PackageProduct.CountsTowardsQualifyingSpend || // US4587
                    Prepaid != PackageProduct.Prepaid ||
                    Quantity != PackageProduct.Quantity.ToString())
                    && qty > 0;
                btnDone.Enabled = doEnable;
                btnAdd.Enabled = doEnable && AllowAddAnother;
            }
        }
        // END : DE3072
        #endregion Constructors
        #region Accept and Cancel Click event handlers
        private void btnAdd_Click(object sender, EventArgs e)
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
        #region PackageProduct
        public PackageProduct PackageProduct { private get; set; }
        public bool AllowAddAnother { get; set; }
        #endregion
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
                if ((value.ProductTypeId == 5 || value.ProductTypeId == 16) && value.Validate == true)//Paper and electronic and validate = true
                {
                    lblNoteValidation.Visible = true;
                }
                else
                {
                    lblNoteValidation.Visible = false;
                }
            }
        }
        #endregion
        #region Card Level
        /// Rally US4516
        /// <summary>
        /// Sets the Card Level List.
        /// </summary>
        public Array CardLevelList
        {
            set
            {
                cboCardLevelList.Items.Clear();
                foreach (CardLevelItem cardLevelListItem in value)
                {
                    cboCardLevelList.Items.Add(new ListItem(cardLevelListItem.CardLevelName,
                                                            cardLevelListItem.CardLevelId.ToString()));
                }
            }
        }
        /// <summary>
        /// Gets the Selected Card Level Id.
        /// </summary>
        public int CardLevelId
        {
            get
            {
                ListItem li = (ListItem)cboCardLevelList.SelectedItem;
                int num = 0;
                if (li != null)
                    int.TryParse(li.Value, out num);
                return num;
            }
        }

        /// <summary>
        /// Gets or Sets the Card Level Name.
        /// </summary>
        public string CardLevelName
        {
            get
            {
                ListItem li = (ListItem)cboCardLevelList.SelectedItem;
                return li != null ? li.Text : string.Empty;
            }
            set { cboCardLevelList.Text = value; }
        }
        #endregion
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
        #region Prepaid
        /// <summary>
        /// Get/Set whether or not the product is prepaid
        /// </summary>
        public bool Prepaid
        {
            get
            {
                return checkBoxPrepaid.Checked;
            }
            set
            {
                checkBoxPrepaid.Checked = value;
            }
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
        #region Decimal Support
        private void Decimal_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Helper.Decimal_Validating(sender, e);
        }
        private void Decimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            Helper.Decimal_KeyPress(sender, e);
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
        private void Number_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Helper.Number_Validating(sender, e);
        }
        private void Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            Helper.Number_KeyPress(sender, e);
        }
        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            int result;
            int.TryParse(txtQuantity.Text, out result);
            bool isOk = result > 0;
            txtQuantity.BackColor = !isOk ? Color.LightPink : Color.FromArgb(215, 251, 193);
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
                ListItem li = (ListItem)cboGameCategoryList.SelectedItem;
                return li != null ? li.Text : string.Empty;
            }
            set { cboGameCategoryList.Text = value; }
        }

        // US3692
        public bool WholePoints { get; set; }
        #endregion
        #region Card Type
        /// <summary>
        /// Sets the Card Type List.
        /// </summary>
        public Array CardTypeList
        {
            set
            {
                cboCardTypeList.Items.Clear();
                foreach(CardTypeListItem cardTypeListItem in value)
                {
                    cboCardTypeList.Items.Add(new ListItem(cardTypeListItem.CardTypeName,
                                                           cardTypeListItem.CardTypeId.ToString()));
                }
            }
        }
        /// <summary>
        /// Gets the Selected Card Type Id.
        /// </summary>
        public int CardTypeId
        {
            get
            {
                ListItem li = (ListItem)cboCardTypeList.SelectedItem;
                int num = 0;
                if(li != null)
                    int.TryParse(li.Value, out num);
                return num;
            }
            set
            {
                foreach(ListItem li in cboCardTypeList.Items)
                {
                    int num = 0;
                    int.TryParse(li.Value, out num);
                    if(num == value)
                    {
                        cboCardTypeList.SelectedItem = li;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or Sets the Card Type Name.
        /// </summary>
        public string CardTypeName
        {
            get
            {
                ListItem li = (ListItem)cboCardTypeList.SelectedItem;
                return li != null ? li.Text : string.Empty;
            }
            set { cboCardTypeList.Text = value; }
        }
        #endregion

    }
}
