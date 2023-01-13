using BankApplication.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Services
{
    public class AdminService:UserService
    {
        public BankService bank;
        public AdminService(string name, string pass,BankService bank):base(name,pass,"Admin")
        {
            this.bank = bank;
        }
        public bool CreateStaffAccount(string name,string pass)
        {
            if (bank.CheckUser(name, "Staff"))
            {
                bank.Users.Add(new UserService(userModel.userName, userModel.password, "Staff")); 
                bank.staffs.Add(new StaffService(userModel.userName, userModel.password, bank));
                return true;
            }
            return false;        
        }
        public bool RemoveStaffAccount(string name)
        {
            if (!bank.CheckUser(name, "Staff"))
            {
                bank.RemoveUser(name, "Staff");
                RemoveStaff(name);
                return true;
            }
            return false;
        }
        public void RemoveStaff(string userName)
        {
            foreach (var i in bank.staffs)
            {
                if (i.userModel.userName == userName)
                {
                    bank.staffs.Remove(i);
                    break;
                }
            }
        }
    }
}
