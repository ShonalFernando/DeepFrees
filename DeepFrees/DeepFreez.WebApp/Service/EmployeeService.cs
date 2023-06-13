using DeepFreez.WebApp.Model;
using DeepFreez.WebApp.Model.SettingModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeepFreez.WebApp.Service
{
    public class EmployeeService
    {
        private readonly IMongoCollection<Employee> _EmployeeCollection;

        public EmployeeService(
            IOptions<DeepFreesDatabaseSettings> EmployeeDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                EmployeeDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                EmployeeDatabaseSettings.Value.DatabaseName);

            _EmployeeCollection = mongoDatabase.GetCollection<Employee>(
                EmployeeDatabaseSettings.Value.EmployeeCollectionName);
        }

        public async Task<List<Employee>> GetAsync() =>
            await _EmployeeCollection.Find(_ => true).ToListAsync();

        public async Task<Employee?> GetAsync(string id) =>
            await _EmployeeCollection.Find(x => x._id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Employee newEmployee) =>
            await _EmployeeCollection.InsertOneAsync(newEmployee);

        public async Task UpdateAsync(string id, Employee updatedEmployee) =>
            await _EmployeeCollection.ReplaceOneAsync(x => x._id == id, updatedEmployee);

        public async Task RemoveAsync(string id) =>
            await _EmployeeCollection.DeleteOneAsync(x => x._id == id);
    }
}
