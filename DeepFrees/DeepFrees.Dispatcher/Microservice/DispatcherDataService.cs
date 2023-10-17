using DeepFrees.Dispatcher.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeepFrees.Dispatcher.Microservice
{
    public class DispatcherDataService
    {
        //private readonly IMongoCollection<DispatchSolutions> _DispatchSolutions;

        //public DispatcherDataService(IOptions<DBSettings> deepfreesDatabaseSettings)
        //{
        //    var mongoClient = new MongoClient(
        //        deepfreesDatabaseSettings.Value.ConnectionString);

        //    var mongoDatabase = mongoClient.GetDatabase(
        //        deepfreesDatabaseSettings.Value.DatabaseName);

        //    _DispatchSolutions = mongoDatabase.GetCollection<DispatchSolutions>(
        //        deepfreesDatabaseSettings.Value.ShoppinzUsersCollectionName[2]);
        //}

        //public async Task<List<DispatchSolutions>> GetAsync() =>
        //    await _DispatchSolutions.Find(_ => true).ToListAsync();

        //public async Task<DispatchSolutions?> GetAsync(int WeekID) =>
        //    await _DispatchSolutions.Find(x => x.WeekID == WeekID).FirstOrDefaultAsync();

        //public async Task CreateAsync(DispatchSolutions DispatchSolutions) =>
        //    await _DispatchSolutions.InsertOneAsync(DispatchSolutions);

        //public async Task UpdateAsync(int WeekID, DispatchSolutions DispatchSolutions) =>
        //    await _DispatchSolutions.ReplaceOneAsync(x => x.WeekID == WeekID, DispatchSolutions);

        //public async Task RemoveAsync(int WeekID) =>
        //    await _DispatchSolutions.DeleteOneAsync(x => x.WeekID == WeekID);
    }
}
