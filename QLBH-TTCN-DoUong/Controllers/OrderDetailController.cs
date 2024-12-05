﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QLBH_TTCN_DoUong.DAO;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.Controllers
{
    public class OrderDetailController
    {
        OrderDetailDAO orderDetailDAO;

        public OrderDetailController()
        {
            orderDetailDAO = new OrderDetailDAO();
        }

        public List<OrderDetailModel> GetOrderDetailsByOrderID(string orderID)
        {
            return orderDetailDAO.GetOrderDetailsByOrderID(orderID);
        }
    }
}