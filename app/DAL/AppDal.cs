using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace app.DAL
{
    
    public static class AppDal
    {
        //מתוך הווב קונפיג לפי שם, במידה ויש כמה ConnectionString מביא 
        private static ConnectionStringSettings GetAppConnectionString(string sConnectionName)
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~/");
            return config.ConnectionStrings.ConnectionStrings[sConnectionName];
        }

        // עם/בלי פרמטרים DBקריאה ל
        public static DataSet SqlCommand(SqlParameter[] Params, string StoredProcedure)
        {
            using (SqlConnection con = new SqlConnection(GetAppConnectionString("AppConnString").ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = StoredProcedure;
                cmd.Parameters.AddRange(Params);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }

        }
        public static DataSet SqlCommand(string StoredProcedure)
        {
            using (SqlConnection con = new SqlConnection(GetAppConnectionString("AppConnString").ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }

        }

    }
}