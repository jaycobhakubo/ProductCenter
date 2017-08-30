using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

namespace GTI.Modules.ProductCenter.Data.TempSQL
{
    class DeleteCoupon
    {
        public int deleteCoupon(int compID)
        {
            SqlConnection sc = new SqlConnection(Properties.Resources.SQLConnection);
            int @StatusID = 1; //0 = Success; 1 = Not Success
            try
            {
                sc.Open();
                using (SqlCommand cmd = new SqlCommand(@"exec spComps_DeleteComp @CompID = @compID , @Status = @status output", sc))
                {
                    cmd.Parameters.AddWithValue("@compID", compID);

                    cmd.Parameters.Add(new SqlParameter("@status", SqlDbType.Int));
                    cmd.Parameters["@status"].Direction = ParameterDirection.Output;

                    //SqlDataReader reader = cmd.ExecuteReader();
                    //SqlDataReader reader = cmd.ExecuteReader();
                    cmd.ExecuteNonQuery();
                    //while (reader.Read())
                    //{
                    //    //You can use get or set it doesnt matter. Lets use set for this one
                    //   // StaffLogin_gs = reader.GetInt32(0);

                    //    //OutpupJCID = (int)cmd.Parameters[10].Value;
                    //}
                    //reader.Close();
                    sc.Close();
                    @StatusID = Convert.ToInt32(cmd.Parameters[1].Value);
                }
            }
            catch
            {

            }
            finally
            {
                //sc.Close();
            }
            return @StatusID;

        }
    }
}
