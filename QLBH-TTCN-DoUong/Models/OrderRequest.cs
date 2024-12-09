using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_TTCN_DoUong.Models
{
    public class OrderRequest
    {
        public float TotalPrice { get; set; }
        public List<OrderDetailModel> OrderDetails { get; set; }
    }
}