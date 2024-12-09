using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QLBH_TTCN_DoUong.Controllers;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.Views.Administrator.Login
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_ServerClick(object sender, EventArgs e)
        {
            string userName = txtUserName.Value.Trim();
            string password = txtPassword.Value.Trim();

            UserController userController = new UserController();
            UserModel user = new UserModel();

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                string script = Common.Create_noti_chain("UserName or Password not Emty!", "error");
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }
            else
            { 
                user = userController.Login(userName, password);
                if (string.IsNullOrEmpty(user.fullName))
                {
                    string script = Common.Create_noti_chain("Sai UserName or Password !", "error");
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                    return;
                }
                
                Session["login"] = user;
                Response.Redirect("../home/revenue-report/");
            }
            
        }
    }
}