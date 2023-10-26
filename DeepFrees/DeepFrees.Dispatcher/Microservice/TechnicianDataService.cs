using Commons.DeepFrees.DatabaseConfiguration;
using DeepFrees.Dispatcher.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeepFrees.Dispatcher.Microservice
{
    public class TechnicianDataService
    {
        private readonly IMongoCollection<Technician> _UserAccountsCollection;

        public TechnicianDataService(IOptions<MongoDBConn> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                MongoDBConn.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                MongoDBConn.DatabaseName);

            _UserAccountsCollection = mongoDatabase.GetCollection<Technician>(
                MongoDBConn.DeepFreesDataCollections[5]);
        }

        public async Task<List<Technician>> GetAsync() =>
            await _UserAccountsCollection.Find(_ => true).ToListAsync();

        public async Task<Technician?> GetAsync(string NIC) =>
            await _UserAccountsCollection.Find(x => x.NIC == NIC).FirstOrDefaultAsync();

        public async Task CreateAsync(Technician newTechnician) =>
            await _UserAccountsCollection.InsertOneAsync(newTechnician);

        public async Task UpdateAsync(string NIC, Technician updatedTechnician) =>
            await _UserAccountsCollection.ReplaceOneAsync(x => x.NIC == NIC, updatedTechnician);

        public async Task RemoveAsync(string NIC) =>
            await _UserAccountsCollection.DeleteOneAsync(x => x.NIC == NIC);
    }
}
