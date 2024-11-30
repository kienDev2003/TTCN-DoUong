using System;
using System.Collections.Generic;
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
                {"@orderPaymentMethod", order.PaymentMethodId }
            };

            int kq = dBConnection.ExecuteNonQuery("Orders_Insert",parameter);

            return kq;
        }
    }
}