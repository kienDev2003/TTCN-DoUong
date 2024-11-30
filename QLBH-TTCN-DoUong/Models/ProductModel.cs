using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_TTCN_DoUong.Models
{
    public class ProductModel
    {
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_Describe { get; set; }
        public float Product_Price { get; set; }
        public int Product_Categori { get; set; }
        public bool Product_Availability {  get; set; }
        public string Product_Image_Url { get; set; }
    }
}