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
        public string bankId { get; set; }
        public string accountId { get; set; }
        public string customerName { get; set; }
        public float balance { get; set; } = 0;
    }
}
