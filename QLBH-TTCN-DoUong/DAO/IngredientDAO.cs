using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;
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
                    ingredient.ingredienPrice = float.Parse(dataReader["Ingredient_Price"].ToString());
                    ingredient.ingredientUnitID = int.Parse(dataReader["Unit_ID"].ToString());
                    ingredient.ingredientUnitName = dataReader["Unit_Name"].ToString();

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
                    ingredient.ingredienPrice = float.Parse(dataReader["Ingredient_Price"].ToString());
                    ingredient.ingredientUnitID = int.Parse(dataReader["Unit_ID"].ToString());
                    ingredient.ingredientUnitName = dataReader["Unit_Name"].ToString();
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

        public List<IngredientModel> SearchIngredientByName(string name)
        {
            List<IngredientModel> ingredients = new List<IngredientModel>();
            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                {"@ingredientName",name }
            };
            using(SqlDataReader dataReader = dBConnection.ExecuteReader("Ingredient_Search_By_Name", parameter))
            {
                while(dataReader.Read())
                {
                    IngredientModel ingredient = new IngredientModel();

                    ingredient.ingredientID = int.Parse(dataReader["Ingredient_ID"].ToString());
                    ingredient.ingredientName = dataReader["Ingredient_Name"].ToString();
                    ingredient.ingredientQuantity = int.Parse(dataReader["Ingredient_Quantity"].ToString());
                    ingredient.ingredienPrice = float.Parse(dataReader["Ingredient_Price"].ToString());
                    ingredient.ingredientUnitID = int.Parse(dataReader["Unit_ID"].ToString());
                    ingredient.ingredientUnitName = dataReader["Unit_Name"].ToString();

                    ingredients.Add(ingredient);
                }
            }
            return ingredients;
        }

        public int Delete(int ingredientID)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                {"@ingredient_ID",ingredientID }
            };

            return dBConnection.ExecuteNonQuery("Ingredients_Delete", parameter);
        }

        public int Add(IngredientModel ingredient)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                {"@ingredient_Name",ingredient.ingredientName },
                {"@ingredient_Quantity", ingredient.ingredientQuantity },
                {"@ingredient_Price", ingredient.ingredienPrice },
                {"@ingredient_Unit", ingredient.ingredientUnitID }
            };

            return dBConnection.ExecuteNonQuery("Ingredients_Insert", parameter);
        }

        public int Update(IngredientModel ingredient)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                {"@ingredient_ID", ingredient.ingredientID },
                {"@ingredient_Name",ingredient.ingredientName },
                {"@ingredient_Quantity", ingredient.ingredientQuantity },
                {"@ingredient_Price", ingredient.ingredienPrice },
                {"@ingredient_Unit", ingredient.ingredientUnitID }
            };

            return dBConnection.ExecuteNonQuery("Ingredients_Update", parameter);
        }
    }
}