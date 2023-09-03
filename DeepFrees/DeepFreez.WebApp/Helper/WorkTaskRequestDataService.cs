using DeepFreez.WebApp.Data;
using DeepFreez.WebApp.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeepFreez.WebApp.Helper
{
    public class WorkTaskRequestDataService
    {
        private readonly IMongoCollection<WeeklyJob> _UserAccountsCollection;

        public WorkTaskRequestDataService(IOptions<DBSettings> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                deepfreesDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                deepfreesDatabaseSettings.Value.DatabaseName);

            _UserAccountsCollection = mongoDatabase.GetCollection<WeeklyJob>(
                deepfreesDatabaseSettings.Value.ShoppinzUsersCollectionName[5]);
        }

        public async Task<List<WeeklyJob>> GetAll() =>
            await _UserAccountsCollection.Find(_ => true).ToListAsync();
    }
}
