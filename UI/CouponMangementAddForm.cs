// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2016 Fortunet

//US4852: Product Center > Coupons: Require spend
//US5155 ability to view awarded coupons

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Properties;
using GTI.Modules.ProductCenter.Data;
using GTI.Controls;
using GTI.Modules.Shared.Business;
using GTI.Modules.ProductCenter.Business;

namespace GTI.Modules.ProductCenter.UI
{
    public partial class CouponMangementAddForm : GradientForm
    {
        #region Constants

        private const string DISCOUNT_TYPE_VALUE = "Value";         // the text that displays when the coupon type is "value"
        private const string DISCOUNT_TYPE_ALT_PACKAGE = "Package"; // the text that displays when the coupon type is "alt package"
        private const string DATE_FORMAT = "MM/dd/yyyy";

        #endregion

        #region Private Members

        private int m_operatorId;
        private string CouponName;
        private DateTime CouponStartDate;
        private DateTime CouponEndDate;
        private decimal CouponValue;
        private bool isSaved;
        private bool isCouponNameValid = false;
        private bool isStartDateNEndDateValid = false;
        private bool isDecimalValueValid = false;
        private long StartDateValue;
        private long EndDateValue;
        private int CouponID;
        private bool IsNew = true;
        GTIListView lvfromCoupon = new GTIListView();
        private bool LoadingValue = false;
        private int CouponMaxUsage;
        private bool IsCouponExpired; //Need to know if the data being modified is expired or not.
        private PlayerComp.AwardTypes m_awardType = PlayerComp.AwardTypes.Manual;
        private List<int> m_restrictedProductIds = new List<int>();
        private List<int> m_restrictedPackageIds = new List<int>();
        private List<int> m_awardedPackageIDs = new List<int>(); // US4941 the packages this coupon earns
        private ProductCenterSettings m_productCenterSettings;
        private bool m_enabled = true; // Whether or not the UI is enabled. Note: don't set this directly, use SetUIEnable()

        #endregion

        #region Public Properties

        public bool dIsCouponExpired
        {
            get { return IsCouponExpired; }
            set { IsCouponExpired = value; }
        }

        public int dCouponMaxUsage
        {
            get { return CouponMaxUsage; }
            set { CouponMaxUsage = value; }
        }

        public bool dIsNew
        {
            get { return IsNew; }
            set { IsNew = value; }
        }

        public int dCouponID
        {
            get { return CouponID; }
            set { CouponID = value; }
        }

        public string dCouponName
        {
            get { return CouponName; }
            set { CouponName = value; }
        }

        public DateTime dStartDate
        {
            get { return CouponStartDate; }
            set { CouponStartDate = value; }
        }

        public DateTime dEndDate
        {
            get { return CouponEndDate; }
            set { CouponEndDate = value; }
        }

        public decimal dValue
        {
            get { return CouponValue; }
            set { CouponValue = value; }
        }

        public PlayerComp.CouponTypes SelectedCouponType
        {
            get
            {
                return (PlayerComp.CouponTypes)(cmbxCouponType.SelectedIndex + 1); // enum value is 1-based
            }
            set
            {
                if (value >= 0 && Enum.IsDefined(typeof(PlayerComp.CouponTypes), value))
                    cmbxCouponType.SelectedIndex = (int)(value-1);
                else
                    cmbxCouponType.SelectedIndex = ((int)PlayerComp.CouponTypes.FixedValue) - 1; // default value. enum value is 1-based 'default(PlayerComp.CouponTypes)' would return zero
            }
        }

        public PlayerComp.AwardTypes AwardType
        {
            get
            {
                if (radiobtnAwardType.Checked)
                    return PlayerComp.AwardTypes.Auto;
                else
                    return PlayerComp.AwardTypes.Manual;
            }
            set
            {
                m_awardType = value;
                if (value == PlayerComp.AwardTypes.Manual)
                    radiobtnAwardType2.Checked = true;
                else
                    radiobtnAwardType.Checked = true;
            }
        }

        public decimal UnlockSpend
        {
            get
            {
                decimal spend = 0;
                Decimal.TryParse(txtBxUnlockSpend.Text, out spend);
                return spend;
            }
            set
            {
                txtBxUnlockSpend.Text = Helper.DecimalStringToMoneyString(value.ToString());
            }
        }

        public int UnlockSessionCount
        {
            get
            {
                int sessCount = 0;
                Int32.TryParse(txtBxSessCount.Text, out sessCount);
                return sessCount;
            }
            set
            {
                txtBxSessCount.Text = value.ToString();
            }
        }

        public decimal MinimumSpendToQualify
        {
            get
            {
                decimal minToQualify;
                decimal.TryParse(txtbxMinimumSpendToQualify.Text, out minToQualify);
                return minToQualify;
            }
            set
            {
                txtbxMinimumSpendToQualify.Text =
                    Helper.DecimalStringToMoneyString(value.ToString(CultureInfo.InvariantCulture));
            }
        }

        public List<int> RestrictedProductIds
        {
            get
            {
                m_restrictedProductIds.Clear();

                foreach (CcBoxItem checkedItem in productExcludeFromQualifyCkCboBx.CheckedItems)
                {
                    m_restrictedProductIds.Add(checkedItem.Value);
                }

                return m_restrictedProductIds;
            }
            set
            {
                m_restrictedProductIds = value;
            }
        }

        public List<int> RestrictedPackageIds
        {
            get
            {
                m_restrictedPackageIds.Clear();

                foreach (CcBoxItem checkedItem in packageExcludeFromQualifyCkCboBx.CheckedItems)
                {
                    m_restrictedPackageIds.Add(checkedItem.Value);
                }

                return m_restrictedPackageIds;
            }
            set
            {
                m_restrictedPackageIds = value;
            }
        }

        public List<int> AwardedPackageIds
        {
            get
            {
                m_awardedPackageIDs.Clear();

                foreach (CcBoxItem checkedItem in cmbxCouponPackage.CheckedItems)
                {
                    m_awardedPackageIDs.Add(checkedItem.Value);
                }

                return m_awardedPackageIDs;
            }
            set
            {
                m_awardedPackageIDs = value;
            }
        }

        public bool IsRestrictionsModified { get; private set; }

        public bool IgnoreValidationsForIgnoredPackages
        {
            get
            {
                return ignoreValChkBx.Checked;
            }
        }
        #endregion

        #region Constructor and Loading

        /// <summary>
        /// The default constructor for the Coupon Management form
        /// </summary>
        public CouponMangementAddForm(int operatorId, ProductCenterSettings settings)
        {
            m_productCenterSettings = settings;

            InitializeComponent();

            if (!m_productCenterSettings.EnableValidation)
                ignoreValChkBx.Visible = false;

            m_operatorId = operatorId;

            AcceptButton = imgbtnAccept; //btnAccept; //ENTER key.
            CancelButton = imgbtnCancel; //btnCancel; ESC Key

            List<PlayerComp.CouponTypes> types = new List<PlayerComp.CouponTypes>(Enum.GetValues(typeof(PlayerComp.CouponTypes)).Cast<PlayerComp.CouponTypes>());
            types.Sort(); // they usually return in-order, but technically it's not guaranteed

            foreach (PlayerComp.CouponTypes value in types)
            {
                cmbxCouponType.Items.Add(EnumToString.GetDescription(value));
            }
            cmbxCouponType.SelectedIndex = 0;
        }
        
        /// <summary>
        /// First initialization.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CouponMangementAddForm_Load(object sender, EventArgs e)
        {
            this.SuspendLayout(); // wait to update the UI till we're done.

            List<PackageItem> packages = null; // don't make this a member variable. Can cause issues.
            List<ProductItemList> products = null;
            try
            {
                packages = new List<PackageItem>(PackageItems.Sorted);
                if (packages != null)
                {
                    packages.Sort((x, y) => String.Compare(x.PackageName, y.PackageName, StringComparison.Ordinal));
                    foreach (var package in packages)
                    {
                        var item = new CcBoxItem(package.PackageName, package.PackageId, package);

                        cmbxCouponPackage.Items.Add(item);
                    }
                    cmbxCouponPackage.DisplayMember = "Name";
                }
            }
            catch (Exception ex)
            {
                string err = "Error loading available packages for coupons: " + ex.ToString();
                Logger.LogSevere(err, "CouponMangementAddForm.cs", 0);
                MessageBox.Show(err);
            }

            try
            {
                //US4852
                //get product list 
                products = new List<ProductItemList>(GetProductItemMessage.GetProductItems(m_operatorId).Where(p => p.ProductItemId > 0 && p.IsActive).ToList());

                if (products != null)
                {
                    products.Sort((x, y) => String.Compare(x.ProductItemName, y.ProductItemName, StringComparison.Ordinal));

                    foreach (var product in products)
                    {
                        var item = new CcBoxItem(product.ProductItemName, product.ProductItemId, product);

                        productExcludeFromQualifyCkCboBx.Items.Add(item);
                    }

                    productExcludeFromQualifyCkCboBx.DisplayMember = "Name";
                }
            }
            catch (Exception ex)
            {
                string err = "Error loading available products for coupons: " + ex.ToString();
                Logger.LogSevere(err, "CouponMangementAddForm.cs", 0);
                MessageBox.Show(err);
            }

            try
            {
                //US4932
                if (packages != null)
                {
                    foreach (var package in packages)
                    {
                        var item = new CcBoxItem(package.PackageName, package.PackageId, package);

                        packageExcludeFromQualifyCkCboBx.Items.Add(item);
                    }

                    packageExcludeFromQualifyCkCboBx.DisplayMember = "Name";
                }
            }
            catch (Exception ex)
            {
                string err = "Error loading available packages for coupon: " + ex.ToString();
                Logger.LogSevere(err, "CouponMangementAddForm.cs", 0);
                MessageBox.Show(err);
            }

            clearErrorMessage();
            if (IsNew == true) // add new coupon
            {
                if (imgbtnAccept.Enabled != false) { imgbtnAccept.Enabled = false; }

                //dtepickerCouponStartDate = new DateTimePicker();
                dtepickerCouponStartDate.Format = DateTimePickerFormat.Custom;
                dtepickerCouponStartDate.CustomFormat = DATE_FORMAT;
                dtepickerCouponEndDate.Format = DateTimePickerFormat.Custom;

                PopulateTimeComboBox();
                populateStartTime(DateTime.Now, null); //Populate the combo box for start time and end time.
                
                txtbxCouponName.Select();//Select coupon name as default.
                txtbxCouponName.Focus();
            }
            else //Edit coupon
            {
                //Do not validate untill loading is complete.
                LoadingValue = true;
                
                //Populate data to its section.
                txtbxCouponName.Text = CouponName;
                if (CouponMaxUsage != 0)
                {
                    txtbxMaxUsage.Text = CouponMaxUsage.ToString();
                }
                else
                {
                    txtbxMaxUsage.Text = string.Empty;
                }
                txtbxValue.Text = Helper.DecimalStringToMoneyString(CouponValue.ToString());

                dtepickerCouponStartDate.Value = CouponStartDate;
                dtepickerCouponStartDate.Format = DateTimePickerFormat.Custom;
                dtepickerCouponStartDate.CustomFormat = "MM/dd/yyyy";

                dtepickerCouponEndDate.Value = CouponEndDate;
                dtepickerCouponEndDate.Format = DateTimePickerFormat.Custom;
                dtepickerCouponEndDate.CustomFormat = "MM/dd/yyyy";

                PopulateTimeComboBox();
                populateStartTime(CouponStartDate, CouponEndDate);

                txtbxCouponName.Select();//Select coupon name as default.
                txtbxCouponName.Focus();

                isCouponNameValid = true; //Set to true since they are previous coupon that has been accepted.
                isDecimalValueValid = true; //Set to true since they are previous coupon that has been accepted.
                isStartDateNEndDateValid = true;

                LoadingValue = false;
                //Enable the accept button since its not expired and data are valid.
                if (IsCouponExpired == false && m_enabled)
                {
                    imgbtnAccept.Enabled = true;
                }

                try
                {
                    //US4852
                    //update checked items in drop down
                    var temp = new List<int>(m_restrictedProductIds); // use a temp list so that it doesn't change out from under us. Might be getting triggers to remake the list
                    foreach (var id in temp)
                    {
                        for (var i = 0; i < productExcludeFromQualifyCkCboBx.Items.Count; i++)
                        {
                            CcBoxItem item = productExcludeFromQualifyCkCboBx.Items[i] as CcBoxItem;

                            if (item != null && item.Value == id)
                            {
                                productExcludeFromQualifyCkCboBx.SetItemChecked(i, true);
                                break; // found the match. Break out early
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string err = "Error loading coupon's restricted products: " + ex.ToString();
                    Logger.LogSevere(err, "CouponMangementAddForm.cs", 0);
                    MessageBox.Show(err);
                }
                
                try
                {
                    //US4932
                    //update checked items in drop down
                    var temp = new List<int>(m_restrictedPackageIds); // use a temp list so that it doesn't change out from under us. Might be getting triggers to remake the list
                    foreach (var id in temp)
                    {
                        for (var i = 0; i < packageExcludeFromQualifyCkCboBx.Items.Count; i++)
                        {
                            CcBoxItem item = cmbxCouponPackage.Items[i] as CcBoxItem;

                            if (item != null && item.Value == id)
                            {
                                packageExcludeFromQualifyCkCboBx.SetItemChecked(i, true);
                                break; // found the match. Break out early
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string err = "Error loading coupon's restricted packages: " + ex.ToString();
                    Logger.LogSevere(err, "CouponMangementAddForm.cs", 0);
                    MessageBox.Show(err);
                }
                
                try
                {
                    //US4941
                    //update checked items in drop down
                    var temp = new List<int>(m_awardedPackageIDs); // use a temp list so that it doesn't change out from under us. Might be getting triggers to remake the list
                    foreach (var id in temp)
                    {
                        for (var i = 0; i < cmbxCouponPackage.Items.Count; i++)
                        {
                            CcBoxItem item = cmbxCouponPackage.Items[i] as CcBoxItem;

                            if (item != null && item.Value == id)
                            {
                                cmbxCouponPackage.SetItemChecked(i, true);
                                break; // found the match. Break out early
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string err = "Error loading coupon's awarded packages: " + ex.ToString();
                    Logger.LogSevere(err, "CouponMangementAddForm.cs", 0);
                    MessageBox.Show(err);
                }

                IsRestrictionsModified = false;
            }
            AwardType = m_awardType;

            this.ResumeLayout();

            radiobtnAwardType_CheckedChanged(this, null); // update the UI
        }

        #endregion

        #region METHODS

        /// US5155
        /// <summary>
        /// Sets all the controls on this ui to enabled or disabled
        ///   note: keeps the return/cancel button enabled
        /// </summary>
        /// <param name="enable"></param>
        public void SetUIEnable(bool enable)
        {
            try
            {
                foreach (Control control in this.Controls)
                {
                    control.Enabled = enable; // if the controls inside ContentControls don't properly disable, we might have to manually disable them as well. Leaving this as-is for now
                }
                
                m_enabled = enable;
            }
            catch (Exception ex)
            {
                MessageForm.Show("Error setting UI enabled status" + ex.ToString());
            }
            finally
            {
                imgbtnCancel.Enabled = true; // still need a way to return
                lblUnableToEditCoupon.Enabled = true; // The user feedback needs to be clear
            }
        }

        /// <summary>
        /// Generates a coupon based on the send in information
        /// </summary>
        /// <returns></returns>
        public PlayerComp GenerateCoupon()
        {
            PlayerComp coupon = new PlayerComp();
            coupon.Id = dCouponID;
            coupon.Name = dCouponName;
            coupon.StartDate = dStartDate;
            coupon.EndDate = dEndDate;
            coupon.Value = dValue;
            coupon.CouponMaxUsage = dCouponMaxUsage;
            coupon.CouponType = SelectedCouponType;
            coupon.EarnedPackageIDs = AwardedPackageIds; // US4941
            coupon.AwardType = AwardType;
            coupon.UnlockSpend = UnlockSpend;
            coupon.UnlockSessionCount = UnlockSessionCount;
            coupon.MinimumSpendToQualify = MinimumSpendToQualify;//US4852
            coupon.RestrictedProductIds = RestrictedProductIds;//US4852
            coupon.RestrictedPackageIds = RestrictedPackageIds; // US4932
            coupon.IgnoreValidationsForIgnoredPackages = ignoreValChkBx.Checked; // US4932

            return coupon;
        }

        /// <summary>
        /// Sets the UI info to the sent in coupon information
        /// </summary>
        /// <param name="coupon"></param>
        public void SetCouponData(PlayerComp coupon)
        {
            dCouponID = coupon.Id;
            dCouponName = coupon.Name;
            dStartDate = coupon.StartDate;
            dEndDate = coupon.EndDate;
            dValue = coupon.Value;
            dCouponMaxUsage = coupon.CouponMaxUsage;
            dIsNew = dCouponID == 0;
            dIsCouponExpired = coupon.IsExpired;
            SelectedCouponType = coupon.CouponType;
            if (coupon.EarnedPackageIDs != null)
                AwardedPackageIds = new List<int>(coupon.EarnedPackageIDs); // US4941
            else
                AwardedPackageIds = new List<int>();
            AwardType = coupon.AwardType;
            UnlockSpend = coupon.UnlockSpend;
            UnlockSessionCount = coupon.UnlockSessionCount;
            MinimumSpendToQualify = coupon.MinimumSpendToQualify;//US4852
            if (coupon.RestrictedProductIds != null)
                RestrictedProductIds = coupon.RestrictedProductIds;//US4852
            else
                RestrictedProductIds = new List<int>();
            if (coupon.RestrictedPackageIds != null)
                RestrictedPackageIds = coupon.RestrictedPackageIds; // US4932
            else
                RestrictedPackageIds = new List<int>();
            ignoreValChkBx.Checked = coupon.IgnoreValidationsForIgnoredPackages; // US4932

            if (coupon.LastAwardedDate != null)
            {
                SetUIEnable(false);

                lblUnableToEditCoupon.Text = "Cannot edit the coupon because it has been awarded.";
                lblUnableToEditCoupon.Visible = true;
            }
        }

        /// <summary>
        /// Check if all entry are valid.
        /// If true then enable accept button else disable.
        /// </summary>
        private void checkAllValidationOfEntry()
        {
            if (!m_enabled) // everything has been forced to disabled, don't do validation
                return;

            if (isCouponNameValid && isStartDateNEndDateValid
                && ((SelectedCouponType == PlayerComp.CouponTypes.AltPricePackage) && AwardedPackageIds.Count > 0
                    || (SelectedCouponType == PlayerComp.CouponTypes.FixedValue) && isDecimalValueValid
                    || (SelectedCouponType == PlayerComp.CouponTypes.PercentPackage) && AwardedPackageIds.Count > 0) // && has min spend
                && (AwardType == PlayerComp.AwardTypes.Manual || UnlockSpend >= 0 && UnlockSessionCount != 0))
            {
                if (imgbtnAccept.Enabled != true) { imgbtnAccept.Enabled = true; }
            }
            else
            {
                if (imgbtnAccept.Enabled != false) { imgbtnAccept.Enabled = false; }
            }
        }
        
        /// <summary>
        /// Clear entry.
        /// </summary>
        private void ClearCouponEntry()
        {
            txtbxCouponName.Text = string.Empty;
            txtbxValue.Text = string.Empty;
            dtepickerCouponStartDate.Value = DateTime.Now;
            dtepickerCouponEndDate.ResetText();
            cmbxCouponType.SelectedIndex = 0;

            for (var i = 0; i < cmbxCouponPackage.Items.Count; i++) // clear out the multi-select combo boxes
                cmbxCouponPackage.SetItemChecked(i, false);
            for (var i = 0; i < packageExcludeFromQualifyCkCboBx.Items.Count; i++)
                packageExcludeFromQualifyCkCboBx.SetItemChecked(i, false);
            for (var i = 0; i < productExcludeFromQualifyCkCboBx.Items.Count; i++)
                productExcludeFromQualifyCkCboBx.SetItemChecked(i, false);
        }

        /// <summary>
        /// Clear any warning or error message.
        /// </summary>
        private void clearErrorMessage()
        {
            if (isSaved == false)
            {
                errorProvider1.Clear();
                if (lblSavedSuccessfully.Visible != false) { lblSavedSuccessfully.Visible = false; }
            }
            else
            {
                isSaved = false;
            }
        }

        /// <summary> 
        /// Populate the Time(hhmmtt) for start time and end time.
        /// </summary>
        private void populateStartTime(DateTime? dtStart, DateTime? dtEnd)
        {
            int tempIndex;

            if (dtEnd.HasValue)// US4657 do the end time
            {
                string EHourNow = dtEnd.Value.ToString("hh");
                string EMinNow = dtEnd.Value.ToString("mm");
                string EAMPMNow = dtEnd.Value.ToString("tt");

                // Hour
                tempIndex = cmbxHourStart.Items.IndexOf(EHourNow);
                cmbxHourEnd.SelectedIndex = tempIndex;

                //Min
                tempIndex = cmbxMinStart.Items.IndexOf(EMinNow);
                cmbxMinEnd.SelectedIndex = tempIndex;

                //AMPM
                tempIndex = cmbxAMPMStart.Items.IndexOf(EAMPMNow);
                cmbxAMPMEnd.SelectedIndex = tempIndex;
            }
            else
            {
                //dtepickerCouponEndDate.CustomFormat = "SELECT A DATE"; // displays this instead of the blank date. Can't use these: M,d,y,h,s,t,z
                dtepickerCouponEndDate.CustomFormat = " "; // clears it out so it doens't display anything.
            }
            if (dtStart.HasValue)
            {
                string HourNow = dtStart.Value.ToString("hh");
                string MinNow = dtStart.Value.ToString("mm");
                string AMPMNow = dtStart.Value.ToString("tt");

                // Hour
                tempIndex = cmbxHourStart.Items.IndexOf(HourNow);
                cmbxHourStart.SelectedIndex = tempIndex;

                //Min
                tempIndex = cmbxMinStart.Items.IndexOf(MinNow);
                cmbxMinStart.SelectedIndex = tempIndex;

                //AMPM
                tempIndex = cmbxAMPMStart.Items.IndexOf(AMPMNow);
                cmbxAMPMStart.SelectedIndex = tempIndex; 
            }           
        }

        private void PopulateTimeComboBox()
        {
            var hours = Enumerable.Range(1, 12).Select(i => i.ToString("d2"));// ("D2"));
            var minutes = Enumerable.Range(00, 60).Select(i => i.ToString("d2"));
            foreach (var h in hours)
            {
                cmbxHourStart.Items.Add(h);
                cmbxHourEnd.Items.Add(h);
            }

            foreach (var m in minutes)
            {
                cmbxMinStart.Items.Add(m);
                cmbxMinEnd.Items.Add(m);
                //   Console.WriteLine(m);
            }

            cmbxAMPMStart.Items.Add("AM");//0 
            cmbxAMPMStart.Items.Add("PM");//1
            cmbxAMPMEnd.Items.Add("AM");//0    
            cmbxAMPMEnd.Items.Add("PM");//1
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Accept or saved new entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgbtnAccept_Click(object sender, EventArgs e)
        {
            CouponName = txtbxCouponName.Text;
            if (!string.IsNullOrEmpty(txtbxMaxUsage.Text))
            {
                CouponMaxUsage = Convert.ToInt32(txtbxMaxUsage.Text);
            }
            else
            {
                CouponMaxUsage = 1;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Delete after completion of project.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearErrorOnEnterEvent(object sender, EventArgs e)
        {
            clearErrorMessage();
        }

        /// <summary>
        /// Cancel changes will go back to Parent window which is the Coupon Management.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgbtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Numeric only and 1 decimal point for coupon value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(sender is TextBox))
                return;
            TextBox txtBxDec = sender as TextBox;

            bool reject = false;

            string x = txtBxDec.Text;
            int count = x.Split('.').Length - 1;

            if (!char.IsControl(e.KeyChar))
            {
                switch (e.KeyChar)
                {
                    case (char)46://period
                        //allow 1 decimal point
                        if (count > 0)
                        {
                            reject = true;
                        }
                        else
                        {
                            reject = false;
                        }
                        break;
                    default:
                        reject = !char.IsDigit(e.KeyChar);
                        if(!reject && sender == txtbxValue && SelectedCouponType == PlayerComp.CouponTypes.PercentPackage) // is a digit and the type is percentage
                        {
                            string futureVal = x.Insert(txtBxDec.SelectionStart, e.KeyChar.ToString());
                            reject = Decimal.Parse(futureVal) > 100.0m; // check if the new value will be over 100, if so, reject it
                        }
                        break;
                }
            }
            else if (e.KeyChar == (char)Keys.Back)
            {
                reject = false;
            }
            else if (Regex.IsMatch(txtbxValue.Text, @"\.\d\d"))
            {
                reject = true;
            }
            e.Handled = reject;
        }

        /// <summary>
        /// Only allow numeric value on max coupon usage textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbxInt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(sender is TextBox))
                return;
            TextBox txtBxInt = sender as TextBox;

            bool result = true;
            if (e.KeyChar == (char)Keys.Back)
            {
                result = false;
            }

            if (result)//If result = true
            {
                //Lets us not allow the user to input 0 value
                if (txtBxInt.Text.Count() == 0)
                {
                    if (e.KeyChar == (char)Keys.D0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = !char.IsDigit(e.KeyChar);
                        //result = false;
                    }
                }
                else //if (result == false)//process only if the char is valid upto this point.
                {
                    result = !char.IsDigit(e.KeyChar);
                }
            }

            e.Handled = result;
        }

        /// <summary>
        /// Actions that occur when the user releases a keyboard key. Validates the entered fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBxInt_KeyUp(object sender, KeyEventArgs e)
        {
            checkAllValidationOfEntry();
        }

        /// <summary>
        /// Manual validation for "StartDateTime" and "EndDateTime".
        /// </summary>
        private void validateStartDateNEndDate()
        {
            if (LoadingValue == false)
            {
                errorProvider1.Clear();

                if (cmbxHourStart.SelectedItem == null || cmbxMinStart.SelectedItem == null || cmbxAMPMStart.SelectedItem == null)
                {
                    isStartDateNEndDateValid = false;
                    return; //exit now if its false   
                }
                string tempStartDate = dtepickerCouponStartDate.Value.ToString("MM/dd/yyyy") + " " + cmbxHourStart.SelectedItem.ToString() + ":" + cmbxMinStart.SelectedItem.ToString() + " " + cmbxAMPMStart.SelectedItem.ToString();
                bool testIsDate = DateTime.TryParse(tempStartDate, out CouponStartDate);

                if (cmbxHourEnd.SelectedItem == null || cmbxMinEnd.SelectedItem == null || cmbxAMPMEnd.SelectedItem == null)
                {
                    isStartDateNEndDateValid = false;
                    return; //exit now if its false   
                }
                string tempEndDate = dtepickerCouponEndDate.Value.ToString("MM/dd/yyyy") + " " + cmbxHourEnd.SelectedItem.ToString() + ":" + cmbxMinEnd.SelectedItem.ToString() + " " + cmbxAMPMEnd.SelectedItem.ToString();
                if(testIsDate) // date values are already messed up, don't bother
                    testIsDate = DateTime.TryParse(tempEndDate, out CouponEndDate);

                if (!testIsDate) // shouldn't happen if the values are loaded from the controls
                {
                    isStartDateNEndDateValid = false;
                    errorProvider1.SetError(dtepickerCouponStartDate, "Start or end times are not valid values.");
                    return; //exit now if its false   
                }
                StartDateValue = long.Parse(CouponStartDate.ToString("yyyyMMddHHmm")); // JAN- seems like we're asking for trouble here when we can just compare dates, but I'm leaving it as-is since it functions
                EndDateValue = long.Parse(CouponEndDate.ToString("yyyyMMddHHmmss"));

                //US3931/TA13963
                /* // JAN- Doesn't make sense to the user. Very annoying to try to use a default value that's invalid removed per US4657
                long tempDateNowValue = long.Parse(DateTime.Now.ToString("yyyyMMddHHmm"));

                if (tempDateNowValue >= StartDateValue)
                {
                    isStartDateNEndDateValid = false;
                    if (imgbtnAccept.Enabled != false) { imgbtnAccept.Enabled = false; }
                    return; //exit now if its false              
                }*/

                StartDateValue = long.Parse(CouponStartDate.ToString("yyyyMMddHHmmss"));
                if (StartDateValue >= EndDateValue)
                {
                    isStartDateNEndDateValid = false;
                    errorProvider1.SetError(dtepickerCouponEndDate, "Value must be greater than start time");
                }
                else
                {
                    isStartDateNEndDateValid = true;
                }
            }
        }

        /// <summary>
        /// Check if the start date and end date value is valid.
        /// If all entry are valid then enable the accept button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtepickerCouponStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (sender is DateTimePicker)
            {
                (sender as DateTimePicker).CustomFormat = DATE_FORMAT;
            }

            if (cmbxHourEnd.SelectedIndex == -1) // nothing has been selected, set to end-of-day
            {
                populateStartTime(null, DateTime.Now.Date.AddMinutes(-1)); // ignores the date portion. Basically take the start of a day and go back a minute to get to the max of another day.
            }

            validateStartDateNEndDate();
            checkAllValidationOfEntry();
        }

        /// <summary>
        /// Check if the Coupon Name is valid.
        /// Check all entry if valid then enable accept button if valid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbxCouponName_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtbxCouponName.Text))
            {
                errorProvider1.Clear();
                if (isCouponNameValid == false)
                {
                    isCouponNameValid = true;
                }
            }
            else
            {
                isCouponNameValid = false;
                errorProvider1.SetError(txtbxCouponName, "Coupon name cannot be empty.");
            }

            checkAllValidationOfEntry();
        }

        /// <summary>
        /// Check if the decimal value is valid.
        /// If all entry are valid then enable accept button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbxValue_KeyUp(object sender, KeyEventArgs e)
        {
            bool isDecimal = Decimal.TryParse(txtbxValue.Text, out CouponValue);
            if (isDecimal == false)
            {
                // errorProvider1.SetError(txtbxValue, "Invalid entry.");
                isDecimalValueValid = false;
            }
            else
            {
                isDecimalValueValid = true;
            }
            checkAllValidationOfEntry();
        }

        private void cmbxHourStart_ValueMemberChanged(object sender, EventArgs e)
        {

        }

        private void cmbxHourStart_TextChanged(object sender, EventArgs e)
        {
            if (cmbxHourStart.SelectedIndex != -1 && cmbxMinStart.SelectedIndex != -1 && cmbxAMPMStart.SelectedIndex != -1 && cmbxHourEnd.SelectedIndex != -1 && cmbxMinEnd.SelectedIndex != -1 && cmbxAMPMEnd.SelectedIndex != -1)
            {
                validateStartDateNEndDate();
                checkAllValidationOfEntry();
            }
        }

        /// <summary>
        /// Actions that occur when the coupon type changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbxCouponType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbxValue.Text = String.Empty;
            if (SelectedCouponType == PlayerComp.CouponTypes.AltPricePackage)
            {
                lblValue.Text = DISCOUNT_TYPE_ALT_PACKAGE;
                txtbxValue.Visible = false;
                cmbxCouponPackage.Visible = true;
                lblPerValue.Visible = false;
            }
            else if (SelectedCouponType == PlayerComp.CouponTypes.PercentPackage)
            {
                lblValue.Text = DISCOUNT_TYPE_ALT_PACKAGE;
                txtbxValue.Visible = true;
                cmbxCouponPackage.Visible = true;
                lblPerValue.Visible = true;
                txtbxValue.Location = new Point(txtbxValue.Location.X, lblPerValue.Location.Y);
            }
            else //if (SelectedCouponType == CouponItem.CouponTypes.FixedValue) // default value
            {
                lblValue.Text = DISCOUNT_TYPE_VALUE;
                txtbxValue.Visible = true;
                cmbxCouponPackage.Visible = false;
                lblPerValue.Visible = false;
                txtbxValue.Location = new Point(txtbxValue.Location.X, lblValue.Location.Y);
            }
            checkAllValidationOfEntry();
        }

        /// <summary>
        /// Actions that occur when the selected coupon package changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbxCouponPackage_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkAllValidationOfEntry();
        }

        /// <summary>
        /// Actions that occur when the help label is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpLbl_Click(object sender, EventArgs e)
        {
            Point helpLocation = lblHelpDisplay.PointToScreen(Point.Empty); // for some reason, it ignores the 'parent' parameter and lays it out on the screen's coordinates
            helpLocation.Y += lblHelpDisplay.Height; // have it display under the control

            Help.ShowPopup(this, Resources.CouponTypeHelpText, helpLocation);
        }

        /// <summary>
        /// Actions that occur when the grouped radio button's status changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radiobtnAwardType_CheckedChanged(object sender, EventArgs e)
        {
            if (radiobtnAwardType.Checked)
            {
                if (!panel1.Visible && this.Visible) // this.Visible means this is loaded
                {
                    panel1.Visible = true;
                    this.Height += panel1.Height;
                }
            }
            else
            {
                if (panel1.Visible)
                {
                    panel1.Visible = false;
                    this.Height -= panel1.Height;
                }
            }
            checkAllValidationOfEntry();
        }

        //US4852
        /// <summary>
        /// Handles the TextChanged event of the excludeFromQualifyCheckedComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void excludeFromQualifyCheckedComboBox_TextChanged(object sender, EventArgs e)
        {
            IsRestrictionsModified = true;
        }
        #endregion
        
        #region VALIDATING EVENTS
        /// <summary>
        /// Check if the coupon name is not null.
        /// Check if the coupon already exists.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbxCouponName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtbxCouponName.Text))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Checks to see if the entered values are valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbxCouponPackage_TextChanged(object sender, EventArgs e)
        {   // "validating" is not called by the checked list box, use this event instead.
            checkAllValidationOfEntry();
        }
        
        /// <summary>
        /// Check if the value is in decimal format.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbxValue_Validating(object sender, CancelEventArgs e)
        {
            TextBox txtBx = (TextBox)sender;
            bool isDecimal = Decimal.TryParse(txtBx.Text, out CouponValue);
            if (isDecimal == false)
            {
                errorProvider1.SetError(txtBx, "Invalid entry.");
                e.Cancel = true;
            }
        }

        private void txtbxValue_Leave(object sender, EventArgs e)
        {
            TextBox txtBx = (TextBox)sender;
            bool isDecimal = Decimal.TryParse(txtBx.Text, out CouponValue);
            if (isDecimal)
                txtBx.Text = Helper.DecimalStringToMoneyString(txtBx.Text);
        }

        /// <summary>
        /// Check if the value is in decimal format.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbxMinSpendValue_Validating(object sender, CancelEventArgs e)
        {
            decimal minSpendToQualify;
            TextBox txtBx = (TextBox)sender;
            bool isDecimal = Decimal.TryParse(txtBx.Text, out minSpendToQualify);
            if (isDecimal == false)
            {
                errorProvider1.SetError(txtBx, "Invalid entry.");
                e.Cancel = true;
            }
        }

        private void txtbxMinSpendValue_Leave(object sender, EventArgs e)
        {
            decimal minSpendToQualify;
            TextBox txtBx = (TextBox)sender;
            bool isDecimal = Decimal.TryParse(txtBx.Text, out minSpendToQualify);
            if (isDecimal)
                txtBx.Text = Helper.DecimalStringToMoneyString(txtBx.Text);
        }
        private void dtepickerCouponStartDate_Validating(object sender, CancelEventArgs e)//delete after project completion
        {
            validateStartDateNEndDate();
            checkAllValidationOfEntry();
        }
        #endregion
    }
}