using System.Security.Cryptography;
using System.Text;

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
    }
}