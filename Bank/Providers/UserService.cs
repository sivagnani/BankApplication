using BankApplication.Contracts;
using BankApplication.Models;

namespace BankApplication.Providers
{
    public class UserService:IUserService
    {
        public List<AccountTransaction> GetTransactions(IRBIService rbis, RBI rbi, string bid,string aid)
        {
            IBankService b = new BankService();
            return b.GetAccount(rbis,rbi, bid, aid).Transactions;
        }
    }
}
