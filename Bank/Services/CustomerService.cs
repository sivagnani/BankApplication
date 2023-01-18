using BankApplication.Contracts;
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
    public class CustomerService:ICustomerService
    {
        public bool DepositeMoney(IRBIService rbis,RBI rbi,string bid,string aid,int amount,string currency)
        {
            Bank bank = rbis.GetBank(rbi,bid);
            IAccountService a = new AccountService();
            IBankService b = new BankService();
            if(bank.Currency.ContainsKey(currency))
            {
                float rate = bank.Currency[currency];
                Account acc = b.GetAccount(rbis,rbi, bid, aid);
                a.AddMoney(acc,amount * rate);
                string transactionId = "TXN"+ DateTime.Now.Microsecond.ToString();
                acc.Transactions.Add(new TransactionModel(TransactionModel.TypeOfTransaction.Deposit.ToString(), amount * rate,transactionId));
                return true;
            }
            return false;            
        }
        public bool WithdrawAmount(IRBIService rbis,RBI rbi, string bid,string aid,int amount)
        {
            IAccountService a = new AccountService();
            IBankService b = new BankService();
            Account acc = b.GetAccount(rbis,rbi,bid,aid);
            if (a.CanRemoveMoney(acc,amount))
            {
                string transactionId = "TXN" + DateTime.Now.Microsecond.ToString();
                acc.Transactions.Add(new TransactionModel(TransactionModel.TypeOfTransaction.Withdrawl.ToString(), amount * (-1),transactionId));
                return true;
            }
            return false;
            
        }
        public float GetBalance(IRBIService rbis, RBI rbi, string bid,string aid)
        {
            IBankService b = new BankService();
            return b.GetAccount(rbis, rbi,bid, aid).Balance;
        }
        public string Transfer(IRBIService rbis, RBI rbi, string senderBankId, string senderAccountId, float senderAmount, string recieverBankId, string recieverAccountId, float reciverAmount)
        {
            Bank senderBank = rbis.GetBank(rbi,senderBankId);
            Bank recieverBank = rbis.GetBank(rbi,recieverBankId);
            IBankService b = new BankService();
            if (b.IsAccountValid(recieverBank, recieverAccountId))
            {
                string transactionId = "TXN" + senderBank.BankId + senderAccountId + DateTime.Now.Microsecond.ToString();
                Account senderAccount = b.GetAccount(rbis,rbi, senderBankId, senderAccountId);
                Account recieverAccount = b.GetAccount(rbis,rbi, recieverBankId, recieverAccountId);
                IAccountService a = new AccountService();
                if (a.CanRemoveMoney(senderAccount, senderAmount))
                {
                    senderAccount.Transactions.Add(new TransactionModel(TransactionModel.TypeOfTransaction.Transfer.ToString(), -1 * senderAmount, transactionId, recieverBankId: recieverBankId, recieverAccountId: recieverAccountId));
                    a.AddMoney(recieverAccount, reciverAmount);
                    recieverAccount.Transactions.Add(new TransactionModel(TransactionModel.TypeOfTransaction.Transfer.ToString(), reciverAmount, transactionId, senderBankId: senderBankId, senderAccountID: senderAccountId));
                    return "Tranfer Successful";
                }
                else
                {
                    return "No Sufficient Balance";
                }
            }
            return "Invalid AccountId";
        }
        public float CalculateAmount(IRBIService rbis, RBI rbi, string bid,float amount,int op,string reciverBankId)
        {
            Bank b = rbis.GetBank(rbi,bid);
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
