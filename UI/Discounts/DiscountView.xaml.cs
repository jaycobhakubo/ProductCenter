#region Copyright
// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2016 FortuNet, Inc.
#endregion

//US4321: (US4319) Discount based on quantity

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GameTech.Elite.UI;
using GTI.Modules.ProductCenter.Business;
using GTI.Modules.ProductCenter.Data;
using GTI.Modules.Shared.Business;
using GTI.Modules.Shared.Data;
using Application = System.Windows.Forms.Application;

namespace GTI.Modules.ProductCenter.UI.Discounts
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class DiscountView
    {
        #region VARIABLES

        private readonly DiscountDetailView m_discountDetailView;
        public static List<DiscountItem> ListOfDiscountsItem
        {
            get;
            private set;
        }
        public static List<FullMenuItem> Menus
        {
            get;
            private set;
        }
        public static bool GettingMenus
        {
            get;
            private set;
        }
        public static Button SavedButton;
        public static Button CancelButton;
        private bool m_showInactiveDiscounts;
        private string m_partialFilterName;
        private ProductCenterSettings m_productCenterSettings;

        #endregion

        #region PROPERTIES

        public int OperatorId { private get; set; }

        #endregion

        #region CONSTRUCTORS

        public DiscountView(ProductCenterSettings settings)
        {
            ListOfDiscountsItem = null; // DE13468
            m_productCenterSettings = settings;
            DataContext = this;

            InitializeComponent();
            Resources.MergedDictionaries.Add(ThemeLoader.LoadTheme(DisplayMode.Windowed));

            m_discountDetailView = new DiscountDetailView(m_productCenterSettings)
            {
                OperatorId = OperatorId
            };
            FilterDisplay.Content = m_partialFilterName = string.Empty;

            SavedButton.Click += SavedButton_Click;
            CancelButton.Click += CancelButton_Click;

            m_showInactiveDiscounts = chkBxShowInactive.IsChecked ?? false;

            BackgroundWorker bgwkr = new BackgroundWorker();
            bgwkr.DoWork += bgwkr_DoWork;
            bgwkr.RunWorkerAsync();
        }

        void bgwkr_DoWork(object sender, DoWorkEventArgs e)
        {
            GettingMenus = true;

            Menus = FullMenuItems.GetList(OperatorId);
            
            Thread.Sleep(5000);

            GettingMenus = false;
        }
        #endregion

        #region METHODS

        public void CloseTransitionControl()
        {
            MainWindowTransitionControl.Content = null;
        }

        public void HookIdle()
        {
        }

        public void UnHookIdle()
        {
        }

        public static bool Saved()
        {
            return true;
        }

        private void getAllAvailableDiscount()
        {
            if (ListOfDiscountsItem != null)
            {
                ListOfDiscountsItem.Clear();
            }
            ListOfDiscountsItem = GetDiscountMessage.GetDiscountList();
        }

        //init any data necessary when the discount tool tip is clicked
        public void InitializeData()
        {
            m_discountDetailView.InitializeData();
        }

        private void LoadDiscountList()
        {
            getAllAvailableDiscount();
            
            FillDiscountUI();
        }

        /// <summary>
        /// Adds all the discounts that were retrieved into the UI
        /// </summary>
        private void FillDiscountUI()
        {
            if (ListOfDiscountsItem != null)
            {
                List<DiscountItem> filteredList = ListOfDiscountsItem.Where(di => (di.IsActive || m_showInactiveDiscounts)
                        && (String.IsNullOrWhiteSpace(di.DiscountName) || String.IsNullOrWhiteSpace(m_partialFilterName)  // don't try to filter on empty names
                        || CultureInfo.CurrentCulture.CompareInfo.IndexOf(di.DiscountName, m_partialFilterName, CompareOptions.IgnoreCase) >= 0)).ToList();

                if (lsbxDiscounts != null) // DE13468
                {
                    lsbxDiscounts.SelectedItem = null;
                    lsbxDiscounts.ItemsSource = filteredList.OrderBy(l => l.DiscountName);
                }
            }
        }

        #endregion

        #region EVENTS

        /// <summary>
        /// Displays the search box on the discount UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ShowSearchBox(object sender, RoutedEventArgs e)
        {
            searchBox.Visibility = Visibility.Visible;
            txtbxDiscountName.Focus();
            if (!String.IsNullOrWhiteSpace(txtbxDiscountName.Text))
                txtbxDiscountName.SelectAll();
        }

        /// <summary>
        /// Actions that occur when the user enters text in the search box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbxSearchName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchButton_Click(this, null);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Actions that occur when the user presses the "search" button on the search box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            m_partialFilterName = txtbxDiscountName.Text;
            FillDiscountUI();
            searchBox.Visibility = Visibility.Hidden;
            if (String.IsNullOrWhiteSpace(m_partialFilterName))
                FilterDisplay.Content = String.Empty;
            else
                FilterDisplay.Content = String.Format("Filtered by: '{0}'", m_partialFilterName);
        }

        /// <summary>
        /// Actions that occur when the user presses the "cancel" button on the search box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelSearchButton_Click(object sender, RoutedEventArgs e)
        {
            searchBox.Visibility = Visibility.Hidden;
        }

        private void MainWindowTransitionControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDiscountList();
        }

        /// <summary>
        /// Add / update in the list of discount.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SavedButton_Click(object sender, RoutedEventArgs e)
        {
            if (m_discountDetailView.IsSave)
            {
                LoadDiscountList();
                m_discountDetailView.ClearAllDataInControls();
                MainWindowTransitionControl.Content = null;
            }
        }

        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            LoadDiscountList();
            m_discountDetailView.ClearAllDataInControls();
            MainWindowTransitionControl.Content = null;
        }

        /// <summary>
        /// actions that occur when the "show inactive" checkbox's status changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBxShowInactive_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox)
            {
                m_showInactiveDiscounts = (sender as CheckBox).IsChecked ?? false;
                FillDiscountUI();
            }
        }

        /// <summary>
        /// Close this application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClosedButton_Click(object sender, RoutedEventArgs e)
        {
            ProductCenterMdiForm Close = new ProductCenterMdiForm();
        }

        /// <summary>
        /// Actions that occur when the pressed state of the "new discount" button changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewDiscountButton_Changed(object sender, RoutedEventArgs e)
        {
            lsbxDiscounts.SelectedItem = null;
            m_discountDetailView.CurrentDiscount = new DiscountItem();
            m_discountDetailView.IsNew = true;
            m_discountDetailView.AvailableProducts = GetProductItemMessage.GetProductItems(OperatorId);
            m_discountDetailView.AvailablePackages = PackageItems.Sorted; // US4942
            m_discountDetailView.ClearAllDataInControls();
            m_discountDetailView.ControlReadOnlyFalse();
            m_discountDetailView.btnSave.IsEnabled = false;

            MainWindowTransitionControl.Content = m_discountDetailView;
        }

        /// <summary>
        /// Actions that occur when the selection changes on the discount list. Shows the info of the selected discount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lsbxDiscounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DiscountItem selectedItem = lsbxDiscounts.SelectedItem as DiscountItem;
            if (selectedItem == null)
                return;

            m_discountDetailView.CurrentDiscount = selectedItem;
            m_discountDetailView.AvailableProducts = GetProductItemMessage.GetProductItems(OperatorId);
            m_discountDetailView.AvailablePackages = PackageItems.Sorted; // US4942
            m_discountDetailView.LoadDiscountDataIntoControls();
            m_discountDetailView.IsNew = false;
            m_discountDetailView.IsSave = false;
            m_discountDetailView.btnSave.IsEnabled = true;

            MainWindowTransitionControl.Content = m_discountDetailView;
        }

        #endregion

    }
}
