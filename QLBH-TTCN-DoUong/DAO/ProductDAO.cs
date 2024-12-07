using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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

        public ProductModel GetByID(int productID)
        {
            ProductModel model = new ProductModel();
            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                {"@product_ID",productID}
            };
            using(SqlDataReader dataReader = dBConnection.ExecuteReader("Products_Select", parameter))
            {
                if(dataReader.Read())
                {
                    model.Product_Id = int.Parse(dataReader["Product_ID"].ToString());
                    model.Product_Name = dataReader["Product_Name"].ToString();
                    model.Product_Categori = int.Parse(dataReader["Product_Categori"].ToString());
                    model.Product_Describe = dataReader["Product_Describe"].ToString();
                    model.Product_Price = float.Parse(dataReader["Product_Price"].ToString());
                    model.Product_Availability = bool.Parse(dataReader["Product_Availability"].ToString());
                    model.Product_Image_Url = dataReader["Product_Image_Url"].ToString();
                }
            }
            return model;
        }
        public List<ProductModel> Gets()
        {
            List<ProductModel> listProduct = new List<ProductModel>();
            using (SqlDataReader dataReader = dBConnection.ExecuteReader("Product_Select_All", null))
            {
                while (dataReader.Read())
                {
                    ProductModel model = new ProductModel();
                    model.Product_Id = int.Parse(dataReader["Product_ID"].ToString());
                    model.Product_Name = dataReader["Product_Name"].ToString();
                    model.Product_Describe = dataReader["Product_Describe"].ToString();
                    model.Product_Categori_Name = dataReader["Categori_Name"].ToString();
                    model.Product_Price = float.Parse(dataReader["Product_Price"].ToString());
                    model.Product_Availability = bool.Parse(dataReader["Product_Availability"].ToString());
                    model.Product_Image_Url = dataReader["Product_Image_Url"].ToString();

                    listProduct.Add(model);
                }
            }
            return listProduct;
        }

        public List<Dictionary<CategoriModel, List<ProductModel>>> GetProductANDCategori()
        {
            List<Dictionary<CategoriModel, List<ProductModel>>> listProductByCategori = new List<Dictionary<CategoriModel, List<ProductModel>>>();
            using (SqlDataReader readerCategori = dBConnection.ExecuteReader("Categoris_Select", null))
            {
                while (readerCategori.Read())
                {
                    CategoriModel categori = new CategoriModel();
                    List<ProductModel> listProduct = new List<ProductModel>();
                    Dictionary<CategoriModel, List<ProductModel>> productByCategori = new Dictionary<CategoriModel, List<ProductModel>>();

                    categori.CategoriId = int.Parse(readerCategori["Categori_ID"].ToString());
                    categori.Name = readerCategori["Categori_Name"].ToString();

                    Dictionary<string, object> prameter = new Dictionary<string, object>()
                    {
                        {"@categoriId",categori.CategoriId }
                    };

                    using (SqlDataReader readerProductByCategori = dBConnection.ExecuteReader("Product_Select_By_Categoris", prameter))
                    {
                        while (readerProductByCategori.Read())
                        {
                            ProductModel model = new ProductModel();
                            model.Product_Id = int.Parse(readerProductByCategori["Product_ID"].ToString());
                            model.Product_Name = readerProductByCategori["Product_Name"].ToString();
                            model.Product_Describe = readerProductByCategori["Product_Describe"].ToString();
                            model.Product_Price = float.Parse(readerProductByCategori["Product_Price"].ToString());
                            model.Product_Availability = bool.Parse(readerProductByCategori["Product_Availability"].ToString());
                            model.Product_Image_Url = readerProductByCategori["Product_Image_Url"].ToString();

                            listProduct.Add(model);
                        }
                        productByCategori.Add(categori, listProduct);
                    }

                    listProductByCategori.Add(productByCategori);
                }
            }
            return listProductByCategori;
        }

        public List<ProductModel> SearchProductByName(string name)
        {
            List<ProductModel> products = new List<ProductModel>();
            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                {"@productName",name}
            };

            using(SqlDataReader dataReader = dBConnection.ExecuteReader("Products_Select_By_Name", parameter))
            {
                while (dataReader.Read())
                {
                    ProductModel model = new ProductModel();
                    model.Product_Id = int.Parse(dataReader["Product_ID"].ToString());
                    model.Product_Name = dataReader["Product_Name"].ToString();
                    model.Product_Describe = dataReader["Product_Describe"].ToString();
                    model.Product_Categori_Name = dataReader["Categori_Name"].ToString();
                    model.Product_Price = float.Parse(dataReader["Product_Price"].ToString());
                    model.Product_Availability = bool.Parse(dataReader["Product_Availability"].ToString());
                    model.Product_Image_Url = dataReader["Product_Image_Url"].ToString();

                    products.Add(model);
                }
            }
            return products;
        }

        public int Update(ProductModel product)
        {
            Dictionary<string, object> prameter = new Dictionary<string, object>
            {
                {"@productID",product.Product_Id },
                {"@productName",product.Product_Name },
                {"@productDescribe", product.Product_Describe },
                {"@productPrice", product.Product_Price},
                {"@productCategori", product.Product_Categori },
                {"@productAvailability", product.Product_Availability },
                {"@productImageUrl", product.Product_Image_Url }
            };

            return dBConnection.ExecuteNonQuery("Product_Update", prameter);
        }

        public int Delete(int productID)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                {"@productId", productID }
            };

            return dBConnection.ExecuteNonQuery("Product_Delete", parameter);
        }

        public int Add(ProductModel product)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                {"@productName",product.Product_Name },
                {"@productDescribe", product.Product_Describe},
                { "@productPrice",product.Product_Price},
                { "@productCategori",product.Product_Categori},
                {"@productAvailability",true },
                {"@productImageUrl",product.Product_Image_Url }
            };

            return dBConnection.ExecuteNonQuery("Product_Insert", parameter);
        }
    }
}