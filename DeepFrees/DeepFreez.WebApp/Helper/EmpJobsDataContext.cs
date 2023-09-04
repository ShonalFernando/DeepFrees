using DeepFreez.WebApp.Data;
using DeepFreez.WebApp.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeepFreez.WebApp.Helper
{
    public class EmpJobsDataContext
    {
        private readonly IMongoCollection<WeeklyJob> _WeeklyJob;

        public EmpJobsDataContext(IOptions<DBSettings> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                deepfreesDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                deepfreesDatabaseSettings.Value.DatabaseName);

            _WeeklyJob = mongoDatabase.GetCollection<WeeklyJob>(
                deepfreesDatabaseSettings.Value.ShoppinzUsersCollectionName[7]);
        }

        public async Task<List<WeeklyJob>> GetAsync() =>
            await _WeeklyJob.Find(_ => true).ToListAsync();

        public async Task<WeeklyJob?> GetAsync(int WeekID) =>
            await _WeeklyJob.Find(x => x.WeekID == WeekID).FirstOrDefaultAsync();

        public async Task CreateAsync(WeeklyJob WeeklyJob) =>
            await _WeeklyJob.InsertOneAsync(WeeklyJob);

        public async Task UpdateAsync(int WeekID, WeeklyJob WeeklyJob) =>
            await _WeeklyJob.ReplaceOneAsync(x => x.WeekID == WeekID, WeeklyJob);

        public async Task RemoveAsync(int WeekID) =>
            await _WeeklyJob.DeleteOneAsync(x => x.WeekID == WeekID);
    }
}
