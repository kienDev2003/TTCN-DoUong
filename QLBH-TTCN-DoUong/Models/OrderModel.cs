using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_TTCN_DoUong.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalAmount { get; set; }
        public int PaymentMethodId { get; set; }

    }
}