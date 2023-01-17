using BankApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Services
{
    public class StaffService
    {
        public void CreateCustomerAccount(RBIService rbis,string bid,string userName, string password)
        { 
                BankService b = new BankService();
                Account a = new Account(userName,bid);
                rbis.GetBank(bid).Accounts.Add(a);
                b.CreateAccount(rbis,bid,userName,password,User.TypesOfUser.Customer.ToString());
        }
        public bool RemoveCustomerAccount(RBIService rbis,string bid,string userName) 
        {
            BankService b = new BankService();
            if (!b.CheckUser(rbis,bid,userName,"Customer"))
            {
                if (DeleteAccount(rbis.GetBank(bid),userName))
                {
                    b.RemoveUser(rbis,bid,userName,User.TypesOfUser.Customer.ToString());
                    return true;
                }
                return false;
            }
            return false;
        }
        public bool DeleteAccount(Bank b,string s)
        {
            foreach (var i in b.Accounts)
            {
                if (i.CustomerName == s)
                {
                    b.Accounts.Remove(i);
                    return true;
                }
            }
            return false;
        }
        public void ChangeUserName(RBIService rbis,string bid,string oldName, string newName)
        {
            Bank b = rbis.GetBank(bid);
            foreach (var i in b.Accounts)
            {
                if (i.CustomerName == oldName)
                {
                    i.CustomerName = newName;
                }
            }
            foreach (var i in b.Users)
            {
                if (i.UserName == oldName && i.UserType==User.TypesOfUser.Customer.ToString())
                {
                    i.UserName = newName;
                }
            }
        }
        public bool UpdatePassword(RBIService rbis,string bid,string u,string p)
        {
            Bank bank = rbis.GetBank(bid);
            BankService b = new BankService();
            if (!b.CheckUser(rbis,bid,u,User.TypesOfUser.Customer.ToString()))
            {
                foreach (var i in bank.Users)
                {
                    if (i.UserName == u && i.UserType == User.TypesOfUser.Customer.ToString())
                    {
                        i.Password = p;
                    }
                }
                return true;
            }
            return false;
        }
        public void AddCurrency(RBIService rbis,string bid,string currency,float rate)
        {
            rbis.GetBank(bid).Currency[currency]=rate;
        }
        public void SetServiceCharges(RBIService rbis,string bid,string serviceCharge,float d)
        {
            Bank b = rbis.GetBank(bid);
            switch(serviceCharge)
            {
                case "RTGS":
                    b.RTGS = d;
                    break;
                case "ORTGS":
                    b.ORTGS = d;
                    break;
                case "IMPS":
                    b.IMPS = d;
                    break;
                case "OIMPS":
                    b.OIMPS = d;
                    break;
            }
        }
    }
}
