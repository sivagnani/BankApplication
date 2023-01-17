using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Services;

namespace BankApplication.Models
{
    public class Account
    {
        public string BankId { get; set; }
        public string AccountId { get; set; }
        public string CustomerName { get; set; }
        public float Balance { get; set; } = 0;
        public List<TransactionModel> Transactions = new List<TransactionModel>();
        public Account(string name, string bankId)
        {
            CustomerName = name;
            bankId = bankId;
            AccountId = name.Substring(0, 3) + DateTime.Now.Microsecond.ToString();
            Balance = 0;
        }
    }
}
