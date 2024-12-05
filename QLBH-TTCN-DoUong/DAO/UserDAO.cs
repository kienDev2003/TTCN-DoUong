using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;
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
                userOutput.Id = int.Parse(dataReader["User_ID"].ToString());
                userOutput.fullName = dataReader["User_FullName"].ToString();
                userOutput.userName = dataReader["User_Account"].ToString();
                userOutput.password = dataReader["User_Password"].ToString();
                userOutput.email = dataReader["User_Email"].ToString();
                userOutput.phone = dataReader["User_PhoneNumber"].ToString();
                userOutput.roleId = int.Parse(dataReader["User_Role"].ToString());
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
                if (dataReader.Read()) return int.Parse(dataReader["User_ID"].ToString());
            }
            return -1;
        }

        public List<UserModel> Gets()
        {
            List<UserModel> users = new List<UserModel>();
            using(SqlDataReader dataReader = dBConnection.ExecuteReader("User_Select", null))
            {
                while (dataReader.Read())
                {
                    UserModel user = new UserModel();

                    user.Id = int.Parse(dataReader["User_ID"].ToString());
                    user.fullName = dataReader["User_FullName"].ToString();
                    user.userName = dataReader["User_Account"].ToString();
                    user.password = dataReader["User_Password"].ToString();
                    user.email = dataReader["User_Email"].ToString();
                    user.phone = dataReader["User_PhoneNumber"].ToString();
                    user.roleId = int.Parse(dataReader["User_Role"].ToString());
                    user.roleName = dataReader["Role_Name"].ToString();

                    users.Add(user);
                }
            }
            return users;
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

        public List<UserModel> SearchUserByName(string name)
        {
            List<UserModel> users = new List<UserModel>();

            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                {"@name",name }
            };

            using(SqlDataReader dataReader = dBConnection.ExecuteReader("User_Search_By_Name", parameter))
            {
                while (dataReader.Read())
                {
                    UserModel user = new UserModel();

                    user.Id = int.Parse(dataReader["User_ID"].ToString());
                    user.fullName = dataReader["User_FullName"].ToString();
                    user.userName = dataReader["User_Account"].ToString();
                    user.password = dataReader["User_Password"].ToString();
                    user.email = dataReader["User_Email"].ToString();
                    user.phone = dataReader["User_PhoneNumber"].ToString();
                    user.roleId = int.Parse(dataReader["User_Role"].ToString());
                    user.roleName = dataReader["Role_Name"].ToString();

                    users.Add(user);
                }
            }
            return users;
        }

        public int Delete(int userID)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                {"@userID",userID }
            };

            return dBConnection.ExecuteNonQuery("User_Delete", parameter);
        }

        public UserModel Get(int userID)
        {
            UserModel user = new UserModel();
            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                {"@userID",userID }
            };

            using (SqlDataReader dataReader = dBConnection.ExecuteReader("User_Select_By_ID", parameter))
            {
                if(dataReader.Read())
                {
                    user.fullName = dataReader["User_FullName"].ToString();
                    user.userName = dataReader["User_Account"].ToString();
                    user.password = dataReader["User_Password"].ToString();
                    user.email = dataReader["User_Email"].ToString();
                    user.phone = dataReader["User_PhoneNumber"].ToString();
                    user.roleId = int.Parse(dataReader["User_Role"].ToString());
                    user.roleName = dataReader["Role_Name"].ToString();
                }
            }
            return user;
        }
    }
}