using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace BusinessTier
{
    class DBHandling
    {

        #region Get Connection
        public static SqlConnection GetConnection()
        {
            SqlConnection Con = new SqlConnection();
            string SConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["QueensUniversityStudentRegistration"].ConnectionString;
            if (SConnectionString != null)
            {
                Con = new SqlConnection(SConnectionString);
            }
            return Con;
        } 
        #endregion
    }
}
