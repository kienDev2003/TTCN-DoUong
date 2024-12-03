using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QLBH_TTCN_DoUong.DAO;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.Controllers
{
    public class OrderController
    {
        OrderDAO orderDAO;

        public OrderController()
        {
            orderDAO = new OrderDAO();
        }

        public List<OrderModel> getOrderNotYetServed()
        {
            List<OrderModel> orderModels = new List<OrderModel>();

            orderModels = orderDAO.GetOrderNotYetServed();

            return orderModels;
        }

        public int UpdateOrderServed(string orderID)
        {
            return orderDAO.UpdateStatusServedOrder(orderID);
        }
    }
}