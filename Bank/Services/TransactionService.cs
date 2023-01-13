using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BankApplication.Models;


namespace BankApplication.Services
{
    public class TransactionService
    {
        public Transaction transactionModel = new Transaction();
        public TransactionService(string senderBankId,string senderAccountID, string reciverBankId, string recieverAccountId,float a)
        {
            transactionModel.senderBankId = senderBankId;
            transactionModel.recieverBankId =reciverBankId;
            transactionModel.senderAccountId = senderAccountID;
            transactionModel.recieverAccountId = recieverAccountId;
            transactionModel.Amount = a;
            DateTime centuryBegin = new DateTime(2001, 1, 1);
            long elapsedTicks = DateTime.Now.Second - centuryBegin.Second;
            transactionModel.transactionId = "TXN"+senderBankId+senderAccountID+elapsedTicks.ToString();
        }
    }
}
