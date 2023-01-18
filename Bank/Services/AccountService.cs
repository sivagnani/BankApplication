using BankApplication.Contracts;
using BankApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankApplication.Services
{
    public class AccountService:IAccountService
    {
        public void AddMoney(Account a,float amount)
        {
            a.Balance += amount;
        }
        public bool CanRemoveMoney(Account a, float amount)
        {
            if (a.Balance >= amount)
            {
                a.Balance -= amount;
                return true;
            }
            return false;
        }
        public TransactionModel GetTransaction(Account a,string tid)
        {
            foreach(var tx in a.Transactions)
            {
                if(tx.TransactionId==tid)
                    return tx;
            }
            return null;
        }
    }
}
