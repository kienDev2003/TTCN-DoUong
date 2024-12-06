using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using QLBH_TTCN_DoUong.Controllers;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.Views.Bar
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string itemHtml = getListOrder();
            list_order.Controls.Clear();
            LiteralControl literalControl = new LiteralControl(itemHtml);
            list_order.Controls.Add(literalControl);
        }

        [WebMethod]
        public static string getListOrder()
        {
            string html = "";
            OrderController orderController = new OrderController();

            List<OrderModel> orders = new List<OrderModel>();

            orders = orderController.GetOrderNotYetServed();

            foreach (var order in orders)
            {
                string itemTemp = $"<li class=\"item\"><a href=\"orderDetails.aspx?orderID={order.OrderId}&tableID={order.TableId}\"><img src=\"./assets/img/images (1).png\"/><p>Bàn số {order.TableId}</p></a></li>";
                html += itemTemp;
            }

            return html;
        }
    }
}