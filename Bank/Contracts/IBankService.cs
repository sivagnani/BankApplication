using BankApplication.Models;
using BankApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Contracts
{
    internal interface IBankService
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
