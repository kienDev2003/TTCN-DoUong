using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_TTCN_DoUong.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int UnitId { get; set; }
        public int CategoriId { get; set; }
        public bool Sell {  get; set; }
        public string PicUrl { get; set; }
    }
}