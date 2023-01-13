using BankApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankApplication.Services
{
    public class CustomerService:UserService
    {
        public AccountService account;
        public BankService bank;
        public Customer customer = new Customer();
        public CustomerService(string name, string pass, AccountService account,BankService bank) : base(name, pass,"Customer")
        {
            this.account = account;
            this.bank = bank;
        }
        public bool DepositeMoney(int amount,string currency)
        {
            if(bank.bankModel.Currency.ContainsKey(currency))
            {
                float rate = bank.bankModel.Currency[currency];
                account.AddMoney(amount * rate);
                string a=account.accountModel.accountId;
                string b= account.accountModel.bankId;
                Console.WriteLine(a + " " + b);
                account.transactions.Add(new TransactionService(a, b, a, b, amount * rate));
                return true;
            }
            return false;            
        }
        public bool WithdrawAmount(int amount)
        {
            if (account.RemoveMoney(amount))
            {
                string a = account.accountModel.accountId;
                string b = account.accountModel.bankId;
                account.transactions.Add(new TransactionService(a, b, a, b, amount * (-1)));
                return true;
            }
            return false;
            
        }
        public float GetBalance()
        {
            return account.accountModel.balance;
        }
        public float CalculateAmount(int amount,int op,string reciverBank)
        {
            return 0;
        }
    }
}
