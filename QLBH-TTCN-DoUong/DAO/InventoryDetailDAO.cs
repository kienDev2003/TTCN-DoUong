using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using QLBH_TTCN;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.DAO
{
    public class InventoryDetailDAO
    {
        DBConnection dBConnection;

        public InventoryDetailDAO()
        {
            dBConnection = new DBConnection();
        }

        public int Add(InventoryDetailModel inventoryDetail)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                {"@inventoryID", inventoryDetail.InventoryID },
                {"@ingredientID", inventoryDetail.IngredientID },
                {"@numberOfSystem", inventoryDetail.NumberOfSystem },
                {"@actualQuantity", inventoryDetail.ActualQuantity }
            };

            return dBConnection.ExecuteNonQuery("InventoryDetail_Insert", parameter);
        }

        public List<InventoryDetailModel> GetAll()
        {
            List<InventoryDetailModel> inventoryDetailModels = new List<InventoryDetailModel>();

            using(SqlDataReader dataReader = dBConnection.ExecuteReader("Inventory_Select_All", null))
            {
                while (dataReader.Read())
                {
                    InventoryDetailModel inventoryDetailModel = new InventoryDetailModel();

                    inventoryDetailModel.Ingredient_Name = dataReader["Ingredient_Name"].ToString();
                    inventoryDetailModel.NumberOfSystem = int.Parse(dataReader["NumberOfSystem"].ToString());
                    inventoryDetailModel.ActualQuantity = int.Parse(dataReader["ActualQuantity"].ToString());

                    inventoryDetailModels.Add(inventoryDetailModel);
                }
            }
            return inventoryDetailModels;
        }
    }
}