// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2016 FortuNet, Inc.

//US4321: (US4319) Discount based on quantity

using System;
// publication occur the following will apply:  © FortuNet 
//


//4320: Limit how many times a discount can be used.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.RegularExpressions;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Data;
using GTI.Modules.Shared.Business;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using GTI.Modules.ProductCenter.Business;

namespace GTI.Modules.ProductCenter.UI.Discounts
{
    /// <summary>
    /// Interaction logic for DiscountAuto_Test_NewDiscount.xaml
    /// </summary>
    public partial class DiscountDetailView : UserControl, INotifyPropertyChanged
    {
        #region Constants
        private Brush NO_INPUT_BRUSH
        {
            get { return Brushes.LightGray; }
        }
        private Brush VALID_BRUSH
        {
            get
            {
                Brush brush = (Brush)this.TryFindResource("BlueBrush");
                return brush ?? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0050AA")); // this is cheating, but it doesn't always load in the proper order.. Should create something like the application controller in a WPF app, but not enough time
            }
        }
        #endregion

        #region VARIABLES
        private readonly RestrictionView m_restrictionView;
        private readonly SpendLevelView m_spendLevelsView;
        private readonly AdvancedQuantityView m_advancedQuantityView;
        private bool m_isFixed;
        private List<ProductItemList> m_productItems = new List<ProductItemList>();
        private List<PackageItem> m_packageItems = new List<PackageItem>(); // US4942
        internal static List<string> m_dayOfWeek;
        private ProductCenterSettings m_productCenterSettings;

        #endregion

        #region PROPERTIES

        private bool PriceIsValid
        {
            get
            {
                if (SelectedType.HasValue && 
                    SelectedType.Value != DiscountType.Open && 
                    CurrentDiscount != null && 
                    CurrentDiscount.AdvancedType != DiscountItem.AdvanceDiscountType.Quantity )
                    return txtbxPrice.Foreground != NO_INPUT_BRUSH;
                else
                    return true; // price is being ignored
            }
        }

        public static int ReturnDiscountID;

        public List<ProductItemList> AvailableProducts
        {
            set 
            {
                m_productItems = new List<ProductItemList>();
                if (value != null)
                {
                    foreach(var productItem in value)
                    {
                        if (productItem.ProductItemId > 0 && productItem.IsActive) // shouldn't be able to selet inactive products (taken from SelectProductForm)
                            m_productItems.Add(productItem);
                    }
                }
            }
        }
        public List<PackageItem> AvailablePackages
        {
            set
            {
                m_packageItems = new List<PackageItem>();
                if (value != null)
                {
                    foreach (var productItem in value)
                    {
                        if (productItem.PackageId > 0 )
                            m_packageItems.Add(productItem);
                    }
                }
            }
        }

        public DiscountItem CurrentDiscount { get; set; }

        public bool IsNew { get; set; }

        public bool IsSave { get; set; }

        public int ToggleViewActive { get; set; }

        public ObservableCollection<ScheduleItem> ScheduleItems
        {
            get;
            set;
        }

        public int OperatorId { private get; set; }

        /// <summary>
        /// the selected discount type this item is
        /// </summary>
        /// <remarks>
        /// Note: this is how the other controls represent their item's data. Then the hosting control is what actually saves it.
        /// </remarks>
        public DiscountType? SelectedType
        {
            get 
            {
                if (cmbxDiscountType.SelectedIndex == -1)
                    return null;
                else
                    return (DiscountType)(cmbxDiscountType.SelectedIndex+1);
            }
        }

        /// <summary>
        /// The select award type applied to the discount
        /// </summary>
        public DiscountItem.AwardTypes? AwardType
        {
            get
            {
                if (cmbxAwardType.SelectedIndex == -1)
                    return null;
                else
                    return (DiscountItem.AwardTypes)(cmbxAwardType.SelectedIndex +1);
            }
        }

        /// <summary>
        /// The error text to display
        /// </summary>
        public string ErrorText
        {
            set
            {
                txtblckErr.Content = value;
                if (String.IsNullOrWhiteSpace(value))
                    errIcon.Visibility = errBackground.Visibility = System.Windows.Visibility.Hidden;
                else
                    errIcon.Visibility = errBackground.Visibility = System.Windows.Visibility.Visible;
            }
        }
        #endregion

        #region CONSTRUCTORS
        public DiscountDetailView(ProductCenterSettings settings)
        {
            IsSave = false;
            IsNew = false;
            m_productCenterSettings = settings;
            InitializeComponent();
            this.DataContext = this;

            m_advancedQuantityView = new AdvancedQuantityView();
            m_advancedQuantityView.AdvancedQuantityChanged += AdvancedSettingsChanged;

            m_restrictionView = new RestrictionView(m_productCenterSettings);

            m_spendLevelsView = new SpendLevelView();
            m_spendLevelsView.SpendLevelCountChanged += SpendLevelsChanged;


            QualificationsControl.Content = m_restrictionView;
            AdvancedContentControl.Content = m_spendLevelsView;

            DiscountView.SavedButton = btnSave;
            DiscountView.CancelButton = btnCancel;

            datePkrStartDate.DisplayDateStart = datePkrEndDate.DisplayDateStart = DateTime.Now;

            m_dayOfWeek = new List<string>();
            m_dayOfWeek.Add("All");
            foreach (DayOfWeek value in Enum.GetValues(typeof(DayOfWeek)))
            {
                m_dayOfWeek.Add(value.ToString());
            }
            
            ScheduleItems = new ObservableCollection<ScheduleItem>();
            AddScheduleButton_Click(this, null);

            //US4320
            //add event to prevent pasting invalid data in a numeric textbox
            DataObject.AddPastingHandler(txtbxMaxUsePerSession, TextBoxPastingNumericOnly);

            //
            AdvancedTypeComboBox.SelectedIndex = 0;
            ScheduleGrid.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region METHODS

        //Init Data
        public void InitializeData()
        {
            m_advancedQuantityView.InitalizeData();
        }

        /// <summary>
        /// Check if the entry is a valid input if yes then enable the save button.
        /// </summary>
        private void CustomValidation()
        {
            if (ValidateDiscountData())
            {
                if (btnSave.IsEnabled != true) { btnSave.IsEnabled = true; }
            }
            else
            {
                if (btnSave.IsEnabled != false) { btnSave.IsEnabled = false; }
            }
        }

        public void LoadDiscountDataIntoControls()
        {
            ControlReadOnlyTrue();
            btnSave.Content = "Edit";
            btnSave.Tag = "1";

            //MainTab.IsSelected = true;
            txtbxDiscountName.Text = CurrentDiscount.DiscountName;

            if (CurrentDiscount.Type == DiscountType.Fixed)
            {
                cmbxDiscountType.SelectedIndex = 0;
            }
            else if (CurrentDiscount.Type == DiscountType.Open)
            {
                cmbxDiscountType.SelectedIndex = 1;
            }
            else if (CurrentDiscount.Type == DiscountType.Percent)
            {
                cmbxDiscountType.SelectedIndex = 2;
            }

            //Award Type
            if (CurrentDiscount.DiscountAwardType == DiscountItem.AwardTypes.Manual)//Manual
            {
                cmbxAwardType.SelectedIndex = 0;
            }
            else if (CurrentDiscount.DiscountAwardType == DiscountItem.AwardTypes.Automatic)//Automatic
            {
                cmbxAwardType.SelectedIndex = 1;
            }

            //Active
            chkbxIsActive.IsChecked = CurrentDiscount.IsActive;

            //Player Required
            chkBxRequiredPlayer.IsChecked = CurrentDiscount.IsPlayerRequired;

            txtbxPrice.Text = Helper.DecimalStringToMoneyString(CurrentDiscount.DiscountAmount.ToString());
            txtbxPointsPerDollar.Text = Helper.DecimalStringToMoneyString(CurrentDiscount.PointsPerDollar.ToString());
            txtbxMinPrice.Text = Helper.DecimalStringToMoneyString(CurrentDiscount.MinimumSpend.ToString());
            txtbxMaxDiscount.Text = Helper.DecimalStringToMoneyString(CurrentDiscount.MaximumDiscount.ToString());
            txtbxPointsPerDollar.Foreground = txtbxMinPrice.Foreground = txtbxMaxDiscount.Foreground = txtbxPrice.Foreground = VALID_BRUSH;
            m_restrictionView.LoadProductItems(m_productItems);
            m_restrictionView.LoadPackageItems(m_packageItems); // US4942
            m_restrictionView.SetRestrictedProducts(CurrentDiscount.RestrictedProductIds);
            m_restrictionView.SetRestrictedPackages(CurrentDiscount.RestrictedPackageIds);
            m_restrictionView.IgnoreValidationsForPackages = CurrentDiscount.IgnoreValidationsForIgnoredPackages;
            m_spendLevelsView.LoadSpendLevels(CurrentDiscount.SpendLevels.OrderBy(x => x.Sequence).ToList());
            
            //JKIM Quantity Discount
            AdvancedTypeComboBox.SelectedIndex = (int) CurrentDiscount.AdvancedType;
            m_advancedQuantityView.Load(CurrentDiscount);

            datePkrEndDate.SelectedDate = CurrentDiscount.EndDate;
            datePkrStartDate.DisplayDateStart = datePkrEndDate.DisplayDateStart = DateTime.Now;
            datePkrStartDate.SelectedDate = CurrentDiscount.StartDate;

            //4320
            txtbxMaxUsePerSession.Text = CurrentDiscount.MaximumUsePerSession.ToString(CultureInfo.InvariantCulture);
            
            ScheduleItems.Clear(); // US3956
            if (CurrentDiscount.DiscountSchedule != null && CurrentDiscount.DiscountSchedule.Count != 0)
            {
                List<ScheduleItem> schedItems = new List<ScheduleItem>(); // use this to group everything
                foreach (var schedule in CurrentDiscount.DiscountSchedule)
                {
                    ScheduleItem match = schedItems.FirstOrDefault(x => x.Schedule.DayOfWeek == schedule.DayOfWeek);
                    if (match == null)
                    {
                        match = new ScheduleItem(schedule); // just re-use it..
                        match.ShowAddButton = System.Windows.Visibility.Hidden;
                        match.RemoveScheduleButtonPressed += RemoveScheduleButton_Click;
                        match.AddScheduleButtonPressed += AddScheduleButton_Click;
                        schedItems.Add(match);
                    }
                    else
                    {
                        match.AppendAdditionalSchedule(schedule);
                    }
                }

                if (schedItems.Count > 0)
                {
                    schedItems.Last().ShowAddButton = System.Windows.Visibility.Visible;
                }

                ScheduleItems = new ObservableCollection<ScheduleItem>(schedItems);
                RaisePropertyChanged("ScheduleItems");
            }
            else
            {
                AddScheduleButton_Click(this, null); // add default
            }
            ErrorText = String.Empty;
            DisplayTabs();
            txtbxDiscountName.Focus();

            if (AwardType == DiscountItem.AwardTypes.Manual && ScheduleTab.IsSelected)
            {
                ScheduleTab.IsSelected = false;
            }
        }

        /// <summary>
        /// Clears out all the last discount's information. Note: used when saving a new discount.
        /// </summary>
        public void ClearAllDataInControls()
        {
            //do not select a tab

            txtbxDiscountName.Text = string.Empty;
            cmbxDiscountType.SelectedIndex = cmbxAwardType.SelectedIndex = -1;
            txtbxPointsPerDollar.Text = txtbxMinPrice.Text = txtbxMaxDiscount.Text = txtbxPrice.Text = Helper.DecimalStringToMoneyString(string.Empty);
            txtbxPointsPerDollar.Foreground = txtbxMinPrice.Foreground = txtbxMaxDiscount.Foreground = txtbxPrice.Foreground = NO_INPUT_BRUSH;
            chkbxIsActive.IsChecked = false;
            chkBxRequiredPlayer.IsChecked = false;
            btnSave.Content = "Save";
            btnSave.Tag = "0";
            m_spendLevelsView.ResetSpendLevels();
            AdvancedTypeComboBox.SelectedIndex = (int)CurrentDiscount.AdvancedType;
            m_advancedQuantityView.Load(CurrentDiscount);
            m_restrictionView.LoadProductItems(m_productItems);
            m_restrictionView.LoadPackageItems(m_packageItems); // US4942
            m_restrictionView.ResetControl();
            datePkrStartDate.SelectedDate = DateTime.Now;
            datePkrEndDate.SelectedDate = null;
            ScheduleItems = new ObservableCollection<ScheduleItem>();
            AddScheduleButton_Click(this, null); // add default
            //MainTab.IsSelected = true;
            ErrorText = String.Empty;
            txtbxMaxUsePerSession.Text = 0.ToString(); //US4320



            DisplayTabs();
            txtbxDiscountName.Focus();
        }

        /// <summary>
        /// disables all controls
        /// </summary>
        public void ControlReadOnlyTrue()
        {
            SetDiscountUIStatus(false);


            //txtbxDiscountName.IsReadOnly = true;
            //txtbxDiscountName.Focusable = false;
            //txtbxDiscountName.IsHitTestVisible = false;
            //cmbxDiscountType.IsReadOnly = true;
            //cmbxDiscountType.IsHitTestVisible = false;
            //cmbxDiscountType.Focusable = false;
            //txtbxPrice.IsReadOnly = true;
            //txtbxPrice.Focusable = false;
            //txtbxPrice.IsHitTestVisible = false;
            //txtbxPointsPerDollar.IsReadOnly = true;
            //txtbxPointsPerDollar.Focusable = false;
            //txtbxPointsPerDollar.IsHitTestVisible = false;
            //cmbxAwardType.IsReadOnly = true;
            //cmbxAwardType.IsHitTestVisible = false;
            //cmbxAwardType.Focusable = false;
            //chkBxRequiredPlayer.IsEnabled = false;
            //chkbxIsActive.IsEnabled = false;
            //MainWindowTransitionControl.IsEnabled = false;
            //datePkrStartDate.IsEnabled = false;
            //datePkrStartDate.Focusable = false;
            //datePkrEndDate.IsEnabled = false;
            //datePkrEndDate.Focusable = false;
            //chkBxExpires.IsEnabled = false;
        }

        /// <summary>
        /// Enable all controls
        /// </summary>
        public void ControlReadOnlyFalse()
        {
            SetDiscountUIStatus(true);
            txtbxDiscountName.Focus();

            //txtbxDiscountName.IsReadOnly = false;
            //txtbxDiscountName.Focusable = true;
            //txtbxDiscountName.IsHitTestVisible = true;
            //cmbxDiscountType.IsReadOnly = false;
            //cmbxDiscountType.IsHitTestVisible = true;
            //cmbxDiscountType.Focusable = true;
            //txtbxPrice.IsReadOnly = false;
            //txtbxPrice.Focusable = true;
            //txtbxPrice.IsHitTestVisible = true;
            //txtbxPointsPerDollar.IsReadOnly = false;
            //txtbxPointsPerDollar.Focusable = true;
            //txtbxPointsPerDollar.IsHitTestVisible = true;
            //chkBxRequiredPlayer.IsEnabled = true;
            //chkbxIsActive.IsEnabled = true;
            //MainWindowTransitionControl.IsEnabled = true;
            //if (m_isSave == true)
            //{
            //    m_RestrictionView.ListProductItem = MemberListProductItem;
            //    m_RestrictionView.LoadProductItem();
            //}

            //if (cmbxAwardType.SelectedIndex != 0)
            //{
            //    cmbxAwardType.IsReadOnly = false;
            //    cmbxAwardType.IsHitTestVisible = true;
            //    cmbxAwardType.Focusable = true;
            //}
            //datePkrStartDate.IsEnabled = true;
            //datePkrStartDate.Focusable = true;
            //if(chkBxExpires.IsChecked.Value)
            //    datePkrEndDate.IsEnabled = true;
            //datePkrEndDate.Focusable = true;
            //chkBxExpires.IsEnabled = true;       
        }

        /// <summary>
        /// Sets the discount's editable controls to enabled or disabled based on the sent-in value
        /// </summary>
        /// <param name="enabled">
        /// true => enables all the controls for editing
        /// false => disables all controls for editing
        /// </param>
        public void SetDiscountUIStatus(bool enabled)
        {
            List<UIElement> children = new List<UIElement>(DiscountInfoGrid.Children.Cast<UIElement>());
            children.AddRange(ScheduleGrid.Children.Cast<UIElement>());
            children.AddRange(RulesGrid.Children.Cast<UIElement>());

            foreach (UIElement child in children) // all discount data is contained within the grids
            {
                if (child is TextBox)
                    (child as TextBox).IsReadOnly = !enabled; // looks nicer. Could theme, but this is easier
                else if (child is ComboBox)
                    (child as ComboBox).IsReadOnly = !enabled;

                child.IsEnabled = enabled;
                child.Focusable = enabled;
                child.IsHitTestVisible = enabled;
            }

            //US4320
            // if player is unchecked, we need to update max usage per session controls
            if (chkBxRequiredPlayer.IsChecked == false && chkBxRequiredPlayer.IsEnabled)
            {
                chkBxRequiredPlayer_Unchecked(null, null);
            }

            // do any special logic here
            AdvancedContentControl.IsEnabled = enabled;
            QualificationsControl.IsEnabled = enabled;
            if (enabled)
            {
                if (IsSave == true)
                {
                    m_restrictionView.LoadProductItems(m_productItems);
                    m_restrictionView.LoadPackageItems(m_packageItems); // US4942
                }

                if (cmbxAwardType.SelectedIndex != 0)
                {
                    cmbxAwardType.IsReadOnly = false;
                    cmbxAwardType.IsHitTestVisible = true;
                    cmbxAwardType.Focusable = true;
                }
            }

            AdvancedTypeLabel.IsEnabled = enabled;
            AdvancedTypeComboBox.IsEnabled = enabled;
        }

        public void SetBorderBrushToDefault()
        {
            txtbxDiscountName.BorderBrush = Brushes.LightGray;
            cmbxDiscountType.BorderBrush = Brushes.LightGray;
            txtbxPrice.BorderBrush = Brushes.LightGray;
            txtbxPointsPerDollar.BorderBrush = Brushes.LightGray;
            cmbxAwardType.BorderBrush = Brushes.LightGray;
        }

        /// <summary>
        /// Checks to see if the sent in discount conflicts with any of the existing saved discounts.
        /// </summary>
        /// <param name="inProgressDiscount"></param>
        /// <returns></returns>
        private bool ScheduleOverlapsWithExisting(DiscountItem inProgressDiscount)
        {
            bool hasConflict = false;
            foreach (DiscountItem discount in DiscountView.ListOfDiscountsItem)
            {
                if (discount.IsActive && discount.DiscountAwardType == DiscountItem.AwardTypes.Automatic && discount.Type == DiscountType.Percent
                    && (!discount.StartDate.HasValue || discount.StartDate <= inProgressDiscount.StartDate ) 
                    && (!discount.EndDate.HasValue || discount.EndDate >= inProgressDiscount.EndDate ) )
                {
                    if (discount.DiscountSchedule != null && inProgressDiscount.DiscountSchedule != null)
                    {
                        foreach (var sched in discount.DiscountSchedule)
                        {
                            if(inProgressDiscount.DiscountSchedule.Any(x=>x.Equals(sched)))
                            {
                                hasConflict = true;
                                break;
                            }
                        }
                    }
                    else // no schedule means valid for all dates
                    {
                        hasConflict = true;
                    }

                    if (hasConflict)
                    {
                        System.Windows.Forms.DialogResult result = MessageForm.Show(String.Format("Discount '{0}' has a schedule that overlaps with this discount; only one can apply to sale. Save anyway?", discount.DiscountName), "", MessageFormTypes.YesCancel);
                        if (result == System.Windows.Forms.DialogResult.OK || result == System.Windows.Forms.DialogResult.Yes) // ignore overlap
                            hasConflict = false;
                    }
                }
            }

            return hasConflict;
        }

        /// <summary>
        /// Returns true if the discount exists in any menus
        /// </summary>
        /// <param name="discountID"></param>
        /// <returns></returns>
        private bool DiscountExistsInMenu(int discountID, string discountName)
        {
            bool exists = false;
            List<string> matchingMenus = new List<string>();

            if (DiscountView.GettingMenus) // should really be a "please wait" spinner
            {
                //WaitForm waitForm = new WaitForm(); // this doesn't work. Waitform expects an image
                //waitForm.UseWaitCursor = true;
                //waitForm.Text = "Please wait while data is being validated.";
                //waitForm.Show();
                //while (MainDiscount.GettingMenus)
                //    Thread.Sleep(100);
                //waitForm.Hide();

                MessageForm.Show("Please try again later, data is still being validated.");
                return true; // just break out early
            }

            List<FullMenuItem> menus = DiscountView.Menus;
            if (menus != null)
            {
                foreach (var menu in menus)
                {
                    if (menu.MenuPages != null)
                    {
                        if(menu.MenuPages.Any(x=>x.DiscountId == discountID))
                        {   // could break out early here, but since the "normal" case of it not finding anything will go through them all, it will take the same amount of time
                            exists = true;
                            matchingMenus.Add(menu.MenuName);
                        }
                    }
                }
            }
            if(exists)
            {
                string message = String.Format("Can't inactivate '{0}' because it is in use. The discount is currently being used in the following menu(s): {1}", 
                    discountName, String.Join(", ", matchingMenus));
                MessageForm.Show(message, "Inactivate Discount", MessageFormTypes.OK);
            }

            return exists;
        }

        /// <summary>
        /// Displays the tabs that match with the selected discount information
        /// </summary>
        private void DisplayTabs()
        {
            //Schedule Tab
            if (!AwardType.HasValue || AwardType.Value == DiscountItem.AwardTypes.Manual)
            {
                ScheduleTab.Visibility = Visibility.Collapsed;
                ScheduleTab.IsSelected = false;
            }
            else
            {
                ScheduleTab.Visibility = Visibility.Visible;
                ScheduleGrid.Visibility = Visibility.Visible;
            }

            //Advanced Tab
            if (CurrentDiscount != null && 
                CurrentDiscount.AdvancedType != DiscountItem.AdvanceDiscountType.None)
            {
                AdvancedTab.Visibility = Visibility.Visible;
            }
            else
            {
                AdvancedTab.Visibility = Visibility.Collapsed;
                AdvancedTab.IsSelected = false;
            }

            //Qualification Tab
            if (!SelectedType.HasValue)
            {
                QualificationTab.Visibility = Visibility.Collapsed;
                QualificationTab.IsSelected = false;
            }
            else if (SelectedType.Value == DiscountType.Open)
            {
                AdvancedTab.Visibility = QualificationTab.Visibility = Visibility.Collapsed;
                AdvancedTab.IsSelected = QualificationTab.IsSelected = false;
            }
            else if (SelectedType.Value == DiscountType.Percent)
            {
                if (AwardType == DiscountItem.AwardTypes.Automatic && 
                    CurrentDiscount != null && 
                    CurrentDiscount.AdvancedType == DiscountItem.AdvanceDiscountType.Quantity)
                {
                    QualificationTab.Visibility = Visibility.Collapsed;
                    QualificationTab.IsSelected = false;
                }
                else
                {
                    QualificationTab.Visibility = Visibility.Visible;
                }
            }
            else if (SelectedType.Value == DiscountType.Fixed && AwardType == DiscountItem.AwardTypes.Automatic)
            {
                if (CurrentDiscount != null &&
                    CurrentDiscount.AdvancedType == DiscountItem.AdvanceDiscountType.Quantity)
                {
                    QualificationTab.Visibility = Visibility.Collapsed;
                    QualificationTab.IsSelected = false;
                }
                else
                {
                    QualificationTab.Visibility = Visibility.Visible;
                }
            }
            else
            {
                QualificationTab.Visibility = Visibility.Collapsed;
                QualificationTab.IsSelected = false;
            }
        }

        #endregion

        #region EVENTS

        /// <summary>
        /// Actions that occur when the controls finishes loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtbxDiscountName.Focus();
        }

        /// <summary>
        /// Save or edit new discount.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (Convert.ToInt32(btn.Tag) == 1)
                EditClick();
            else
                SaveClick();
        }

        /// <summary>
        /// Actions that occur when the user presses the "Edit" Button
        /// </summary>
        private void EditClick()
        {
            btnSave.Tag = "0";
            btnSave.Content = "Save";
            ControlReadOnlyFalse();

            IsSave = false;

            txtbxDiscountName.Focus();
        }

        /// <summary>
        /// Actions that occur when the user presses the "Save" Button
        /// </summary>
        private void SaveClick()
        {
            if (!m_spendLevelsView.IsValidForSave)
            {
                return;
            }

            if (!ValidateDiscountData(true))
                return;

            bool TempIsActive;
            bool TempIsPlayeredRequired;
            int tempDiscountID = 0;
            List<int> restrictedProducts = new List<int>();
            List<int> restrictedPackages = new List<int>();

            TempIsActive = chkbxIsActive.IsChecked ?? false;
            TempIsPlayeredRequired = chkBxRequiredPlayer.IsChecked ?? false;
            
            if (IsNew == false) { tempDiscountID = CurrentDiscount.DiscountId; }

            if (!IsNew && AwardType.Value == DiscountItem.AwardTypes.Manual && TempIsActive != CurrentDiscount.IsActive && !TempIsActive)
            { // if it's existing and the active status has changed, check to see if this discount exists in sales menus
                if (DiscountExistsInMenu(tempDiscountID, txtbxDiscountName.Text))
                    return;
            }

            foreach (KeyValuePair<int, string> d in m_restrictionView.SelectedProducts)
            {
                restrictedProducts.Add(d.Key);
            }
            foreach (KeyValuePair<int, string> d in m_restrictionView.SelectedPackages)
            {
                restrictedPackages.Add(d.Key);
            }            

            decimal discountAmt, pointsPerDollar, maxDiscount, minSpend;
            byte maxUsePerSession; //US4320

            Decimal.TryParse(txtbxPrice.Text, out discountAmt);
            Decimal.TryParse(txtbxPointsPerDollar.Text, out pointsPerDollar);
            Decimal.TryParse(txtbxMaxDiscount.Text, out maxDiscount);
            Decimal.TryParse(txtbxMinPrice.Text, out minSpend);
            byte.TryParse(txtbxMaxUsePerSession.Text, out maxUsePerSession);//US4320


            List<DiscountItem.Schedule> scheduleItems = new List<Shared.Business.DiscountItem.Schedule>(); // US3956
            if (ScheduleItems != null)
            {
                foreach (var schedule in ScheduleItems)
                {
                    DiscountItem.Schedule sched = new DiscountItem.Schedule();
                    sched.DayOfWeek = schedule.Schedule.DayOfWeek;

                    if (schedule.SelectedSessions.Count > 0)
                    {
                        if (!schedule.SelectedSessions.Any(x => String.Equals(x.Value, ScheduleItem.ALL_SCHEDULE_ITEMS)))
                        {
                            foreach (KeyValuePair<int, string> pair in schedule.SelectedSessions)
                            {
                                DiscountItem.Schedule sched2 = new DiscountItem.Schedule() 
                                { 
                                    DayOfWeek = sched.DayOfWeek, 
                                    SessionNumber = pair.Key 
                                };
                                scheduleItems.Add(sched2);
                            }
                        }
                        else // else defaults to null. It's fine
                        {
                            scheduleItems.Add(sched);
                        }
                    }
                    else // else defaults session to null. It's fine
                    {
                        scheduleItems.Add(sched);
                    }
                }
            }

            DiscountItem discItem = new DiscountItem
            {
                DiscountId = tempDiscountID,
                Type = SelectedType.Value,
                DiscountAmount = discountAmt,
                PointsPerDollar = pointsPerDollar,
                IsActive = TempIsActive,
                DiscountName = txtbxDiscountName.Text,
                DiscountAwardType = AwardType.Value,
                SpendLevels = m_spendLevelsView.ListOfSpendLevels,
                RestrictedProductIds = restrictedProducts,
                RestrictedPackageIds = restrictedPackages,
                IgnoreValidationsForIgnoredPackages = m_restrictionView.IgnoreValidationsForPackages,
                IsPlayerRequired = TempIsPlayeredRequired,
                StartDate = datePkrStartDate.SelectedDate,
                EndDate = datePkrEndDate.SelectedDate,
                AllowPartialDiscounts = chkBxAllowPartialDiscount.IsChecked ?? false,
                MaximumDiscount = maxDiscount,
                MinimumSpend = minSpend,
                DiscountSchedule = scheduleItems,
                AdvancedType = (DiscountItem.AdvanceDiscountType)AdvancedTypeComboBox.SelectedIndex,
                AdvancedQuantityDiscount =
                {
                    //US4321: (US4319) Discount based on quantity
                    BuyPackageId = m_advancedQuantityView.BuyPackageId,
                    BuyQuantity = m_advancedQuantityView.BuyQuantity,
                    GetPackageId = m_advancedQuantityView.GetPackageId,
                    GetQuantity = m_advancedQuantityView.GetQuantity
                },
                MaximumUsePerSession = maxUsePerSession, //4320
            };

            //check for advance type. Can only be with award type auto 
            if (discItem.DiscountAwardType == DiscountItem.AwardTypes.Manual &&
                discItem.AdvancedType == DiscountItem.AdvanceDiscountType.Quantity)
            {
                discItem.AdvancedType = DiscountItem.AdvanceDiscountType.None;
            }

            //if (discItem.IsActive && discItem.DiscountAwardType == Shared.Business.DiscountItem.AwardTypes.Automatic && discItem.Type == DiscountType.Percent // commented out because functionality is not desired at this time
            //    && ScheduleOverlapsWithExisting(discItem))
            //    return; // don't save. Allow user to edit discount

            try
            {
                ReturnDiscountID = Data.SetDiscountMessage.Save(discItem);
                IsSave = true;
            }
            catch (Exception ex)
            {
                string error = "Error saving new discount " + ex.ToString();
                MessageBox.Show(error);
                Logger.LogSevere(error, "NewDiscount.xaml.cs", 411);
            }
        }

        /// <summary>
        /// Checks whether or not the entered data is valid
        /// </summary>
        /// <param name="showPopup"></param>
        /// <returns></returns>
        private bool ValidateDiscountData(bool showPopup = false)
        {
            bool retVal = true;
            string message = String.Empty;
            
            if (datePkrStartDate.SelectedDate.HasValue && datePkrEndDate.SelectedDate.HasValue &&
                datePkrStartDate.SelectedDate > datePkrEndDate.SelectedDate)
            {
                message = String.Format("The start date must be before the end date.");
                retVal = false;
            }
            else if (String.IsNullOrWhiteSpace(txtbxDiscountName.Text))
            {
                message = String.Format("Must specify a discount name.");
                retVal = false;
            }
            else if (!PriceIsValid 
                && ( m_spendLevelsView == null || m_spendLevelsView.ListOfSpendLevels == null || m_spendLevelsView.ListOfSpendLevels.Count == 0))
            {
                message = String.Format("Must specify a price or spend level.");
                retVal = false;
            }
            else if (cmbxDiscountType.SelectedIndex == -1)
            {
                message = String.Format("Please select a valid discount type.");
                retVal = false;
            }
            else if (cmbxAwardType.SelectedIndex == -1)
            {
                message = String.Format("Please select a valid award type.");
                retVal = false;
            }
            else if (!AwardType.HasValue)
            {
                message = String.Format("Please select the award type");
                retVal = false;
            }
            else if (!SelectedType.HasValue)
            {
                message = String.Format("Please select the discount type");
                retVal = false;
            }
            else if (AwardType.Value == DiscountItem.AwardTypes.Automatic && SelectedType.Value == DiscountType.Open)
            {
                message = String.Format("Cannot have an 'Open' automatic discount");
                retVal = false;
            }
            else if (CurrentDiscount.AdvancedType == DiscountItem.AdvanceDiscountType.Quantity && 
                    CurrentDiscount.DiscountAwardType == DiscountItem.AwardTypes.Automatic &&
                    !string.IsNullOrEmpty(m_advancedQuantityView.IsValid()))
            {
                message = m_advancedQuantityView.IsValid();
                retVal = false;
            }


            if (!retVal)
            {
                if (showPopup)
                    MessageForm.Show(message);
                else
                    ErrorText = message;
            }
            else
            {
                ErrorText = String.Empty;
            }

            return retVal;
        }

        #region Handing For Money Fields

        /// <summary>
        /// Handle price format ##.##
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbxPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool NotAllow = false;
            TextBox y = (TextBox)sender;
            string x = y.Text;
            x = x.Insert(y.SelectionStart, e.Text);
            int count = x.Split('.').Length - 1;

            if (count > 1)//One decimal point only.
            {
                NotAllow = true;
            }
            else if ((Convert.ToChar(e.Text)) == '.')
            {
                NotAllow = false;
            }
            else if (Char.IsNumber(Convert.ToChar(e.Text)))
            {
                NotAllow = false;
                if (m_isFixed == true || sender != txtbxPrice)
                {
                    if (Regex.IsMatch(x, @"\.\d\d\d"))//Only allow .## 
                    {
                        NotAllow = true;
                    }
                }
                else//Percent
                {
                    if (Regex.IsMatch(x, @"\.\d\d\d"))//Only allow .## 
                    {
                        NotAllow = true;
                    }
                    //Limit only to 100 percent
                    decimal PercentValue;
                    bool result =  decimal.TryParse(x, out PercentValue );
                    if (result == true && PercentValue > 100M)
                    {
                        NotAllow = true;
                    }                    
                }
            }
            else
            {
                NotAllow = true;
            }
            e.Handled = NotAllow;
        }
        
        private void txtbxPrice_PreviewTextInput2(object sender, TextCompositionEventArgs e)
        {
            bool NotAllow = false;
            TextBox y = (TextBox)sender;
            string x = y.Text;
            x = x.Insert(y.SelectionStart, e.Text);
            int count = x.Split('.').Length - 1;//Count how many decimal places on the text input

            if (count > 1)//One decimal point only.
            {
                NotAllow = true;
            }
            else if ((Convert.ToChar(e.Text)) == '.')
            {
                NotAllow = false;
            }
            else if (Char.IsNumber(Convert.ToChar(e.Text)))
            {
                NotAllow = false;

                if (Regex.IsMatch(x, @"\.\d\d\d"))//Only allow .## 
                {
                    NotAllow = true;
                }
            }
            else
            {
                NotAllow = true;
            }

            e.Handled = NotAllow;
        }
        
        /// <summary>
        /// Handle keyinput for space and and backspace.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbxPrice_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            bool notAllow = false;

            if (e.Key == Key.Space)
            {
                notAllow = true;
            }
            else if (e.Key == Key.Back)
            {
                notAllow = false;
            }
            e.Handled = notAllow;
        }

        /// <summary>
        /// Fixed input value for "Price or Percent Value".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbxPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txtBx = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(txtBx.Text)) // put in empty text
            {
                txtBx.Foreground = NO_INPUT_BRUSH;
                if (txtBx == txtbxPointsPerDollar)
                    txtBx.Text = "0.00";
            }
            if(txtBx != txtbxPointsPerDollar) // don't format this one as money
                txtBx.Text = Helper.DecimalStringToMoneyString(txtBx.Text); // format all text as a money decimal
            CustomValidation();
        }

        private void txtbxPrice_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txtBx = (TextBox)sender;
            if (txtBx.Foreground == NO_INPUT_BRUSH) // displaying placeholder text
            {
                txtBx.Text = string.Empty;
                txtBx.Foreground = VALID_BRUSH;
            }
        }
        #endregion

        private void cmbxAwardType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((DiscountItem.AdvanceDiscountType)AdvancedTypeComboBox.SelectedIndex == 
                DiscountItem.AdvanceDiscountType.Quantity)
            {
                m_advancedQuantityView.EnableQuantityDiscountControls(AwardType == DiscountItem.AwardTypes.Automatic);
            }
            else if ((DiscountItem.AdvanceDiscountType) AdvancedTypeComboBox.SelectedIndex ==
                     DiscountItem.AdvanceDiscountType.SpendLevel)
            {
                m_spendLevelsView.EnableSpendLevelContent(AwardType == DiscountItem.AwardTypes.Automatic);
            }
            DisplayTabs();
            CustomValidation();
        }

        private void cmbxDiscountType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmbx = (ComboBox)sender;

            cmbxAwardType.Visibility = txtBlckAwardType.Visibility = AdvancedTypeLabel.Visibility = AdvancedTypeComboBox.Visibility = Visibility.Visible;

            var visibility = (DiscountItem.AdvanceDiscountType)AdvancedTypeComboBox.SelectedIndex == DiscountItem.AdvanceDiscountType.Quantity
                                ? Visibility.Collapsed
                                : Visibility.Visible;
            txtblckPrice.Visibility = txtbxPrice.Visibility = txtbxPointsPerDollar.Visibility = txtblckPointsPerDollar.Visibility = visibility;

            if (cmbx.SelectedIndex == 0)//Fixed
            {
                txtblckPrice.Content = "* Price: ";
                txtbxPrice.Text = Helper.DecimalStringToMoneyString("0");
                txtbxPrice.HorizontalContentAlignment = HorizontalAlignment.Right;
                m_isFixed = true;
            }
            else if (cmbx.SelectedIndex == 1)//Open
            {
                cmbxAwardType.Visibility = txtBlckAwardType.Visibility = txtblckPrice.Visibility = txtbxPrice.Visibility = AdvancedTypeLabel.Visibility = AdvancedTypeComboBox.Visibility = Visibility.Collapsed;
                m_isFixed = false;              
                cmbxAwardType.SelectedIndex = 0;           
            }
            else if (cmbx.SelectedIndex == 2)//Percent
            {
                txtblckPrice.Content = "* Percent: ";
                txtbxPrice.Text = Helper.DecimalStringToMoneyString("0");
                txtbxPrice.HorizontalContentAlignment = HorizontalAlignment.Right;  //HorizontalAlignment.Left; JAN - changed per Travis 4-21
                m_isFixed = false;
            }

            DisplayTabs();
            CustomValidation();
        }

        /// <summary>
        /// actions that occur when the number of spend levels change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void AdvancedSettingsChanged(object sender, EventArgs args)
        {
            CustomValidation();
        }

        /// <summary>
        /// actions that occur when the number of spend levels change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void SpendLevelsChanged(object sender, EventArgs args)
        {
            CustomValidation();
        }
        
        private void txtbxDiscountName_KeyUp(object sender, KeyEventArgs e)
        {
            CustomValidation();
        }

        private void datePkr_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CustomValidation();
        }
        
        /// <summary>
        /// Actions that occur when the user presses the add button next to a schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddScheduleButton_Click(object sender, EventArgs e)
        {
            ScheduleItem schedule = new ScheduleItem();
            schedule.RemoveScheduleButtonPressed += RemoveScheduleButton_Click;
            schedule.AddScheduleButtonPressed += AddScheduleButton_Click;
            ScheduleItems.Add(schedule);
            RaisePropertyChanged("ScheduleItems");
        }

        /// <summary>
        /// Actions that occur when the user presses the remove button next to a schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveScheduleButton_Click(object sender, EventArgs e)
        {
            if (ScheduleItems.Contains(sender))
            {
                bool enableAdd = false;
                ScheduleItem schedule = (sender as ScheduleItem);
                if (schedule.ShowAddButton == System.Windows.Visibility.Visible)
                    enableAdd = true;

                schedule.RemoveScheduleButtonPressed -= RemoveScheduleButton_Click;
                schedule.AddScheduleButtonPressed -= AddScheduleButton_Click;
                ScheduleItems.Remove(schedule);

                if (enableAdd && ScheduleItems.Count > 0)
                    ScheduleItems.Last().ShowAddButton = System.Windows.Visibility.Visible;
            }

            if (ScheduleItems.Count == 0) // basically refreshing the list
                AddScheduleButton_Click(this, null);
            else
                RaisePropertyChanged("ScheduleItems");
        }

        private void AdvancedTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var visibility = (DiscountItem.AdvanceDiscountType) AdvancedTypeComboBox.SelectedIndex ==
                             DiscountItem.AdvanceDiscountType.Quantity
                ? Visibility.Collapsed
                : Visibility.Visible;
            txtblckPrice.Visibility = txtbxPrice.Visibility = txtbxPointsPerDollar.Visibility = txtblckPointsPerDollar.Visibility = visibility;

            switch ((DiscountItem.AdvanceDiscountType)AdvancedTypeComboBox.SelectedIndex)
            {
                case DiscountItem.AdvanceDiscountType.Quantity:
                    //AdvancedTab.Visibility = Visibility.Visible;
                    AdvancedTab.Header = "Quantity";
                    if (CurrentDiscount != null)
                    {
                        CurrentDiscount.AdvancedType = DiscountItem.AdvanceDiscountType.Quantity;
                    }

                    m_advancedQuantityView.EnableQuantityDiscountControls(AwardType == DiscountItem.AwardTypes.Automatic);

                    AdvancedContentControl.Visibility = Visibility.Visible;
                    AdvancedContentControl.Content = m_advancedQuantityView;

                    AdvancedTab.IsSelected = true;
                    break;
                case DiscountItem.AdvanceDiscountType.SpendLevel:
                    //AdvancedTab.Visibility = Visibility.Visible;
                    AdvancedTab.Header = "Spend Level";
                    if (CurrentDiscount != null)
                    {
                        CurrentDiscount.AdvancedType = DiscountItem.AdvanceDiscountType.SpendLevel;
                    }

                    m_spendLevelsView.EnableSpendLevelContent(AwardType == DiscountItem.AwardTypes.Automatic);
                    
                    AdvancedContentControl.Visibility = Visibility.Visible;
                    AdvancedContentControl.Content = m_spendLevelsView;

                    AdvancedTab.IsSelected = true;
                    break;
                default:
                    if (CurrentDiscount != null)
                    {
                        CurrentDiscount.AdvancedType = DiscountItem.AdvanceDiscountType.None;
                        AdvancedTab.Visibility = Visibility.Hidden;
                    }
                    
                    AdvancedContentControl.Visibility = Visibility.Hidden;
                    AdvancedTab.IsSelected = false;
                    break;
            }

            DisplayTabs();
            CustomValidation();
        }

        //US4320 *** Begin ***
        private void txtbxMaxDiscountPerSession_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        //only allow numeric characters
        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        //prevent pasting for numeric textbox
        private void TextBoxPastingNumericOnly(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        //prevent spaces for numeric textbox
        private void txtbxMaxDiscountPerSession_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void chkBxRequiredPlayer_Checked(object sender, RoutedEventArgs e)
        {
            if (txtbxDiscountName.IsEnabled)
            {
                txtbxMaxUsePerSession.IsEnabled = true;
                txtbxMaxUsePerSession.IsReadOnly = false;
                txtbxMaxUsePerSession.ToolTip = null;
                lblMaxUsePerSession.ToolTip = null;
            }
        }

        private void chkBxRequiredPlayer_Unchecked(object sender, RoutedEventArgs e)
        {
            if (txtbxDiscountName.IsEnabled)
            {
                txtbxMaxUsePerSession.IsEnabled = false;
                txtbxMaxUsePerSession.IsReadOnly = true;
                txtbxMaxUsePerSession.ToolTip = "Require player card must be set";
                lblMaxUsePerSession.ToolTip = "Require player card must be set";
            }
        }

        private void txtbxMaxDiscountPerSession_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtbxMaxUsePerSession.Text.Trim()))
            {
                txtbxMaxUsePerSession.Text = "0";
            }

            int maxUsePerSession = int.Parse(txtbxMaxUsePerSession.Text);

            if (maxUsePerSession > byte.MaxValue)
            {
                txtbxMaxUsePerSession.Text = byte.MaxValue.ToString();
            }
        }

        //US4320 *** End ***
        #endregion

        #region INotifier Implementation

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies any listeners that a property has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that has
        /// changed.</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion
    }

    public class ScheduleItem : GameTech.Elite.Base.ViewModelBase
    {
        #region Constants

        private const int ALL_SCHEDULE_ITEMS_KEY = 0;
        /// <summary>
        /// Used to represent all schedulable items
        /// </summary>
        public const string ALL_SCHEDULE_ITEMS = "All";
        public const int SESSION_MAX = 50;

        #endregion

        #region Events

        public event EventHandler AddScheduleButtonPressed;
        public event EventHandler RemoveScheduleButtonPressed;
        
        #endregion

        #region Private Members

        private Visibility _showAddButton = Visibility.Visible;

        #endregion
        
        #region Public Properties

        /// <summary>
        /// The original schedule object. Used to grab info that's similar amongst all the contained schedules (currently Day Of Week).
        /// </summary>
        public DiscountItem.Schedule Schedule
        {
            get;
            set;
        }

        /// <summary>
        /// All the available days of the week
        /// </summary>
        public ObservableCollection<string> DaysOfWeek
        {
            get;
            set;
        }
        /// <summary>
        /// The day of the week that this schedule represents
        /// </summary>
        public string DayOfWeekDisplay
        {
            get
            {
                if (Schedule.DayOfWeek.HasValue)
                    return Schedule.DayOfWeek.Value.ToString();
                else
                    return ALL_SCHEDULE_ITEMS;
            }
            set
            {
                // Convert it from something readable to something that can be stored easily
                if (String.Equals(ALL_SCHEDULE_ITEMS, value))
                {
                    Schedule.DayOfWeek = null;
                }
                else
                {
                    foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                    {
                        if (String.Equals(day.ToString(), value))
                        {
                            Schedule.DayOfWeek = day;
                            break;
                        }
                    }
                }
                RaisePropertyChanged("DayOfWeekDisplay");
            }
        }
        
        /// <summary>
        /// All the available sessions of the day that this object can be
        /// </summary>
        public Dictionary<int, string> SessionsOfDay
        {
            get;
            set;
        }
        /// <summary>
        /// All the sessions selected for this schedule
        /// </summary>
        public Dictionary<int, string> SelectedSessions
        {
            get;
            set;
        }
        public string SessionNumberDisplay
        {
            get
            {
                if (Schedule.SessionNumber.HasValue)
                    return "Session " + Schedule.SessionNumber.Value;
                else
                    return ALL_SCHEDULE_ITEMS;
            }
            set
            {
                // Convert it from something readable to something that can be stored easily
                if(String.Equals(ALL_SCHEDULE_ITEMS, value))
                {
                    Schedule.SessionNumber = null;
                }
                else
                {
                    string[] tokens = value.Split(' ');
                    int sess = 0;
                    if (int.TryParse(tokens.Last(), out sess))
                        Schedule.SessionNumber = sess;
                    else
                        Schedule.SessionNumber = null;
                }
            }
        }

        public Visibility ShowAddButton
        {
            get { return _showAddButton; }
            set
            {
                if (_showAddButton != value)
                {
                    _showAddButton = value;
                    RaisePropertyChanged("ShowAddButton");
                }
            }
        }
        
        #endregion

        public ScheduleItem(DiscountItem.Schedule schedule = null)
        {
            SelectedSessions = new Dictionary<int, string>();
            SessionsOfDay = new Dictionary<int, string>();
            SessionsOfDay.Add(ALL_SCHEDULE_ITEMS_KEY, ALL_SCHEDULE_ITEMS);
            for(int i=1; i <=SESSION_MAX; i++)
            {
                SessionsOfDay.Add(i, "Session " + i);
            }

            DaysOfWeek = new ObservableCollection<string>(DiscountDetailView.m_dayOfWeek);
            Schedule = schedule;
            if (Schedule == null)
            {
                SelectedSessions.Add(0, ALL_SCHEDULE_ITEMS);
                Schedule = new DiscountItem.Schedule();
            }
            else
            {
                AppendAdditionalSchedule(schedule);
            }
        }

        /// <summary>
        /// Adds the new schedule as a linked item to this schedule
        /// </summary>
        /// <param name="schedule"></param>
        public void AppendAdditionalSchedule(DiscountItem.Schedule schedule)
        {
            int mappedSessId = schedule.SessionNumber ?? ALL_SCHEDULE_ITEMS_KEY;
            if (mappedSessId == ALL_SCHEDULE_ITEMS_KEY) // should only display "all"
                SelectedSessions.Clear();

            if (!SelectedSessions.ContainsKey(mappedSessId) && !SelectedSessions.ContainsKey(ALL_SCHEDULE_ITEMS_KEY))
            {
                SelectedSessions.Add(mappedSessId, SessionsOfDay[mappedSessId]);
            }
        }

        #region AddScheduleCommand
        /// <summary>
        /// 
        /// </summary>
        private RelayCommand addScheduleCommand;
        public ICommand AddScheduleCommand
        {
            get
            {
                if (addScheduleCommand == null)
                    addScheduleCommand = new RelayCommand(param => this.OnAddScheduleCommand());
                return addScheduleCommand;
            }
        }
        private void OnAddScheduleCommand()
        {
            var handler = AddScheduleButtonPressed;
            if (handler != null)
            {
                ShowAddButton = Visibility.Hidden;
                handler(this, new EventArgs());
            }
        }
        #endregion

        #region RemoveScheduleCommand
        /// <summary>
        /// 
        /// </summary>
        private RelayCommand removeScheduleCommand;
        public ICommand RemoveScheduleCommand
        {
            get
            {
                if (removeScheduleCommand == null)
                    removeScheduleCommand = new RelayCommand(param => this.OnRemoveScheduleCommand());
                return removeScheduleCommand;
            }
        }
        private void OnRemoveScheduleCommand()
        {
            var handler = RemoveScheduleButtonPressed;
            if (handler != null)
                handler(this, new EventArgs());
        }
        #endregion
    }
}

