using BankApplication.Contracts;
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
    public class AdminService:IAdminService
    {
        public void CreateStaffAccount(IRBIService rbis,RBI rbi,string bid,string name,string pass)
        {
            IBankService b = new BankService();
            b.CreateAccount(rbis, rbi,bid, name, pass,User.TypesOfUser.Staff.ToString());
        }
        public void RemoveStaffAccount(IRBIService rbis,RBI rbi, string bid, string name)
        {
            IBankService b  =new BankService();
            b.RemoveUser(rbis, rbi,bid, name,User.TypesOfUser.Staff.ToString());
        }

    }
}
