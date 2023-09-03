using DeepFrees.EmployeeService.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeepFrees.EmployeeService.MicroService
{
    public class EmployeeAccountsService
    {
        private readonly IMongoCollection<Employee> _UserAccountsCollection;

        public EmployeeAccountsService(IOptions<DBSettings> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                deepfreesDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                deepfreesDatabaseSettings.Value.DatabaseName);

            _UserAccountsCollection = mongoDatabase.GetCollection<Employee>(
                deepfreesDatabaseSettings.Value.ShoppinzUsersCollectionName[3]);
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
