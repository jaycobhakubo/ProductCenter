using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GTI.Modules.ProductCenter.Data
{

    /// <summary>
    /// This script is being replaced with SetCompAwardedToPlayer.cs.
    /// Direct communication between application and database.
    /// </summary>
    class SetCompAwardedToGroupPlayer
    {
        private int m_defID;
        private int m_CompiD;
        private int m_MaxCompUsage;
        private int m_OperatorID;


        public int OperatorID
        {
            get { return m_OperatorID; }
            set { m_OperatorID = value; }
        }

        public int defID 
        {
            get { return m_defID; }
            set { m_defID = value; }
        }

        public int CompID
        {
            get { return m_CompiD; }
            set { m_CompiD = value; }
        }

        public int MaxCompUsage
        {
            get { return m_MaxCompUsage; }
            set { m_MaxCompUsage = value; }
        }


        public void AwardgroupSQL()
        {
            SqlConnection sc = new SqlConnection(Properties.Resources.SQLConnection);

            

            try
            {
                sc.Open();
                using (SqlCommand cmd = new SqlCommand(@"exec spComps_AwardGroupPlayerComps @spDefID = @DefID, @spCompiD = @CompiD, @spCompUsage = @CompUsage , @spOperatorID = @OperatorID", sc))
                {
                    cmd.Parameters.AddWithValue("DefID", m_defID);
                    cmd.Parameters.AddWithValue("CompiD", m_CompiD);
                    cmd.Parameters.AddWithValue("CompUsage", m_MaxCompUsage);
                    cmd.Parameters.AddWithValue("OperatorID", m_OperatorID);
                     cmd.ExecuteScalar();
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
