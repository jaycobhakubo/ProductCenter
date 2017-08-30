using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GTI.Modules.ProductCenter.Data;

namespace GTI.Modules.ProductCenter.UI.New_Discount
{
    class ProductItemList_
    {
            
          ObservableCollection<WrappedProduct> _Products = new ObservableCollection<WrappedProduct>();

          public ProductItemList_()
        {
           
              foreach (ProductItemList x in ProductCenterMdiForm.m_listProductItem)
              {
                  _Products.Add(new WrappedProduct(new Product { ProductID = x.ProductItemId, ProductName = x.ProductItemName}));
              }        
        }

        public ObservableCollection<WrappedProduct> Products { get { return _Products; } }

    }

    public class WrappedProduct
    {
        public bool IsSelected { get; set; }
        public Product TheProduct { get; set; }

        public WrappedProduct(Product p)
        {
            TheProduct = p;
            IsSelected = false;
        }
    }


    public class Product
    {
        private string productName;
        private Int32 productID;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        public Int32 ProductID
        {
            get { return productID; }
            set { productID = value; }
        }
    }

}