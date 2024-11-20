using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QLBH_TTCN_DoUong.DAO;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.Controllers
{
    public class CategoriController
    {
        CategoriDAO categoriDAO;

        public CategoriController()
        {
            categoriDAO = new CategoriDAO();
        }

        public string Get()
        {
            List<CategoriModel> listCatrgori = new List<CategoriModel>();
            listCatrgori = categoriDAO.Get();

            string html = "";
            for(int i = 0; i < listCatrgori.Count; i++)
            {
                CategoriModel categori = listCatrgori[i];
                string htmlCategori = $"<a href=\"#{categori.CategoriId}\">{categori.Name}</a>";

                html += htmlCategori;
            }
            return html;
        }
    }
}