using MongoDB.Bson.Serialization.Attributes;

namespace DeepFreesAccountsServices.Model
{
    public class UserAccount
    {
        [BsonId]
        public string AccountID { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string Password { get; set; }
    }
}
