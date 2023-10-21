using DeepFrees.EmployeeService.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Commons.DeepFrees.DatabaseConfiguration;
using Commons.DeepFrees.NetworkConfiguration;

namespace DeepFrees.PayrollServices.MicroService
{
    public class DataService
    {
        private readonly IMongoCollection<EmployeePR> _EmployeeCollection;

        public DataService(IOptions<MongoDBConn> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                MongoDBConn.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                MongoDBConn.DatabaseName);

            _EmployeeCollection = mongoDatabase.GetCollection<EmployeePR>(
                MongoDBConn.DeepFreesDataCollections[3]);
        }

        public async Task<List<EmployeePR>> GetEmployee() =>
            await _EmployeeCollection.Find(_ => true).ToListAsync();

        public async Task<EmployeePR?> GetEmployee(string NIC) =>
            await _EmployeeCollection.Find(x => x.NIC == NIC).FirstOrDefaultAsync();

        public async Task CreateEmployee(EmployeePR newuser) =>
            await _EmployeeCollection.InsertOneAsync(newuser);

        public async Task UpdateEmployee(string NIC, EmployeePR updateuser) =>
            await _EmployeeCollection.ReplaceOneAsync(x => x.NIC == NIC, updateuser);

        public async Task RemoveEmployee(string NIC) =>
            await _EmployeeCollection.DeleteOneAsync(x => x.NIC == NIC);
    }
}

