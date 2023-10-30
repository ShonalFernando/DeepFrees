using Commons.DeepFrees.DatabaseConfiguration;
using DeepFrees.SettingsService.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DeepFrees.SettingsService.Microservice
{
    public class SettingsDataService
    {
        private readonly IMongoCollection<SettingsModel> _SettingsModel;

        public SettingsDataService(IOptions<MongoDBConn> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                MongoDBConn.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                MongoDBConn.DatabaseName);

            _SettingsModel = mongoDatabase.GetCollection<SettingsModel>(
                MongoDBConn.DeepFreesDataCollections[9]);
        }

        //Change this to call history
        public async Task<List<SettingsModel>> GetAsync() =>
            await _SettingsModel.Find(_ => true).ToListAsync();

        public async Task<SettingsModel?> GetAsync(ObjectId EmpID) =>
            await _SettingsModel.Find(x => x._id == EmpID).FirstOrDefaultAsync();

        public async Task CreateAsync(SettingsModel Call) =>
            await _SettingsModel.InsertOneAsync(Call);

        public async Task UpdateAsync(ObjectId EmpID, SettingsModel SettingsModel) =>
            await _SettingsModel.ReplaceOneAsync(x => x._id == EmpID, SettingsModel);

        public async Task RemoveAsync(ObjectId EmpID) =>
            await _SettingsModel.DeleteOneAsync(x => x._id == EmpID);
    }
}

