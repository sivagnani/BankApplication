using BankApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Services
{
    public class UserService
    {
        public User userModel = new User();
        public UserService(string userName, string password, string userType)
        { 
            userModel.password = password;
            userModel.userName = userName;
            userModel.userType = userType;
        }
        public void UpdateUserName(string userName)
        {
            userModel.userName = userName;
        }
        public void UpdatePassword(string password)
        {
            userModel.password = password;
        }
    }
}
