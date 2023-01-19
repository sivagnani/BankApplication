using BankApplication.Contracts;
using BankApplication.Models;

namespace BankApplication.Providers
{
    public class RBIService:IRBIService
    {
        //public RBI Rbi { get; set; }
        public string CreateBank(RBI rbi,string name)
        {
            if(name.Length < 3)
            {
                throw new InvalidDataException();
            }
            Bank bank = new Bank(name);
            rbi.Banks.Add(bank);
            return bank.BankId;
        }
        public bool ValidateBank(RBI rbi,string bankId)
        {
            foreach (var a in rbi.Banks)
            {
                if (a.BankId == bankId) return true;
            }
            return false;
        }
        public Bank GetBank(RBI rbi,string id) 
        {
            foreach (var a in rbi.Banks)
            {
                if (a.BankId == id) return a;
            }
            return null;
        }
    }
}
