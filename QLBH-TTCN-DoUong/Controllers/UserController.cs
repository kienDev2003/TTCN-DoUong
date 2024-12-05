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
            password = Common.MD5_Hash(password+UserId.ToString());
            
            userOutput = userDAO.Login(username, password);
            return userOutput;
        }

        public bool Register(UserModel userInput)
        {
            if(userInput == null) return false;

            int isUserName = userDAO.GetId(userInput.userName);
            if (isUserName > 0) return false;

            int temp = userDAO.Register(userInput);
            if (temp < 0) return false;

            userInput.Id = int.Parse(userDAO.GetId(userInput.userName).ToString());
            userInput.password = Common.MD5_Hash(userInput.password + userInput.Id.ToString());

            int exec = userDAO.Update(userInput);

            if (exec > 0) return true;
            return false;
        }

        public List<UserModel> Gets()
        {
            return userDAO.Gets();
        }

        public UserModel Get(int userID)
        {
            return userDAO.Get(userID);
        }

        public List<UserModel> SearchUserByName(string name)
        {
            return userDAO.SearchUserByName(name);
        }

        public bool Detele(int userID)
        {
            int exec = userDAO.Delete(userID);

            if(exec > 0) return true;
            return false;
        }

        public bool Update(UserModel user)
        {
            int exec = userDAO.Update(user);

            if (exec > 0) return true;
            return false;
        }
    }
}