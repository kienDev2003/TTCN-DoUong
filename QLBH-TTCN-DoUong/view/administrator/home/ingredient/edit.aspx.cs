using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QLBH_TTCN_DoUong.Controllers;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.view.administrator.home.ingredient
{
    public partial class edit : System.Web.UI.Page
    {
        int ingredientID = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ingredientID"] != null) ingredientID = int.Parse(Request.QueryString["ingredientID"]);
            if (!IsPostBack)
            {
                LoadCboUnit();
                if (ingredientID > 0) LoadDataToUpdate(ingredientID);
            }
        }

        private void LoadCboUnit()
        {
            List<UnitModel> units = new List<UnitModel>();
            UnitController unitController = new UnitController();

            units = unitController.Gets();

            foreach (UnitModel unit in units)
            {
                ingredientCboUnit.Items.Add(new ListItem(unit.UnitName, unit.UnitID.ToString()));
            }
        }

        private void LoadDataToUpdate(int ingredientID)
        {
            IngredientModel ingredient = new IngredientModel();
            IngredientsController ingredientsController = new IngredientsController();

            ingredient = ingredientsController.GetByID(ingredientID);

            txtIngredients_Name.Value = ingredient.ingredientName;
            txtIngredients_Price.Value = ingredient.ingredienPrice.ToString();
            txtIngredients_Quantity.Value = ingredient.ingredientQuantity.ToString();
            ingredientCboUnit.SelectedValue = ingredient.ingredientUnitID.ToString();

        }

        protected void btnCRUD_ServerClick(object sender, EventArgs e)
        {
            IngredientModel ingredient = new IngredientModel();

            ingredient.ingredientName = txtIngredients_Name.Value;
            ingredient.ingredienPrice = float.Parse(txtIngredients_Price.Value);
            ingredient.ingredientQuantity = int.Parse(txtIngredients_Quantity.Value);
            ingredient.ingredientUnitID = int.Parse(ingredientCboUnit.SelectedValue);

            if (ingredientID < 0)
            {
                Add(ingredient);
            }
            else
            {
                ingredient.ingredientID = ingredientID;
                Update(ingredient);
            }
        }

        private void Update(IngredientModel ingredient)
        {
            IngredientsController ingredientsController = new IngredientsController();
            bool exec = ingredientsController.Update(ingredient);

            if (exec)
            {
                string script = "Swal.fire({title: 'Thông báo!', text: '', icon: 'success', confirmButtonText: 'OK'}).then((result) => {if (result.isConfirmed) { window.location.href = './'; } });";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }
        }

        private void Add(IngredientModel ingredient)
        {
            IngredientsController ingredientsController = new IngredientsController();
            bool exec = ingredientsController.Add(ingredient);

            if (exec)
            {
                string script = "Swal.fire({title: 'Thông báo!', text: '', icon: 'success', confirmButtonText: 'OK'}).then((result) => {if (result.isConfirmed) { window.location.href = './'; } });";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }
        }
    }
}