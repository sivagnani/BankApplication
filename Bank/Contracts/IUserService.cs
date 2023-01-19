using BankApplication.Models;

namespace BankApplication.Contracts
{
    public interface IUserService
    {
        List<AccountTransaction> GetTransactions(IRBIService rbis,RBI rbi, string bid, string aid);
    }
}
