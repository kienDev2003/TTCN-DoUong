using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_TTCN_DoUong.Models
{
    public class IngredientModel
    {
        public int ingredientID { get; set; }
        public string ingredientName { get; set; }
        public int ingredientQuantity { get; set; }
        public float ingredienPrice { get; set; }
        public string ingredientUnitName { get; set; }
        public int ingredientUnitID { get; set; }
    }
}