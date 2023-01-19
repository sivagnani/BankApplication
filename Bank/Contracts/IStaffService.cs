using BankApplication.Models;


namespace BankApplication.Contracts
{
    public interface IStaffService
    {
        void CreateCustomerAccount(IRBIService rbis,RBI rbi, string bid, string userName, string password);
        bool RemoveCustomerAccount(IRBIService rbis, RBI rbi, string bid, string userName);
        bool DeleteAccount(Bank b, string s);
        void ChangeUserName(IRBIService rbis, RBI rbi, string bid, string oldName, string newName);
        bool UpdatePassword(IRBIService rbis, RBI rbi, string bid, string u, string p);
        void AddCurrency(IRBIService rbis, RBI rbi, string bid, string currency, float rate);
        void SetServiceCharges(IRBIService rbis, RBI rbi, string bid, string serviceCharge, float d);
        void RevertTransaction(IRBIService rbis, RBI rbi, string bid, string aid, string transId);
    }
}
