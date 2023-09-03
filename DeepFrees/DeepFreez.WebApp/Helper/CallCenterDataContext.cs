using DeepFreez.WebApp.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using static DeepFreez.WebApp.Model.CallTechModel;

namespace DeepFreez.WebApp.Helper
{
    public class CallCenterDataContext
    {
        private readonly IMongoCollection<CallPoolSolution> _CallPoolSolutions;

        public CallCenterDataContext(IOptions<DBSettings> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                deepfreesDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                deepfreesDatabaseSettings.Value.DatabaseName);

            _CallPoolSolutions = mongoDatabase.GetCollection<CallPoolSolution>(
                deepfreesDatabaseSettings.Value.ShoppinzUsersCollectionName[2]);
        }

        public async Task<List<CallPoolSolution>> GetAsync() =>
            await _CallPoolSolutions.Find(_ => true).ToListAsync();
    }
}
