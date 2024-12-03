using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QLBH_TTCN_DoUong.DAO;

namespace QLBH_TTCN_DoUong.Controllers
{
    public class IngredientsController
    {
        IngredientDAO ingredientDAO;

        public IngredientsController()
        {
            ingredientDAO = new IngredientDAO();
        }

        //public int Detele(int id)
        //{
        //    //return ingredientDAO.UpdateQuantity(id);
        //}
    }
}