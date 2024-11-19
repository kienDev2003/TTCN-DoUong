using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QLBH_TTCN_DoUong.DAO;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.Controllers
{
    public class UserController
    {
        UserDAO userDAO;
        public UserController()
        {
            if (userDAO == null) userDAO = new UserDAO();
        }

        public UserModel Login(string username, string password)
        {
            UserModel userOutput = new UserModel();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return userOutput;
            
            int UserId = userDAO.GetId(username);
            password = Common.MD5Hash(password+UserId.ToString());
            
            userOutput = userDAO.Login(username, password);
            return userOutput;
        }

        public int Register(UserModel userInput)
        {
            if(userInput == null) return -1;

            int isUserName = userDAO.GetId(userInput.userName);
            if (isUserName > 0) return -2;

            int temp = userDAO.Register(userInput);
            if (temp < 0) return -3;

            userInput.Id = int.Parse(userDAO.GetId(userInput.userName).ToString());
            userInput.password = Common.MD5Hash(userInput.password + userInput.Id.ToString());

            int exec = userDAO.Update(userInput);
            return exec;
        }
    }
}