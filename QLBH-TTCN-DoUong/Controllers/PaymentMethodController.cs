using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QLBH_TTCN_DoUong.DAO;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.Controllers
{
    public class PaymentMethodController
    {
        PaymentMethodDAO paymentMethodDAO;
        public PaymentMethodController()
        {
            paymentMethodDAO = new PaymentMethodDAO();
        }
        public List<PaymentMethodModel> get()
        {
            List<PaymentMethodModel> listPaymentMethod = new List<PaymentMethodModel>();
            listPaymentMethod = paymentMethodDAO.get();

            return listPaymentMethod;
        }
    }
}