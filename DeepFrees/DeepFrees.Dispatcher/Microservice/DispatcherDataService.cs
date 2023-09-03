using DeepFrees.Dispatcher.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeepFrees.Dispatcher.Microservice
{
    public class DispatcherDataService
    {
        private readonly IMongoCollection<DispatchSolution> _UserAccountsCollection;

        public DispatcherDataService(IOptions<DBSettings> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                deepfreesDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                deepfreesDatabaseSettings.Value.DatabaseName);

            _UserAccountsCollection = mongoDatabase.GetCollection<DispatchSolution>(
                deepfreesDatabaseSettings.Value.ShoppinzUsersCollectionName[0]);
        }

        public async Task<List<DispatchSolution>> GetAsync() =>
            await _UserAccountsCollection.Find(_ => true).ToListAsync();

        public async Task<DispatchSolution?> GetAsync(int TaskID) =>
            await _UserAccountsCollection.Find(x => x.TaskID == TaskID).FirstOrDefaultAsync();

        public async Task CreateAsync(DispatchSolution DispatchSolution) =>
            await _UserAccountsCollection.InsertOneAsync(DispatchSolution);

        public async Task UpdateAsync(int TaskID, DispatchSolution UpdatedDispatchSolution) =>
            await _UserAccountsCollection.ReplaceOneAsync(x => x.TaskID == TaskID, UpdatedDispatchSolution);

        public async Task RemoveAsync(int TaskID) =>
            await _UserAccountsCollection.DeleteOneAsync(x => x.TaskID == TaskID);
    }
}
