using BankApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankApplication.Services
{
    public class AccountService
    {
        public Account accountModel = new Account();
        public List<TransactionService> transactions = new List<TransactionService>();
        public AccountService(string name, string bankId)
        {
            accountModel.customerName = name;
            accountModel.bankId = bankId;
            DateTime centuryBegin = new DateTime(2001, 1, 1);
            long elapsedTicks = DateTime.Now.Second - centuryBegin.Second;
            accountModel.accountId = name.Substring(0, 3) + elapsedTicks.ToString();
            accountModel.balance = 0;
        }
        public void UpdateAccount(string n)
        {
            accountModel.customerName = n;
            DateTime centuryBegin = new DateTime(2001, 1, 1);
            long elapsedTicks = DateTime.Now.Ticks - centuryBegin.Ticks;
            accountModel.accountId = n.Substring(0, 3) + elapsedTicks.ToString();
        }
        public void AddMoney(float amount)
        {
            accountModel.balance += amount;
        }
        public bool RemoveMoney(float amount)
        {
            if (accountModel.balance >= amount)
            {
                accountModel.balance -= amount;
                return true;
            }
            return false;
        }
    }
}
