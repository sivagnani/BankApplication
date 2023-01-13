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
        public Bank bankModel = new Bank();
        public List<AccountService> accounts = new List<AccountService>();
        public List<AdminService> admins = new List<AdminService>();
        public List<StaffService> staffs = new List<StaffService>();
        public List<CustomerService> Customers = new List<CustomerService>();
        public List<UserService> Users = new List<UserService>();
        public BankService(string s)
        {
            bankModel.Name = s;
            DateTime centuryBegin = new DateTime(2001, 1, 1);
            long elapsedTicks = DateTime.Now.Second - centuryBegin.Second;
            bankModel.bankId = s.Substring(0, 3) + elapsedTicks.ToString();
            bankModel.Currency.Add("INR", 1);
        }
        public void RemoveUser(string userName,string userType)
        {
            foreach(var i in Users)
            {
                if(i.userModel.userName ==userName && i.userModel.userType==userType)
                {
                    Users.Remove(i);
                    break;
                }
            }
            
        }
        public bool CheckUser(string userName,string userType)
        {
            foreach (var i in Users)
            {
                if (i.userModel.userName == userName && i.userModel.userType == userType) return false;
            }
            return true;
        }
        public void SenderUpdate(string senderBankName, string senderAccountID, string reciverBankName, string recieverAccountId, float a)
        {

        }
        public void RecieverUpdate(string senderBankName, string senderAccountID, string reciverBankName, string recieverAccountId, float a)
        {

        }
    }
}
