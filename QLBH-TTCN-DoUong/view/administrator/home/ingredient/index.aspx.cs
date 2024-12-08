using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using QLBH_TTCN_DoUong.Controllers;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.view.administrator.home.ingredient
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<IngredientModel> GetIngredient()
        {
            IngredientsController ingredientsController = new IngredientsController();

            return ingredientsController.Gets();
        }

        [WebMethod]
        public static bool DeleteIngredient(int ingredientID)
        {
            IngredientsController ingredientsController = new IngredientsController();

            return ingredientsController.Delete(ingredientID);
        }

        [WebMethod]
        public static List<IngredientModel> SearchIngredientByName(string name)
        {
            IngredientsController ingredientsController = new IngredientsController();

            return ingredientsController.SearchIngredientByName(name);
        }
    }
}