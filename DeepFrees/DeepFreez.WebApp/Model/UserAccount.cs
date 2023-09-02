using MongoDB.Bson.Serialization.Attributes;

namespace DeepFreez.WebApp.Model
{
    public class UserAccount
    {
        public string AccountID { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? UserType { get; set; }
        public string Password { get; set; } = null!;

        public string _Password { get; set; } = null!;


        public UserAccount(string accountID, string userName, string? userType, string password)
        {
            AccountID = accountID;
            UserName = userName;
            UserType = userType;
            _Password = password;
        }
    }
}
