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
using GTI.Modules.ProductCenter.Business;//RALLY DE 4125
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Properties;
using GTI.Modules.ProductCenter.Data;
using GTI.Modules.Shared.Data;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class BingoProductDetailForm : GradientForm
    {
        #region Local variables
        private int gameTypeMultipleNumber;
        private enum GameTypeMultiples
        {
            Enum0On = 0,
            Enum2On = 2,
            Enum3On = 3,
            Enum4On = 4,
            Enum6On = 6,
            Enum9On = 9,
            Enum12On = 12,
            Enum15On = 15,
            Enum18On = 18
        };
        #endregion Constants
        #region Constructors
        public BingoProductDetailForm()
        {
            InitializeComponent();

            // Create and assign the form's idle event
            Application.Idle += OnIdle;
            AcceptButton = btnDone;
            CancelButton = btnCancel;

            //System.Drawing.Color defaultBackground = System.Drawing.ColorTranslator.FromHtml("#44658D");
            //this.BackColor = defaultBackground;
            //this.ForeColor = System.Drawing.Color.White;
        }

        public void CheckForWholePoints()
        {
            if (Settings.WholeProductPoints)
            {
                lblPointsPerDollarLabel.Visible = false;
                txtPointsPerDollar.Visible = false;

                txtPointsPerQuantity.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Integer;
                txtPointsToRedeem.Mask = GTI.Controls.TextBoxNumeric.TextBoxType.Integer;
            }
        }

        private void BingoProductDetailForm_Load(object sender, EventArgs e)
        {
          
            cboCardLevelList.Focus();

            CheckForWholePoints();
        }

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
                    CardLevelName != PackageProduct.CardLevelName ||
                    CardTypeName != PackageProduct.CardTypeName ||
                    cardcount != PackageProduct.CardCount ||
                    CardMediaName != PackageProduct.CardMediaName ||
                    GameCategoryName != PackageProduct.GameCategoryName ||
                    GameTypeName != PackageProduct.GameTypeName ||
                    PointsPerQuantity != PackageProduct.PointsPerQuantity ||
                    PointsPerDollar != PackageProduct.PointsPerDollar ||
                    PointsToRedeem != PackageProduct.PointsToRedeem ||
                    price1 != price2 || altPrice1 != altPrice2 ||
                    CountsTowardsQualifyingSpend != PackageProduct.CountsTowardsQualifyingSpend || // US4587
                    IsTaxed != PackageProduct.IsTaxed ||
                    qty != PackageProduct.Quantity);
                doEnable = doEnable
                    && (qty > 0)
                    && ValidateMultiples()
                    && CardCount != String.Empty
                    && cardcount > 0
                    && Quantity != String.Empty
                    && Price != String.Empty
                    && AltPrice != String.Empty
                    && PointsToRedeem != String.Empty
                    && PointsPerDollar != String.Empty
                    && PointsPerQuantity != String.Empty;
                btnDone.Enabled = doEnable;
                btnAdd.Enabled = doEnable && AllowAddAnother;
            }
        }
        #endregion Constructors

        #region Add, Done and Cancel Click event handlers
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateProductCardCount())//If true then cancel.
            {
                return;
            }

            if (!ValidateMultiples())
            {
                MessageForm.Show(Resources.InvalidMultipleNumber, Resources.ValidateMultiplesTitle, MessageFormTypes.OK);//RALLY DE 6657
            }
            else
            {
                Application.Idle -= OnIdle;
                DialogResult = DialogResult.Retry;
                Close();
            }
        }

        private void DoneClick(object sender, EventArgs e)
        {
            if (ValidateProductCardCount())//If true then cancel.
            {
                //Application.Idle -= OnIdle;
                return;
            }

            if (!ValidateMultiples())
            {
                MessageForm.Show(Resources.InvalidMultipleNumber, Resources.ValidateMultiplesTitle, MessageFormTypes.OK);//RALLY DE 6657
            }
            else
            {
                Application.Idle -= OnIdle;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void CancelClick(object sender, EventArgs e)
        {
            Application.Idle -= OnIdle;
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion

        private bool ValidateProductCardCount()
        {
            return false; //JKIM disable validation for now
            bool result = false;
            if (productItem.ProductTypeId == 5 || productItem.ProductTypeId == 16)
            {
                //Check if the card is validate
                if (productItem.Validate == true)
                {
                    //Get the validate product count.
                    if (ValidateCardCount != Convert.ToInt32(txtCardCount.Text))
                    {
                        MessageForm.Show("Validate Card Count Error: The Card Count should be divisible by " + ValidateCardCount.ToString() + "." /*to be validated."*/, "Validate Card Count", /*MessageFormTypes.OK_FlatDesign*/ MessageFormTypes.OK);//RALLY DE 6657
                        result = true;
                    }
                }
            }
            return result;
        }


        #region ValidateMultiples support method
        private bool ValidateMultiples()
        {
            // Game Type Multiple Number
            switch (GameTypeId)
            {
                case 5:
                    gameTypeMultipleNumber = (int)GameTypeMultiples.Enum2On;
                    break;
                case 6:
                    gameTypeMultipleNumber = (int)GameTypeMultiples.Enum3On;
                    break;
                //START RALLY US 1121
                case 7:  // FourCard
                case 16: // potofgold
                    gameTypeMultipleNumber = (int)GameTypeMultiples.Enum4On;
                    break;
                //END RALLY US 1121
                case 8:
                    gameTypeMultipleNumber = (int)GameTypeMultiples.Enum6On;
                    break;
                case 9:
                    gameTypeMultipleNumber = (int)GameTypeMultiples.Enum9On;
                    break;
                case 10:
                    gameTypeMultipleNumber = (int)GameTypeMultiples.Enum12On;
                    break;
                case 11:
                    gameTypeMultipleNumber = (int)GameTypeMultiples.Enum15On;
                    break;
                case 12:
                    gameTypeMultipleNumber = (int)GameTypeMultiples.Enum18On;
                    break;
                default:
                    gameTypeMultipleNumber = (int)GameTypeMultiples.Enum0On;
                    break;
            }

            if (gameTypeMultipleNumber != 0)
            {
                // Verify if is an integer number
                decimal dCardCount;
                if (!decimal.TryParse(txtCardCount.Text, out dCardCount))
                    return false;

                var dCardCountReminder = dCardCount % 1;

                if (dCardCountReminder != 0)
                {
                    return false;
                }

                // Verify if Card Count is greater or equal than the Multiple
                int iCardCount = (int)dCardCount;
                if (iCardCount < gameTypeMultipleNumber)
                {
                    return false;
                }

                // Verify if is a Multiple of the On game
                var iCardCountReminder = iCardCount % gameTypeMultipleNumber;
                if (iCardCountReminder != 0)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion Methods
        public PackageProduct PackageProduct { private get; set; }
        public bool AllowAddAnother { get; set; }

        public int ValidateCardCount
        {
            get;
            set;
        }

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
        #region Card Type
        /// <summary>
        /// Sets the Card Type List.
        /// </summary>
        public Array CardTypeList
        {
            set
            {
                cboCardTypeList.Items.Clear();
                foreach (CardTypeListItem cardTypeListItem in value)
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
                if (li != null)
                    int.TryParse(li.Value, out num);
                return num;
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
            set{cboCardTypeList.Text = value;}
        }
        #endregion
        #region Card Media
        
        /// <summary>
        /// Gets the Selected Card Media Id.
        /// </summary>
        public int CardMediaId
        {
            get
            {
                return 1; //RALLY DE 6643
            }
        }

        /// <summary>
        /// Gets or Sets the Card Media Name.
        /// </summary>
        public string CardMediaName
        {
            //RALLY DE 6643
            get
            {
                return CardMedia.Electronic.ToString();
            }      
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
        #endregion
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
                    // FIX TA5759
                    //if (gameTypeListItem.GameTypeName == "Crystal Ball")
                    if (gameTypeListItem.GameTypeId == (int)GameType.CrystalBall)
                    {
                        if ((ProductItem.ProductTypeId > 0) && (ProductItem.ProductTypeId < 5)) // CBB types 1, 2, 3, 4
                        {
                            cboGameTypeList.Items.Add(new ListItem(gameTypeListItem.GameTypeName, gameTypeListItem.GameTypeId.ToString()));
                        }
                    }
                    else if (gameTypeListItem.GameTypeId != (int)GameType.PickYurPlatter)
                    {
                        cboGameTypeList.Items.Add(new ListItem(gameTypeListItem.GameTypeName, gameTypeListItem.GameTypeId.ToString()));
                    }
                    // END TA5759
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
        private void cboGameTypeList_SelectedValueChanged(object sender, EventArgs e)
        {
            //START RALLY 4125
            //START RALLY TA 5744
            if (Settings.PlayWithPaper)
            {
                CardTypeName = "Standard";
                cboCardTypeList.Enabled = false;
            }
            //END RALLY TA 5744
            
            
            else
            {
                switch ((GameType)GameTypeId)
                {
                    case GameType.B13:
                    case GameType.AllStar:
                        CardTypeName = "No Free";
                        cboCardTypeList.Enabled = false;
                        break;
                    case GameType.TwoOn:
                    case GameType.ThreeOn:
                    case GameType.FourOn:
                    case GameType.SixOn:
                    case GameType.NineOn:
                    case GameType.TwelveOn:
                    case GameType.FifteenOn:
                    case GameType.EighteenOn:
                    case GameType.DoubleAction:
                    case GameType.NinetyNumberBingo:
                    case GameType.PotOfGold:
                    case GameType.BingoStorm:
                        CardTypeName = "Standard";
                        cboCardTypeList.Enabled = false;
                        break;
                    //START RALLY DE 6741 card type must be standard for slingo
                    case GameType.Slingo:
                        CardTypeName = "Standard";
                        cboCardTypeList.Enabled = false;
                        break;
                    //END RALLY DE 6741
                    default:
                        cboCardTypeList.Enabled = true;
                        break;
                }
            }       
            // END DE4125


            // FIX TA5873
            if (productItem.PaperLayoutCount == 0)
            {
                if (GameTypeName.ToLower().Contains("on") ||
                    GameTypeName.ToLower().Contains("pot-of-gold"))
                {
                    if (!ValidateMultiples())
                    {
                        CardCount = gameTypeMultipleNumber.ToString();
                    }
                }
            }
            // END TA5873
        }
        #endregion
        // FIX DE4125
        #region Settings
        public ProductCenterSettings Settings
        {
            get;
            set;
        }
        #endregion
        
        // END DE4125
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
        #region Decimal Support
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
        #region Card Count / Quantity
        public string CardCount
        {
            get { return txtCardCount.Text; }
            set { txtCardCount.Text = string.IsNullOrEmpty(value) ? "1" : value; } 
        }
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
        private void txtCardCount_TextChanged(object sender, EventArgs e)
        {
            // START RALLY TA5873
            if (productItem.PaperLayoutCount > 0)
            {
                txtCardCount.BackColor = Color.FromArgb(215, 251, 193);
            }
            // END RALLY TA5873
            // START RALLY DE4125
            if (GameTypeName.ToLower().Contains("on") ||
                
                GameTypeName.ToLower().Contains("pot-of-gold"))
                // END DE4125
            {
                txtCardCount.BackColor = !ValidateMultiples() ? Color.LightPink : Color.FromArgb(215, 251, 193);
            }
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
