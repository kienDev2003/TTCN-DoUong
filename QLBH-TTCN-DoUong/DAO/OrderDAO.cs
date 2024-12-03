using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using QLBH_TTCN;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.DAO
{
    public class OrderDAO
    {
        DBConnection dBConnection;

        public OrderDAO()
        {
            dBConnection = new DBConnection();
        }

        public int AddOrder(OrderModel order)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                {"@orderID",order.OrderId },
                {"@orderDate", order.OrderDate },
                {"@orderTableID", order.TableId},
                {"@orderTotalMoney", order.TotalAmount},
                {"@orderPaymentMethod", order.PaymentMethodId },
                {"@orderServed",false }
            };

            int kq = dBConnection.ExecuteNonQuery("Orders_Insert",parameter);

            return kq;
        }

        public List<OrderModel> GetOrderNotYetServed()
        {
            List<OrderModel> orders = new List<OrderModel>();
            using(SqlDataReader dataReader = dBConnection.ExecuteReader("Orders_Select_NotYetServed", null))
            {
                while(dataReader.Read())
                {
                    OrderModel orderModel = new OrderModel();

                    orderModel.OrderId = dataReader["Order_ID"].ToString();
                    orderModel.TableId = int.Parse(dataReader["Order_TableID"].ToString());
                    orderModel.OrderDate = dataReader["Order_Date"].ToString();

                    orders.Add(orderModel);
                }
            }
            return orders;
        }

        public int UpdateStatusServedOrder(string orderID)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                {"@orderID",orderID }
            };
            return dBConnection.ExecuteNonQuery("Order_Update_Status_Served",parameter);
        }
    }
}