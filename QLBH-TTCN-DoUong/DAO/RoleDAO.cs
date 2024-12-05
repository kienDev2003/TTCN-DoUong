using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using QLBH_TTCN;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.DAO
{
    public class RoleDAO
    {
        DBConnection dBConnection;

        public RoleDAO()
        {
            dBConnection = new DBConnection();
        }

        public List<RoleModel> Gets()
        {
            List<RoleModel> roles = new List<RoleModel>();

            using(SqlDataReader dataReader = dBConnection.ExecuteReader("Role_Select", null))
            {
                while (dataReader.Read())
                {
                    RoleModel role = new RoleModel();

                    role.Id = int.Parse(dataReader["Role_ID"].ToString());
                    role.Name = dataReader["Role_Name"].ToString();

                    roles.Add(role);
                }
            }
            return roles;
        }
    }
}