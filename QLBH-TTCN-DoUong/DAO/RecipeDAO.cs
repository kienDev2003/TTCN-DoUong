using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using QLBH_TTCN;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.DAO
{
    public class RecipeDAO
    {
        DBConnection dbConnection;

        public RecipeDAO()
        {
            dbConnection = new DBConnection();
        }
        public List<RecipeModel> gets()
        {
            List<RecipeModel> listRecipe = new List<RecipeModel>();

            using (SqlDataReader dataReader = dbConnection.ExecuteReader("Recipe_Select", null))
            {
                while(dataReader.Read())
                {
                    RecipeModel recipe = new RecipeModel();

                    recipe.recipeID = int.Parse(dataReader["Recipe_ID"].ToString());
                    recipe.productID = int.Parse(dataReader["Product_ID"].ToString());
                    recipe.ingredientID = int.Parse(dataReader["Ingredient_ID"].ToString());
                    recipe.recipeMaterialQty = int.Parse(dataReader["Recipe_Materia_Qty"].ToString());

                    listRecipe.Add(recipe);
                }
            }
            return listRecipe;
        }

        public List<RecipeModel> getByProductId(int productID)
        {
            List<RecipeModel> listRecipe = new List<RecipeModel>();

            Dictionary<string, object> prameter = new Dictionary<string, object>()
            {
                {"@productID",productID }
            };

            using (SqlDataReader dataReader = dbConnection.ExecuteReader("Recipes_Select_By_ProductID", prameter))
            {
                while (dataReader.Read())
                {
                    RecipeModel recipe = new RecipeModel();

                    recipe.recipeID = int.Parse(dataReader["Recipe_ID"].ToString());
                    recipe.productID = int.Parse(dataReader["Product_ID"].ToString());
                    recipe.ingredientID = int.Parse(dataReader["Ingredient_ID"].ToString());
                    recipe.recipeMaterialQty = int.Parse(dataReader["Recipe_Material_Qty"].ToString());

                    listRecipe.Add(recipe);
                }
            }
            return listRecipe;
        }
    }
}