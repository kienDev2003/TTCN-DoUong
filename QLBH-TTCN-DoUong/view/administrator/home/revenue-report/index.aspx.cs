using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLBH_TTCN_DoUong.view.administrator.home.report
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                dtpStart.Value = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                dtpEnd.Value = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
    }
}