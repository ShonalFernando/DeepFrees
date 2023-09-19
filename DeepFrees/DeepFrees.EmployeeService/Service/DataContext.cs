using DeepFrees.EmployeeService.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Commons.DeepFrees.DatabaseConfiguration;
using Commons.DeepFrees.NetworkConfiguration;

namespace DeepFrees.EmployeeService.MicroService
{
    public class DataContext
    {
        private readonly IMongoCollection<Employee> _UserAccountsCollection;

        public DataContext(IOptions<MongoDBConn> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                MongoDBConn.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                MongoDBConn.DatabaseName);

            _UserAccountsCollection = mongoDatabase.GetCollection<Employee>(
                MongoDBConn.DeepFreesDataCollections[3]);
        }

        public async Task<List<Employee>> GetAsync() =>
            await _UserAccountsCollection.Find(_ => true).ToListAsync();

        public async Task<Employee?> GetAsync(string NIC) =>
            await _UserAccountsCollection.Find(x => x.NIC == NIC).FirstOrDefaultAsync();

        public async Task CreateAsync(Employee newuser) =>
            await _UserAccountsCollection.InsertOneAsync(newuser);

        public async Task UpdateAsync(string NIC, Employee updateuser) =>
            await _UserAccountsCollection.ReplaceOneAsync(x => x.NIC == NIC, updateuser);

        public async Task RemoveAsync(string NIC) =>
            await _UserAccountsCollection.DeleteOneAsync(x => x.NIC == NIC);
    }
}
