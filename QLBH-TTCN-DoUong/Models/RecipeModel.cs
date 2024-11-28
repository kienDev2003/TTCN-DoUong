using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_TTCN_DoUong.Models
{
    public class RecipeModel
    {
        public int recipeID { get; set; }
        public int productID { get; set; }
        public int ingredientID { get; set; }
        public int recipeMaterialQty {  get; set; }

    }
}