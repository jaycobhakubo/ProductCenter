
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;


namespace GTI.Modules.ProductCenter.Data
{
    /*
    public struct CouponItem // Old coupon item definition. Should no longer be used. Use PlayerComp instead
    {
        /// <summary>
        /// The type of discounts the coupon can be
        /// </summary>
        public enum CouponTypes
        {
            [Description("Fixed Value")]
            FixedValue = 0,
            [Description("Alt-Price Package")]
            AltPricePackage = 1,
        }

        public int CouponID;
        public string Name;
        public DateTime StartDate;
        public DateTime EndDate;
        public decimal Value;
        public bool IsTaxed;//Unused.
        public int CouponMaxUsage;
        public bool isExpired;
        public DateTime? LastAwardedDate;
        public CouponTypes CouponType { get; set; }
        /// <summary>
        /// If the Coupon is an "alt price" type, then this is the package that the coupon applies to.
        /// </summary>
        public int PackageID { get; set; }
        /// <summary>
        /// The short name display of this coupon. Note: not currently used. Please update this comment if that changes
        /// </summary>
        public string ShortName { get; set; }

    }

    //Ignore all code below this line. 
    //---------------------------------------------------------------------------

    public class LCouponItem
    {
        public static List<CouponItem> lCouponItem = new List<CouponItem>();
    }

    public class LCouponItem2
    {
        public static List<CouponItem> lCouponItem = new List<CouponItem>();
    }


    public class CouponSData
    {
        //public string NameTest;
        public int cID { get; set; }
        public string cName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Value { get; set; }
    }

    public class LCouponData
    {
        public static BindingList<CouponSData> lCouponData = new BindingList<CouponSData>();
    }


    class GetCoupon
    {
        public BindingList<CouponSData> getCoupon()
        {
            SqlConnection sc = new SqlConnection(Properties.Resources.SQLConnection);
            try
            {
                LCouponData.lCouponData.Clear();
                sc.Open();
                using (SqlCommand command = new SqlCommand(@"select 
                                                          Name,
                                                            StartDate, 
                                                            EndDate, 
                                                            Value                                                  
                                                            from dbo.coupon", sc))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        CouponSData csd = new CouponSData();
                        csd.cName = reader.GetString(0);
                        csd.StartDate = reader.GetDateTime(1);
                        csd.EndDate = reader.GetDateTime(2);
                        csd.Value = reader.GetDecimal(3);
                        LCouponData.lCouponData.Add(csd);
                    }
                }
            }
            catch
            {

            }
            finally
            {
                sc.Close();
            }

            return LCouponData.lCouponData;
        }



        public static List<CouponItem> getCoupon2()
        {
            SqlConnection sc = new SqlConnection(Properties.Resources.SQLConnection);
            try
            {
                LCouponItem.lCouponItem.Clear();
                sc.Open();
                using (SqlCommand command = new SqlCommand(@"select 
                                                          Name,
                                                            StartDate, 
                                                            EndDate, 
                                                            Value                                                  
                                                            from dbo.coupon", sc))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        CouponItem csd = new CouponItem();
                        csd.Name = reader.GetString(0);
                        csd.StartDate = reader.GetDateTime(1);
                        csd.EndDate = reader.GetDateTime(2);
                        csd.Value = reader.GetDecimal(3);
                        //LCouponData.lCouponData.Add(csd);
                        LCouponItem.lCouponItem.Add(csd);
                    }
                }
            }
            catch
            {

            }
            finally
            {
                sc.Close();
            }

            return LCouponItem.lCouponItem;
        }
    }
    */
}
