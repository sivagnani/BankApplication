namespace BankApplication.Models
{
    public class Bank
    {
        public Dictionary<string, float> Currency = new Dictionary<string, float>();
        public List<Account> Accounts = new List<Account>();
        public List<User> Users = new List<User>();
        public string BankId { get; set; }
        public string Name { get; set; }
        public string DefaultCurrency { get; set; } = "INR";
        public float RTGS { get; set; } = 0;
        public float IMPS { get; set; } = 5;
        public float ORTGS { get; set; } = 2;
        public float OIMPS { get; set; } = 6;
        public Bank(string s)
        {
            Name = s;
            BankId = s.Substring(0, 3) + DateTime.Now.Microsecond.ToString();
            Currency.Add("INR", 1);
        }
    }
}
