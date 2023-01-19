using BankApplication.helper;

namespace BankApplication.Models
{
    public class Bank
    {
        public Dictionary<string, float> Currency = new Dictionary<string, float>();
        public List<Account> Accounts = new List<Account>();
        public List<User> Users = new List<User>();
        public string BankId { get; set; }
        public string Name { get; set; }
        public string DefaultCurrency { get; set; } = Constants.defaultCurrency;
        public float RTGS { get; set; } = Constants.initialRTGS;
        public float IMPS { get; set; } = Constants.initialIMPS;
        public float ORTGS { get; set; } = Constants.initialORTGS;
        public float OIMPS { get; set; } = Constants.initialOIMPS;
        public Bank(string s)
        {
            Name = s;
            BankId = s.Substring(0, 3) + DateTime.Now.Microsecond.ToString();
            Currency.Add("INR", 1);
        }
    }
}
