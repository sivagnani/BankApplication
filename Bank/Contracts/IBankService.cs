using BankApplication.Models;

namespace BankApplication.Contracts
{
    public interface IBankService
    {
        void CreateAccount(IRBIService rbis,RBI rbi, string bid, string userName, string password, string userType);
        bool IsUserValid(IRBIService rbis, RBI rbi, string bid, string userId, string password, string userType);
        bool CheckUser(IRBIService rbis, RBI rbi, string bid, string userName, string userType);
        void RemoveUser(IRBIService rbis, RBI rbi, string bid, string userName, string userType);
        bool IsAccountValid(Bank bank, string accountId);
        string GetAccountId(IRBIService rbis, RBI rbi, string bid, string name);
        Account GetAccount(IRBIService rbis, RBI rbi, string bid, string accountId);
    }
}
