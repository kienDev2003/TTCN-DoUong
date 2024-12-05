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

        public int Insert(OrderModel order)
        {
            Dictionary<string, object> prameter = new Dictionary<string, object>
            {
                {"@orderID",order.OrderId },
                {"@orderTableID",order.TableId },
                {"@orderDate", order.OrderDate },
                {"@orderPrice", order.TotalAmount},
                {"@orderPaymentMethod", order.PaymentMethodId },
                {"@orderServed", order.Served },
                {"@orderStatusPayment", order.StatusPayment }
            };

            int kq = dBConnection.ExecuteNonQuery("Orders_Insert", prameter);

            return kq;
        }

        public List<OrderModel> GetOrderNotYetPayment()
        {
            List<OrderModel> orders = new List<OrderModel>();
            using (SqlDataReader dataReader = dBConnection.ExecuteReader("Orders_Select_NotYetPayment", null))
            {
                while (dataReader.Read())
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

        public int UpdateStatusPayment(string orderID)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                {"@orderID",orderID }
            };
            return dBConnection.ExecuteNonQuery("Order_Update_Status_Payment", parameter);
        }
    }
}