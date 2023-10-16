using DeepFrees.CallDirecting.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeepFrees.CallDirecting.Microservice
{
    public class CallPoolDataService
    {
        private readonly IMongoCollection<Call> _Calls;

        public CallPoolDataService(IOptions<DatabaseSettings> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                deepfreesDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                deepfreesDatabaseSettings.Value.DatabaseName);

            _Calls = mongoDatabase.GetCollection<Call>(
                deepfreesDatabaseSettings.Value.ShoppinzUsersCollectionName[0]);
        }

        public async Task<List<Call>> GetAsync() =>
            await _Calls.Find(_ => true).ToListAsync();

        public async Task<Call?> GetAsync(int CallID) =>
            await _Calls.Find(x => x.CallIndex == CallID).FirstOrDefaultAsync();

        public async Task CreateAsync(Call Call) =>
            await _Calls.InsertOneAsync(Call);

        public async Task UpdateAsync(int CallID, Call Call) =>
            await _Calls.ReplaceOneAsync(x => x.CallIndex == CallID, Call);

        public async Task RemoveAsync(int CallID) =>
            await _Calls.DeleteOneAsync(x => x.CallIndex == CallID);
    }
}
