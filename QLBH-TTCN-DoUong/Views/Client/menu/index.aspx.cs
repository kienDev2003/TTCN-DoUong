using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QLBH_TTCN_DoUong.Controllers;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.Views.Client
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void LoadProduct()
        {
            content_container.Controls.Clear();

            ProductController productController = new ProductController();
            string html = productController.GetProductAndCategoriHtml();

            LiteralControl literalControl = new LiteralControl(html);
            content_container.Controls.Add(literalControl);
        }
    }
}