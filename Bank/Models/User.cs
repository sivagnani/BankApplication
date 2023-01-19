namespace BankApplication.Models
{
    public class User
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public User(string userName, string password, string userType)
        {
            Password = password;
            UserName = userName;
            UserType = userType;
        }

    }
}
