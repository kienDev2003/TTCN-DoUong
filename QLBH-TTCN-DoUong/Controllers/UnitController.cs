using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QLBH_TTCN_DoUong.DAO;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.Controllers
{
    public class UnitController
    {
        UnitDAO unitDAO;

        public UnitController()
        {
            unitDAO = new UnitDAO();
        }

        public List<UnitModel> Gets()
        {
            return unitDAO.Gets();
        }
    }
}