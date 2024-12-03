using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using QLBH_TTCN;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.DAO
{
    public class IngredientDAO
    {
        DBConnection dBConnection;

        public IngredientDAO()
        {
            dBConnection = new DBConnection();
        }

        public List<IngredientModel> getList()
        {
            List<IngredientModel> listIngredient = new List<IngredientModel>();
            using (SqlDataReader dataReader = dBConnection.ExecuteReader("Ingredients_Select", null))
            {
                while (dataReader.Read())
                {
                    IngredientModel ingredient = new IngredientModel();

                    ingredient.ingredientID = int.Parse(dataReader["Ingredient_ID"].ToString());
                    ingredient.ingredientName = dataReader["Ingredient_Name"].ToString();
                    ingredient.ingredientQuantity = int.Parse(dataReader["Ingredient_Quantity"].ToString());
                    ingredient.ingredientUnitID = int.Parse(dataReader["Ingredient_Unit"].ToString());

                    listIngredient.Add(ingredient);
                }
            }
            return listIngredient;
        }

        public IngredientModel getByIngredientID(int id)
        {
            IngredientModel ingredient = new IngredientModel();

            Dictionary<string, object> prameter = new Dictionary<string, object>()
            {
                { "@ingredient_ID",id }
            };

            using(SqlDataReader dataReader = dBConnection.ExecuteReader("Ingredients_Select_By_ID", prameter))
            {
                if (dataReader.Read())
                {
                    ingredient.ingredientID = int.Parse(dataReader["Ingredient_ID"].ToString());
                    ingredient.ingredientName = dataReader["Ingredient_Name"].ToString();
                    ingredient.ingredientQuantity = int.Parse(dataReader["Ingredient_Quantity"].ToString());
                    ingredient.ingredientUnitID = int.Parse(dataReader["Ingredient_Unit"].ToString());
                }
            }
            return ingredient;
        }

        public int UpdateQuantity(int id,int quantity)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                {"@ingredient_ID",id },
                {"@ingredient_Quantity", quantity}
            };

            int check = dBConnection.ExecuteNonQuery("Ingredients_UpdateQuantity", parameter);
            return check;
        }
    }
}