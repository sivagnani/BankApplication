using BankApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BankApplication.Models
{
    public class Staff:User
    {
        public Staff(string name, string pass) : base(name, pass, "Admin")
        {

        }
    }
}
