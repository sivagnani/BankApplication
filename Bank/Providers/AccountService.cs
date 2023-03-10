using BankApplication.Contracts;
using BankApplication.Models;

namespace BankApplication.Providers
{
    public class AccountService:IAccountService
    {
        public void AddMoney(Account a,float amount)
        {
            a.Balance += amount;
        }
        public bool CanDeductMoney(Account a, float amount)
        {
            if (a.Balance >= amount)
            {
                a.Balance -= amount;
                return true;
            }
            return false;
        }
        public AccountTransaction GetTransaction(Account a,string tid)
        {
            foreach(var tx in a.Transactions)
            {
                if(tx.TransactionId==tid)
                    return tx;
            }
            return null;
        }
    }
}
