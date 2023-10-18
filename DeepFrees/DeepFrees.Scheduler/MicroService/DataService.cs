using DeepFrees.Scheduler.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeepFrees.Scheduler.MicroService
{
    public class DataService
    {
        //private readonly IMongoCollection<WeeklyTaskSolutions> _WeeklyTaskSolutions;

        //public DataService(IOptions<DatabaseSettings> deepfreesDatabaseSettings)
        //{
        //    var mongoClient = new MongoClient(
        //        deepfreesDatabaseSettings.Value.ConnectionString);

        //    var mongoDatabase = mongoClient.GetDatabase(
        //        deepfreesDatabaseSettings.Value.DatabaseName);

        //    _WeeklyTaskSolutions = mongoDatabase.GetCollection<WeeklyTaskSolutions>(
        //        deepfreesDatabaseSettings.Value.ShoppinzUsersCollectionName[4]);
        //}

        //public async Task<List<WeeklyTaskSolutions>> GetAsync() =>
        //    await _WeeklyTaskSolutions.Find(_ => true).ToListAsync();

        //public async Task<WeeklyTaskSolutions?> GetAsync(int WeekID) =>
        //    await _WeeklyTaskSolutions.Find(x => x.WeekID == WeekID).FirstOrDefaultAsync();

        //public async Task CreateAsync(WeeklyTaskSolutions WeeklyTaskSolutions) =>
        //    await _WeeklyTaskSolutions.InsertOneAsync(WeeklyTaskSolutions);

        //public async Task UpdateAsync(int WeekID, WeeklyTaskSolutions WeeklyTaskSolutions) =>
        //    await _WeeklyTaskSolutions.ReplaceOneAsync(x => x.WeekID == WeekID, WeeklyTaskSolutions);

        //public async Task RemoveAsync(int WeekID) =>
        //    await _WeeklyTaskSolutions.DeleteOneAsync(x => x.WeekID == WeekID);
    }
}
