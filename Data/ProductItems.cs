using System;
using System.Collections.Generic;
using GTI.Modules.Shared;
using GTI.Modules.ProductCenter.Business;

namespace GTI.Modules.ProductCenter.Data
{

    public static class ProductItems
    {

        public static List<ProductItemList> UnSorted(int operatorId, bool creditEnabled, bool showInactive) //RALLY DE 6809
        {
            List<ProductItemList> srtlist = GetProductItemMessage.GetProductItems(operatorId);


            if(!creditEnabled)//RALLY DE 6809
            {
                List<ProductItemList> noCreditList = new List<ProductItemList>();
                foreach(var item in srtlist)
                {
                    if(item.ProductTypeId == (int)ProductType.CreditRefundableOpen ||
                        item.ProductTypeId == (int)ProductType.CreditRefundableFixed ||
                        item.ProductTypeId == (int)ProductType.CreditNonRefundableFixed ||
                        item.ProductTypeId == (int)ProductType.CreditNonRefundableOpen &&
                        item.ProductItemId < 0) continue; //RALLY DE 6770

                    if(showInactive || item.IsActive)
                    {
                        noCreditList.Add(item);
                    }
                }
                return noCreditList;
            }
            //START RALLY DE 6770 
            List<ProductItemList> returnList = new List<ProductItemList>();
            foreach(ProductItemList productItem in srtlist)
            {
                if(productItem.ProductItemId > 0 && (showInactive || productItem.IsActive))
                {
                    returnList.Add(productItem);
                }
            }

            return returnList;
            //END RALLY DE 6770
        }

        public static List<ProductItemList> NameSorted(int operatorId, bool creditEnabled, bool showInactive)//RALLY DE 6809
        {
            List<ProductItemList> srtlist = UnSorted(operatorId, creditEnabled, showInactive);//RALLY DE 6809
            srtlist.Sort((x, y) => x.ProductItemName.CompareTo(y.ProductItemName));
            return srtlist;
        }

        public static List<ProductItemList> TypeSorted(int operatorId, bool creditEnabled, bool showInactive)
        {
            List<ProductItemList> srtlist = UnSorted(operatorId, creditEnabled, showInactive);
            srtlist.Sort((x, y) => x.ProductTypeId.CompareTo(y.ProductTypeId));
            return srtlist;
        }

        public static List<ProductItemList> NameFilteredBy(int operatorId, string searchString, bool creditEnabled, bool showInactive)
        {
            List<ProductItemList> srtlist = NameSorted(operatorId, creditEnabled, showInactive);
            return !string.IsNullOrEmpty(searchString)
                     ? srtlist.FindAll(item => item.ProductItemName.ToUpper().Contains(searchString.ToUpper()))
                     : srtlist;
        }
        //START RALLY US1796
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="productName"></param>
        /// <param name="productTypeIdString"></param>
        /// <param name="creditEnabled"></param>
        /// <returns></returns>
        internal static List<ProductItemList> SearchProducts(int operatorId, string productName, string productTypeIdString, bool creditEnabled, bool showInactive)
        {
            List<ProductItemList> srtlist = NameSorted(operatorId, creditEnabled, showInactive);
            int productTypeID;
            if(int.TryParse(productTypeIdString, out productTypeID))
            {
                //search by name only
                if(productTypeID == 0)
                {
                    if(string.IsNullOrEmpty(productName))
                    {
                        //no parameters entered (reset)
                        return srtlist;
                    }
                    else
                    {
                        return srtlist.FindAll(i => i.ProductItemName.ToUpper().Contains(productName.ToUpper()));
                    }
                }
                //search by product type only
                else if(string.IsNullOrEmpty(productName))
                {
                    return srtlist.FindAll(i => i.ProductTypeId == productTypeID);
                }
                //search by name and product type
                else
                {
                    return srtlist.FindAll(
                        i => i.ProductItemName.ToUpper().Contains(productName.ToUpper()) 
                        && i.ProductTypeId == productTypeID
                        );
                }
            }
            return srtlist;
        }
    }
    //END RALLY US1796
    public class ProductItemsException : Exception
    {
        public ProductItemsException()
        {
        }
        public ProductItemsException(string message)
            : base(message)
        {
        }
        public ProductItemsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
