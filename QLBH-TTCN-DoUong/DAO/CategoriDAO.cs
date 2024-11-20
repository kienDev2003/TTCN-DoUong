using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using QLBH_TTCN;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.DAO
{
    public class CategoriDAO
    {
        DBConnection dBConnection;
        public CategoriDAO()
        {
            dBConnection = new DBConnection();
        }

        public List<CategoriModel> Get()
        {
            List<CategoriModel> listCategori = new List<CategoriModel>();

            using(SqlDataReader reader = dBConnection.ExecuteReader("Categori_Select_All", null))
            {
                while (reader.Read())
                {
                    CategoriModel categori = new CategoriModel();
                    categori.CategoriId = int.Parse(reader["CategoriId"].ToString());
                    categori.Name = reader["Name"].ToString();

                    listCategori.Add(categori);
                }
            }
            return listCategori;
        }
    }
}