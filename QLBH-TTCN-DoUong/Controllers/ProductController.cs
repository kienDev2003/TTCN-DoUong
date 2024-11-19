using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QLBH_TTCN_DoUong.DAO;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.Controllers
{
    public class ProductController
    {
        ProductDAO productDAO;
        public ProductController()
        {
            productDAO = new ProductDAO();
        }

        public List<ProductModel> GetProductAll()
        {
            return productDAO.Get();
        }

        public string GetProductAndCategoriHtml()
        {
            List<Dictionary<CategoriModel, List<ProductModel>>> listProductAndCategori = productDAO.GetProductANDCategori();
            string html = "";

            for (int i = 0; i < listProductAndCategori.Count; i++)
            {
                Dictionary<CategoriModel, List<ProductModel>> productAndCategori = listProductAndCategori[i];
                string htmlCategoori = "";
                foreach (var entry in productAndCategori)
                {
                    CategoriModel categori = entry.Key;
                    List<ProductModel> listProduct = entry.Value;

                    string htmlProduct = "";

                    foreach (var product in listProduct)
                    {
                        string htmlProductItem = $"<div class=\"item\">" +
                                                    $"<img src=\"{product.PicUrl}\" alt=\"\" />" +
                                                    $"<div class=\"des\">" +
                                                        $"<p class=\"nameItem\">{product.Name}</p>" +
                                                        $"<p class=\"dseItem\">" +
                                                            $"{product.Description}" +
                                                        $"</p>" +
                                                        $"<div class=\"dseNav\">" +
                                                            $"<p class=\"priceItem\">{product.Price}</p>" +
                                                            $"<div class=\"btnAddItem\" tag=\"{product.ProductId}\">" +
                                                                $"<img src=\"./assets/img/icon-add-item.svg\" alt=\"\" />" +
                                                            $"</div>" +
                                                        $"</div>" +
                                                    $"</div>" +
                                                 $"</div>";

                        htmlProduct += htmlProductItem;
                    }

                    string htmlCategoriItem = $"<div class=\"category\" id=\"{categori.CategoriId}\" >" +
                                                $"<h2>{categori.Name}</h2>" +
                                                $"<div class=\"categoryContent\">" +
                                                    $"{htmlProduct}" +
                                                $"</div>" +
                                              $"</div>";

                    htmlCategoori += htmlCategoriItem;
                }
                html += htmlCategoori;
            }
            return html;
        }
    }
}