using BankApplication.Providers;
using BankApplication.Models;
using BankApplication.Contracts;
using BankApplication.helper;

namespace BankApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IRBIService rbis = new RBIService();
            IBankService bankService = new BankService();
            IAdminService adminService = new AdminService();
            ICustomerService customerService = new CustomerService();
            IUserService userService = new UserService();
            IStaffService staffService = new StaffService();
            RBI rbi = new RBI();
            InputsAndOutputs io = new InputsAndOutputs();
            Program controlObject = new Program();
            controlObject.CreateBankStatements(io, rbis, rbi, bankService, adminService, staffService, customerService, userService);
        }
    }
}
