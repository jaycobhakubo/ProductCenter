using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace GTI.Modules.ProductCenter.Data.TempSQL
{
    class SavedCoupon
    {
        public SavedCoupon(string CouponName,  DateTime StartDate, DateTime EndDate, decimal Value)
        {
            SqlConnection sc = new SqlConnection(Properties.Resources.SQLConnection);
            try
            {
                sc.Open();
                using (SqlCommand cmd = new SqlCommand(@"insert into dbo.Coupon (Name, StartDate, EndDate, Value)
                                                        values (@CouponName, @CouponStartDate, @CouponEndDate, @Value)  ", sc))
                {
                    cmd.Parameters.AddWithValue("@CouponName", CouponName);
                    cmd.Parameters.AddWithValue("@CouponStartDate", StartDate);
                    cmd.Parameters.AddWithValue("@CouponEndDate", EndDate);
                    cmd.Parameters.AddWithValue("@Value", Value);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {

            }
            finally
            {
                sc.Close();
            }

        }
    }
}
