using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QLBH_TTCN_DoUong.Models;
using static System.Net.Mime.MediaTypeNames;

namespace QLBH_TTCN_DoUong.Views.Client.checkout
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["order"] != null)
                {
                    Dictionary<OrderModel, List<OrderDetailModel>> order = new Dictionary<OrderModel, List<OrderDetailModel>>();

                    order = Session["order"] as Dictionary<OrderModel, List<OrderDetailModel>>;

                    OrderModel _order = new OrderModel();
                    List<OrderDetailModel> _orderDetails = new List<OrderDetailModel>();

                    foreach (var item in order)
                    {
                        _order = item.Key;
                        _orderDetails = item.Value;
                    }

                    if (_order.PaymentMethodId != int.Parse(ConfigurationManager.ConnectionStrings["payment_bank_id"].ConnectionString))
                    {
                        bodyContent.Controls.Clear();

                        string scriptNoti = $@"
                                                document.getElementById('bodyContent').innerHTML = ``;
                                                Swal.fire({{
                                                    title: 'Thông báo',
                                                    text: 'Thanh toán thành công!',
                                                    icon: 'success',
                                                    confirmButtonText: 'OK'
                                                }}).then((result) => {{
                                                    if (result.isConfirmed) {{
                                                        window.location.href = '../thank/';
                                                    }}
                                                }});
                                               ";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", scriptNoti, true);
                        return;
                    }
                    else
                    {
                        loadContentPayment(_order);
                    }
                }
            }
        }

        private void loadContentPayment(OrderModel order)
        {
            string id_bank = ConfigurationManager.ConnectionStrings["id_bank"].ConnectionString;
            string name_bank = ConfigurationManager.ConnectionStrings["name_bank"].ConnectionString;
            string stk_bank = ConfigurationManager.ConnectionStrings["stk_bank"].ConnectionString;
            string template_bank = ConfigurationManager.ConnectionStrings["template_bank"].ConnectionString;
            string content_bank = Common.MD5Hash(DateTime.Now.ToString("yyyyMMddHHmmssffff"));

            string url_qr_bank = $"https://img.vietqr.io/image/{id_bank}-{stk_bank}-{template_bank}?amount={order.TotalAmount.ToString()}&addInfo={content_bank}&accountName={name_bank}";
            img_qr_bank.Src = url_qr_bank;
            bank.Value = id_bank;
            nameAccount.Value = name_bank;
            account_number.Value = stk_bank;
            bank_content.Value = content_bank;
            amount.Value = order.TotalAmount.ToString();
        }
    }
}