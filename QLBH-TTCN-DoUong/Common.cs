using System.Collections.Generic;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong
{
    public class Common
    {
        public static string MD5Hash(string input)
        {
            // Tạo đối tượng MD5
            using (MD5 md5 = MD5.Create())
            {
                // Chuyển chuỗi đầu vào thành mảng byte
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);

                // Tính toán giá trị băm MD5
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Chuyển mảng byte kết quả thành chuỗi Hex (mã hex)
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));  // "x2" sẽ tạo ra chuỗi hex
                }

                return sb.ToString();
            }
        }

        public static string Create_noti_chain(string text, string icon)
        {
            return $"Swal.fire({{title: 'Thông báo!',text: '{text}',icon: '{icon}',confirmButtonText: 'OK'}});";
        }

        public static void AddCookieOrder(string name, List<OrderDetailModel> listOrderDetail, int time)
        {
            HttpCookie jsonCookie = new HttpCookie(name);
            jsonCookie.Expires = DateTime.Now.AddMinutes(time);
            string json = JsonConvert.SerializeObject(listOrderDetail);
            jsonCookie.Value = json;
            HttpContext.Current.Response.Cookies.Add(jsonCookie);
        }
     
        public static void RemoveAllCookie()
        {
            var cookieNames = new List<string>();
            foreach (string cookieName in HttpContext.Current.Request.Cookies)
            {
                cookieNames.Add(cookieName);
            }

            foreach (string cookieName in cookieNames)
            {
                HttpCookie cookie = new HttpCookie(cookieName);
                cookie.Expires = DateTime.Now.AddDays(-1);

                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public static List<OrderDetailModel> ReturnOrderList(string name)
        {
            List<OrderDetailModel> order_s = new List<OrderDetailModel>();
            string json = "";
            if(HttpContext.Current.Request.Cookies[name] != null) json = HttpContext.Current.Request.Cookies[name].Value;
            order_s = JsonConvert.DeserializeObject<List<OrderDetailModel>>(json);
            return order_s;
        }
    }
}