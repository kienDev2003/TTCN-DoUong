using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QLBH_TTCN
{
    public class DBConnection
    {
        private string strConn;
        private SqlConnection conn;

        public DBConnection()
        {
            strConn = ConfigurationManager.ConnectionStrings["strConn_local"].ConnectionString;
            conn = new SqlConnection(strConn);
            conn.Open();
        }

        public SqlConnection closeConn()
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            return conn;
        }

        public int ExecuteNonQuery(string procedureName, Dictionary<string, object> parameters)
        {
            if(conn.State == ConnectionState.Closed) conn.Open();
            using (SqlCommand cmd = new SqlCommand(procedureName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                //Thêm các tham số vào command
                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }

                return cmd.ExecuteNonQuery();
            }
        }
        public SqlDataReader ExecuteReader(string procedureName, Dictionary<string, object> parameters)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            using (SqlCommand cmd = new SqlCommand(procedureName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                
                if(parameters == null) return cmd.ExecuteReader(CommandBehavior.CloseConnection);

                // Thêm các tham số vào command
                foreach (var param in parameters)
                {
                    if(param.Value == null) cmd.Parameters.AddWithValue(param.Key, DBNull.Value);
                    else cmd.Parameters.AddWithValue(param.Key, param.Value);
                }
                return cmd.ExecuteReader();
            }
        }
    }
}