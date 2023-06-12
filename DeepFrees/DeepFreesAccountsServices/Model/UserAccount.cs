using DeepFreesAccountsServices.Services;
using MongoDB.Bson.Serialization.Attributes;

namespace DeepFreesAccountsServices.Model
{
    public class UserAccount
    {
        [BsonId]
        public string AccountID { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? UserType { get; set; }
        private string Password { get; set; } = null!;

        public string _Password
        {
            get
            {
                return EncryptionService.DecryptPassword(Password);
            }
            set
            {
                Password = EncryptionService.EncryptPassword(value);
            }
        }

        public UserAccount(string accountID, string userName, string? userType, string password)
        {
            AccountID = accountID;
            UserName = userName;
            UserType = userType;
            _Password = password;
        }
    }
}
