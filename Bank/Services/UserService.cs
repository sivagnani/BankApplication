using BankApplication.Contracts;
using BankApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Services
{
    public class UserService:IUserService
    {
        public List<TransactionModel> GetTransactions(IRBIService rbis, RBI rbi, string bid,string aid)
        {
            IBankService b = new BankService();
            return b.GetAccount(rbis,rbi, bid, aid).Transactions;
        }
    }
}
