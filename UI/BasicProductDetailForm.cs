// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © FortuNet dba GameTech
// International, Inc.
//
// US3692 Adding support for whole points


using System;
using System.Drawing;
using System.Windows.Forms;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Data;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class BasicProductDetailForm : GradientForm
    {
        #region Local variables
        public string Title { set { Text = value + " Product Details"; } }
        private bool m_hideAllowAnotherProductButton;//US4459
        #endregion Constants
        #region Constructors
        public BasicProductDetailForm()
        {
            InitializeComponent();
            Application.Idle += OnIdle;

            //System.Drawing.Color defaultBackground = System.Drawing.ColorTranslator.FromHtml("#44658D");
            //this.BackColor = defaultBackground;
            //this.ForeColor = System.Drawing.Color.White;
        }

        private void BasicProductDetailForm_Load(object sender, EventArgs e)
        {
            txtPointsPerQuantity.SelectionStart = 0;
            txtPointsPerQuantity.SelectionLength = txtPointsPerQuantity.Text.Length;
            AcceptButton = btnDone;
            CancelButton = btnCancel;
            txtPointsPerQuantity.Focus();

            CheckForWholePoints(); // US3692
        }
        //START RALLY DE 6644
        public void SetProductType(int productType)
        {
            switch (productType)
            {
               
                //credit
                case(10):
                case(11):
                case(12):
                case(13):
                    txtQuantity.Visible = false;
                    lblQuantity.Visible = false;
                    txtPrice.Visible = false;
                    lblPrice.Visible = false;
                    Quantity = "1";
                    Price = "0";
                    AltPrice = "0";
                    break;
                default:
                    break;
            }

        }
        //END RALLY DE 6644

        // US3692
        private void CheckForWholePoints()
        {
            if (WholePoints)
            {
                lblPointsPerDollarLabel.Visible = false;
                txtPointsPerDollar.Visible = false;

                // Don't allow decimal points
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
                bool doEnable = 
                    (PointsPerQuantity != PackageProduct.PointsPerQuantity ||
                    PointsPerDollar != PackageProduct.PointsPerDollar ||
                    PointsToRedeem != PackageProduct.PointsToRedeem ||
                    price1 != price2 || altPrice1 != altPrice2 ||
                    CountsTowardsQualifyingSpend != PackageProduct.CountsTowardsQualifyingSpend || // US4587
                    Prepaid != PackageProduct.Prepaid ||
                    IsTaxed != PackageProduct.IsTaxed ||
                    Quantity != PackageProduct.Quantity.ToString())
                    && (qty > 0);
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
        //US4459
        public bool HideAllowAnotherProductButton {
            get { return m_hideAllowAnotherProductButton; }
            set
            {
                m_hideAllowAnotherProductButton = value;
                btnAdd.Visible = !value;
            }
        }
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

        // US3692
        public bool WholePoints {get; set;}

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
        /// Get/Set whether the product has been pre-paid
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
        /// <summary>
        /// The alternative price used for coupons/discounts
        /// </summary>
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
    }
}
