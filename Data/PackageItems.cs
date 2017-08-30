using System;
using System.Collections.Generic;
using GTI.Modules.Shared;
using GTI.Modules.Shared.Business;


namespace GTI.Modules.ProductCenter.Data
{
    public static class PackageItems
    {
        public static List<PackageItem> Sorted
        {
            get
            {
                var srtlist = GetPackageItemMessage.GetPackageList(0);
                srtlist.Sort((x, y) => x.PackageName.CompareTo(y.PackageName));
                return srtlist;
            }
        }

        public static List<PackageItem> FilteredBy(int operatorId, string searchString, string accrualIDstring)//RALLY US1796
        {
            //START RALLY US1796    
            var sortlist = Sorted;

            List<PackageItem> accrualSortedList = new List<PackageItem>();
            int accrualID = 0; 
            
            if(int.TryParse(accrualIDstring, out accrualID))
            {
                if (accrualID > 0)
                {
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        sortlist = sortlist.FindAll(item => item.PackageName.ToUpper().Contains(searchString.ToUpper()));
                    }

                    foreach (PackageItem item in sortlist)
                    {
                        List<PackageProduct> productList = GetPackageProductMessage.GetPackageProducts(item.PackageId, operatorId);
                        foreach (PackageProduct packageProduct in productList)
                        {
                            if (packageProduct.AccrualList.Exists(i => i.Id == accrualID))
                            {
                                accrualSortedList.Add(item);
                                break;
                            }
                        }
                    }
                    return accrualSortedList;
                }
            }
            //END RALLY US1796

            return !string.IsNullOrEmpty(searchString) 
                ? sortlist.FindAll(item => item.PackageName.ToUpper().Contains(searchString.ToUpper()))
                : sortlist;
        }

        public static PackageItem GetPackageItem(int id)
        {
            if (id < 1)
                throw new PackageItemsException("Id of zero not allowed.");
            var packageList = GetPackageItemMessage.GetPackageList(id);
            if (packageList.Count > 1)
                throw new PackageItemsException("More than one package with same id.");
            return packageList[0];
        }
    }

    public class PackageItemsException : Exception
    {
        public PackageItemsException()
        {
        }
        public PackageItemsException(string message)
            : base(message)
        {
        }
        public PackageItemsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
