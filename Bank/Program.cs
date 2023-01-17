using BankApplication;
using BankApplication.Services;
using System.Xml.Linq;
using BankApplication.Models;

class Program
{
    static RBIService rbis = new RBIService();
    static BankService bankService = new BankService();
    static AdminService adminService = new AdminService();
    static CustomerService customerService = new CustomerService();
    static AccountService accountService = new AccountService();
    static StaffService staffService = new StaffService();
    static TransactionService transactionService = new TransactionService();
    static Operations obj = new Operations();
    public static void Main(string[] args)
    {
        Program controlObject = new Program();
        controlObject.CreateBankStatements();
        controlObject.CredentialStatements();
    }
    public void CreateBankStatements()
    {
        while (true)
        {
            Console.Write("Do you want to create bank 1:Yes 2:No ");
            int op = obj.GetIntInput();
            if (op == 1)
            {
                Console.Write("Enter bank name : ");
                string bankName = Console.ReadLine();
                string bid = rbis.CreateBank(bankName);
                Console.WriteLine("Bank Created Successfully");
                Console.WriteLine("Enter Admin credentials");
                obj.EnterUserName();
                string userName = Console.ReadLine();
                obj.EnterPassword();
                string password = Console.ReadLine();
                bankService.CreateAccount(rbis,bid, userName, password,User.TypesOfUser.Admin.ToString());
            }
            else
            {
                break;
            }
        }
    }
    public void CredentialStatements(){
        while(true)
        {
            Console.WriteLine("List of banks");
            foreach (var a in rbis.rbi.Banks)
            {
                Console.WriteLine(a.BankId);
            }
            Console.Write("Enter bank id to login : ");
            string bid = Console.ReadLine();
            if (rbis.ValidateBank(bid))
            {
                Console.WriteLine("1.Admin");
                Console.WriteLine("2.Staff");
                Console.WriteLine("3.Customer");
                Console.WriteLine("4.Exit");
                int option1 = obj.GetIntInput();
                string userType="";
                switch (option1)
                {
                    case 1: userType = User.TypesOfUser.Admin.ToString(); break;
                    case 2: userType= User.TypesOfUser.Staff.ToString(); break;
                    case 3: userType = User.TypesOfUser.Staff.ToString(); break;
                    case 4: Console.WriteLine("Exiting...."); break;
                }
                if(option1== 4)
                {
                    break;
                }
                obj.EnterUserName();
                string userId = Console.ReadLine();
                obj.EnterPassword();
                string password = Console.ReadLine();
                if (bankService.ValidateUserLogin(rbis,bid,userId,password,userType))
                {
                    switch (option1)
                    {
                        case 1:
                            Console.WriteLine("Admin");
                            AdminOptions(bid);
                            break;
                        case 2:
                            Console.WriteLine("Staff");
                            StaffOptions(bid);
                            break;
                        case 3:
                            Console.WriteLine("Customer");
                            string aid = bankService.GetAccountId(rbis, bid, userId);
                            CustomerOptions(bid,aid);
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
    public bool AdminOptions(string bid)
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
                    obj.EnterUserName();
                    string userId = Console.ReadLine();
                    obj.EnterPassword();
                    string password = Console.ReadLine();
                    if(!bankService.CheckUser(rbis,bid,userId,User.TypesOfUser.Staff.ToString()))
                    {
                        adminService.CreateStaffAccount(rbis,bid,userId,password);
                        Console.WriteLine("Account Created Successfully");
                    }   
                    else
                        Console.WriteLine("Account Already Exist");
                    break;
                case 2:
                    Console.Write("Enter Staff Username to delete: ");
                    userId = Console.ReadLine();
                    if(bankService.CheckUser(rbis,bid, userId,User.TypesOfUser.Staff.ToString()))
                    {
                        adminService.RemoveStaffAccount(rbis,bid,userId);
                        Console.WriteLine("Account Deleted Successfully");
                    }  
                    else
                        Console.WriteLine("Account Doesn't Exist");
                    break;
                default:
                    Console.WriteLine("Logging out..");
                    return true;
            }

        }
    }
    public bool StaffOptions(string bid)
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
            string aid = "";
            switch (option)
            {
                case 1:
                    obj.EnterUserName();
                    string userId = Console.ReadLine();
                    Console.Write("Ënter Password: ");
                    string password = Console.ReadLine();
                    if (bankService.CheckUser(rbis,bid,userId,"Customer"))
                    {
                        staffService.CreateCustomerAccount(rbis,bid,userId, password);
                        Console.WriteLine("Account Created Successfully");
                    }
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
                                if (bankService.CheckUser(rbis,bid, oldName, "Customer"))
                                {
                                    staffService.ChangeUserName(rbis,bid,oldName, newName);
                                    Console.WriteLine("Updated Successfully");
                                }
                                else
                                    Console.WriteLine("Not Updated");
                                break;
                            case 2:
                                obj.EnterUserName();
                                string user = Console.ReadLine();
                                Console.Write("Enter new password: ");
                                string pass = Console.ReadLine();
                                if (staffService.UpdatePassword(rbis,bid,user, pass))
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
                        if (staffService.RemoveCustomerAccount(rbis,bid,userId))
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
                    staffService.AddCurrency(rbis,bid,currency, rate);
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
                            staffService.SetServiceCharges(rbis,bid,"RTGS", val);
                            Console.WriteLine("New RTGS for same bank is :"+rbis.GetBank(bid).RTGS.ToString());
                            break;
                        case 2:
                            Console.Write("Enter the IMPS: ");
                            val  = obj.GetFloatInput();
                            staffService.SetServiceCharges(rbis, bid, "IMPS", val);
                            Console.WriteLine("New IMPS for same bank is :" + rbis.GetBank(bid).IMPS.ToString());
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
                            staffService.SetServiceCharges(rbis, bid, "ORTGS", val);
                            Console.WriteLine("New RTGS for other bank is :" + rbis.GetBank(bid).ORTGS.ToString());
                            break;
                        case 2:
                            Console.Write("Enter the IMPS: ");
                            val = obj.GetFloatInput();
                            staffService.SetServiceCharges(rbis, bid, "OIMPS", val);
                            Console.WriteLine("New IMPS for other bank is :" + rbis.GetBank(bid).OIMPS.ToString());
                            break;
                    }
                    break;
                case 6:
                    Console.WriteLine("All the accounts in the bank are: ");
                    List<Account> l = rbis.GetBank(bid).Accounts;
                    foreach(var i in l)
                    {
                        Console.WriteLine(i.AccountId+"\t"+i.CustomerName);
                    }
                    Console.WriteLine("Enter one Account Id from above");
                    string str = Console.ReadLine();
                    Account a = bankService.GetAccount(rbis,bid,str);
                    PrintTransactions(a.Transactions);
                    break;


                default:
                    Console.WriteLine("Logging out..");
                    return true;
            }
        }
    }
    public bool CustomerOptions(string bid,string aid)
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
                    if (customerService.DepositeMoney(rbis,bid,aid,amount, currency))
                    {
                        Console.WriteLine("Deposite is successful");
                        Console.WriteLine("Your Account Balance is: " + customerService.GetBalance(rbis,bid,aid).ToString());
                    }
                    else
                    {
                        Console.WriteLine("The currency is not accepted by the bank");
                    }
                    break;
                case 2:
                    Console.Write("Enter the Amount to Withdraw: ");
                    amount = obj.GetIntInput();
                    if (customerService.WithdrawAmount(rbis,bid,aid,amount))
                        Console.WriteLine("Your Account Balance is: " + customerService.GetBalance(rbis,bid,aid).ToString());
                    else
                        Console.WriteLine("Insufficient Balance");
                    break;
                case 3:
                    Console.Write("Enter Reciever BankId: ");
                    string b = Console.ReadLine();
                    Console.Write("Enter Reciever AccountId: ");
                    string i = Console.ReadLine();
                    Console.Write("Select one 1.RTGS 2.IMPS : ");
                    option = obj.GetIntInput();
                    Console.Write("Enter the amount to be transeferred: ");
                    float a = obj.GetFloatInput();
                    float ra = customerService.CalculateAmount(rbis,bid,a,option,aid);
                    if (rbis.ValidateBank(b))
                    {
                        Console.WriteLine(rbis.Transfer(bid, aid, a,b,i,ra));
                    }
                    else
                    {
                        Console.WriteLine("Bank doesn't Exist");
                    }

                    break;
                case 4:
                    PrintTransactions(bankService.GetAccount(rbis,bid,aid).Transactions);
                    break;

                default:
                    Console.WriteLine("Logging out..");
                    return true;
            }
        }
    }
    public void PrintTransactions(List<TransactionModel> trans)
    {
        Console.WriteLine("TransactionIdS. BankId S.AccountId R.BankId R.AccountId TransactionType Amount");
        float sum = 0;
        foreach (var i in trans)
        {
            Console.WriteLine(i.TransactionId + " " + i.SenderBankId + " " + i.SenderAccountId + " " + i.RecieverBankId + " " + i.RecieverAccountId + " " + i.TransactionType + " " + i.Amount.ToString());
            sum += i.Amount;
        
        }
        Console.WriteLine("Total\t \t \t \t \t \t "+sum.ToString());
    }
}