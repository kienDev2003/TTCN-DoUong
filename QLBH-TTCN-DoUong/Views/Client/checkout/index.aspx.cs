using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using QLBH_TTCN_DoUong.Controllers;
using QLBH_TTCN_DoUong.DAO;
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
                        try
                        {
                            AddOrderAndOrderDetails();
                            UpdateIngredientQuantity();
                        }
                        catch(Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
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
                    }
                    else
                    {
                        loadContentPayment(_order);
                    }
                }
                else
                {
                    string scriptNoti = $@"
                                            document.getElementById('bodyContent').innerHTML = ``;
                                            Swal.fire({{
                                                title: 'Thông báo',
                                                text: 'Từ chối truy cập !',
                                                icon: 'error',
                                                confirmButtonText: 'OK'
                                            }});
                                            ";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", scriptNoti, true);
                }
            }
        }

        private void loadContentPayment(OrderModel order)
        {
            string id_bank = ConfigurationManager.ConnectionStrings["id_bank"].ConnectionString;
            string name_bank = ConfigurationManager.ConnectionStrings["name_bank"].ConnectionString;
            string stk_bank = ConfigurationManager.ConnectionStrings["stk_bank"].ConnectionString;
            string template_bank = ConfigurationManager.ConnectionStrings["template_bank"].ConnectionString;
            string content_bank = Common.MD5_Hash(DateTime.Now.ToString("yyyyMMddHHmmssffff"));
            string totalPrice_bank = order.TotalAmount.ToString();

            string url_qr_bank = $"https://img.vietqr.io/image/{id_bank}-{stk_bank}-{template_bank}?amount={totalPrice_bank}&addInfo={content_bank}&accountName={name_bank}";
            img_qr_bank.Src = url_qr_bank;
            bank.Value = id_bank;
            nameAccount.Value = name_bank;
            account_number.Value = stk_bank;
            bank_content.Value = content_bank;
            amount.Value = totalPrice_bank;
        }

        [WebMethod]
        public static void AddOrderAndOrderDetails()
        {
            Dictionary<OrderModel, List<OrderDetailModel>> order = new Dictionary<OrderModel, List<OrderDetailModel>>();
            if (HttpContext.Current.Session["order"] != null)
            {
                order = HttpContext.Current.Session["order"] as Dictionary<OrderModel, List<OrderDetailModel>>;
            }
            OrderModel _order = new OrderModel();
            List<OrderDetailModel> _orderDetails = new List<OrderDetailModel>();

            foreach (var item in order)
            {
                _order = item.Key;
                _orderDetails = item.Value;
            }
            int check = -1;
            check = AddOrder(_order);
            if (check > 0) check = AddOrderDetails(_orderDetails);
        }

        private static int AddOrder(OrderModel order)
        {
            OrderDAO orderDAO = new OrderDAO();
            int kq = -1;
            kq = orderDAO.AddOrder(order);
            return kq;
        }

        private static int AddOrderDetails(List<OrderDetailModel> orderDetails)
        {
            OrderDetailDAO orderDetailDAO = new OrderDetailDAO();
            int kq = -1;
            kq = orderDetailDAO.AddOrderDetails(orderDetails);
            return kq;
        }

        private static int UpdateIngredientQuantity()
        {
            Dictionary<OrderModel, List<OrderDetailModel>> order = new Dictionary<OrderModel, List<OrderDetailModel>>();
            if (HttpContext.Current.Session["order"] != null)
            {
                order = HttpContext.Current.Session["order"] as Dictionary<OrderModel, List<OrderDetailModel>>;
            }
            OrderModel _order = new OrderModel();
            List<OrderDetailModel> _orderDetails = new List<OrderDetailModel>();

            foreach (var item in order)
            {
                _order = item.Key;
                _orderDetails = item.Value;
            }

            int kq = 0;
            for(int i=0;i<_orderDetails.Count;i++)
            {
                RecipeDAO recipeDAO = new RecipeDAO();
                IngredientDAO ingredientDAO = new IngredientDAO();
                List<RecipeModel> recipes = new List<RecipeModel>();

                recipes = recipeDAO.getByProductId(_orderDetails[i].ProductId);

                for(int j=0;j<recipes.Count;j++)
                {
                    RecipeModel recipe = recipes[j];
                    IngredientModel ingredient = new IngredientModel();

                    ingredient = ingredientDAO.getByIngredientID(recipe.ingredientID);

                    int lastQuantity = ingredient.ingredientQuantity - recipe.recipeMaterialQty * _orderDetails[i].Quantity;

                    int temp = ingredientDAO.UpdateQuantity(recipe.ingredientID, lastQuantity);

                    kq += temp;
                }
            }
            return kq;
        }

        [WebMethod]
        public static bool CheckPaymentBank(string content_payment, string price)
        {
            bool status_payment = false;

            string url_check_payment = $"https://script.google.com/macros/s/AKfycby7-FooKyZ9DEPjMj9DbbcpXX8V2KOyWgh9lVd6tGgfmnnulB-aFf_mZRW-NI-Ks1C9rw/exec?description={content_payment}&value={price}";
            using (WebClient webClient = new WebClient())
            {
                string status = webClient.DownloadString(url_check_payment);
                if (status.Trim().Equals("true", StringComparison.OrdinalIgnoreCase)) status_payment = true;
            }

            if (status_payment)
            {
                try
                {
                    AddOrderAndOrderDetails();
                    UpdateIngredientQuantity();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
                
            }
            return false;
        }
    }
}