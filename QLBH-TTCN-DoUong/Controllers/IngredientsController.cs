using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QLBH_TTCN_DoUong.DAO;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.Controllers
{
    public class IngredientsController
    {
        IngredientDAO ingredientDAO;

        public IngredientsController()
        {
            ingredientDAO = new IngredientDAO();
        }

        public List<IngredientModel> SearchIngredientByName(string name)
        {
            return ingredientDAO.SearchIngredientByName(name);
        }

        public IngredientModel GetByID(int ingredientID)
        {
            return ingredientDAO.getByIngredientID(ingredientID);
        }

        public List<IngredientModel> Gets()
        {
            return ingredientDAO.getList();
        }

        public bool Delete(int ingredientID)
        {
            int exec = ingredientDAO.Delete(ingredientID);

            if (exec > 0) return true;
            return false;
        }

        public bool Update(IngredientModel ingredient)
        {
            int exec = ingredientDAO.Update(ingredient);

            if (exec > 0) return true;
            return false;
        }

        public bool Add(IngredientModel ingredient)
        {
            int exec = ingredientDAO.Add(ingredient);

            if (exec > 0) return true;
            return false;
        }
    }
}