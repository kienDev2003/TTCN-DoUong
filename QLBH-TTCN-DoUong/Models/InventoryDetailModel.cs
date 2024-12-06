using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_TTCN_DoUong.Models
{
    public class InventoryDetailModel
    {
        public int InventoryID { get; set; }
        public int IngredientID {  get; set; }
        public string Ingredient_Name {  get; set; }
        public int NumberOfSystem { get; set; }
        public int ActualQuantity {  get; set; }
    }
}