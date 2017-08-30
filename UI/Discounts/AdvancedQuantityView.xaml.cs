#region Copyright
// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2016 FortuNet, Inc.
#endregion

//US4321: (US4319) Discount based on quantity

using System;
using System.Windows;
using System.Windows.Input;
using GTI.Modules.ProductCenter.Data;
using GTI.Modules.Shared.Business;

// ReSharper disable once CheckNamespace
namespace GTI.Modules.ProductCenter.UI.Discounts
{
    /// <summary>
    /// Interaction logic for Bonus_Packs.xaml
    /// </summary>
    public partial class AdvancedQuantityView
    {
        #region Local Variables

        private DiscountItem m_discount;

        #endregion

        #region Constructor

        public AdvancedQuantityView()
        {
            InitializeComponent();
            InitalizeData();
        }

        #endregion

        #region Events

        public EventHandler AdvancedQuantityChanged;

        #endregion

        #region Properties

        public int BuyQuantity
        {
            get
            {
                int quantity;
                return int.TryParse(BuyTextBox.Text, out quantity) ? quantity : 0;   
            }
        }

        public int BuyPackageId
        {
            get
            {
                var buyPackage = BuyComboBox.SelectedItem as PackageItem;

                return buyPackage != null ? buyPackage.PackageId: 0;
            }
        }

        public int GetQuantity
        {
            get
            {
                int quantity;
                return int.TryParse(GetTextBox.Text, out quantity) ? quantity : 0;
            }
        }
        
        public int GetPackageId
        {
            get
            {
                var getPackage = GetComboBox.SelectedItem as PackageItem;

                return getPackage != null ? getPackage.PackageId : 0;
            }
        }

        #endregion

        #region Methods

        public void InitalizeData()
        {
            BuyComboBox.ItemsSource = PackageItems.Sorted;
            GetComboBox.ItemsSource = PackageItems.Sorted;
        }

        public void Load(DiscountItem discount)
        {
            m_discount = discount;

            if (m_discount.AdvancedQuantityDiscount.BuyQuantity < 1)
            {
                m_discount.AdvancedQuantityDiscount.BuyQuantity = 1;
            }

            if (m_discount.AdvancedQuantityDiscount.GetQuantity < 1)
            {
                m_discount.AdvancedQuantityDiscount.GetQuantity = 1;
            }

            //populate quantities
            BuyTextBox.Text = m_discount.AdvancedQuantityDiscount.BuyQuantity.ToString();
            GetTextBox.Text = m_discount.AdvancedQuantityDiscount.GetQuantity.ToString();

            var buySelectedIndex = 0;
            var getSelectedIndex = 0;
            for (int i = 0; i < BuyComboBox.Items.Count; i++)
            {
                var item = BuyComboBox.Items[i] as PackageItem;
                if (item == null)
                {
                    continue;
                }

                if (item.PackageId == discount.AdvancedQuantityDiscount.BuyPackageId)
                {
                    buySelectedIndex = i;
                }

                if (item.PackageId == discount.AdvancedQuantityDiscount.GetPackageId)
                {
                    getSelectedIndex = i;
                }

                if (getSelectedIndex != 0 && buySelectedIndex != 0)
                {
                    break;
                }
            } 

            //reset
            BuyComboBox.SelectedIndex = buySelectedIndex;
            GetComboBox.SelectedIndex = getSelectedIndex;

            EnableQuantityDiscountControls(discount.DiscountAwardType != DiscountItem.AwardTypes.Manual);
        }

        public void Save()
        {
            int buyQuantity, getQuantity;

            //(BuyComboBox.SelectedIndex = 0 || GetComboBox.SelectionBoxItem)
            if (int.TryParse(BuyTextBox.Text, out buyQuantity))
            {
                //failed to parse buy quantities
                return;
            }
            if (int.TryParse(GetTextBox.Text, out getQuantity))
            {
                //failed to parse get quantity
                return;
            }

            m_discount.AdvancedQuantityDiscount.BuyQuantity = buyQuantity;
            m_discount.AdvancedQuantityDiscount.GetQuantity= getQuantity;
        }

        internal string IsValid()
        {
            int buyQuantity, getQuantity;
            
            if (!int.TryParse(BuyTextBox.Text, out buyQuantity) || buyQuantity <= 0)
            {
                //failed to parse buy quantities
                return "Please enter a valid value for Buy Quantity";
            }
            if (!int.TryParse(GetTextBox.Text, out getQuantity) || getQuantity <= 0)
            {
                //failed to parse get quantity
                return "Please enter a valid value for Get Quantity";
            }

            return string.Empty;
        }

        internal void EnableQuantityDiscountControls(bool enable)
        {
            if (enable)
            {
                BuyGetBorder.Visibility = Visibility.Visible;
                ContentDisabledBorder.Visibility = Visibility.Hidden;
            }
            else
            {
                BuyGetBorder.Visibility = Visibility.Hidden;
                ContentDisabledBorder.Visibility = Visibility.Visible;
            }
        }

        private void RaiseEvent()
        {
            var handler = AdvancedQuantityChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textbox = sender as System.Windows.Controls.TextBox;

            //check for null
            if (textbox == null)
            {
                e.Handled = true;
                return;
            }

            //check for length
            if (textbox.Text.Length == 2)
            {
                e.Handled = true;
            }

            //check for numeric
            char c = Convert.ToChar(e.Text);
            if (!Char.IsNumber(c))
            {
                e.Handled = true;
            }

            OnPreviewTextInput(e);
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void BuyTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            RaiseEvent();
        }

        private void GetTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            RaiseEvent();
        }

        private void ComboBoxSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            RaiseEvent();
        }

        #endregion
    }
}
