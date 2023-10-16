using DeepFreesAccountsServices.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeepFreesAccountsServices.Model
{
    public class UserAccount
    {
        [BsonId]
        public ObjectId? _id { get; set; }

        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserType { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? SessionID { get; set; } 
        public string? WinUID { get; set; }

        public UserAccount()
        {
            this._id = ObjectId.GenerateNewId(); 
        }
    }
}
