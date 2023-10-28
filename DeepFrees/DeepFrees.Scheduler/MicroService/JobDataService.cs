using DeepFrees.Scheduler.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeepFrees.Scheduler.MicroService
{
    public class JobDataService
    {
        private readonly IMongoCollection<JobTask> _WeeklyTaskSolutions;

        public JobDataService(IOptions<DatabaseSettings> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                deepfreesDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                deepfreesDatabaseSettings.Value.DatabaseName);

            _WeeklyTaskSolutions = mongoDatabase.GetCollection<JobTask>(
                deepfreesDatabaseSettings.Value.ShoppinzUsersCollectionName[0]);
        }

        public async Task<List<JobTask>> GetAsync() =>
            await _WeeklyTaskSolutions.Find(_ => true).ToListAsync();

        public async Task<JobTask?> GetAsync(int WeekID) =>
            await _WeeklyTaskSolutions.Find(x => x.weekID == WeekID).FirstOrDefaultAsync();

        public async Task CreateAsync(JobTask WeeklyTaskSolutions) =>
            await _WeeklyTaskSolutions.InsertOneAsync(WeeklyTaskSolutions);

        public async Task UpdateAsync(int WeekID, JobTask WeeklyTaskSolutions) =>
            await _WeeklyTaskSolutions.ReplaceOneAsync(x => x.weekID == WeekID, WeeklyTaskSolutions);

        public async Task RemoveAsync(int WeekID) =>
            await _WeeklyTaskSolutions.DeleteOneAsync(x => x.weekID == WeekID);
    }
}
