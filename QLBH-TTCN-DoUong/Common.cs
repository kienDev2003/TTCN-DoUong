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
        public static string SHA_256_Hash(string input)
        {
            // Tạo một đối tượng SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                // Chuyển chuỗi đầu vào thành mảng byte sử dụng Encoding.UTF8
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                // Tính toán băm SHA-256
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                // Chuyển mảng byte thành chuỗi hexa và loại bỏ dấu "-" giữa các byte
                StringBuilder hexString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hexString.Append(b.ToString("x2"));  // Chuyển byte thành chuỗi hex với 2 ký tự
                }

                return hexString.ToString();
            }
        }

        public static string MD5_Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                // Băm chuỗi thành mảng byte
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Chuyển mảng byte thành chuỗi hex
                StringBuilder sb = new StringBuilder();
                foreach (byte b in data)
                {
                    sb.Append(b.ToString("x2"));  // "x2" giúp hiển thị dưới dạng hex
                }

                return sb.ToString();  // Trả về kết quả là chuỗi MD5
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