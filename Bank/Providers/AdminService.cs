using BankApplication.Contracts;
using BankApplication.Models;
using BankApplication.helper;

namespace BankApplication.Providers
{
    public class AdminService:IAdminService
    {
        public void CreateStaffAccount(IRBIService rbis,RBI rbi,string bid,string name,string pass)
        {
            IBankService b = new BankService();
            b.CreateAccount(rbis, rbi,bid, name, pass,TypeOfUsers.Staff.ToString());
        }
        public void RemoveStaffAccount(IRBIService rbis,RBI rbi, string bid, string name)
        {
            IBankService b  =new BankService();
            b.RemoveUser(rbis, rbi,bid, name,TypeOfUsers.Staff.ToString());
        }

    }
}
