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
    public class CustomerService
    {
        public bool DepositeMoney(RBIService rbis,string bid,string aid,int amount,string currency)
        {
            Bank bank = rbis.GetBank(bid);
            AccountService a = new AccountService();
            BankService b = new BankService();
            if(bank.Currency.ContainsKey(currency))
            {
                float rate = bank.Currency[currency];
                Account acc = b.GetAccount(rbis, bid, aid);
                a.AddMoney(acc,amount * rate);
                string transactionId = "TXN"+ DateTime.Now.Microsecond.ToString();
                acc.Transactions.Add(new TransactionModel(TransactionModel.TypeOfTransaction.Deposit.ToString(), amount * rate,transactionId));
                return true;
            }
            return false;            
        }
        public bool WithdrawAmount(RBIService rbis, string bid,string aid,int amount)
        {
            AccountService a = new AccountService();
            BankService b = new BankService();
            Account acc = b.GetAccount(rbis,bid,aid);
            if (a.CanRemoveMoney(acc,amount))
            {
                string transactionId = "TXN" + DateTime.Now.Microsecond.ToString();
                acc.Transactions.Add(new TransactionModel(TransactionModel.TypeOfTransaction.Withdrawl.ToString(), amount * (-1),transactionId));
                return true;
            }
            return false;
            
        }
        public float GetBalance(RBIService rbis, string bid,string aid)
        {
            BankService b = new BankService();
            return b.GetAccount(rbis, bid, aid).Balance;
        }
        public float CalculateAmount(RBIService rbis, string bid,float amount,int op,string reciverBankId)
        {
            Bank b = rbis.GetBank(bid);
            if (b.BankId == reciverBankId)
            {
                switch (op)
                {
                    case 1:
                        return amount * (1 - (b.RTGS / 100));
                    case 2:
                        return amount * (1 - (b.IMPS / 100));
                }
            }
            else
            {
                switch (op)
                {
                    case 1:
                        return amount * (1 - (b.ORTGS / 100));
                    case 2:
                        return amount * (1 - (b.OIMPS / 100));
                }
            }
            return 0;
        }
    }
}
