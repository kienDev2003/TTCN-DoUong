using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
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

        public List<OrderDetailModel> AddProductToOrder(int productId)
        {
            OrderDetailModel orderDetailModel = new OrderDetailModel();

            List<OrderDetailModel> listOrderDetail = Common.ReturnOrderList("orderDetail") ?? new List<OrderDetailModel>();

            if (listOrderDetail != null)
            {
                foreach (var order in listOrderDetail)
                {
                    if (order.ProductId == productId)
                    {
                        order.Quantity += 1;
                        return listOrderDetail;
                    }
                }
            }

            orderDetailModel.ProductId = productId;
            orderDetailModel.Quantity = 1;
            
            listOrderDetail.Add(orderDetailModel);
            return listOrderDetail;
        }

        public bool RawMaterial(int productID)
        {
            IngredientDAO ingredientDAO = new IngredientDAO();
            RecipeDAO recipeDAO = new RecipeDAO();

            List<RecipeModel> listRecipe = recipeDAO.getByProductId(productID);

            if (listRecipe.Count <= 0) return false;

            for(int i = 0;i < listRecipe.Count; i++)
            {
                RecipeModel recipe = listRecipe[i];
                IngredientModel ingredient = new IngredientModel();

                ingredient = ingredientDAO.getByIngredientID(recipe.ingredientID);

                int check = ingredient.ingredientQuantity - recipe.recipeMaterialQty;

                if (check >= 0) continue;
                return false;
            }
            return true;
        }
    }
}