using System;
using System.Collections.Generic;
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

        public int AddOrderDetails(List<OrderDetailModel> orderDetails)
        {
            int kq = 0;
            for (int i = 0; i < orderDetails.Count; i++)
            {
                Dictionary<string, object> parameter = new Dictionary<string, object>()
                {
                    {"@orderID", orderDetails[i].OrderId},
                    {"@productID", orderDetails[i].ProductId },
                    {"@quantity", orderDetails[i].Quantity },
                    {"@totalMoney", orderDetails[i].TotalPrice }
                };

                int check = dBConnection.ExecuteNonQuery("OrderDetails_Insert",parameter);
                kq += check;
            }
            return kq;
        }
    }
}