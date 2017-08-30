using System.Collections.Generic;
using GTI.Modules.ProductCenter.Data;
using GTI.Modules.Shared.Business;
using GTI.Modules.ProductCenter.Business;

// ReSharper disable once CheckNamespace
namespace GTI.Modules.ProductCenter.UI.Discounts
{
    /// <summary>
    /// Interaction logic for Restriction.xaml
    /// </summary>
    public partial class RestrictionView
    {
        #region PRIVATE MEMBERS
                
        private Dictionary<int, string> _productItems;
        private Dictionary<int, string> _packageItems; // US4942
        private ProductCenterSettings m_productCenterSettings;

        #endregion

        #region PUBLIC PROPERTIES

        public Dictionary<int, string> SelectedProducts
        {
            get
            {
                return MultiSelectComboBoxProductItem.SelectedItems ?? new Dictionary<int, string>();
            }
        }

        //US4942
        public Dictionary<int, string> SelectedPackages
        {
            get
            {
                return MultiSelectComboBoxPackageItem.SelectedItems ?? new Dictionary<int, string>();
            }
        }

        // US4942
        public bool IgnoreValidationsForPackages
        {
            get { return chkBxIgnoreValidations.IsChecked ?? false; }
            set { chkBxIgnoreValidations.IsChecked = value; } 
        }
        #endregion

        #region CONSTRUCTOR
        public RestrictionView(ProductCenterSettings settings)
        {
            m_productCenterSettings = settings;
            InitializeComponent();

            if (!m_productCenterSettings.EnableValidation) // Hide if no validation
                chkBxIgnoreValidations.Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion

        #region METHODS

        /// <summary>
        /// Resets this control to its defaults
        /// </summary>
        public void ResetControl()
        {
            MultiSelectComboBoxProductItem.SetToEmpty();
            MultiSelectComboBoxProductItem.UncheckAllItems();

            MultiSelectComboBoxPackageItem.SetToEmpty();
            MultiSelectComboBoxPackageItem.UncheckAllItems();
        }

        /// <summary>
        /// Loads the available products into the UI
        /// </summary>
        /// <param name="packageItems"></param>
        public void LoadProductItems(List<ProductItemList> productItems)
        {
            _productItems = new Dictionary<int, string>();

            foreach (ProductItemList product in productItems)
            {
                if (product.ProductItemId > 0)
                {
                    _productItems.Add(product.ProductItemId, product.ProductItemName);
                }
            }

            MultiSelectComboBoxProductItem.ItemsSource = _productItems;
        }

        /// <summary>
        /// Updates the list of checked restricted products to match the sent in list
        /// </summary>
        /// <param name="productIDs"></param>
        public void SetRestrictedProducts(List<int> productIDs)
        {
            Dictionary<int, string> selectedProducts = new Dictionary<int, string>();

            if (productIDs != null)
            {
                foreach (int productID in productIDs)
                {
                    if (_productItems.ContainsKey(productID))
                        selectedProducts.Add(productID, _productItems[productID]);
                }
            }

            MultiSelectComboBoxProductItem.SelectedItems = selectedProducts;
        }

        /// US4942
        /// <summary>
        /// Loads the available packages into the UI
        /// </summary>
        /// <param name="packageItems"></param>
        public void LoadPackageItems(List<PackageItem> packageItems)
        {
            _packageItems = new Dictionary<int, string>();

            foreach (PackageItem package in packageItems)
            {
                if (package.PackageId > 0)
                {
                    _packageItems.Add(package.PackageId, package.PackageName);
                }
            }

            MultiSelectComboBoxPackageItem.ItemsSource = _packageItems;
        }

        /// US4942
        /// <summary>
        /// Updates the list of checked restricted products to match the sent in list
        /// </summary>
        /// <param name="ProductID"></param>
        public void SetRestrictedPackages(List<int> packageIDs)
        {
            Dictionary<int, string> selectedPackages = new Dictionary<int, string>();

            if (packageIDs != null)
            {
                foreach (int packageID in packageIDs)
                {
                    if (_packageItems.ContainsKey(packageID))
                        selectedPackages.Add(packageID, _packageItems[packageID]);
                }
            }

            MultiSelectComboBoxPackageItem.SelectedItems = selectedPackages;
        }

        #endregion


    }
}
