using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using QLBH_TTCN;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.DAO
{
    public class PaymentMethodDAO
    {
        DBConnection dBConnection;

        public PaymentMethodDAO()
        {
            dBConnection = new DBConnection();
        }

        public List<PaymentMethodModel> get()
        {
            List<PaymentMethodModel> listPaymentmethod = new List<PaymentMethodModel>();

            using(SqlDataReader dataReader = dBConnection.ExecuteReader("PaymentMethods_Select", null))
            {
                while (dataReader.Read())
                {
                    PaymentMethodModel paymentMethod = new PaymentMethodModel();

                    paymentMethod.Id = int.Parse(dataReader["PaymentMethod_ID"].ToString());
                    paymentMethod.Name = dataReader["PaymentMethod_Name"].ToString();

                    listPaymentmethod.Add(paymentMethod);
                }
            }
            return listPaymentmethod;
        }
    }
}