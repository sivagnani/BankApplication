using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;

namespace BankApplication.Services
{
    public class RBIService
    {
        public RBI rbi = new RBI();
        public string CreateBank(string name)
        {
            Bank bank = new Bank(name);
            rbi.Banks.Add(bank);
            return bank.BankId;
        }
        public bool ValidateBank(string bankId)
        {
            foreach (var a in rbi.Banks)
            {
                if (a.BankId == bankId) return true;
            }
            return false;
        }
        public Bank GetBank(string id) 
        {
            foreach (var a in rbi.Banks)
            {
                if (a.BankId == id) return a;
            }
            return null;
        }
        public string Transfer(string senderBankId,string senderAccountId,float senderAmount,string recieverBankId, string recieverAccountId, float reciverAmount)
        {
            Bank senderBank = GetBank(senderBankId);
            Bank recieverBank = GetBank(recieverBankId);
            BankService b = new BankService();
            if (b.ValidateAccount(recieverBank,recieverAccountId))
            {
                string transactionId = "TXN"+senderBank.BankId+senderAccountId+ DateTime.Now.Microsecond.ToString();
                Account senderAccount = b.GetAccount(this,senderBankId,senderAccountId);
                Account recieverAccount = b.GetAccount(this,recieverBankId,recieverAccountId);
                AccountService a = new AccountService();
                if (a.CanRemoveMoney(senderAccount,senderAmount))
                {
                    senderAccount.Transactions.Add(new TransactionModel(TransactionModel.TypeOfTransaction.Transfer.ToString(),-1*senderAmount, transactionId, recieverBankId: recieverBankId, recieverAccountId: recieverAccountId));
                    a.AddMoney(recieverAccount,reciverAmount);
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
        /*public string Revert(string transId)
        {
            foreach
        }*/
    }
}
