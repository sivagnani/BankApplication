namespace BankApplication.Models
{
    public class AccountTransaction
    {
        public string SenderBankId { get; set; }
        public string RecieverBankId { get; set; }
        public string SenderAccountId { get; set; }
        public string RecieverAccountId { get; set; }
        public string TransactionId { get; set; }
        public string TransactionType { get; set; }
        public float Amount { get; set; }
        public AccountTransaction(string transactionType, float a, string transactionId, string senderBankId = "\t", string senderAccountID = "\t", string recieverBankId = "\t", string recieverAccountId = "\t")
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
