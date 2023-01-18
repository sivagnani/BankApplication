using BankApplication.Contracts;
using BankApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Services
{
    public class StaffService: IStaffService
    {
        public void CreateCustomerAccount(IRBIService rbis, RBI rbi, string bid,string userName, string password)
        { 
                IBankService b = new BankService();
                Account a = new Account(userName,bid);
                rbis.GetBank(rbi,bid).Accounts.Add(a);
                b.CreateAccount(rbis,rbi,bid,userName,password,User.TypesOfUser.Customer.ToString());
        }
        public bool RemoveCustomerAccount(IRBIService rbis, RBI rbi, string bid,string userName) 
        {
            IBankService b = new BankService();
            if (!b.CheckUser(rbis,rbi,bid,userName,User.TypesOfUser.Customer.ToString()))
            {
                if (DeleteAccount(rbis.GetBank(rbi,bid),userName))
                {
                    b.RemoveUser(rbis,rbi,bid,userName,User.TypesOfUser.Customer.ToString());
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
        public void ChangeUserName(IRBIService rbis, RBI rbi, string bid,string oldName, string newName)
        {
            Bank b = rbis.GetBank(rbi,bid);
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
        public bool UpdatePassword(IRBIService rbis, RBI rbi, string bid,string u,string p)
        {
            Bank bank = rbis.GetBank(rbi, bid);
            IBankService b = new BankService();
            if (b.CheckUser(rbis,rbi,bid,u,User.TypesOfUser.Customer.ToString()))
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
        public void AddCurrency(IRBIService rbis, RBI rbi, string bid,string currency,float rate)
        {
            rbis.GetBank(rbi,bid).Currency[currency]=rate;
        }
        public void SetServiceCharges(IRBIService rbis, RBI rbi, string bid, string serviceCharge, float d)
        {
            Bank b = rbis.GetBank(rbi,bid);
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
        public void RevertTransaction(IRBIService rbis, RBI rbi, string bid, string aid, string transId)
        {
            IBankService bs = new BankService();
            IAccountService a = new AccountService();
            Account a1 = bs.GetAccount(rbis,rbi, bid, aid);
            TransactionModel t = a.GetTransaction(a1, transId);
            float am;
            string aid2;
            string bid2;
            if (t.Amount < 0)
            {
                aid2 = t.RecieverAccountId;
                bid2 = t.RecieverBankId;
                am = (-1) * t.Amount;
            }
            else
            {
                aid2 = t.SenderAccountId;
                bid2 = t.SenderBankId;
                am = a.GetTransaction(bs.GetAccount(rbis,rbi, bid2, aid2), transId).Amount;
            }
            a.AddMoney(a1,am);
            a.AddMoney(bs.GetAccount(rbis,rbi, bid2, aid2), (-1)*am);
            a1.Transactions.Add(new TransactionModel(TransactionModel.TypeOfTransaction.Reverted.ToString(),am, transId));
            bs.GetAccount(rbis,rbi, bid2, aid2).Transactions.Add(new TransactionModel(TransactionModel.TypeOfTransaction.Reverted.ToString(), -1*am, transId));
        }
    }
}
