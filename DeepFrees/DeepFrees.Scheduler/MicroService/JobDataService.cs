using DeepFrees.Scheduler.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography;

namespace DeepFrees.Scheduler.MicroService
{
    public class JobDataService
    {
        private readonly IMongoCollection<JobCollection> _WeeklyTaskSolutions;

        public JobDataService(IOptions<DatabaseSettings> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                deepfreesDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                deepfreesDatabaseSettings.Value.DatabaseName);

            _WeeklyTaskSolutions = mongoDatabase.GetCollection<JobCollection>(
                deepfreesDatabaseSettings.Value.ShoppinzUsersCollectionName[0]);
        }

        public async Task<List<JobCollection>> GetAsync() =>
            await _WeeklyTaskSolutions.Find(_ => true).ToListAsync();

        public async Task<JobCollection?> GetAsync(ObjectId _id) =>
            await _WeeklyTaskSolutions.Find(x => x._id == _id).FirstOrDefaultAsync();

        public async Task CreateAsync(JobCollection WeeklyTaskSolution) =>
            await _WeeklyTaskSolutions.InsertOneAsync(WeeklyTaskSolution);

        public async Task UpdateAsync(ObjectId _id, JobCollection WeeklyTaskSolutions) =>
            await _WeeklyTaskSolutions.ReplaceOneAsync(x => x._id == _id, WeeklyTaskSolutions);

        public async Task RemoveAsync(ObjectId _id) =>
            await _WeeklyTaskSolutions.DeleteOneAsync(x => x._id == _id);
    }
}
