using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using QLBH_TTCN;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.DAO
{
    public class UnitDAO
    {
        DBConnection dBConnection;

        public UnitDAO()
        {
            dBConnection = new DBConnection();
        }

        public List<UnitModel> Gets()
        {
            List<UnitModel> units = new List<UnitModel>();

            using(SqlDataReader dataReader = dBConnection.ExecuteReader("Unit_Select_All", null))
            {
                while (dataReader.Read())
                {
                    UnitModel model = new UnitModel();

                    model.UnitID = int.Parse(dataReader["Unit_ID"].ToString());
                    model.UnitName = dataReader["Unit_Name"].ToString();

                    units.Add(model);
                }
            }
            return units;
        }
    }
}