using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using QLBH_TTCN;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.DAO
{
    public class InventoryDAO
    {
        DBConnection dBConnection;

        public InventoryDAO()
        {
            dBConnection = new DBConnection();
        }

        public int Add(InventoryModel inventory)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                {"@inventoryID", inventory.Id },
                {"@userID", inventory.UserID},
                {"@inventoryDate", inventory.InventoryDate}
            };

            return dBConnection.ExecuteNonQuery("Inventory_Insert", parameter);
        }

        public List<InventoryModel> GetAll()
        {
            List<InventoryModel> inventories = new List<InventoryModel>();

            using(SqlDataReader dataReader = dBConnection.ExecuteReader("Inventory_Select_All", null))
            {
                while (dataReader.Read())
                {
                    InventoryModel inventoryModel = new InventoryModel();

                    inventoryModel.UserName = dataReader["User_FullName"].ToString();
                    inventoryModel.Id = dataReader["Inventory_ID"].ToString();
                    inventoryModel.InventoryDate = dataReader["Inventory_Date"].ToString();

                    inventories.Add(inventoryModel);
                }
            }
            return inventories;
        }

        public List<InventoryModel> SearchByUserName(string userName)
        {
            List<InventoryModel> inventories = new List<InventoryModel>();
            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                {"@userName", userName }
            };
            using (SqlDataReader dataReader = dBConnection.ExecuteReader("Inventory_Select_By_UserName", parameter))
            {
                while (dataReader.Read())
                {
                    InventoryModel inventoryModel = new InventoryModel();

                    inventoryModel.UserName = dataReader["User_FullName"].ToString();
                    inventoryModel.Id = dataReader["Inventory_ID"].ToString();
                    inventoryModel.InventoryDate = dataReader["Inventory_Date"].ToString();

                    inventories.Add(inventoryModel);
                }
            }
            return inventories;
        }
    }
}