using BankApplication.Models;

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
