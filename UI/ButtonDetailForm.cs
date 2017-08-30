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

namespace GTI.Modules.ProductCenter.UI
{
    public partial class ButtonDetailForm : GradientForm
    {
        protected DisplayMode DisplayMode = new NormalDisplayMode();
        private readonly int packageWidth;
        private readonly int nonPackageWidth;
        private readonly int operatorId;
        private static ButtonGraphicSelection buttonGraphicSelection;
       
        public ButtonDetailForm(int opId, bool wholePoints)
        {
            Canceled = true;
            operatorId = opId;
            InitializeComponent();
            Application.Idle += OnIdle;
            AcceptButton = btnAccept;
            CancelButton = btnCancel;
            packageWidth = Width;
            nonPackageWidth = listViewProducts.Left;

            // US3692
            WholePoints = wholePoints;

            //Set flat design
            //System.Drawing.Color defaultBackground = System.Drawing.ColorTranslator.FromHtml("#44658D");
            //BackColor = defaultBackground;
            //this.ForeColor = System.Drawing.Color.White;
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

            imgButtonGraphic.Stretch = isStretch;
            imgButtonGraphic.ImageNormal = curImage;
        }

        private void OnIdle(object sender, EventArgs e)
        {
            btnAccept.Enabled = (PackageComboBox.SelectedIndex != -1 ||
                DiscountComboBox.SelectedIndex != -1 ||
                FunctionComboBox.SelectedIndex != -1) &&
                KeyTextBox.Text != string.Empty;
        }

        public bool IsCreateMode { get; set; }
        public bool Canceled { get; set; }
        public bool Cleared { get; private set; }
        public int PageNumber { get; set; }
        public int KeyNumber { get; set; }
        public bool WholePoints { get; set; } //US3692
        public System.Drawing.Image curImage { get; set; } //US4935
        public bool isStretch { get; set; }//US4935

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
                            if (PackageComboBox.SelectedIndex == -1) PackageComboBox.SelectedIndex = 0;
                            li = (ListItem)PackageComboBox.Items[PackageComboBox.SelectedIndex];
                            KeyText = li.Text.Length > 35 ? li.Text.Substring(0, 35) : li.Text;//RALLY DE 6659
                        }
                        Width = packageWidth;
                        checkBoxPlayerRequired.Enabled = true; // RALLY DE12882
                        PlayerRequired = false;
                        break;
                    case "DiscountRB":
                        DiscountComboBox.Visible = true;
                        if (DiscountComboBox.Items.Count > 0)
                        {
                            if (DiscountComboBox.SelectedIndex == -1) DiscountComboBox.SelectedIndex = 0;

                            DiscountComboBox_SelectedIndexChanged(this, null); // RALLY DE12882 Note: don't need to do the same "KeyText" as the other cases since it's the same logic as in this method
                        }
                        Width = nonPackageWidth;
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
                        break;
                }
            }
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
                    var strPackName = String.Format("{0,-35}{1,6:C}", packageItemList.PackageName, Convert.ToDecimal(packageItemList.PackagePrice));
                    PackageComboBox.Items.Add(new ListItem(strPackName, packageItemList.PackageId.ToString()));
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
                    //string info = String.Format("{0,-9}", (DiscountType)item.Type);
                    //string amnt;
                    //string pnts = String.Format("{0,10} Pts/$", item.PointsPerDollar);
                    //switch (item.Type)
                    //{
                    //    case DiscountType.Fixed:
                    //        amnt = Helper.DecimalStringToMoneyString(item.DiscountAmount.ToString()); // FIX: DE1858 TA2521 Cannot enter negative money value
                    //        info += amnt + pnts;
                    //        break;
                    //    case DiscountType.Open:
                    //        info += "---" + pnts;
                    //        break;
                    //    case DiscountType.Percent:
                    //        amnt = String.Format("{0}%", item.DiscountAmount);
                    //        info += amnt + pnts;
                    //        break;
                    //}

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

        /// <summary>
        /// Populates the Button Graphics Combo Box
        /// </summary>
        /*public Array PopulateButtonGraphicsComboBox //US4935 removed
        {
            set
            {
                // Clear the list
                ButtonGraphicsComboBox.Items.Clear();

                // Populate the List
                foreach (ButtonGraphic graphic in value)
                {
                    ButtonGraphicsComboBox.Items.Add(new ListItem(graphic.ButtonGraphicDescription, graphic.ButtonGraphicId.ToString()));
                }
            }
        }*/
        
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

        private void AcceptClick(object sender, EventArgs e)
        {
            Application.Idle -= OnIdle;
            Canceled = false;
            Cleared = false;
            Close();
        }

        private void DeleteClick(object sender, EventArgs e)
        {
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
                ListItem li = (ListItem)PackageComboBox.Items[PackageComboBox.SelectedIndex];
                if (li != null)
                {
                    KeyText = li.Text.Length > 25 ? li.Text.Substring(0, 25) : li.Text;//RALLY DE 6659
                    int packageId = int.Parse(li.Value);
                    var packageProducts = GetPackageProductMessage.GetPackageProducts(packageId, operatorId);
                    listViewProducts.Items.Clear();
                    foreach (var packageProductListItem in packageProducts)
                    {
                        var lvi = listViewProducts.Items.Add(packageProductListItem.ProductName);
                        lvi.SubItems.Add(packageProductListItem.CardMediaName);
                    }
                }
            }
        }

    }
}