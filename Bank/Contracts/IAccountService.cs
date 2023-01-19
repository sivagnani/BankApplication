using BankApplication.Models;

namespace BankApplication.Contracts
{
    public interface IAccountService
    {
        void AddMoney(Account a, float amount);
        bool CanDeductMoney(Account a, float amount);
        AccountTransaction GetTransaction(Account a, string tid);
    }
}
