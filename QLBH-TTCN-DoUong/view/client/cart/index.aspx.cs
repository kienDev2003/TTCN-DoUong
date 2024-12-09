using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using QLBH_TTCN_DoUong.Controllers;
using QLBH_TTCN_DoUong.DAO;
using QLBH_TTCN_DoUong.Models;
using QLBH_TTCN_DoUong.Views.Bar;

namespace QLBH_TTCN_DoUong.Views.Client.cart
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static object checkOrder(OrderRequest orderRequest)
        {
            OrderModel order = new OrderModel();
            List<OrderDetailModel> listOrderDetail = new List<OrderDetailModel>();

            order.OrderId = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            order.OrderDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            order.TableId = int.Parse(HttpContext.Current.Session["tableID"].ToString());
            order.TotalAmount = float.Parse(orderRequest.TotalPrice.ToString());
            order.PaymentMethodId = int.Parse(ConfigurationManager.ConnectionStrings["payment_bank_id"].ConnectionString);
            order.Served = false;
            order.StatusPayment = false;

            listOrderDetail = orderRequest.OrderDetails;

            for(int i = 0; i < listOrderDetail.Count; i++)
            {
                listOrderDetail[i].OrderId = order.OrderId;
                listOrderDetail[i].TotalPrice = listOrderDetail[i].Price * listOrderDetail[i].Quantity;
            }

            for(int i = 0; i < listOrderDetail.Count; i++)
            {
                ProductController productController = new ProductController();

                bool checkRawMaterial = productController.RawMaterial(listOrderDetail[i].ProductId, listOrderDetail[i].Quantity);

                ProductModel product = new ProductModel();
                product = productController.get(listOrderDetail[i].ProductId);
                
                if (!checkRawMaterial)  
                {
                    return new
                    {
                        key = -1,
                        content = $"Sản phẩm {product.Product_Name} không đủ nguyên liệu làm {listOrderDetail[i].Quantity} cốc. Vui lòng giảm số lượng sản phẩm hoặc chọn sản phẩm khác !"
                    };
                }
            }

            AddOrderAndOrderDetails(order, listOrderDetail);
            UpdateIngredientQuantity(listOrderDetail);

            return new
            {
                key = 1,
                content = "../thanks/"
            };
        }

        private static void AddOrderAndOrderDetails(OrderModel order,List<OrderDetailModel> orderDetails)
        {
            int check = -1;
            check = AddOrder(order);
            if (check > 0) check = AddOrderDetails(orderDetails,order.OrderId);
        }

        private static int AddOrderDetails(List<OrderDetailModel> orderDetails, string orderID)
        {
            int kq = 0;
            OrderDetailController orderDetailController = new OrderDetailController();
            foreach(var orderDetail in orderDetails)
            {
                orderDetail.OrderId = orderID;
                int k = orderDetailController.Add(orderDetails);
                kq += k;
            }
            return kq;
        }

        private static int AddOrder(OrderModel order)
        {
            OrderDAO orderDAO = new OrderDAO();
            int kq = -1;
            kq = orderDAO.Insert(order);
            return kq;
        }

        private static void UpdateIngredientQuantity(List<OrderDetailModel> orderDetails)
        {
            for (int i = 0; i < orderDetails.Count; i++)
            {
                RecipeDAO recipeDAO = new RecipeDAO();
                IngredientDAO ingredientDAO = new IngredientDAO();
                List<RecipeModel> recipes = new List<RecipeModel>();

                recipes = recipeDAO.getByProductId(orderDetails[i].ProductId);

                for (int j = 0; j < recipes.Count; j++)
                {
                    RecipeModel recipe = recipes[j];
                    IngredientModel ingredient = new IngredientModel();

                    ingredient = ingredientDAO.getByIngredientID(recipe.ingredientID);

                    int lastQuantity = ingredient.ingredientQuantity - recipe.recipeMaterialQty * orderDetails[i].Quantity;

                    int temp = ingredientDAO.UpdateQuantity(recipe.ingredientID, lastQuantity);
                }
            }
        }
    }
}