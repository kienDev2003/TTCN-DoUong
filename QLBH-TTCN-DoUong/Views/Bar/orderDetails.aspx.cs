using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using QLBH_TTCN_DoUong.Controllers;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.Views.Bar
{
    public partial class orderDetails : System.Web.UI.Page
    {
        private string orderID;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int tableID = int.Parse(Request.QueryString["tableID"].ToString());
                orderID = Request.QueryString["orderID"].ToString();

                nameTable.InnerText = $"Chi tiết đơn bàn {tableID}";
                GetProductInOrderDetails(orderID);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(\"truy cap khong hop le\")</script>");
            }

        }

        private void GetProductInOrderDetails(string OrderID)
        {
            content_table.Controls.Clear();
            string contentHtml = "";

            OrderDetailController orderDetailController = new OrderDetailController();

            List<OrderDetailModel> orderDetails = orderDetailController.GetOrderDetailsByOrderID(OrderID);

            foreach(var orderDetail in  orderDetails)
            {
                ProductController productController = new ProductController();
                ProductModel product = productController.get(orderDetail.ProductId);

                string trTemp = $"<tr><th>{product.Product_Name}</th><th>{orderDetail.Quantity}</th></tr>";
                contentHtml += trTemp;
            }

            LiteralControl literalControl = new LiteralControl(contentHtml);
            content_table.Controls.Add(literalControl);
        }

        protected void btnSuccess_ServerClick(object sender, EventArgs e)
        {
            OrderController orderController = new OrderController();

            int c = orderController.UpdateOrderServed(orderID);

            if (c >= 0) Response.Redirect("./");
        }
    }
}