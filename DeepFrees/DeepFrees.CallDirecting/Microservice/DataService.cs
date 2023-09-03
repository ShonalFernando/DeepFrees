using DeepFrees.CallDirecting.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeepFrees.CallDirecting.Microservice
{
    public class DataService
    {
        private readonly IMongoCollection<CallPoolSolution> _CallPoolSolutions;

        public DataService(IOptions<DatabaseSettings> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                deepfreesDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                deepfreesDatabaseSettings.Value.DatabaseName);

            _CallPoolSolutions = mongoDatabase.GetCollection<CallPoolSolution>(
                deepfreesDatabaseSettings.Value.ShoppinzUsersCollectionName[1]);
        }

        public async Task<List<CallPoolSolution>> GetAsync() =>
            await _CallPoolSolutions.Find(_ => true).ToListAsync();

        public async Task<CallPoolSolution?> GetAsync(int EmpID) =>
            await _CallPoolSolutions.Find(x => x.EmpID == EmpID).FirstOrDefaultAsync();

        public async Task CreateAsync(CallPoolSolution newuser) =>
            await _CallPoolSolutions.InsertOneAsync(newuser);

        public async Task UpdateAsync(int EmpID, CallPoolSolution updateuser) =>
            await _CallPoolSolutions.ReplaceOneAsync(x => x.EmpID == EmpID, updateuser);

        public async Task RemoveAsync(int EmpID) =>
            await _CallPoolSolutions.DeleteOneAsync(x => x.EmpID == EmpID);
    }
}
