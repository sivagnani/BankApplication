using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Models
{
    public class User
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public enum TypesOfUser
        {
            Admin,
            Staff,
            Customer
        }
        public string UserType { get; set; }
        public User(string userName, string password, string userType)
        {
            Password = password;
            UserName = userName;
            UserType = userType;
        }

    }
}
