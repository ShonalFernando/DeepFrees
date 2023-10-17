using DeepFrees.CallDirecting.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeepFrees.CallDirecting.Microservice
{
    public class CallDataService
    {
        private readonly IMongoCollection<Call> _Calls;

        public CallDataService(IOptions<DatabaseSettings> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                deepfreesDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                deepfreesDatabaseSettings.Value.DatabaseName);

            _Calls = mongoDatabase.GetCollection<Call>(
                deepfreesDatabaseSettings.Value.ShoppinzUsersCollectionName[1]);
        }

        //Change this to call history
        public async Task<List<Call>> GetAsync() =>
            await _Calls.Find(_ => true).ToListAsync();

        public async Task<Call?> GetAsync(int EmpID) =>
            await _Calls.Find(x => x.CallIndex == EmpID).FirstOrDefaultAsync();

        public async Task CreateAsync(Call Call) =>
            await _Calls.InsertOneAsync(Call);

        public async Task UpdateAsync(int EmpID, Call Call) =>
            await _Calls.ReplaceOneAsync(x => x.CallIndex == EmpID, Call);

        public async Task RemoveAsync(int EmpID) =>
            await _Calls.DeleteOneAsync(x => x.CallIndex == EmpID);
    }
}
