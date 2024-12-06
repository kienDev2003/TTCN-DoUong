using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using QLBH_TTCN;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.DAO
{
    public class OrderDetailDAO
    {
        DBConnection dBConnection;

        public OrderDetailDAO()
        {
            dBConnection = new DBConnection();
        }

        public int AddOrderDetails(OrderDetailModel orderDetail)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>()
                {
                    {"@orderID", orderDetail.OrderId},
                    {"@productID", orderDetail.ProductId },
                    {"@quantity", orderDetail.Quantity },
                    {"@totalMoney", orderDetail.TotalPrice }
                };

            return dBConnection.ExecuteNonQuery("OrderDetails_Insert", parameter);

        }

        public List<OrderDetailModel> GetOrderDetailsByOrderID(string orderID)
        {
            List<OrderDetailModel> orderDetailModels = new List<OrderDetailModel>();

            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                {"@orderID",orderID }
            };

            using (SqlDataReader dataReader = dBConnection.ExecuteReader("OrderDetails_Select_By_OrderID", parameter))
            {
                while (dataReader.Read())
                {
                    OrderDetailModel orderDetailModel = new OrderDetailModel();

                    orderDetailModel.ProductId = int.Parse(dataReader["Product_ID"].ToString());
                    orderDetailModel.Quantity = int.Parse(dataReader["Quantity"].ToString());

                    orderDetailModels.Add(orderDetailModel);
                }
            }

            return orderDetailModels;
        }
    }
}