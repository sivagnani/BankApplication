using BankApplication.Contracts;
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
    public class BankService:IBankService
    {
        public void CreateAccount(IRBIService rbis,RBI rbi,string bid,string userName, string password,string userType)
        {
            rbis.GetBank(rbi,bid).Users.Add(new User(userName, password,userType));
        }
        public bool IsUserValid(IRBIService rbis, RBI rbi, string bid, string userId, string password, string userType)
        {
            foreach(var a in rbis.GetBank(rbi, bid).Users)
            {
                if(a.UserName==userId && a.UserType==userType && a.Password==password)
                    return true;
            }
            return false;
        }
        public bool CheckUser(IRBIService rbis, RBI rbi, string bid, string userName, string userType)
        {
            foreach (var i in rbis.GetBank(rbi, bid).Users)
            {
                if (i.UserName == userName && i.UserType == userType) return true;
            }
            return false;
        }
        public void RemoveUser(IRBIService rbis, RBI rbi, string bid,string userName,string userType)
        {
            foreach(var i in rbis.GetBank(rbi, bid).Users)
            {
                if(i.UserName ==userName && i.UserType ==userType)
                {
                    rbis.GetBank(rbi, bid).Users.Remove(i);
                    break;
                }
            }
            
        }
        
        public bool IsAccountValid(Bank bank,string accountId)
        {
            foreach(var a in bank.Accounts)
            {
                if(a.AccountId== accountId) return true;
            }
            return false;
        }
        public string GetAccountId(IRBIService rbis, RBI rbi, string bid, string name)
        {
            foreach(var a in rbis.GetBank(rbi, bid).Accounts)
            {
                if(a.CustomerName== name) return a.AccountId;
            }
            return null;
        }
        public Account GetAccount(IRBIService rbis,RBI rbi, string bid,string accountId)
        {
            foreach (var a in rbis.GetBank(rbi,bid).Accounts)
            {
                if (a.AccountId == accountId) return a;
            }
            return null;
        }
    }
}
