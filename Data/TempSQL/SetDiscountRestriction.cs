using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;


namespace GTI.Modules.ProductCenter.Data.TempSQL
{
    /// <summary>
    /// Use this class to bypass server message. Use for quick demo.
    /// </summary>
//    public class ProductItemRestriction
//    {
//        public int ProductID;
//        public int DiscountID;
//    }

//    class DeleteDBRestrictionProductPerDiscount
//    {
//        public static void DeleteDBRestrictionProductPerDiscount_(int discountID)
//        {
//            SqlConnection sc = new SqlConnection(Properties.Resources.SQLConnection);

//            try
//            {
//                sc.Open();
//                using (SqlCommand cmd = new SqlCommand(@"Delete from DiscountRestriction where DiscountID =  @DiscountID", sc))                                                                   
//                {
//                    cmd.Parameters.AddWithValue("DiscountID", discountID);
//                    cmd.ExecuteNonQuery();
//                }
//            }
//            catch
//            {

//            }
//            finally
//            {
//                sc.Close();
//            }
//        }
//    }

//    class SetDiscountRestriction
//    {
//        public static void SetDiscountRestriction_(int productID, int discountID)
//        {
//            SqlConnection sc = new SqlConnection(Properties.Resources.SQLConnection);
            
//            try
//            {
//                sc.Open();
//                using (SqlCommand cmd = new SqlCommand(@"spSetDiscountProductItemRestriction
//                                                        @ProductID,
//                                                        @DiscountID"
//                                                       , sc))                                                         
//                {
//                    cmd.Parameters.AddWithValue("ProductID", productID);
//                    cmd.Parameters.AddWithValue("DiscountID", discountID);
                  
//                    cmd.ExecuteNonQuery();
//                }
//            }
//            catch
//            {

//            }
//            finally
//            {
//                sc.Close();
//            }   
//        }
//    }

//    class GetDiscountRestriction
//    {
//        public static List<ProductItemRestriction> GetDiscountRestriction_(int discountID)
//        {
//            SqlConnection sc = new SqlConnection(Properties.Resources.SQLConnection);
//            List<ProductItemRestriction> Listpir = new List<ProductItemRestriction>();
//            try
//            {
//                sc.Open();
//                using (SqlCommand cmd = new SqlCommand(@"spGetDiscountProductItemRestriction
//                                                 
//                                                        @DiscountID"
//                                                       , sc))
//                {
//                    cmd.Parameters.AddWithValue("DiscountID", discountID);
//                    SqlDataReader reader = cmd.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        ProductItemRestriction pir2 = new ProductItemRestriction();
//                        pir2.ProductID = reader.GetInt32(0);
//                        pir2.DiscountID = reader.GetInt32(1);
//                        Listpir.Add(pir2);
//                    }
//                }
//            }
//            catch
//            {

//            }
//            finally
//            {
//                sc.Close();
//            }

//            return Listpir;
//        }
//    }
}
