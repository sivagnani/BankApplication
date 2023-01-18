using BankApplication.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Services
{
    public class Operations
    {
        public int GetIntInput()
        {
            return Convert.ToInt32(Console.ReadLine());
        }
        public float GetFloatInput()
        {
            return Convert.ToSingle(Console.ReadLine());
        }
        public void EnterUserName()
        {
            Console.Write("Enter User Name: ");
        }
        public void EnterPassword()
        {
            Console.Write("Enter Password: ");
        }
    }
}
