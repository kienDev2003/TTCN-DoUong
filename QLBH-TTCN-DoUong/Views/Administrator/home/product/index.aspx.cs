using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using QLBH_TTCN_DoUong.Controllers;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.Views.Administrator.home.product
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<ProductModel> GetProducts()
        {
            ProductController productController = new ProductController();
            return productController.GetProductAll();
        }

        [WebMethod]
        public static List<ProductModel> SearchProductByName(string name)
        {
            ProductController productController = new ProductController();
            return productController.SearchProductByName(name);
        }

        [WebMethod]
        public static bool DeleteProduct(int productID)
        {
            ProductController productController = new ProductController();
            return productController.Delete(productID);
        }
    }
}