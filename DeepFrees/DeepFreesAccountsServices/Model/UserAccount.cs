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
        public string Password { get; set; } = null!;
    }
}
