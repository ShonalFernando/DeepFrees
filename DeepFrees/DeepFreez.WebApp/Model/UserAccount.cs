using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DeepFreez.WebApp.Model
{
    public class UserAccount
    {
        [BsonId]
        public ObjectId? _id { get; set; }

        public string UserName { get; set; } = null!;
        public string? UserType { get; set; }
        public string Password { get; set; } = null!;
    }
}
