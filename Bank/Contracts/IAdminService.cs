using BankApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;

namespace BankApplication.Contracts
{
    internal interface IAdminService
    {
        void CreateStaffAccount(IRBIService rbis, RBI rbi, string bid, string name, string pass);
        void RemoveStaffAccount(IRBIService rbis, RBI rbi, string bid, string name);
    }
}
