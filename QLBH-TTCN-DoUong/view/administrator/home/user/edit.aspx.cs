using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using QLBH_TTCN_DoUong.Controllers;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.view.administrator.home.user
{
    public partial class edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRole();
            }
        }

        private void LoadRole()
        {
            List<RoleModel> roles = new List<RoleModel>();
            RoleController roleController = new RoleController();

            roles = roleController.GetRoleList();

            for (int i = 0; i < roles.Count; i++)
            {
                cboRole.Items.Add(new ListItem($"{roles[i].Name}", $"{roles[i].Id}"));
            }
        }

        [WebMethod]
        public static bool Insert(UserModel user)
        {
            UserController userController = new UserController();

            return userController.Register(user);
        }

        [WebMethod]
        public static bool Update(UserModel user)
        {
            UserController userController = new UserController();

            return userController.Update(user);
        }

        [WebMethod]
        public static UserModel Get(int userID)
        {
            UserController userController = new UserController();

            return userController.Get(userID);
        }
    }
}