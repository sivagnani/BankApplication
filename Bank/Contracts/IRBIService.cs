using BankApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Contracts
{
    public interface IRBIService
    {
        //RBI Rbi { get; set; }
        string CreateBank(RBI rbi, string name);
        bool ValidateBank(RBI rbi, string bankId);
        Bank GetBank(RBI rbi, string id);
    }
}
