using BankApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Services
{
    public class StaffService:UserService
    {
        public BankService bank;
        public StaffService(string name, string pass, BankService bank) : base(name, pass,"Staff")
        {
            this.bank=bank;
        }
        public bool CreateCustomerAccount(string userName, string password)
        {
            if (bank.CheckUser(userName,"Customer"))
            {
                AccountService a = new AccountService(userName,bank.bankModel.bankId);
                bank.accounts.Add(a);
                bank.Users.Add(new UserService(userName, password, "Customer"));
                bank.Customers.Add(new CustomerService(userName, password, a, bank));
                return true;
            }
            return false;
        }
        public bool RemoveCustomerAccount(string userName) 
        {
            if (!bank.CheckUser(userName,"Customer"))
            {
                if (DeleteAccount(userName))
                {
                    bank.RemoveUser(userName, "Customer");
                    RemoveCustomer(userName);
                    return true;
                }
                return false;
            }
            return false;
        }
        public void RemoveCustomer(string userName)
        {
            foreach (var i in bank.Customers)
            {
                if (i.userModel.userName == userName)
                {
                    bank.Customers.Remove(i);
                    break;
                }
            }
        }
        public bool DeleteAccount(string s)
        {
            foreach (var i in bank.accounts)
            {
                if (i.accountModel.customerName == s)
                {
                    bank.accounts.Remove(i);
                    return true;
                }
            }
            return false;
        }
        public bool UpdateUsername(string o,string n)
        {
            if(!bank.CheckUser(o, "Customer"))
            {
                ChangeUserName(o,n);
                return true;
            }
            return false;
        }
        public void ChangeUserName(string oldName, string newName)
        {
            foreach (var i in bank.accounts)
            {
                if (i.accountModel.customerName == oldName)
                {
                    i.UpdateAccount(newName);
                }
            }
            foreach (var i in bank.Customers)
            {
                if (i.userModel.userName == oldName)
                {
                    i.UpdateUserName(newName);
                }
            }
            foreach (var i in bank.Users)
            {
                if (i.userModel.userName == oldName && i.userModel.userType == "Customer")
                {
                    i.UpdateUserName(newName);
                }
            }
        }
        public bool UpdatePassword(string u,string p)
        {
            if (!bank.CheckUser(u, "Customer"))
            {
                ChangePassword(u, p);
                return true;
            }
            return false;
        }
        public void ChangePassword(string user, string pass)
        {
            foreach (var i in bank.Customers)
            {
                if (i.userModel.userName == user)
                {
                    i.UpdatePassword(pass);
                }
            }
            foreach (var i in bank.Users)
            {
                if (i.userModel.userName == user && i.userModel.userType == "Customer")
                {
                    i.UpdatePassword(pass);
                }
            }
        }
        public void AddCurrency(string currency,float rate)
        {
            bank.bankModel.Currency[currency]=rate;
        }
        public void SetServiceCharges(string serviceCharge,float d)
        {
            switch(serviceCharge)
            {
                case "RTGS":
                    bank.bankModel.RTGS = d;
                    break;
                case "ORTGS":
                    bank.bankModel.ORTGS = d;
                    break;
                case "IMPS":
                    bank.bankModel.IMPS = d;
                    break;
                case "OIMPS":
                    bank.bankModel.OIMPS = d;
                    break;
            }
        }
    }
}
