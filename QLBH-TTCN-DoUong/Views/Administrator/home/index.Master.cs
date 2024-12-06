using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.Views.Administrator.home
{
    public partial class index : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //if (Session["login"] == null) Response.Redirect("../../login/");
                //else
                //{
                //    UserModel user = Session["login"] as UserModel;
                //    nameUserLogin.InnerText = user.fullName;
                //}
            }
        }

        protected void btnLogout_ServerClick(object sender, EventArgs e)
        {
            if (Session["login"] != null) Session["login"] = null;
            Response.Redirect("../../login/");
        }
    }
}