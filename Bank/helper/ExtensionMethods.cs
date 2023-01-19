using BankApplication.Contracts;
using BankApplication.Models;

namespace BankApplication.helper
{
    public static class ExtensionMethods
    {
        public static void CreateBankStatements(this Program p, InputsAndOutputs io, IRBIService rbis, RBI rbi, IBankService bankService, IAdminService adminService, IStaffService staffService, ICustomerService customerService, IUserService userService)
        {
            while (true)
            {
                io.Display("Do you want to create bank 1:Yes 2:No ");
                int op = io.GetInput<int>();
                switch (op)
                {
                    case 1:
                        io.Display("Enter bank name : ");
                        string bankName = io.GetInput<string>();
                        string bid = "";
                        try
                        {
                            bid = rbis.CreateBank(rbi, bankName);
                            io.Display("Bank Created Successfully\n");
                            io.Display("Enter Admin credentials\n");
                            io.Display("Enter User Name: ");
                            string userName = io.GetInput<string>();
                            io.Display("Enter Password: ");
                            string password = io.GetInput<string>();
                            bankService.CreateAccount(rbis, rbi, bid, userName, password, TypeOfUsers.Admin.ToString());
                        }
                        catch (Exception)
                        {
                            io.Display("The Length of bank must be more than 3 characters\n");
                        }
                        break;
                    case 2:
                        CredentialStatements(io, rbi, rbis, bankService, adminService, staffService, customerService, userService);
                        break;
                }
            }
        }
        public static void CredentialStatements(InputsAndOutputs io, RBI rbi, IRBIService rbis, IBankService bankService, IAdminService adminService, IStaffService staffService, ICustomerService customerService, IUserService userService)
        {
            while (true)
            {
                io.Display("List of banks\n");
                foreach (var a in rbi.Banks)
                {
                    io.Display(a.BankId+"\n");
                }
                io.Display("Enter bank id to login : ");
                string bid = io.GetInput<string>();
                if (rbis.ValidateBank(rbi, bid))
                {
                    io.Display("1.Admin\n2.Staff\n3.Customer\n4.Exit\n");
                    int option1 = io.GetInput<int>();
                    string userType = "";
                    switch (option1)
                    {
                        case 1: userType = TypeOfUsers.Admin.ToString(); break;
                        case 2: userType = TypeOfUsers.Staff.ToString(); break;
                        case 3: userType = TypeOfUsers.Customer.ToString(); break;
                        case 4: io.Display("Exiting....\n"); break;
                    }
                    if (option1 >= 4)
                    {
                        break;
                    }
                    io.Display("Enter User Name: ");
                    string userId = io.GetInput<string>();
                    io.Display("Enter Password: ");
                    string password = io.GetInput<string>();
                    if (bankService.IsUserValid(rbis, rbi, bid, userId, password, userType))
                    {
                        switch (option1)
                        {
                            case 1:
                                AdminOptions(io, rbi, rbis, bankService, adminService, bid);
                                break;
                            case 2:
                                StaffOptions(rbi, io, rbis, bankService, staffService, userService, bid);
                                break;
                            case 3:
                                string aid = bankService.GetAccountId(rbis, rbi, bid, userId);
                                CustomerOptions(rbi, io, rbis, bankService, customerService, userService, bid, aid);
                                break;
                            default:
                                io.Display("Please select valid option\n");
                                break;
                        }
                    }
                    else
                    {
                        io.Display("Invalid Credentials\n");
                    }
                }
                else
                {
                    io.Display("Enter valid Bank Name\n");
                }
            }
        }
        public static bool AdminOptions(InputsAndOutputs io, RBI rbi, IRBIService rbis, IBankService bankService, IAdminService adminService, string bid)
        {
            while (true)
            {
                io.Display("1. Create Staff Account\n2. Delete Staff Account\n3. Logout\n");
                io.Display("Select one option from above\n");
                int option = io.GetInput<int>();
                switch (option)
                {
                    case 1:
                        io.Display("Enter User Name: ");
                        string userId = io.GetInput<string>();
                        io.Display("Enter Password: ");
                        string password = io.GetInput<string>();
                        if (!bankService.CheckUser(rbis, rbi, bid, userId, TypeOfUsers.Staff.ToString()))
                        {
                            adminService.CreateStaffAccount(rbis, rbi, bid, userId, password);
                            io.Display("Account Created Successfully\n");
                        }
                        else
                            io.Display("Account Already Exist\n");
                        break;
                    case 2:
                        io.Display("Enter Staff Username to delete: ");
                        userId = io.GetInput<string>();
                        if (bankService.CheckUser(rbis, rbi, bid, userId, TypeOfUsers.Staff.ToString()))
                        {
                            adminService.RemoveStaffAccount(rbis, rbi, bid, userId);
                            io.Display("Account Deleted Successfully\n");
                        }
                        else
                            io.Display("Account Doesn't Exist\n");
                        break;
                    case 3:
                        io.Display("Logging out..\n");
                        return true;
                    default:
                        io.Display("Enter valid Option\n");
                        break;
                }

            }
        }
        public static bool StaffOptions(RBI rbi, InputsAndOutputs io, IRBIService rbis, IBankService bankService, IStaffService staffService, IUserService userService, string bid)
        {
            while (true)
            {
                io.Display("1. Create New Account\n2. Update/Delete Account\n3. Add new Accepted currency with exchange rate\n" +
                    "4. Add service charges for same bank account\n5. Add service charges for different bank account\n6. View Transaction History\n" +
                    "7. Revert Transaction\n8. Logout");
                io.Display("Select one option from above\n");
                int option = io.GetInput<int>();
                string aid;
                switch (option)
                {
                    case 1:
                        io.Display("Enter User Name");
                        string userId = io.GetInput<string>();
                        io.Display("Enter Password: ");
                        string password = io.GetInput<string>();
                        if (userId.Length < 3)
                        {
                            io.Display("The user name must be more than 3 to create a account\n");
                        }
                        else
                        {
                            if (!bankService.CheckUser(rbis, rbi, bid, userId, TypeOfUsers.Customer.ToString()))
                            {
                                staffService.CreateCustomerAccount(rbis, rbi, bid, userId, password);
                                io.Display("Account Created Successfully\n");
                            }
                            else
                                io.Display("Account Already Exist\n");
                        }
                        break;
                    case 2:
                        io.Display("1.Edit Account\n2.Delete Account\n");
                        int op = io.GetInput<int>();
                        if (op == 1)
                        {
                            io.Display("1. Update UserName\n2. Update Password\n");
                            op = io.GetInput<int>();
                            switch (op)
                            {
                                case 1:
                                    io.Display("Enter old user name: ");
                                    string oldName = io.GetInput<string>();
                                    io.Display("Enter new user name: ");
                                    string newName = io.GetInput<string>();
                                    if (bankService.CheckUser(rbis, rbi, bid, oldName, TypeOfUsers.Customer.ToString()))
                                    {
                                        staffService.ChangeUserName(rbis, rbi, bid, oldName, newName);
                                        io.Display("Updated Successfully\n");
                                    }
                                    else
                                        io.Display("Not Updated\n");
                                    break;
                                case 2:
                                    io.Display("Enter User Name");
                                    string user = io.GetInput<string>();
                                    io.Display("Enter new password: ");
                                    string pass = io.GetInput<string>();
                                    if (staffService.UpdatePassword(rbis, rbi, bid, user, pass))
                                        io.Display("Password changed\n");
                                    else
                                        io.Display("Password Not changed\n");
                                    break;
                                default:
                                    io.Display("Select Valid Option\n");
                                    break;
                            }
                        }
                        else if (op == 2)
                        {
                            io.Display("Enter UserName to delete: ");
                            userId = io.GetInput<string>();
                            if (staffService.RemoveCustomerAccount(rbis, rbi, bid, userId))
                                io.Display("Account Deleted Successfully\n");
                            else
                                io.Display("Account doesn't Exist\n");
                        }
                        else
                            io.Display("Enter valid option");
                        break;
                    case 3:
                        io.Display("Enter the name of new Accepted currency: ");
                        string currency = io.GetInput<string>();
                        io.Display("Enter the exchange rate of the currency with INR : ");
                        float rate = io.GetInput<float>();
                        staffService.AddCurrency(rbis, rbi, bid, currency, rate);
                        break;
                    case 4:
                        io.Display("1. RTGS\n2. IMPS\n");
                        op = io.GetInput<int>();
                        switch (op)
                        {
                            case 1:
                                io.Display("Enter the RTGS: ");
                                float val = io.GetInput<float>();
                                staffService.SetServiceCharges(rbis, rbi, bid, "RTGS", val);
                                io.Display("New RTGS for same bank is :" + rbis.GetBank(rbi, bid).RTGS.ToString()+"\n");
                                break;
                            case 2:
                                io.Display("Enter the IMPS: ");
                                val = io.GetInput<float>();
                                staffService.SetServiceCharges(rbis, rbi, bid, "IMPS", val);
                                io.Display("New IMPS for same bank is :" + rbis.GetBank(rbi, bid).IMPS.ToString()+"\n");
                                break;
                        }
                        break;
                    case 5:
                        io.Display("1. RTGS\n2. IMPS\n");
                        op = io.GetInput<int>();
                        switch (op)
                        {
                            case 1:
                                io.Display("Enter the RTGS: ");
                                float val = io.GetInput<float>();
                                staffService.SetServiceCharges(rbis, rbi, bid, "ORTGS", val);
                                io.Display("New RTGS for other bank is :" + rbis.GetBank(rbi, bid).ORTGS.ToString() + "\n");
                                break;
                            case 2:
                                io.Display("Enter the IMPS: ");
                                val = io.GetInput<float>();
                                staffService.SetServiceCharges(rbis, rbi, bid, "OIMPS", val);
                                io.Display("New IMPS for other bank is :" + rbis.GetBank(rbi, bid).OIMPS.ToString() + "\n");
                                break;
                        }
                        break;
                    case 6:
                        io.Display("All the accounts in the bank are: \n");
                        List<Account> l = rbis.GetBank(rbi, bid).Accounts;
                        foreach (var i in l)
                        {
                            io.Display(i.AccountId + "\t" + i.CustomerName+"\n");
                        }
                        io.Display("Enter one Account Id from above\n");
                        string str = io.GetInput<string>();
                        PrintTransactions(io, userService.GetTransactions(rbis, rbi, bid, str));
                        break;
                    case 7:
                        io.Display("Enter the Account id: ");
                        aid = io.GetInput<string>();
                        if (bankService.IsAccountValid(rbis.GetBank(rbi, bid), aid))
                        {
                            io.Display("Enter the transaction id: ");
                            string tid = io.GetInput<string>();
                            staffService.RevertTransaction(rbis, rbi, bid, aid, tid);
                        }
                        else
                        {
                            io.Display("The Account doesn't Exist\n");
                        }

                        break;
                    case 8:
                        io.Display("Logging out..\n");
                        return true;
                    default:
                        io.Display("Enter valid option\n");
                        break;
                }
            }
        }
        public static bool CustomerOptions(RBI rbi, InputsAndOutputs io, IRBIService rbis, IBankService bankService, ICustomerService customerService, IUserService userService, string bid, string aid)
        {
            while (true)
            {
                io.Display("1. Deposit Amount\n2. Withdraw Amount\n3. Transfer Funds\n4. View Transaction History\n5. Logout\n");
                io.Display("Select one option from above\n");
                int option = io.GetInput<int>();
                switch (option)
                {
                    case 1:
                        io.Display("Enter Amout to deposit: ");
                        int amount = io.GetInput<int>();
                        io.Display("Enter the currency of deposit: ");
                        string currency = io.GetInput<string>();
                        if (customerService.DepositeMoney(rbis, rbi, bid, aid, amount, currency))
                        {
                            io.Display("Deposite is successful\n");
                            io.Display("Your Account Balance is: " + customerService.GetBalance(rbis, rbi, bid, aid).ToString()+"\n");
                        }
                        else
                        {
                            io.Display("The currency is not accepted by the bank\n");
                        }
                        break;
                    case 2:
                        io.Display("Enter the Amount to Withdraw: ");
                        amount = io.GetInput<int>();
                        if (customerService.WithdrawAmount(rbis, rbi, bid, aid, amount))
                            io.Display("Your Account Balance is: " + customerService.GetBalance(rbis, rbi, bid, aid).ToString()+"\n");
                        else
                            io.Display("Insufficient Balance\n");
                        break;
                    case 3:
                        io.Display("Enter Reciever BankId: ");
                        string b = io.GetInput<string>();
                        io.Display("Enter Reciever AccountId: ");
                        string i = io.GetInput<string>();
                        io.Display("Select one 1.RTGS 2.IMPS : ");
                        option = io.GetInput<int>();
                        io.Display("Enter the amount to be transeferred: ");
                        float a = io.GetInput<float>();
                        float ra = customerService.CalculateAmount(rbis, rbi, bid, a, option, b);
                        if (rbis.ValidateBank(rbi, b))
                        {
                            io.Display(customerService.Transfer(rbis, rbi, bid, aid, a, b, i, ra)+"\n");
                        }
                        else
                        {
                            io.Display("Bank doesn't Exist\n");
                        }

                        break;
                    case 4:
                        PrintTransactions(io, userService.GetTransactions(rbis, rbi, bid, aid));
                        break;
                    case 5:
                        io.Display("Logging out..\n");
                        return true;
                    case 6:
                        io.Display("Enter valid option\n");
                        break;
                }
            }
        }
        public static void PrintTransactions(InputsAndOutputs io, List<AccountTransaction> trans)
        {
            io.Display("TransactionId\tS.BId\tS.AId\tR.BId\tR.AId\tTransactionType\tAmount\n");
            float sum = 0;
            foreach (var i in trans)
            {
                io.Display(i.TransactionId + "\t" + i.SenderBankId + "\t" + i.SenderAccountId + "\t" + i.RecieverBankId + "\t" + i.RecieverAccountId + "\t" + i.TransactionType + "\t" + i.Amount.ToString()+"\n");
                sum += i.Amount;

            }
            io.Display("Total\t\t\t\t\t\t\t\t\t\t\t " + sum.ToString()+"\n");
        }
    }
}
