using DeepFreez.WebApp.Data;
using DeepFreez.WebApp.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeepFreez.WebApp.Helper
{
    public class EmployeeDataContext
    {
        private readonly IMongoCollection<Employee> _UserAccountsCollection;

        public EmployeeDataContext(IOptions<DBSettings> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                deepfreesDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                deepfreesDatabaseSettings.Value.DatabaseName);

            _UserAccountsCollection = mongoDatabase.GetCollection<Employee>(
                deepfreesDatabaseSettings.Value.ShoppinzUsersCollectionName[3]);
        }

        public async Task<List<Employee>> GetAll() =>
            await _UserAccountsCollection.Find(_ => true).ToListAsync();
    }
}
