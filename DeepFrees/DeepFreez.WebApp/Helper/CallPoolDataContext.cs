using DeepFreez.WebApp.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using static DeepFreez.WebApp.Model.CallTechModel;

namespace DeepFreez.WebApp.Helper
{
    public class CallPoolDataContext
    {
        private readonly IMongoCollection<Call> _Calls;

        public CallPoolDataContext(IOptions<DBSettings> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                deepfreesDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                deepfreesDatabaseSettings.Value.DatabaseName);

            _Calls = mongoDatabase.GetCollection<Call>(
                deepfreesDatabaseSettings.Value.ShoppinzUsersCollectionName[4]);
        }

        public async Task<List<Call>> GetAsync() =>
            await _Calls.Find(_ => true).ToListAsync();

        public async Task<Call?> GetAsync(int CallID) =>
            await _Calls.Find(x => x.CallID == CallID).FirstOrDefaultAsync();

        public async Task CreateAsync(Call Call) =>
            await _Calls.InsertOneAsync(Call);

        public async Task UpdateAsync(int CallID, Call Call) =>
            await _Calls.ReplaceOneAsync(x => x.CallID == CallID, Call);

        public async Task RemoveAsync(int CallID) =>
            await _Calls.DeleteOneAsync(x => x.CallID == CallID);
    }
}
