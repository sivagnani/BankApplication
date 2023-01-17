using BankApplication.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Services
{
    public class AdminService
    {
        public void CreateStaffAccount(RBIService rbis,string bid,string name,string pass)
        {
            BankService b = new BankService();
            b.CreateAccount(rbis, bid, name, pass,User.TypesOfUser.Staff.ToString());
        }
        public void RemoveStaffAccount(RBIService rbis, string bid, string name)
        {
            BankService b  =new BankService();
            b.RemoveUser(rbis, bid, name,User.TypesOfUser.Staff.ToString());
        }

    }
}
