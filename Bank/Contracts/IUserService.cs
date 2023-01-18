using BankApplication.Models;
using BankApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Contracts
{
    public interface IUserService
    {
        List<TransactionModel> GetTransactions(IRBIService rbis,RBI rbi, string bid, string aid);
    }
}
