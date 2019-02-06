using System;
using System.Collections.Generic;
using GTI.Modules.Shared;
//using System.Linq;
//using System.Text;

namespace GTI.Modules.ProductCenter.Data
{
    /// <summary>
    /// Run the message to get the coupon item and order it by name.
    /// </summary>
   public static  class CouponItems
    {
       public static int OperatorID;
       public static int CompID;

       public static List<PlayerComp> Sorted(string couponName)
        {
            var srtlist = GetCompMessage.RunMessage();
            srtlist.Sort((x, y) => x.Name.CompareTo(y.Name)); 
            return srtlist;
        }

       public static List<PlayerComp> NameFilteredBy(string couponName, string searchString)
        {
            var srtlist = Sorted(couponName);
            return !string.IsNullOrEmpty(searchString)
                     ? srtlist.FindAll(item => item.Name.ToUpper().Contains(searchString.ToUpper()))
                     : srtlist;
        }
    }

   public class CouponItemsException : Exception
   {
       public CouponItemsException()
       {
       }
       public CouponItemsException(string message)
           : base(message)
       {
       }
       public CouponItemsException(string message, Exception inner)
           : base(message, inner)
       {
       }
   }
}
