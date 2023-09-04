using DeepFreez.WebApp.Data;
using DeepFreez.WebApp.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeepFreez.WebApp.Helper
{
    public class DispJobsDataContext
    {
        private readonly IMongoCollection<DispatchRequestList> _DispatchRequestList;

        public DispJobsDataContext(IOptions<DBSettings> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                deepfreesDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                deepfreesDatabaseSettings.Value.DatabaseName);

            _DispatchRequestList = mongoDatabase.GetCollection<DispatchRequestList>(
                deepfreesDatabaseSettings.Value.ShoppinzUsersCollectionName[6]);
        }

        public async Task<List<DispatchRequestList>> GetAsync() =>
            await _DispatchRequestList.Find(_ => true).ToListAsync();

        public async Task<DispatchRequestList?> GetAsync(int WeekID) =>
            await _DispatchRequestList.Find(x => x.WeekID == WeekID).FirstOrDefaultAsync();

        public async Task CreateAsync(DispatchRequestList WeeklyJob) =>
            await _DispatchRequestList.InsertOneAsync(WeeklyJob);

        public async Task UpdateAsync(int WeekID, DispatchRequestList WeeklyJob) =>
            await _DispatchRequestList.ReplaceOneAsync(x => x.WeekID == WeekID, WeeklyJob);

        public async Task RemoveAsync(int WeekID) =>
            await _DispatchRequestList.DeleteOneAsync(x => x.WeekID == WeekID);
    }
}
