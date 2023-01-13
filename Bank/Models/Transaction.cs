using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Models
{
    public class Transaction
    {
        public string senderBankId { get; set; }
        public string recieverBankId { get; set; }
        public string senderAccountId { get; set; }
        public string recieverAccountId { get; set; }
        public string transactionId { get; set; }
        public string transactionType { get; set; }
        public float Amount { get; set; }
    }
}
