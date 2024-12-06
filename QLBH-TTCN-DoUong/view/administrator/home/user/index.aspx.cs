using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using QLBH_TTCN_DoUong.Controllers;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.view.administrator.home.uer
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<UserModel> GetUsers()
        {
            List<UserModel> users = new List<UserModel>();
            UserController userController = new UserController();

            users = userController.Gets();

            return users;
        }

        [WebMethod]
        public static bool DeleteUser(int userID)
        {
            UserController userController = new UserController();

            return userController.Detele(userID);
        }

        [WebMethod]
        public static List<UserModel> SearchUserByName(string name)
        {
            List<UserModel> users = new List<UserModel>();
            UserController userController = new UserController();

            users = userController.SearchUserByName(name);

            return users;
        }
    }
}