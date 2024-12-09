using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using QLBH_TTCN_DoUong.Controllers;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.view.administrator.home.pay
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        [WebMethod]
        public static List<OrderModel> GetOrderNotPay()
        {
            OrderController orderController = new OrderController();

            return orderController.GetOrderNotYetPayment();
        }

        [WebMethod]
        public static bool Pay(string orderID)
        {
            OrderController orderController = new OrderController();

            return orderController.UpdateStatusPayment(orderID);
        }
    }
}