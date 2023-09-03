using DeepFrees.PayrollServices.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PayrollServices.Model;

namespace DeepFrees.PayrollServices.MicroService
{
    public class DataService
    {
        private readonly IMongoCollection<SallaryModel> _SallaryModels;

        public DataService(IOptions<DataSettings> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                deepfreesDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                deepfreesDatabaseSettings.Value.DatabaseName);

            _SallaryModels = mongoDatabase.GetCollection<SallaryModel>(
                deepfreesDatabaseSettings.Value.ShoppinzUsersCollectionName[4]);
        }

        public async Task<List<SallaryModel>> GetAsync() =>
            await _SallaryModels.Find(_ => true).ToListAsync();

        public async Task<SallaryModel?> GetAsync(string EmpID) =>
            await _SallaryModels.Find(x => x.EmpID == EmpID).FirstOrDefaultAsync();

        public async Task CreateAsync(SallaryModel SallaryModel) =>
            await _SallaryModels.InsertOneAsync(SallaryModel);

        public async Task UpdateAsync(string EmpID, SallaryModel SallaryModel) =>
            await _SallaryModels.ReplaceOneAsync(x => x.EmpID == EmpID, SallaryModel);

        public async Task RemoveAsync(string EmpID) =>
            await _SallaryModels.DeleteOneAsync(x => x.EmpID == EmpID);
    }
}
}
