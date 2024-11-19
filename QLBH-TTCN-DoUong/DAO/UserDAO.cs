using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using QLBH_TTCN;
using QLBH_TTCN_DoUong.Models;

namespace QLBH_TTCN_DoUong.DAO
{
    public class UserDAO
    {
        DBConnection dBConnection;
        public UserDAO()
        {
            if(dBConnection == null) dBConnection = new DBConnection();
        }
        public UserModel Login(string userName, string password)
        {
            Dictionary<string, object> prameter = new Dictionary<string, object>
            {
                {"@userName",userName },
                {"@password",password }
            };

            SqlDataReader dataReader = dBConnection.ExecuteReader("User_Login", prameter);
            
            UserModel userOutput = new UserModel();
            if (dataReader.Read())
            {
                userOutput.Id = int.Parse(dataReader["UserId"].ToString());
                userOutput.fullName = dataReader["FullName"].ToString();
                userOutput.userName = dataReader["UserName"].ToString();
                userOutput.password = dataReader["Password"].ToString();
                userOutput.email = dataReader["Email"].ToString();
                userOutput.phone = dataReader["PhoneNumber"].ToString();
                userOutput.roleId = int.Parse(dataReader["RoleId"].ToString());
            }
            return userOutput;
        }

        public int GetId(string userName)
        {
            Dictionary<string, object> prameter = new Dictionary<string, object>
            {
                {"@userName",userName }
            };

            using (SqlDataReader dataReader = dBConnection.ExecuteReader("User_Select_Id_By_UserName", prameter))
            {
                if (dataReader.Read()) return int.Parse(dataReader["UserId"].ToString());
            }
            return -1;
        }

        public int Register(UserModel userInput)
        {
            if (userInput == null) return -1;

            Dictionary<string, object> prameter = new Dictionary<string, object>
            {
                {"@fullName",userInput.fullName },
                {"@userName", userInput.userName },
                {"@password", userInput.password },
                {"@email", userInput.email },
                {"@phoneNumber", userInput.phone },
                {"@roleId", userInput.roleId }
            };

            int exec = dBConnection.ExecuteNonQuery("User_Insert", prameter);
            if (exec > 0) return exec;
            return -1;
        }

        public int Update(UserModel userInput)
        {
            if (userInput == null) return -1;

            Dictionary<string, object> prameter = new Dictionary<string, object>
            {
                {"@userId",userInput.Id },
                {"@fullName",userInput.fullName },
                {"@userName", userInput.userName },
                {"@password", userInput.password },
                {"@email", userInput.email },
                {"@phoneNumber", userInput.phone },
                {"@roleId", userInput.roleId }
            };

            int exec = dBConnection.ExecuteNonQuery("User_Update", prameter);
            if (exec > 0) return exec;
            return -1;
        }
    }
}