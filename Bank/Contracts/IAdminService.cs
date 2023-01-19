using BankApplication.Models;

namespace BankApplication.Contracts
{
    public interface IAdminService
    {
        void CreateStaffAccount(IRBIService rbis, RBI rbi, string bid, string name, string pass);
        void RemoveStaffAccount(IRBIService rbis, RBI rbi, string bid, string name);
    }
}
