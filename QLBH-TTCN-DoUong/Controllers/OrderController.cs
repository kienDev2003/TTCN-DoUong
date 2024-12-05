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

        public List<OrderModel> GetOrderNotYetServed()
        {
            return orderDAO.GetOrderNotYetServed();
        }

        public bool UpdateOrderServed(string orderID)
        {
            int exec = orderDAO.UpdateStatusServedOrder(orderID);

            if (exec > 0) return true;
            return false;
        }

        public bool UpdateStatusPayment(string orderID)
        {
            int exec = orderDAO.UpdateStatusPayment(orderID);

            if(exec > 0) return true;
            return false;
        }

        public bool Insert(OrderModel order)
        {
            int exec = orderDAO.Insert(order);

            if (exec > 0) return true;
            return false;
        }

        public List<OrderModel> GetOrderNotYetPayment()
        {
            return orderDAO.GetOrderNotYetPayment();
        }
    }
}