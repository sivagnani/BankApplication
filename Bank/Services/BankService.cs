using BankApplication.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankApplication.Services
{
    public class BankService
    {
        public string GetBankId(Bank b)
        {
            return b.BankId;
        }
        public void CreateAccount(RBIService rbis,string bid,string userName, string password,string userType)
        {
            rbis.GetBank(bid).Users.Add(new User(userName, password,userType));
        }
        public bool ValidateUserLogin(RBIService rbis, string bid, string userId, string password, string userType)
        {
            foreach(var a in rbis.GetBank(bid).Users)
            {
                if(a.UserName==userId && a.UserType==userType)
                    return true;
            }
            return false;
        }
        public bool CheckUser(RBIService rbis,string bid, string userName, string userType)
        {
            foreach (var i in rbis.GetBank(bid).Users)
            {
                if (i.UserName == userName && i.UserName == userType) return true;
            }
            return false;
        }
        public void RemoveUser(RBIService rbis,string bid,string userName,string userType)
        {
            foreach(var i in rbis.GetBank(bid).Users)
            {
                if(i.UserName ==userName && i.UserName ==userType)
                {
                    rbis.GetBank(bid).Users.Remove(i);
                    break;
                }
            }
            
        }
        
        public bool ValidateAccount(Bank bank,string accountId)
        {
            foreach(var a in bank.Accounts)
            {
                if(a.AccountId== accountId) return true;
            }
            return false;
        }
        public string GetAccountId(RBIService rbis, string bid, string name)
        {
            foreach(var a in rbis.GetBank(bid).Accounts)
            {
                if(a.CustomerName== name) return a.AccountId;
            }
            return null;
        }
        public Account GetAccount(RBIService rbis,string bid,string accountId)
        {
            foreach (var a in rbis.GetBank(bid).Accounts)
            {
                if (a.AccountId == accountId) return a;
            }
            return null;
        }
    }
}
