﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
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
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["tableid"] == null)
                    {
                        string scriptNoti = Common.Create_noti_chain("Không có số bàn. Truy cập bị từ chối", "error");
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", scriptNoti, true);
                        return;
                    }
                    int tableId = int.Parse(Request.QueryString["tableid"]);

                    if (tableId >= 0)
                    {
                        Session["tableID"] = tableId;
                        LoadCategori();
                        LoadProduct();
                    }
                }
                catch (Exception ex)
                {
                    string scriptNoti = ex.Message;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", scriptNoti, true);
                }
            }
        }

        private void LoadProduct()
        {
            content_container.Controls.Clear();

            ProductController productController = new ProductController();
            string html = productController.GetProductAndCategoriHtml();

            LiteralControl literalControl = new LiteralControl(html);
            content_container.Controls.Add(literalControl);
        }

        private void LoadCategori()
        {
            nav.Controls.Clear();

            CategoriController categoriController = new CategoriController();
            string html = categoriController.Get();

            LiteralControl literalControl = new LiteralControl(html);
            nav.Controls.Add(literalControl);
        }

        [WebMethod]
        public static bool CheckRawMaterial(int productId, int quantity)
        {
            ProductController productController = new ProductController();

            if (productController.RawMaterial(productId, quantity)) return true;
            return false;
        }
    }
}