using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using QLBH_TTCN;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.DAO
{
    public class ProductDAO
    {
        DBConnection dBConnection;

        public ProductDAO()
        {
            dBConnection = new DBConnection();
        }
        public List<ProductModel> Get()
        {
            List<ProductModel> listProduct = new List<ProductModel>();
            using (SqlDataReader dataReader = dBConnection.ExecuteReader("Product_Select_All", null))
            {
                while (dataReader.Read())
                {
                    ProductModel product = new ProductModel();
                    product.ProductId = int.Parse(dataReader["ProductId"].ToString());
                    product.Name = dataReader["Name"].ToString();
                    product.Description = dataReader["Des"].ToString();
                    product.Price = float.Parse(dataReader["Price"].ToString());
                    product.CategoriId = int.Parse(dataReader["CategoriId"].ToString());
                    product.Sell = bool.Parse(dataReader["Sell"].ToString());
                    product.PicUrl = dataReader["PicUrl"].ToString();

                    listProduct.Add(product);
                }
            }
            return listProduct;
        }

        public List<Dictionary<CategoriModel, List<ProductModel>>> GetProductANDCategori()
        {
            List<Dictionary<CategoriModel, List<ProductModel>>> listProductByCategori = new List<Dictionary<CategoriModel, List<ProductModel>>>();
            using (SqlDataReader readerCategori = dBConnection.ExecuteReader("Categori_Select_All", null))
            {
                while (readerCategori.Read())
                {
                    CategoriModel categori = new CategoriModel();
                    List<ProductModel> listProduct = new List<ProductModel>();
                    Dictionary<CategoriModel, List<ProductModel>> productByCategori = new Dictionary<CategoriModel, List<ProductModel>>();

                    categori.CategoriId = int.Parse(readerCategori["CategoriId"].ToString());
                    categori.Name = readerCategori["Name"].ToString();

                    Dictionary<string, object> prameter = new Dictionary<string, object>()
                    {
                        {"@CategoriId",categori.CategoriId }
                    };

                    using (SqlDataReader readerProductByCategori = dBConnection.ExecuteReader("Product_Select_By_Categori", prameter))
                    {
                        while (readerProductByCategori.Read())
                        {
                            ProductModel product = new ProductModel();
                            product.ProductId = int.Parse(readerProductByCategori["ProductId"].ToString());
                            product.Name = readerProductByCategori["Name"].ToString();
                            product.Description = readerProductByCategori["Des"].ToString();
                            product.Price = float.Parse(readerProductByCategori["Price"].ToString());
                            product.Sell = bool.Parse(readerProductByCategori["Sell"].ToString());
                            product.PicUrl = readerProductByCategori["PicUrl"].ToString();

                            listProduct.Add(product);
                        }
                        productByCategori.Add(categori, listProduct);
                    }

                    listProductByCategori.Add(productByCategori);
                }
            }
            return listProductByCategori;
        }
    }
}