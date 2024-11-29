using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using QLBH_TTCN_DoUong.Controllers;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.Views.Client.cart
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPaymentMethod();
            }
        }

        private void LoadPaymentMethod()
        {
            PaymentMethodController paymentMethodController = new PaymentMethodController();
            List<PaymentMethodModel> optionPayment = paymentMethodController.get();

            for(int i = 0; i < optionPayment.Count; i++)
            {
                payment.Items.Add(new ListItem($"{optionPayment[i].Name}", $"{optionPayment[i].Id}"));
            }
        }

        [WebMethod]
        public static string checkOrder(OrderRequest orderRequest)
        {
            OrderModel order = new OrderModel();
            List<OrderDetailModel> listOrderDetail = new List<OrderDetailModel>();

            order.OrderId = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            order.OrderDate = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            order.PaymentMethodId = orderRequest.PaymentMethod_ID;
            order.TableId = int.Parse(HttpContext.Current.Session["tableID"].ToString());
            order.TotalAmount = float.Parse(orderRequest.TotalPrice.ToString());

            listOrderDetail = orderRequest.OrderDetails;

            for(int i = 0; i < listOrderDetail.Count; i++)
            {
                listOrderDetail[i].OrderId = order.OrderId;
                listOrderDetail[i].TotalPrice = listOrderDetail[i].Price * listOrderDetail[i].Quantity;
            }

            return "ok";
        }
    }
}