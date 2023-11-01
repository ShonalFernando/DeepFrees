using DeepFrees.Scheduler.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography;

namespace DeepFrees.Scheduler.MicroService
{
    public class DataService
    {
        private readonly IMongoCollection<AssignedJobs> _WeeklyTaskSolutions;

        public DataService(IOptions<DatabaseSettings> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                deepfreesDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                deepfreesDatabaseSettings.Value.DatabaseName);

            _WeeklyTaskSolutions = mongoDatabase.GetCollection<AssignedJobs>(
                deepfreesDatabaseSettings.Value.ShoppinzUsersCollectionName[4]);
        }

        public async Task<List<AssignedJobs>> GetAsync() =>
            await _WeeklyTaskSolutions.Find(_ => true).ToListAsync();

        public async Task<AssignedJobs?> GetAsync(ObjectId _id) =>
            await _WeeklyTaskSolutions.Find(x => x._id == _id).FirstOrDefaultAsync();

        public async Task CreateAsync(AssignedJobs WeeklyTaskSolutions) =>
            await _WeeklyTaskSolutions.InsertOneAsync(WeeklyTaskSolutions);

        public async Task UpdateAsync(ObjectId _id, AssignedJobs WeeklyTaskSolutions) =>
            await _WeeklyTaskSolutions.ReplaceOneAsync(x => x._id == _id, WeeklyTaskSolutions);

        public async Task RemoveAsync(ObjectId _id) =>
            await _WeeklyTaskSolutions.DeleteOneAsync(x => x._id == _id);
    }
}
