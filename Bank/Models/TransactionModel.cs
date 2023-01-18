using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Models
{
    public class TransactionModel
    {
        public string SenderBankId { get; set; }
        public string RecieverBankId { get; set; }
        public string SenderAccountId { get; set; }
        public string RecieverAccountId { get; set; }
        public string TransactionId { get; set; }
        public enum TypeOfTransaction
        {
            Deposit,
            Withdrawl,
            Transfer,
            Reverted
        }
        public string TransactionType { get; set; }
        public float Amount { get; set; }
        public TransactionModel(string transactionType, float a, string transactionId, string senderBankId = "\t", string senderAccountID = "\t", string recieverBankId = "\t", string recieverAccountId = "\t")
        {
            SenderBankId = senderBankId;
            RecieverBankId = recieverBankId;
            SenderAccountId = senderAccountID;
            RecieverAccountId = recieverAccountId;
            Amount = a;
            TransactionType = transactionType;
            TransactionId = transactionId;
        }
    }
}
