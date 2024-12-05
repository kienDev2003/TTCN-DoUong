using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QLBH_TTCN_DoUong.DAO;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.Controllers
{
    public class RoleController
    {
        RoleDAO roleDAO;
        public RoleController()
        {
            roleDAO = new RoleDAO();
        }

        public List<RoleModel> GetRoleList()
        {
            return roleDAO.Gets();
        }
    }
}