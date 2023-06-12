using DeepFreesAccountsServices.AppSettings;
using DeepFreesAccountsServices.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeepFreesAccountsServices.Services
{
    public class UserAccountService
    {
        private readonly IMongoCollection<UserAccount> _UserAccountsCollection;

        public UserAccountService(IOptions<DeepFreesDatabaseSettings> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                deepfreesDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                deepfreesDatabaseSettings.Value.DatabaseName);

            _UserAccountsCollection = mongoDatabase.GetCollection<UserAccount>(
                deepfreesDatabaseSettings.Value.ShoppinzUsersCollectionName[0]);
        }

        public async Task<List<UserAccount>> GetAsync() =>
            await _UserAccountsCollection.Find(_ => true).ToListAsync();

        public async Task<UserAccount?> GetAsync(string Username) =>
            await _UserAccountsCollection.Find(x => x.UserName == Username).FirstOrDefaultAsync();

        public async Task CreateAsync(UserAccount newuser) =>
            await _UserAccountsCollection.InsertOneAsync(newuser);

        public async Task UpdateAsync(string id, UserAccount updateuser) =>
            await _UserAccountsCollection.ReplaceOneAsync(x => x.AccountID == id, updateuser);

        public async Task RemoveAsync(string id) =>
            await _UserAccountsCollection.DeleteOneAsync(x => x.AccountID == id);
    }
}
