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
        public BankService CreateBank(string name)
        {
            BankService bank = new BankService(name);
            return bank;
        }
        public bool ValidateBank(List<BankService> bankList, string bankName)
        {
            foreach (var a in bankList)
            {
                if (a.bankModel.Name == bankName) return true;
            }
            return false;
        }
        public BankService CreateAdminAccount(BankService bank, string userName, string password)
        {
            bank.Users.Add(new UserService(userName, password, "Admin"));
            bank.admins.Add(new AdminService(userName, password, bank));
            return bank;
        }
        public bool SelectLogin(List<BankService> bankList, string bankName, string userId, string password,string userType)
        {
            foreach (var a in bankList)
            {
                if (a.bankModel.Name == bankName)
                {
                    return ValidateUserLogin(a.Users, userId, password,userType);
                }
            }
            return false;
        }
        public bool ValidateUserLogin(List<UserService> list, string userId, string password,string userType)
        {
            foreach (var a in list)
            {
                if (a.userModel.userName == userId && a.userModel.password== password && a.userModel.userType==userType)
                {
                    return true;
                }
            }
            return false;
        }
        public object GetObject(List<BankService> bankList, string bankName, string userId, string userType)
        {
            object b = null;
            foreach (var i in bankList)
            {
                if (i.bankModel.Name == bankName)
                {
                    switch (userType)
                    {
                        case "Admin":
                            foreach(var a in i.admins)
                            {
                                if(a.userModel.userName == userId)
                                {
                                    return a;
                                }
                            }
                            break;
                        case "Staff":
                            foreach (var a in i.staffs)
                            {
                                if (a.userModel.userName == userId)
                                {
                                    return a;
                                }
                            }
                            break;
                        case "Customer":
                            foreach (var a in i.Customers)
                            {
                                if (a.userModel.userName == userId)
                                {

                                    return a;
                                }
                            }
                            break;
                    }
                }
            }
            return b;
        }
        public bool PerformTransfer(List<BankService> banks, string senderBankName, string senderAccountID, string reciverBankName, string recieverAccountId, float a)
        {
            foreach(var i in banks)
            {
                if(i.bankModel.Name == senderBankName)
                {
                    //SenderUpdate(senderBankName, senderAccountID, reciverBankName, recieverAccountId, a);
                }
                if(i.bankModel.Name == reciverBankName)
                {
                    //RecieverUpdate(senderBankName, senderAccountID, reciverBankName, recieverAccountId, a);
                }
            }
            return true;
        }
    }
}
