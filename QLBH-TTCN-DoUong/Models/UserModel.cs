using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_TTCN_DoUong.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string fullName {  get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public int roleId { get; set; }
    }
}