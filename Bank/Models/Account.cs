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
            BankId = bankId;
            AccountId = name.Substring(0, 3) + DateTime.Now.Microsecond.ToString();
            Balance = 0;
        }
    }
}
