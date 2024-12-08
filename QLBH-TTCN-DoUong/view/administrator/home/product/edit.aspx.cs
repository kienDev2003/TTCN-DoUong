using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using QLBH_TTCN_DoUong.Controllers;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.view.administrator.home.product
{
    public partial class edit : System.Web.UI.Page
    {
        int productID = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["productID"] != null) productID = int.Parse(Request.QueryString["productID"]);
            if (!IsPostBack)
            {
                LoadCaterogi();
                if (productID > 0) LoadDataToUpdate(productID);
            }
        }

        private void LoadCaterogi()
        {
            List<CategoriModel> categoris = new List<CategoriModel>();
            CategoriController categoriController = new CategoriController();

            categoris = categoriController.GetList();

            foreach (var category in categoris)
            {
                cboCategori.Items.Add(new ListItem(category.Name, category.CategoriId.ToString()));
            }
        }

        public void Add(ProductModel product)
        {
            ProductController productController = new ProductController();
            bool exec = productController.Add(product);

            if (exec)
            {
                string script = "Swal.fire({title: 'Thông báo!', text: '', icon: 'success', confirmButtonText: 'OK'}).then((result) => {if (result.isConfirmed) { window.location.href = './'; } });";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }
        }

        private string SaveImageFile()
        {
            if (fileImageProduct.HasFile)
            {
                string fileName = Path.GetFileName(fileImageProduct.PostedFile.FileName);
                string filePath = Server.MapPath("../../../imageproduct/") + fileName;
                fileImageProduct.SaveAs(filePath);
                return "../../../imageproduct/" + fileName;
            }
            return string.Empty;
        }

        private void DeleteImage(int productID)
        {
            ProductModel product = new ProductModel();
            ProductController productController = new ProductController();

            product = productController.get(productID);

            if (product.Product_Image_Url != null || product.Product_Image_Url != "")
            {
                string physicalPath = Server.MapPath(product.Product_Image_Url);
                if (File.Exists(physicalPath))
                {
                    File.Delete(physicalPath);
                }
            }

        }

        protected void btnCRUD_ServerClick(object sender, EventArgs e)
        {
            ProductModel product = new ProductModel();
            product.Product_Name = txtProduct_Name.Value;
            product.Product_Describe = txtProduct_Describe.Value;
            product.Product_Price = int.Parse(txtProduct_Price.Value);
            product.Product_Categori = int.Parse(cboCategori.SelectedValue);
            product.Product_Image_Url = SaveImageFile();
            
            if(cboProductAvailability.SelectedValue == "1") product.Product_Availability = true;
            else product.Product_Availability = false;


            if (productID < 0)
            {
                Add(product);
            }
            else
            {
                product.Product_Id = productID;
                if (product.Product_Image_Url != "")
                {
                    DeleteImage(productID);
                }
                else
                {
                    ProductModel temp = new ProductModel();
                    ProductController productController = new ProductController();

                    temp = productController.get(productID);
                    product.Product_Image_Url = temp.Product_Image_Url;
                }
                Update(product);
            }
        }

        private void Update(ProductModel product)
        {
            ProductController productController = new ProductController();
            bool exec = productController.Update(product);

            if (exec)
            {
                string script = "Swal.fire({title: 'Thông báo!', text: '', icon: 'success', confirmButtonText: 'OK'}).then((result) => {if (result.isConfirmed) { window.location.href = './'; } });";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }
        }

        private void LoadDataToUpdate(int productID)
        {
            ProductModel model = new ProductModel();
            ProductController productController = new ProductController();

            model = productController.get(productID);

            txtProduct_Name.Value = model.Product_Name;
            txtProduct_Describe.Value = model.Product_Describe;
            txtProduct_Price.Value = model.Product_Price.ToString();
            cboCategori.SelectedValue = model.Product_Categori.ToString();
            if (bool.Parse(model.Product_Availability.ToString())) cboProductAvailability.SelectedValue = "1";
            else cboProductAvailability.SelectedValue = "0";
        }
    }
}