using BankApplication;
using BankApplication.Services;
using System.Xml.Linq;

class Program
{
    public static void Main(string[] args)
    {
        Operations obj = new Operations();
        Program controlObject = new Program();
        List<BankService> bankList = new List<BankService>();
        bankList = controlObject.createBank(obj);
        controlObject.Credentials(obj,bankList);
    }
    public List<BankService> createBank(Operations ob)
    {
        List<BankService> bankList = new List<BankService>();
        while (true)
        {
            Console.Write("Do you want to create bank 1:Yes 2:No ");
            int op = ob.GetIntInput();
            if (op == 1)
            {
                Console.Write("Enter bank name : ");
                string bankName = Console.ReadLine();
                if (!ob.ValidateBank(bankList, bankName))
                {
                    BankService bank = ob.CreateBank(bankName);
                    Console.WriteLine("Bank Created Successfully");
                    Console.WriteLine("Enter Admin credentials");
                    Console.Write("Enter Admin User Name: ");
                    string userName = Console.ReadLine();
                    Console.Write("Enter Admin password: ");
                    string password = Console.ReadLine();
                    bank = ob.CreateAdminAccount(bank, userName, password);
                    bankList.Add(bank);
                }
                else
                {
                    Console.WriteLine("Bank Arleady Exists..");
                }
            }
            else
            {
                break;
            }
        }
        return bankList;
    }
    public void Credentials(Operations obj,List<BankService> bankList){
        while(true)
        {
            Console.WriteLine("List of banks");
            foreach (var a in bankList)
            {
                Console.WriteLine(a.bankModel.Name);
            }
            Console.Write("Enter bank name to login : ");
            string bankName = Console.ReadLine();
            if (obj.ValidateBank(bankList, bankName))
            {
                Console.WriteLine("1.Admin");
                Console.WriteLine("2.Staff");
                Console.WriteLine("3.Customer");
                Console.WriteLine("4.Exit");
                int option1 = obj.GetIntInput();
                string userType="";
                switch (option1)
                {
                    case 1: userType = "Admin"; break;
                    case 2: userType= "Staff"; break;
                    case 3: userType = "Customer"; break;
                    case 4: Console.WriteLine("Exiting...."); break;
                }
                if(option1== 4)
                {
                    break;
                }
                Console.Write("Enter UserId: ");
                string userId = Console.ReadLine();
                Console.Write("Ënter Password: ");
                string password = Console.ReadLine();
                if (obj.SelectLogin(bankList, bankName, userId, password,userType))
                {
                    switch (option1)
                    {
                        case 1:
                            Console.WriteLine("Admin");
                            AdminOptions((AdminService)obj.GetObject(bankList, bankName, userId, "Admin"), obj);
                            break;
                        case 2:
                            Console.WriteLine("Staff");
                            StaffOptions((StaffService)obj.GetObject(bankList, bankName, userId, "Staff"), obj,bankList);
                            break;
                        case 3:
                            Console.WriteLine("Customer");
                            CustomerOptions((CustomerService)obj.GetObject(bankList, bankName, userId, "Customer"), obj,bankList);
                            break;
                        default:
                            Console.WriteLine("Please select valid option");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Credentials");
                }
            }
            else
            {
                Console.WriteLine("Enter valid Bank Name");
            }
        }
    }
    public bool AdminOptions(AdminService a,Operations obj)
    {
        while(true)
        {
            Console.WriteLine("1. Create Staff Account");
            Console.WriteLine("2. Delete Staff Account");
            Console.WriteLine("3. Logout");
            Console.WriteLine("Select one option from above");
            int option = obj.GetIntInput();
            switch(option)
            {
                case 1:
                    Console.Write("Enter Staff Username: ");
                    string userId = Console.ReadLine();
                    Console.Write("Ënter Staff Password: ");
                    string password = Console.ReadLine();
                    if (a.CreateStaffAccount(userId, password))
                        Console.WriteLine("Account Created Successfully");
                    else
                        Console.WriteLine("Account Already Exist");
                    break;
                case 2:
                    Console.Write("Enter Staff Username to delete: ");
                    userId = Console.ReadLine();
                    if (a.RemoveStaffAccount(userId))
                        Console.WriteLine("Account Deleted Successfully");
                    else
                        Console.WriteLine("Account Does'nt Exist");
                    break;
                default:
                    Console.WriteLine("Logging out..");
                    return true;

            }

        }
    }
    public bool StaffOptions(StaffService s,Operations obj, List<BankService> bankList)
    {
        while (true)
        {
            Console.WriteLine("1. Create New Account");
            Console.WriteLine("2. Update/Delete Account");
            Console.WriteLine("3. Add new Accepted currency with exchange rate");
            Console.WriteLine("4. Add service charges for same bank account");
            Console.WriteLine("5. Add service charges for different bank account");
            Console.WriteLine("6. View Transaction History");
            Console.WriteLine("7. Revert Transaction");
            Console.WriteLine("8. Logout");
            Console.WriteLine("Select one option from above");
            int option = obj.GetIntInput();
            switch (option)
            {
                case 1:
                    Console.Write("Enter Username: ");
                    string userId = Console.ReadLine();
                    Console.Write("Ënter Password: ");
                    string password = Console.ReadLine();
                    if (s.CreateCustomerAccount(userId, password))
                        Console.WriteLine("Account Created Successfully");
                    else
                        Console.WriteLine("Account Already Exist");
                    break;
                case 2:
                    Console.WriteLine("1.Edit Account");
                    Console.WriteLine("2.Delete Account");
                    int op = obj.GetIntInput();
                    if (op == 1)
                    {
                        Console.WriteLine("1. Update UserName");
                        Console.WriteLine("2. Update Password");
                        op = obj.GetIntInput();
                        switch (op)
                        {
                            case 1: 
                                Console.Write("Enter old user name: ");
                                string oldName = Console.ReadLine(); 
                                Console.Write("Enter new user name: ");
                                string newName = Console.ReadLine();
                                if (s.UpdateUsername(oldName, newName))
                                    Console.WriteLine("Updated Successfully");
                                else
                                    Console.WriteLine("Not Updated");
                                break;
                            case 2:
                                Console.Write("Enter user name: ");
                                string user = Console.ReadLine();
                                Console.Write("Enter new password: ");
                                string pass = Console.ReadLine();
                                if (s.UpdatePassword(user, pass))
                                    Console.WriteLine("Password changed");
                                else
                                    Console.WriteLine("Password Not changed");
                                break;
                            default:
                                Console.WriteLine("Select Valid Option");
                                break;
                        }
                    }
                    else if (op == 2)
                    {
                        Console.Write("Enter UserName to delete: ");
                        userId = Console.ReadLine();
                        if (s.RemoveCustomerAccount(userId))
                            Console.WriteLine("Account Deleted Successfully");
                        else
                            Console.WriteLine("Account doesn't Exist");
                    }
                    break;
                case 3:
                    Console.Write("Enter the name of new Accepted currency: ");
                    string currency = Console.ReadLine();
                    Console.Write("Enter the exchange rate of the currency with INR : ");
                    float rate = obj.GetFloatInput();
                    s.AddCurrency(currency, rate);
                    break;
                case 4:
                    Console.WriteLine("1. RTGS");
                    Console.WriteLine("2. IMPS");
                    op = obj.GetIntInput();
                    switch(op)
                    {
                        case 1:
                            Console.Write("Enter the RTGS: ");
                            float val = obj.GetFloatInput();
                            s.SetServiceCharges("RTGS", val);
                            Console.WriteLine("New RTGS for same bank is :"+s.bank.bankModel.RTGS.ToString());
                            break;
                        case 2:
                            Console.Write("Enter the IMPS: ");
                            val  = obj.GetFloatInput();
                            s.SetServiceCharges("IMPS", val);
                            Console.WriteLine("New IMPS for same bank is :" + s.bank.bankModel.IMPS.ToString());
                            break;
                    }
                    break;
                case 5:
                    Console.WriteLine("1. RTGS");
                    Console.WriteLine("2. IMPS");
                    op = obj.GetIntInput();
                    switch (op)
                    {
                        case 1:
                            Console.Write("Enter the RTGS: ");
                            float val = obj.GetFloatInput();
                            s.SetServiceCharges("ORTGS", val);
                            Console.WriteLine("New RTGS for other bank is :" + s.bank.bankModel.ORTGS.ToString());
                            break;
                        case 2:
                            Console.Write("Enter the IMPS: ");
                            val = obj.GetFloatInput();
                            s.SetServiceCharges("OIMPS", val);
                            Console.WriteLine("New IMPS for other bank is :" + s.bank.bankModel.OIMPS.ToString());
                            break;
                    }
                    break;
                case 6:
                    Console.WriteLine("All the accounts in the bank are: ");
                    List<AccountService> l = s.bank.accounts;
                    foreach(var i in l)
                    {
                        Console.WriteLine(i.accountModel.accountId+"\t"+i.accountModel.customerName);
                    }
                    Console.WriteLine("Enter one Account Id from above");
                    string str = Console.ReadLine();
                    foreach(var i in l)
                    {
                        if (i.accountModel.accountId == str)
                        {
                            PrintTransactions(i.transactions);
                        }
                    }
                    break;


                default:
                    Console.WriteLine("Logging out..");
                    return true;
            }
        }
    }
    public bool CustomerOptions(CustomerService c, Operations obj, List<BankService> bankList)
    {
        while (true)
        {
            Console.WriteLine("1. Deposit Amount");
            Console.WriteLine("2. Withdraw Amount");
            Console.WriteLine("3. Transfer Funds");
            Console.WriteLine("4. View Transaction History");
            Console.WriteLine("5. Logout");
            Console.WriteLine("Select one option from above");
            int option = obj.GetIntInput();
            switch (option)
            {
                case 1:
                    Console.Write("Enter Amout to deposit: ");
                    int amount = obj.GetIntInput();
                    Console.Write("Enter the currency of deposit: ");
                    string currency = Console.ReadLine();
                    if (c.DepositeMoney(amount, currency))
                    {
                        Console.WriteLine("Deposite is successful");
                        Console.WriteLine("Your Account Balance is: " + c.GetBalance().ToString());
                    }
                    else
                    {
                        Console.WriteLine("The currency is not accepted by the bank");
                    }
                    break;
                case 2:
                    Console.Write("Enter the Amount to Withdraw: ");
                    amount = obj.GetIntInput();
                    if (c.WithdrawAmount(amount))
                        Console.WriteLine("Your Account Balance is: " + c.GetBalance().ToString());
                    else
                        Console.WriteLine("Insufficient Balance");
                    break;
                case 3:
                    Console.Write("Enter Reciever Bankname: ");
                    string b = Console.ReadLine();
                    Console.Write("Enter Reciever AccountId: ");
                    string i = Console.ReadLine();
                    Console.Write("Select one 1.RTGS 2.IMPS : ");
                    option = obj.GetIntInput();
                    Console.Write("Enter the amount to be transeferred: ");
                    int a = obj.GetIntInput();
                    Console.WriteLine(c.CalculateAmount(a,option,b));

                    break;
                case 4:
                    PrintTransactions(c.account.transactions);
                    break;

                default:
                    Console.WriteLine("Logging out..");
                    return true;
            }
        }
    }
    public void PrintTransactions(List<TransactionService> trans)
    {
        Console.WriteLine("TransactionId\tS.BankId\tS.AccountId\tR.BankId\tR.AccountId\tAmount");
        foreach (var i in trans)
        {
            Console.WriteLine(i.transactionModel.transactionId + "\t" + i.transactionModel.senderBankId + "\t\t" + i.transactionModel.senderAccountId + "\t\t" + i.transactionModel.recieverBankId + "\t\t" + i.transactionModel.recieverAccountId + "\t\t" + i.transactionModel.Amount.ToString());
        }
    }
}