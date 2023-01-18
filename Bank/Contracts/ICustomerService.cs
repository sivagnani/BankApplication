using BankApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;

namespace BankApplication.Contracts
{
    public interface ICustomerService
    {
        bool DepositeMoney(IRBIService rbis,RBI rbi, string bid, string aid, int amount, string currency);
        bool WithdrawAmount(IRBIService rbis, RBI rbi, string bid, string aid, int amount);
        float GetBalance(IRBIService rbis, RBI rbi, string bid, string aid);
        string Transfer(IRBIService rbis, RBI rbi, string senderBankId, string senderAccountId, float senderAmount, string recieverBankId, string recieverAccountId, float reciverAmount);
        float CalculateAmount(IRBIService rbis, RBI rbi, string bid, float amount, int op, string reciverBankId);
    }
}
