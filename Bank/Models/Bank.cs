using BankApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Models
{
    public class Bank
    {
        public Dictionary<string, float> Currency = new Dictionary<string, float>();
        public string bankId;
        public string Name { get; set; }
        public string defaultCurrency { get; set; } = "INR";
        public float RTGS { get; set; } = 0;
        public float IMPS { get; set; } = 5;
        public float ORTGS { get; set; } = 2;
        public float OIMPS { get; set; } = 6;
    }
}
