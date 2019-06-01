// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2016 Fortunet

// US4852: Product Center > Coupons: Require spend
// US5155: ability to view awarded coupons
// US5417: Advanced coupon limits

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
        private const int NO_LIMIT = 0;

        #endregion

        #region Private Members

        private int m_operatorId;
        private DateTime couponStartDate;
        private DateTime couponEndDate;
        private bool isCouponNameValid = false;
        private bool isStartDateNEndDateValid = false;
        private int CouponID = 0;
        GTIListView lvfromCoupon = new GTIListView();
        private bool LoadingValue = false;
        private bool IsCouponExpired; //Need to know if the data being modified is expired or not.
        private PlayerComp.AwardTypes m_awardType = PlayerComp.AwardTypes.Manual;
        private List<int> m_restrictedProductIds = new List<int>();
        private List<int> m_restrictedPackageIds = new List<int>();
        private List<int> m_awardedPackageIDs = new List<int>(); // US4941 the packages this coupon earns
        private ProductCenterSettings m_productCenterSettings;
        private bool m_enabled = true; // Whether or not the UI is enabled. Note: don't set this directly, use SetUIEnable()

        private System.Data.DataRow programLimitR;
        private System.Data.DataRow dailyLimitR;
        private System.Data.DataRow weeklyLimitR;
        private System.Data.DataRow monthlyLimitR;
        private System.Data.DataRow yearlyLimitR;
        private System.Data.DataTable programLimitsDT;
        private System.Data.DataTable dayOfWeekLimitsDT;
        private System.Data.DataTable monthOfYearLimitsDT;
        private System.Data.DataTable sessionLimitsDT;
        private PlayerComp m_referenceCoupon;

        #endregion

        #region Public Properties

        public bool Modified
        {
            get
            {
                if(m_referenceCoupon == null)
                    return true;
                else
                {
                    if(dCouponID != m_referenceCoupon.Id
                        || dCouponName != m_referenceCoupon.Name
                        || dStartDate != m_referenceCoupon.StartDate
                        || dEndDate != m_referenceCoupon.EndDate
                        || CouponMaxUsage != m_referenceCoupon.CouponMaxUsage
                        || dIsCouponExpired != m_referenceCoupon.IsExpired
                        || SelectedCouponType != m_referenceCoupon.CouponType
                        || AwardType != m_referenceCoupon.AwardType
                        || UnlockSpend != m_referenceCoupon.UnlockSpend
                        || UnlockSessionCount != m_referenceCoupon.UnlockSessionCount
                        || WindowAwardDaysBefore != m_referenceCoupon.WindowAwardDaysBefore
                        || WindowAwardDaysFollowing != m_referenceCoupon.WindowAwardDaysFollowing
                        || MinimumSpendToQualify != m_referenceCoupon.MinimumSpendToQualify
                        || IgnoreValidationsForIgnoredPackages != m_referenceCoupon.IgnoreValidationsForIgnoredPackages
                        || ProgramLimit != m_referenceCoupon.ProgramLimit
                        || DailyLimit != m_referenceCoupon.DailyLimit
                        || WeeklyLimit != m_referenceCoupon.WeeklyLimit
                        || MonthlyLimit != m_referenceCoupon.MonthlyLimit
                        || YearlyLimit != m_referenceCoupon.YearlyLimit
                        || dValue != m_referenceCoupon.Value
                        )
                        return true;

                    if((m_referenceCoupon.EarnedPackageIDs == null ? 0 : m_referenceCoupon.EarnedPackageIDs.Count) != AwardedPackageIds.Count)
                        return true;

                    if(AwardedPackageIds.Count > 0)
                    {
                        var a = m_referenceCoupon.EarnedPackageIDs.Except(AwardedPackageIds);
                        if(a.Count() != 0)
                            return true;
                    }

                    if((m_referenceCoupon.RestrictedProductIds == null ? 0 : m_referenceCoupon.RestrictedProductIds.Count) != RestrictedProductIds.Count)
                        return true;

                    if(RestrictedProductIds.Count > 0)
                    {
                        var a = m_referenceCoupon.RestrictedProductIds.Except(RestrictedProductIds);
                        if(a.Count() != 0)
                            return true;
                    }

                    if((m_referenceCoupon.RestrictedPackageIds == null ? 0 : m_referenceCoupon.RestrictedPackageIds.Count) != RestrictedPackageIds.Count)
                        return true;

                    if(RestrictedPackageIds.Count > 0)
                    {
                        var a = m_referenceCoupon.RestrictedPackageIds.Except(RestrictedPackageIds);
                        if(a.Count() != 0)
                            return true;
                    }

                    for(int i = 0; i < 7; ++i)
                    {
                        var dow = (DayOfWeek)i;
                        var r = dayOfWeekLimitsDT.Rows.Find(dow);
                        if(UIUseLimitToString(r[1]) != NIntToString(m_referenceCoupon.DayOfWeekLimits[dow]))
                            return true;
                    }

                    for(int monthNum = 1; monthNum <= 12; ++monthNum)
                    {
                        var r = monthOfYearLimitsDT.Rows.Find(monthNum);
                        if(UIUseLimitToString(r[1]) != NIntToString(m_referenceCoupon.MonthOfYearLimits[monthNum]))
                            return true;
                    }

                    #region Check Program Limits
                    //Have existing limits been changed or removed?
                    foreach(var pl in m_referenceCoupon.ProgramLimits)
                    {
                        var progId = pl.Key;
                        var limit = pl.Value;
                        var r = programLimitsDT.Rows.Find(progId);
                        if(r == null || UIUseLimitToString(r[1]) != NIntToString(limit))
                            return true;
                    }

                    //Have new limits been added?
                    List<int> pendingProgIds = new List<int>();
                    var refProgIds = from pl in m_referenceCoupon.ProgramLimits
                                     select pl.Key;

                    foreach(System.Data.DataRow r in programLimitsDT.Rows)
                    {
                        var progId = (int)r[2];
                        pendingProgIds.Add(progId);
                    }
                    if(pendingProgIds.Except(refProgIds).Count() != 0)
                        return true;
                    #endregion

                    #region Check session number limits
                    //Have existing limits been changed or removed?
                    foreach(var l in m_referenceCoupon.SessionNumberLimits)
                    {
                        var sNum = l.Key;
                        var limit = l.Value;
                        var r = programLimitsDT.Rows.Find(sNum);
                        if(r == null || UIUseLimitToString(r[1]) != NIntToString(limit))
                            return true;
                    }

                    //Have new limits been added?
                    List<int> pendingSessionNums = new List<int>();
                    var refSessionNums = from l in m_referenceCoupon.SessionNumberLimits
                                         select l.Key;

                    foreach(System.Data.DataRow r in sessionLimitsDT.Rows)
                    {
                        var sessionNum = (int)r[2];
                        pendingSessionNums.Add(sessionNum);
                    }
                    if(pendingSessionNums.Except(refSessionNums).Count() != 0)
                        return true;
                    #endregion

                    return false;

                }
            }
        }

        public bool dIsCouponExpired
        {
            get { return IsCouponExpired; }
            set { IsCouponExpired = value; }
        }

        public int? CouponMaxUsage
        {
            get
            {
                int? retVal = 0;
                int parseVal = 0;
                if(String.IsNullOrWhiteSpace(maxUsageTxtNm.Text))
                    retVal = null;
                else if(Int32.TryParse(maxUsageTxtNm.Text, out parseVal) && parseVal >= 0)
                    retVal = parseVal;
                else
                    retVal = 1;
                return retVal;
            }
            private set
            {
                maxUsageTxtNm.Text = value.HasValue ? value.ToString() : String.Empty;
            }
        }

        public bool dIsNew { get { return dCouponID == 0; } }

        public int dCouponID
        {
            get { return CouponID; }
            private set { CouponID = value; }
        }

        public string dCouponName
        {
            get { return couponNameTxt.Text; }
            private set { couponNameTxt.Text = value; }
        }

        public DateTime dStartDate
        {
            get { return couponStartDate; }
            private set { couponStartDate = value; }
        }

        public DateTime dEndDate
        {
            get { return couponEndDate; }
            private set { couponEndDate = value; }
        }

        public decimal dValue
        {
            get
            {
                decimal retVal;
                decimal.TryParse(txtbxValue.Text, out retVal);
                return retVal;
            }
            private set
            {
                txtbxValue.Text = Helper.DecimalStringToMoneyString(value);
            }
        }

        public PlayerComp.CouponTypes SelectedCouponType
        {
            get
            {
                return (PlayerComp.CouponTypes)(couponTypeCombo.SelectedIndex + 1); // enum value is 1-based
            }
            private set
            {
                if(value >= 0 && Enum.IsDefined(typeof(PlayerComp.CouponTypes), value))
                    couponTypeCombo.SelectedIndex = (int)(value - 1);
                else
                    couponTypeCombo.SelectedIndex = ((int)PlayerComp.CouponTypes.FixedValue) - 1; // default value. enum value is 1-based 'default(PlayerComp.CouponTypes)' would return zero
            }
        }

        public PlayerComp.AwardTypes AwardType
        {
            get
            {
                return (PlayerComp.AwardTypes)Enum.Parse(typeof(PlayerComp.AwardTypes), (awardTypeCombo.SelectedItem as string).Replace(' ', '_'));
            }
            private set
            {
                m_awardType = value;
                awardTypeCombo.SelectedItem = Enum.GetName(typeof(PlayerComp.AwardTypes), value).Replace('_', ' ');
            }
        }

        public decimal UnlockSpend
        {
            get
            {
                decimal spend = 0;
                Decimal.TryParse(awardTypeAutoUnlockSpendTxt.Text, out spend);
                return spend;
            }
            private set
            {
                awardTypeAutoUnlockSpendTxt.Text = Helper.DecimalStringToMoneyString(value.ToString());
            }
        }

        public int UnlockSessionCount
        {
            get
            {
                int sessCount = 0;
                Int32.TryParse(awardTypeAutoUnlockSessionsTxt.Text, out sessCount);
                return sessCount;
            }
            private set
            {
                awardTypeAutoUnlockSessionsTxt.Text = value.ToString();
            }
        }

        public int? WindowAwardDaysBefore
        {
            get
            {
                int p;
                if(Int32.TryParse(this.awardTypeWindowTypeDaysBeforeTxtNm.Text, out p))
                    return p;
                else
                    return null;
            }
            set
            {
                if(value == null)
                    this.awardTypeWindowTypeDaysBeforeTxtNm.Text = "";
                else
                    this.awardTypeWindowTypeDaysBeforeTxtNm.Text = value.Value.ToString();
            }
        }

        public int? WindowAwardDaysFollowing
        {
            get
            {
                int p;
                if(Int32.TryParse(this.awardTypeWindowTypeDaysFollowingTxtNm.Text, out p))
                    return p;
                else
                    return null;
            }
            set
            {
                if(value == null)
                    this.awardTypeWindowTypeDaysFollowingTxtNm.Text = "";
                else
                    this.awardTypeWindowTypeDaysFollowingTxtNm.Text = value.Value.ToString();
            }
        }

        public decimal MinimumSpendToQualify
        {
            get
            {
                decimal minToQualify = 0;
                decimal.TryParse(minSpendToQualifyTxtNm.Text, out minToQualify);
                return minToQualify;
            }
            private set
            {
                if(value < 0)
                    value = 0;
                minSpendToQualifyTxtNm.Text = Helper.DecimalStringToMoneyString(value.ToString(CultureInfo.InvariantCulture));
            }
        }

        public List<int> RestrictedProductIds
        {
            get
            {
                m_restrictedProductIds.Clear();

                foreach(ProductItemList item in productExclusionsLst.Items)
                {
                    m_restrictedProductIds.Add(item.ProductItemId);
                }

                return m_restrictedProductIds;
            }
            private set
            {
                m_restrictedProductIds = value;
            }
        }

        public List<int> RestrictedPackageIds
        {
            get
            {
                m_restrictedPackageIds.Clear();

                foreach(PackageItem item in packageExclusionsLst.Items)
                {
                    m_restrictedPackageIds.Add(item.PackageId);
                }

                return m_restrictedPackageIds;
            }
            private set
            {
                m_restrictedPackageIds = value;
            }
        }

        public List<int> AwardedPackageIds
        {
            get
            {
                m_awardedPackageIDs.Clear();

                foreach(CcBoxItem checkedItem in cmbxCouponPackage.CheckedItems)
                {
                    m_awardedPackageIDs.Add(checkedItem.Value);
                }

                return m_awardedPackageIDs;
            }
            private set
            {
                m_awardedPackageIDs = value;
            }
        }

        public bool IgnoreValidationsForIgnoredPackages
        {
            get { return ignoreValChkBx.Checked; }
            set { ignoreValChkBx.Checked = value; }
        }

        #region Limits

        public int? ProgramLimit
        {
            get { return UIUseLimitToNInt(programLimitR[1]); }
            private set { programLimitR[1] = value; }
        }
        public int? DailyLimit
        {
            get { return UIUseLimitToNInt(dailyLimitR[1]); }
            private set { dailyLimitR[1] = value; }
        }
        public int? WeeklyLimit
        {
            get { return UIUseLimitToNInt(weeklyLimitR[1]); }
            private set { weeklyLimitR[1] = value; }
        }
        public int? MonthlyLimit
        {
            get { return UIUseLimitToNInt(monthlyLimitR[1]); }
            private set { monthlyLimitR[1] = value; }
        }
        public int? YearlyLimit
        {
            get { return UIUseLimitToNInt(yearlyLimitR[1]); }
            private set { yearlyLimitR[1] = value; }
        }

        #endregion

        #endregion

        #region Constructor and Loading

        /// <summary>
        /// The default constructor for the Coupon Management form
        /// </summary>
        public CouponMangementAddForm(int operatorId, ProductCenterSettings settings)
        {
            m_productCenterSettings = settings;

            InitializeComponent();

            var atNames = from n in Enum.GetNames(typeof(PlayerComp.AwardTypes))
                          select n.Replace('_', ' ');
            awardTypeCombo.Items.AddRange(atNames.OrderBy((n) => n).ToArray());

            if(!m_productCenterSettings.EnableValidation)
                ignoreValChkBx.Visible = false;

            m_operatorId = operatorId;

            AcceptButton = acceptBtn; //btnAccept; //ENTER key.
            CancelButton = cancelBtn; //btnCancel; ESC Key

            List<PlayerComp.CouponTypes> types = new List<PlayerComp.CouponTypes>(Enum.GetValues(typeof(PlayerComp.CouponTypes)).Cast<PlayerComp.CouponTypes>());
            types.Sort(); // they usually return in-order, but technically it's not guaranteed

            foreach(PlayerComp.CouponTypes value in types)
            {
                couponTypeCombo.Items.Add(EnumToString.GetDescription(value));
            }
            couponTypeCombo.SelectedIndex = 0;

            CouponMaxUsage = 1;
            LoadCouponLimitControls();
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
                if(packages != null)
                {
                    packages.Sort((x, y) => String.Compare(x.PackageName, y.PackageName, StringComparison.Ordinal));
                    foreach(var package in packages)
                    {
                        var item = new CcBoxItem(package.PackageName, package.PackageId, package);

                        cmbxCouponPackage.Items.Add(item);
                    }
                    cmbxCouponPackage.DisplayMember = "Name";
                }
            }
            catch(Exception ex)
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

                if(products != null)
                {
                    var temp = new List<int>(m_restrictedProductIds); // use a temp list so that it doesn't change out from under us. Might be getting triggers to remake the list
                    products.Sort((x, y) => String.Compare(x.ProductItemName, y.ProductItemName, StringComparison.Ordinal));

                    foreach(var product in products)
                    {
                        var item = new CcBoxItem(product.ProductItemName, product.ProductItemId, product);
                        if(temp.Any(x => x == product.ProductItemId))
                        {
                            productExclusionsLst.Items.Add(product);
                            temp.Remove(product.ProductItemId); // shorten the list to make lookups faster
                        }
                        else
                        {
                            productExclusionCandidatesLst.Items.Add(product);
                        }
                    }

                    productExclusionsLst.DisplayMember = productExclusionCandidatesLst.DisplayMember = "ProductItemName";
                }
            }
            catch(Exception ex)
            {
                string err = "Error loading available products for coupons: " + ex.ToString();
                Logger.LogSevere(err, "CouponMangementAddForm.cs", 0);
                MessageBox.Show(err);
            }

            try
            {
                //US4932
                if(packages != null)
                {
                    var temp = new List<int>(m_restrictedPackageIds); // use a temp list so that it doesn't change out from under us. Might be getting triggers to remake the list

                    foreach(var package in packages)
                    {
                        var item = new CcBoxItem(package.PackageName, package.PackageId, package);
                        if(temp.Any(x => x == package.PackageId))
                        {
                            packageExclusionsLst.Items.Add(package);
                            temp.Remove(package.PackageId); // shorten the list to make lookups faster
                        }
                        else
                        {
                            packageExclusionCandidatesLst.Items.Add(package);
                        }
                    }

                    packageExclusionsLst.DisplayMember = packageExclusionCandidatesLst.DisplayMember = "PackageName";
                }
            }
            catch(Exception ex)
            {
                string err = "Error loading available packages for coupon: " + ex.ToString();
                Logger.LogSevere(err, "CouponMangementAddForm.cs", 0);
                MessageBox.Show(err);
            }

            ClearErrorMessage();
            if(dIsNew == true) // add new coupon
            {
                if(acceptBtn.Enabled != false) { acceptBtn.Enabled = false; }

                //dtepickerCouponStartDate = new DateTimePicker();
                beginWhenDateDTP.Format = DateTimePickerFormat.Custom;
                beginWhenDateDTP.CustomFormat = DATE_FORMAT;
                endWhenDateDTP.Format = DateTimePickerFormat.Custom;

                PopulateTimeComboBox();
                PopulateStartTime(DateTime.Now, null); //Populate the combo box for start time and end time.

                couponNameTxt.Select();//Select coupon name as default.
                couponNameTxt.Focus();
            }
            else //Edit coupon
            {
                //Do not validate untill loading is complete.
                LoadingValue = true;

                //Populate data to its section.
                beginWhenDateDTP.Value = couponStartDate;
                beginWhenDateDTP.Format = DateTimePickerFormat.Custom;
                beginWhenDateDTP.CustomFormat = "MM/dd/yyyy";

                endWhenDateDTP.Value = couponEndDate;
                endWhenDateDTP.Format = DateTimePickerFormat.Custom;
                endWhenDateDTP.CustomFormat = "MM/dd/yyyy";

                PopulateTimeComboBox();
                PopulateStartTime(couponStartDate, couponEndDate);

                couponNameTxt.Select();//Select coupon name as default.
                couponNameTxt.Focus();

                isCouponNameValid = true; //Set to true since they are previous coupon that has been accepted.
                isStartDateNEndDateValid = true;

                LoadingValue = false;
                //Enable the accept button since its not expired and data are valid.
                if(IsCouponExpired == false && m_enabled)
                {
                    acceptBtn.Enabled = true;
                }

                try
                {
                    //US4941
                    //update checked items in drop down
                    var temp = new List<int>(m_awardedPackageIDs); // use a temp list so that it doesn't change out from under us. Might be getting triggers to remake the list
                    foreach(var id in temp)
                    {
                        for(var i = 0; i < cmbxCouponPackage.Items.Count; i++)
                        {
                            CcBoxItem item = cmbxCouponPackage.Items[i] as CcBoxItem;

                            if(item != null && item.Value == id)
                            {
                                cmbxCouponPackage.SetItemChecked(i, true);
                                break; // found the match. Break out early
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    string err = "Error loading coupon's awarded packages: " + ex.ToString();
                    Logger.LogSevere(err, "CouponMangementAddForm.cs", 0);
                    MessageBox.Show(err);
                }
            }

            AwardType = m_awardType;

            this.ResumeLayout();

            awardTypeCombo_SelectedIndexChanged(awardTypeCombo, null);

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
                foreach(Control control in this.Controls)
                {
                    control.Enabled = enable; // if the controls inside ContentControls don't properly disable, we might have to manually disable them as well. Leaving this as-is for now
                }

                foreach(Control control in basicTP.Controls)
                {
                    control.Enabled = enable;
                }

                foreach(Control control in usageLimitsTP.Controls)
                {
                    control.Enabled = enable;
                }

                foreach(Control control in qualifyingSpendTP.Controls)
                {
                    control.Enabled = enable;
                }

                m_enabled = enable;
            }
            catch(Exception ex)
            {
                MessageForm.Show("Error setting UI enabled status" + ex.ToString());
            }
            finally
            {
                mainTC.Enabled = true;
                cancelBtn.Enabled = true; // still need a way to return
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
            coupon.CouponMaxUsage = CouponMaxUsage;


            coupon.CouponType = SelectedCouponType;
            coupon.AwardType = AwardType;
            coupon.UnlockSpend = UnlockSpend;
            coupon.UnlockSessionCount = UnlockSessionCount;
            coupon.WindowAwardDaysBefore = WindowAwardDaysBefore;
            coupon.WindowAwardDaysFollowing = WindowAwardDaysFollowing;
            coupon.MinimumSpendToQualify = MinimumSpendToQualify;//US4852
            coupon.RestrictedProductIds = RestrictedProductIds;//US4852
            coupon.EarnedPackageIDs = AwardedPackageIds; // US4941
            coupon.RestrictedPackageIds = RestrictedPackageIds; // US4932
            coupon.IgnoreValidationsForIgnoredPackages = ignoreValChkBx.Checked; // US4932

            coupon.ProgramLimit = ProgramLimit;
            coupon.DailyLimit = DailyLimit;
            coupon.WeeklyLimit = WeeklyLimit;
            coupon.MonthlyLimit = MonthlyLimit;
            coupon.YearlyLimit = YearlyLimit;

            try
            {

                foreach(System.Data.DataRow r in dayOfWeekLimitsDT.Rows)
                    coupon.DayOfWeekLimits.Add((DayOfWeek)r[2], UIUseLimitToNInt(r[1]));

                foreach(System.Data.DataRow r in monthOfYearLimitsDT.Rows)
                    coupon.MonthOfYearLimits.Add((byte)r[2], UIUseLimitToNInt(r[1]));

                foreach(System.Data.DataRow r in programLimitsDT.Rows)
                    coupon.ProgramLimits.Add((int)r[2], UIUseLimitToNInt(r[1]));

                foreach(System.Data.DataRow r in sessionLimitsDT.Rows)
                    coupon.SessionNumberLimits.Add((int)r[2], UIUseLimitToNInt(r[1]));

            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                ;
            }

            return coupon;
        }

        /// <summary>
        /// Sets the UI info to the sent in coupon information
        /// </summary>
        /// <param name="coupon"></param>
        public void SetCouponData(PlayerComp coupon)
        {
            m_referenceCoupon = coupon;
            LoadReferenceCoupon();
        }

        private void LoadReferenceCoupon()
        {
            dCouponID = m_referenceCoupon.Id;
            dCouponName = m_referenceCoupon.Name;
            dStartDate = m_referenceCoupon.StartDate;
            dEndDate = m_referenceCoupon.EndDate;
            CouponMaxUsage = m_referenceCoupon.CouponMaxUsage;
            dIsCouponExpired = m_referenceCoupon.IsExpired;
            SelectedCouponType = m_referenceCoupon.CouponType;

            if(m_referenceCoupon.EarnedPackageIDs != null)
                AwardedPackageIds = new List<int>(m_referenceCoupon.EarnedPackageIDs); // US4941
            else
                AwardedPackageIds = new List<int>();
            AwardType = m_referenceCoupon.AwardType;
            UnlockSpend = m_referenceCoupon.UnlockSpend;
            UnlockSessionCount = m_referenceCoupon.UnlockSessionCount;
            WindowAwardDaysBefore = m_referenceCoupon.WindowAwardDaysBefore;
            WindowAwardDaysFollowing = m_referenceCoupon.WindowAwardDaysFollowing;
            MinimumSpendToQualify = m_referenceCoupon.MinimumSpendToQualify;//US4852
            if(m_referenceCoupon.RestrictedProductIds != null)
                RestrictedProductIds = m_referenceCoupon.RestrictedProductIds;//US4852
            else
                RestrictedProductIds = new List<int>();
            if(m_referenceCoupon.RestrictedPackageIds != null)
                RestrictedPackageIds = m_referenceCoupon.RestrictedPackageIds; // US4932
            else
                RestrictedPackageIds = new List<int>();
            IgnoreValidationsForIgnoredPackages = m_referenceCoupon.IgnoreValidationsForIgnoredPackages; // US4932

            if(m_referenceCoupon.LastAwardedDate != null)
            {
                SetUIEnable(false);

                lblUnableToEditCoupon.Text = "Cannot edit the coupon because it has been awarded.";
                lblUnableToEditCoupon.Visible = true;
            }
            dValue = m_referenceCoupon.Value;

            ProgramLimit = m_referenceCoupon.ProgramLimit;
            DailyLimit = m_referenceCoupon.DailyLimit;
            WeeklyLimit = m_referenceCoupon.WeeklyLimit;
            MonthlyLimit = m_referenceCoupon.MonthlyLimit;
            YearlyLimit = m_referenceCoupon.YearlyLimit;

            for(int i = 0; i < 7; ++i)
            {
                var dow = (DayOfWeek)i;
                var r = dayOfWeekLimitsDT.Rows.Find(dow);
                r[1] = NIntToString(m_referenceCoupon.DayOfWeekLimits[dow]);
            }

            for(int monthNum = 1; monthNum <= 12; ++monthNum)
            {
                var r = monthOfYearLimitsDT.Rows.Find(monthNum);
                r[1] = NIntToString(m_referenceCoupon.MonthOfYearLimits[monthNum]);
            }

            foreach(var pl in m_referenceCoupon.ProgramLimits)
            {
                var progId = pl.Key;
                var limit = pl.Value;
                var r = programLimitsDT.Rows.Find(progId);
                if(r != null)
                    r[1] = NIntToString(limit);
            }

            foreach(var sl in m_referenceCoupon.SessionNumberLimits)
            {
                var sessionNum = sl.Key;
                var limit = sl.Value;
                var r = sessionLimitsDT.Rows.Find(sessionNum);
                if(r != null)
                    r[1] = NIntToString(limit);
                else
                    sessionLimitsDT.Rows.Add(sessionNum, NIntToString(limit));
            }
        }

        /// <summary>
        /// Check if all entry are valid.
        /// If true then enable accept button else disable.
        /// </summary>
        private void CheckAllValidationOfEntry()
        {
            if(!m_enabled) // everything has been forced to disabled, don't do validation
                return;

            if(isCouponNameValid
                && isStartDateNEndDateValid
                && ((SelectedCouponType == PlayerComp.CouponTypes.AltPricePackage && AwardedPackageIds.Count > 0)
                    || (SelectedCouponType == PlayerComp.CouponTypes.FixedValue) //&& dValue != 0 // zero is valid. They can't enter anything invalid now, so don't check?
                    || (SelectedCouponType == PlayerComp.CouponTypes.PercentPackage && AwardedPackageIds.Count > 0)
                // && has min spend
                    )
                && (AwardType == PlayerComp.AwardTypes.Manual
                    || AwardType == PlayerComp.AwardTypes.Instant
                    || (AwardType == PlayerComp.AwardTypes.Auto && UnlockSpend >= 0 && UnlockSessionCount != 0)
                    || AwardType == PlayerComp.AwardTypes.Birth_Day
                    || AwardType == PlayerComp.AwardTypes.Birth_Week
                    || AwardType == PlayerComp.AwardTypes.Birth_Month
                    || (AwardType == PlayerComp.AwardTypes.Birth_Window && WindowAwardDaysBefore.HasValue && WindowAwardDaysFollowing.HasValue)
                    )
                )
            {
                acceptBtn.Enabled = true;
            }
            else
            {
                acceptBtn.Enabled = false;
            }
        }

        /// <summary>
        /// Clear any warning or error message.
        /// </summary>
        private void ClearErrorMessage()
        {
            formEP.Clear();
            if(saveSuccessLbl.Visible != false) { saveSuccessLbl.Visible = false; }
        }

        /// <summary> 
        /// Populate the Time(hhmmtt) for start time and end time.
        /// </summary>
        private void PopulateStartTime(DateTime? dtStart, DateTime? dtEnd)
        {
            int tempIndex;

            if(dtEnd.HasValue)// US4657 do the end time
            {
                string EHourNow = dtEnd.Value.ToString("hh");
                string EMinNow = dtEnd.Value.ToString("mm");
                string EAMPMNow = dtEnd.Value.ToString("tt");

                // Hour
                tempIndex = beginWhenHourCombo.Items.IndexOf(EHourNow);
                endWhenHourCombo.SelectedIndex = tempIndex;

                //Min
                tempIndex = beginWhenMinuteCombo.Items.IndexOf(EMinNow);
                endWhenMinuteCombo.SelectedIndex = tempIndex;

                //AMPM
                tempIndex = beginWhenAMPMCombo.Items.IndexOf(EAMPMNow);
                endWhenAMPMCombo.SelectedIndex = tempIndex;
            }
            else
            {
                //dtepickerCouponEndDate.CustomFormat = "SELECT A DATE"; // displays this instead of the blank date. Can't use these: M,d,y,h,s,t,z
                endWhenDateDTP.CustomFormat = " "; // clears it out so it doens't display anything.
            }
            if(dtStart.HasValue)
            {
                string HourNow = dtStart.Value.ToString("hh");
                string MinNow = dtStart.Value.ToString("mm");
                string AMPMNow = dtStart.Value.ToString("tt");

                // Hour
                tempIndex = beginWhenHourCombo.Items.IndexOf(HourNow);
                beginWhenHourCombo.SelectedIndex = tempIndex;

                //Min
                tempIndex = beginWhenMinuteCombo.Items.IndexOf(MinNow);
                beginWhenMinuteCombo.SelectedIndex = tempIndex;

                //AMPM
                tempIndex = beginWhenAMPMCombo.Items.IndexOf(AMPMNow);
                beginWhenAMPMCombo.SelectedIndex = tempIndex;
            }
        }

        private void PopulateTimeComboBox()
        {
            var hours = Enumerable.Range(1, 12).Select(i => i.ToString("d2"));// ("D2"));
            var minutes = Enumerable.Range(00, 60).Select(i => i.ToString("d2"));
            foreach(var h in hours)
            {
                beginWhenHourCombo.Items.Add(h);
                endWhenHourCombo.Items.Add(h);
            }

            foreach(var m in minutes)
            {
                beginWhenMinuteCombo.Items.Add(m);
                endWhenMinuteCombo.Items.Add(m);
                //   Console.WriteLine(m);
            }

            beginWhenAMPMCombo.Items.Add("AM");//0 
            beginWhenAMPMCombo.Items.Add("PM");//1
            endWhenAMPMCombo.Items.Add("AM");//0    
            endWhenAMPMCombo.Items.Add("PM");//1
        }

        public static int? UIUseLimitToNInt(object uiVal)
        {
            string stringVal = uiVal == DBNull.Value ? null : (string)uiVal;
            int parseVal;
            if(String.IsNullOrWhiteSpace(stringVal) || !int.TryParse(stringVal, out parseVal))
                return null;
            else
                return parseVal;
        }

        public static string NIntToString(int? nintVal) { return nintVal.HasValue ? nintVal.Value.ToString() : String.Empty; }
        public static string UIUseLimitToString(object uiVal) { return NIntToString(UIUseLimitToNInt(uiVal)); }

        private void LoadCouponLimitControls()
        {
            var dcs = new DataGridViewCellStyle()
            {
                SelectionBackColor = SystemColors.Window,
                SelectionForeColor = SystemColors.WindowText
            };

            System.Data.DataColumn currentCol;

            var limitPerText = "Limit per";
            //limitPerText = "Max Usage";

            #region General Limits
            var generalLimitsDT = new System.Data.DataTable();
            generalLimitsDT.Columns.Add("General", typeof(String)).ReadOnly = true;
            generalLimitsDT.Columns.Add(limitPerText, typeof(String));
            programLimitR = generalLimitsDT.Rows.Add("Session/Program", "");
            if(!m_productCenterSettings.ActiveSalesSessionEnabled)
                generalLimitsDT.Rows.Remove(programLimitR);
            dailyLimitR = generalLimitsDT.Rows.Add("Daily", "");
            weeklyLimitR = generalLimitsDT.Rows.Add("Weekly", "");
            monthlyLimitR = generalLimitsDT.Rows.Add("Monthly", "");
            yearlyLimitR = generalLimitsDT.Rows.Add("Yearly", "");

            generalLimitsDGV.DataSource = generalLimitsDT;
            generalLimitsDGV.EditingControlShowing += limitsDGV_EditingControlShowing;
            generalLimitsDGV.Columns[0].DefaultCellStyle = dcs;
            generalLimitsDGV.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            generalLimitsDGV.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            #endregion

            #region Specific Program Limits
            programLimitsDT = new System.Data.DataTable();
            programLimitsDT.Columns.Add("Program Name", typeof(String)).ReadOnly = true;
            programLimitsDT.Columns.Add(limitPerText, typeof(String));
            currentCol = programLimitsDT.Columns.Add("ProgramId", typeof(int));
            programLimitsDT.PrimaryKey = new[] { currentCol };

            var progData = GTI.Modules.Shared.Data.GetProgramDataMessage.GetProgramData(0);
            foreach(var pd in progData)
                if(pd.IsActive)
                    programLimitsDT.Rows.Add(pd.Name, "", pd.ProgramId);
            foreach(var pd in progData)
                if(!pd.IsActive)
                    programLimitsDT.Rows.Add("*" + pd.Name, "", pd.ProgramId);

            programLimitsDGV.DataSource = programLimitsDT;

            if(!m_productCenterSettings.ActiveSalesSessionEnabled)
            {
                programLimitsLbl.Visible = false;
                programLimitsDGV.Visible = false;
                programLimitsNoteLbl.Visible = false;
            }
            else
            {
                programLimitsDGV.Columns[0].DefaultCellStyle = dcs;
                programLimitsDGV.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                programLimitsDGV.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                programLimitsDGV.Columns[2].Visible = false;
                programLimitsDGV.EditingControlShowing += limitsDGV_EditingControlShowing;
            }
            #endregion

            #region Day of Week Limits
            dayOfWeekLimitsDT = new System.Data.DataTable();
            dayOfWeekLimitsDT.Columns.Add("Day Of Week", typeof(String)).ReadOnly = true;
            dayOfWeekLimitsDT.Columns.Add(limitPerText, typeof(String));
            currentCol = dayOfWeekLimitsDT.Columns.Add("DayOfWeek", typeof(DayOfWeek));
            dayOfWeekLimitsDT.PrimaryKey = new[] { currentCol };

            dayOfWeekLimitsDT.Rows.Add("Mon", "", DayOfWeek.Monday);
            dayOfWeekLimitsDT.Rows.Add("Tue", "", DayOfWeek.Tuesday);
            dayOfWeekLimitsDT.Rows.Add("Wed", "", DayOfWeek.Wednesday);
            dayOfWeekLimitsDT.Rows.Add("Thu", "", DayOfWeek.Thursday);
            dayOfWeekLimitsDT.Rows.Add("Fri", "", DayOfWeek.Friday);
            dayOfWeekLimitsDT.Rows.Add("Sat", "", DayOfWeek.Saturday);
            dayOfWeekLimitsDT.Rows.Add("Sun", "", DayOfWeek.Sunday);

            dayOfWeekLimitsDGV.DataSource = dayOfWeekLimitsDT;
            dayOfWeekLimitsDGV.Columns[0].DefaultCellStyle = dcs;
            dayOfWeekLimitsDGV.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dayOfWeekLimitsDGV.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dayOfWeekLimitsDGV.Columns[2].Visible = false;
            dayOfWeekLimitsDGV.EditingControlShowing += limitsDGV_EditingControlShowing;
            #endregion

            #region Month of Year Limits
            monthOfYearLimitsDT = new System.Data.DataTable();
            monthOfYearLimitsDT.Columns.Add("Month", typeof(String)).ReadOnly = true;
            monthOfYearLimitsDT.Columns.Add(limitPerText, typeof(String));
            currentCol = monthOfYearLimitsDT.Columns.Add("MonthNum", typeof(byte));
            monthOfYearLimitsDT.PrimaryKey = new[] { currentCol };

            monthOfYearLimitsDT.Rows.Add("Jan", "", 1);
            monthOfYearLimitsDT.Rows.Add("Feb", "", 2);
            monthOfYearLimitsDT.Rows.Add("Mar", "", 3);
            monthOfYearLimitsDT.Rows.Add("Apr", "", 4);
            monthOfYearLimitsDT.Rows.Add("May", "", 5);
            monthOfYearLimitsDT.Rows.Add("Jun", "", 6);
            monthOfYearLimitsDT.Rows.Add("Jul", "", 7);
            monthOfYearLimitsDT.Rows.Add("Aug", "", 8);
            monthOfYearLimitsDT.Rows.Add("Sep", "", 9);
            monthOfYearLimitsDT.Rows.Add("Oct", "", 10);
            monthOfYearLimitsDT.Rows.Add("Nov", "", 11);
            monthOfYearLimitsDT.Rows.Add("Dec", "", 12);

            monthOfYearLimitsDGV.DataSource = monthOfYearLimitsDT;
            monthOfYearLimitsDGV.Columns[0].DefaultCellStyle = dcs;
            monthOfYearLimitsDGV.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            monthOfYearLimitsDGV.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            monthOfYearLimitsDGV.Columns[2].Visible = false;
            monthOfYearLimitsDGV.EditingControlShowing += limitsDGV_EditingControlShowing;

            #endregion

            #region Sesssion Number Limits
            sessionLimitsDT = new System.Data.DataTable();
            sessionLimitsDT.Columns.Add("Session #", typeof(String)).ReadOnly = true;
            sessionLimitsDT.Columns.Add(limitPerText, typeof(String));
            currentCol = sessionLimitsDT.Columns.Add("Session Number", typeof(int));
            sessionLimitsDT.PrimaryKey = new[] { currentCol };

            for(int i = 1; i <= 24; ++i)
                sessionLimitsDT.Rows.Add(String.Format("Session {0}", i), "", i);

            sessionLimitsDGV.DataSource = sessionLimitsDT;

            if(!m_productCenterSettings.ActiveSalesSessionEnabled)
            {
                sessionLimitsLbl.Visible = false;
                sessionLimitsDGV.Visible = false;
            }
            else
            {
                sessionLimitsDGV.Columns[0].DefaultCellStyle = dcs;
                sessionLimitsDGV.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                sessionLimitsDGV.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                sessionLimitsDGV.Columns[2].Visible = false;
                sessionLimitsDGV.EditingControlShowing += limitsDGV_EditingControlShowing;
                sessionLimitsDGV.ScrollBars = ScrollBars.Vertical;
            }
            #endregion
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Accept or saved new entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void acceptBtn_Click(object sender, EventArgs e)
        {
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
            ClearErrorMessage();
        }

        /// <summary>
        /// Cancel changes will go back to Parent window which is the Coupon Management.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Numeric only and 1 decimal point for coupon value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!(sender is TextBox))
                return;
            TextBox txtBxDec = sender as TextBox;

            bool reject = false;

            string x = txtBxDec.Text;
            int count = x.Split('.').Length - 1;

            if(!char.IsControl(e.KeyChar))
            {
                switch(e.KeyChar)
                {
                    case (char)46://period
                        //allow 1 decimal point
                        if(count > 0)
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
            else if(e.KeyChar == (char)Keys.Back)
            {
                reject = false;
            }
            else if(Regex.IsMatch(txtbxValue.Text, @"\.\d\d"))
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
            if(!(sender is TextBox))
                return;
            TextBox txtBxInt = sender as TextBox;

            bool result = true;
            if(e.KeyChar == (char)Keys.Back)
            {
                result = false;
            }

            if(result)//If result = true
            {
                //Lets us not allow the user to input 0 value
                if(txtBxInt.Text.Count() == 0)
                {
                    if(e.KeyChar == (char)Keys.D0)
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
            CheckAllValidationOfEntry();
        }

        /// <summary>
        /// Manual validation for "StartDateTime" and "EndDateTime".
        /// </summary>
        private void validateActiveDateBounds()
        {
            if(LoadingValue == false)
            {
                formEP.Clear();

                if(beginWhenHourCombo.SelectedItem == null || beginWhenMinuteCombo.SelectedItem == null || beginWhenAMPMCombo.SelectedItem == null)
                {
                    isStartDateNEndDateValid = false;
                    return; //exit now if its false   
                }
                string tempStartDate = beginWhenDateDTP.Value.ToString("MM/dd/yyyy") + " " + beginWhenHourCombo.SelectedItem.ToString() + ":" + beginWhenMinuteCombo.SelectedItem.ToString() + " " + beginWhenAMPMCombo.SelectedItem.ToString();
                bool testIsDate = DateTime.TryParse(tempStartDate, out couponStartDate);

                if(endWhenHourCombo.SelectedItem == null || endWhenMinuteCombo.SelectedItem == null || endWhenAMPMCombo.SelectedItem == null)
                {
                    isStartDateNEndDateValid = false;
                    return; //exit now if its false   
                }
                string tempEndDate = endWhenDateDTP.Value.ToString("MM/dd/yyyy") + " " + endWhenHourCombo.SelectedItem.ToString() + ":" + endWhenMinuteCombo.SelectedItem.ToString() + " " + endWhenAMPMCombo.SelectedItem.ToString();
                if(testIsDate) // date values are already messed up, don't bother
                    testIsDate = DateTime.TryParse(tempEndDate, out couponEndDate);

                if(!testIsDate) // shouldn't happen if the values are loaded from the controls
                {
                    isStartDateNEndDateValid = false;
                    formEP.SetError(beginWhenDateDTP, "Start or end times are not valid values.");
                    return; //exit now if its false   
                }

                if(couponStartDate >= couponEndDate)
                {
                    isStartDateNEndDateValid = false;
                    formEP.SetError(endWhenDateDTP, "Value must be greater than start time");
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
        private void whenDateDTP_ValueChanged(object sender, EventArgs e)
        {
            if(sender is DateTimePicker)
            {
                (sender as DateTimePicker).CustomFormat = DATE_FORMAT;
            }

            if(endWhenHourCombo.SelectedIndex == -1) // nothing has been selected, set to end-of-day
            {
                PopulateStartTime(null, DateTime.Now.Date.AddMinutes(-1)); // ignores the date portion. Basically take the start of a day and go back a minute to get to the max of another day.
            }

            validateActiveDateBounds();
            CheckAllValidationOfEntry();
        }

        /// <summary>
        /// Check if the Coupon Name is valid.
        /// Check all entry if valid then enable accept button if valid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbxCouponName_KeyUp(object sender, KeyEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(couponNameTxt.Text))
            {
                formEP.Clear();
                if(isCouponNameValid == false)
                {
                    isCouponNameValid = true;
                }
            }
            else
            {
                isCouponNameValid = false;
                formEP.SetError(couponNameTxt, "Coupon name cannot be empty.");
            }

            CheckAllValidationOfEntry();
        }

        private void whenCombo_TextChanged(object sender, EventArgs e)
        {
            if(beginWhenHourCombo.SelectedIndex != -1 && beginWhenMinuteCombo.SelectedIndex != -1 && beginWhenAMPMCombo.SelectedIndex != -1 && endWhenHourCombo.SelectedIndex != -1 && endWhenMinuteCombo.SelectedIndex != -1 && endWhenAMPMCombo.SelectedIndex != -1)
            {
                validateActiveDateBounds();
                CheckAllValidationOfEntry();
            }
        }

        /// <summary>
        /// Actions that occur when the coupon type changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void couponTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbxValue.Text = String.Empty;
            if(SelectedCouponType == PlayerComp.CouponTypes.AltPricePackage)
            {
                valueLbl.Text = DISCOUNT_TYPE_ALT_PACKAGE;
                txtbxValue.Visible = false;
                cmbxCouponPackage.Visible = true;
                lblPerValue.Visible = false;
            }
            else if(SelectedCouponType == PlayerComp.CouponTypes.PercentPackage)
            {
                valueLbl.Text = DISCOUNT_TYPE_ALT_PACKAGE;
                txtbxValue.Visible = true;
                cmbxCouponPackage.Visible = true;
                lblPerValue.Visible = true;
                txtbxValue.Location = new Point(lblPerValue.Location.X, txtbxValue.Location.Y);
            }
            else //if (SelectedCouponType == CouponItem.CouponTypes.FixedValue) // default value
            {
                valueLbl.Text = DISCOUNT_TYPE_VALUE;
                txtbxValue.Visible = true;
                cmbxCouponPackage.Visible = false;
                lblPerValue.Visible = false;
                txtbxValue.Location = new Point(valueLbl.Location.X, txtbxValue.Location.Y);
            }
            CheckAllValidationOfEntry();
        }

        /// <summary>
        /// Actions that occur when the selected coupon package changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbxCouponPackage_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAllValidationOfEntry();
        }

        /// <summary>
        /// Actions that occur when the help label is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpLbl_Click(object sender, EventArgs e)
        {
            Point helpLocation = helpDisplayLbl.PointToScreen(Point.Empty); // for some reason, it ignores the 'parent' parameter and lays it out on the screen's coordinates
            helpLocation.Y += helpDisplayLbl.Height; // have it display under the control

            Help.ShowPopup(this, Resources.CouponTypeHelpText, helpLocation);
        }

        /// <summary>
        /// Actions that occur when a user presses one of the add/remove qualify spend excluded products buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void availProdBtn_Click(object sender, EventArgs e)
        {
            if(sender == productExclusionsAddAllBtn)
            {
                if(productExclusionCandidatesLst.Items.Count > 0)
                {
                    List<ProductItemList> prods = new List<ProductItemList>(productExclusionCandidatesLst.Items.Cast<ProductItemList>());
                    productExclusionCandidatesLst.Items.Clear();
                    foreach(var prod in prods)
                    {
                        productExclusionsLst.Items.Add(prod);
                    }
                }
            }
            else if(sender == productExclusionsAddSelectedBtn)
            {
                if(productExclusionCandidatesLst.SelectedItems.Count > 0)
                {
                    List<ProductItemList> prods = new List<ProductItemList>(productExclusionCandidatesLst.SelectedItems.Cast<ProductItemList>());
                    foreach(var prod in prods)
                    {
                        productExclusionCandidatesLst.Items.Remove(prod);
                        productExclusionsLst.Items.Add(prod);
                    }
                }
            }
            else if(sender == productExclusionsRemoveSelectedBtn)
            {
                if(productExclusionsLst.SelectedItems.Count > 0)
                {
                    List<ProductItemList> prods = new List<ProductItemList>(productExclusionsLst.SelectedItems.Cast<ProductItemList>());
                    foreach(var prod in prods)
                    {
                        productExclusionsLst.Items.Remove(prod);
                        productExclusionCandidatesLst.Items.Add(prod);
                    }
                }
            }
            else if(sender == productExclusionsClearBtn)
            {
                if(productExclusionsLst.Items.Count > 0)
                {
                    List<ProductItemList> prods = new List<ProductItemList>(productExclusionsLst.Items.Cast<ProductItemList>());
                    productExclusionsLst.Items.Clear();
                    foreach(var prod in prods)
                    {
                        productExclusionCandidatesLst.Items.Add(prod);
                    }
                }
            }

            CheckAllValidationOfEntry();
        }

        /// <summary>
        /// Actions that occur when a user presses one of the add/remove qualify spend excluded packages buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void availPackBtn_Click(object sender, EventArgs e)
        {
            if(sender == packageExclusionsAddAllBtn)
            {
                if(packageExclusionCandidatesLst.Items.Count > 0)
                {
                    List<PackageItem> packs = new List<PackageItem>(packageExclusionCandidatesLst.Items.Cast<PackageItem>());
                    packageExclusionCandidatesLst.Items.Clear();
                    foreach(var pack in packs)
                    {
                        packageExclusionsLst.Items.Add(pack);
                    }
                }
            }
            else if(sender == packageExclusionsAddSelectedBtn)
            {
                if(packageExclusionCandidatesLst.SelectedItems.Count > 0)
                {
                    List<PackageItem> packs = new List<PackageItem>(packageExclusionCandidatesLst.SelectedItems.Cast<PackageItem>());
                    foreach(var pack in packs)
                    {
                        packageExclusionCandidatesLst.Items.Remove(pack);
                        packageExclusionsLst.Items.Add(pack);
                    }
                }
            }
            else if(sender == packageExclusionsRemoveSelectedBtn)
            {
                if(packageExclusionsLst.SelectedItems.Count > 0)
                {
                    List<PackageItem> packs = new List<PackageItem>(packageExclusionsLst.SelectedItems.Cast<PackageItem>());
                    foreach(var pack in packs)
                    {
                        packageExclusionsLst.Items.Remove(pack);
                        packageExclusionCandidatesLst.Items.Add(pack);
                    }
                }
            }
            else if(sender == packageExclusionsClearBtn)
            {
                if(packageExclusionsLst.Items.Count > 0)
                {
                    List<PackageItem> packs = new List<PackageItem>(packageExclusionsLst.Items.Cast<PackageItem>());
                    packageExclusionsLst.Items.Clear();
                    foreach(var pack in packs)
                    {
                        packageExclusionCandidatesLst.Items.Add(pack);
                    }
                }
            }

            CheckAllValidationOfEntry();
        }

        #endregion

        #region VALIDATING EVENTS
        /// <summary>
        /// Check if the coupon name is not null.
        /// Check if the coupon already exists.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void couponNameTxt_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(couponNameTxt.Text))
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
            CheckAllValidationOfEntry();
        }

        private void activeDateBoundsDTP_Validating(object sender, CancelEventArgs e)//delete after project completion
        {
            validateActiveDateBounds();
            CheckAllValidationOfEntry();
        }

        private void txtbxValue_KeyUp(object sender, KeyEventArgs e)
        {
            CheckAllValidationOfEntry();
        }
        #endregion

        private void limitsDGV_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= uintTB_KeyPress;
            e.Control.TextChanged -= uintTB_TextChanged;
            var dgv = sender as DataGridView;
            if(dgv.CurrentCell.ColumnIndex == 1) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if(tb != null)
                {
                    tb.KeyPress += uintTB_KeyPress;
                    e.Control.TextChanged += uintTB_TextChanged;
                }
            }
        }

        void uintTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void uintTB_TextChanged(object sender, EventArgs e)
        {
            uint parseVal;
            var tb = sender as TextBox;
            if(tb != null && !uint.TryParse(tb.Text, out parseVal))
                tb.Text = "";

            if(tb == maxUsageTxtNm)
                maxUsageUnlimitedLbl.Visible = String.IsNullOrWhiteSpace(tb.Text);
        }

        private void limitsDGV_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            var dgv = sender as DataGridView;
            if(dgv.IsCurrentCellDirty)
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);

        }

        private void limitsDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgv = sender as DataGridView;
            var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if(!cell.ReadOnly)
                dgv.BeginEdit(true);
        }

        private void minSpendToQualifyTxtNm_KeyPress(object sender, KeyPressEventArgs e)
        {
            var c = sender as TextBoxNumeric;
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void awardTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            awardTypeAutoPnl.Visible = (AwardType == PlayerComp.AwardTypes.Auto);
            awardTypeWindowTypePnl.Visible = (AwardType == PlayerComp.AwardTypes.Birth_Window);
            CheckAllValidationOfEntry();
        }

    }
}