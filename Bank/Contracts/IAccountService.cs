using BankApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Contracts
{
    public interface IAccountService
    {
        void AddMoney(Account a, float amount);
        bool CanRemoveMoney(Account a, float amount);
        TransactionModel GetTransaction(Account a, string tid);
    }
}
