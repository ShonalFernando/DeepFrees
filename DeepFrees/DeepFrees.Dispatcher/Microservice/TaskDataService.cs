using Commons.DeepFrees.DatabaseConfiguration;
using DeepFrees.Dispatcher.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DeepFrees.Dispatcher.Microservice
{
    public class TaskDataService
    {
            private readonly IMongoCollection<WorkTask> _UserAccountsCollection;

            public TaskDataService(IOptions<MongoDBConn> deepfreesDatabaseSettings)
            {
                var mongoClient = new MongoClient(
                    MongoDBConn.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(
                    MongoDBConn.DatabaseName);

                _UserAccountsCollection = mongoDatabase.GetCollection<WorkTask>(
                    MongoDBConn.DeepFreesDataCollections[4]);
            }

            public async Task<List<WorkTask>> GetAsync() =>
                await _UserAccountsCollection.Find(_ => true).ToListAsync();

            public async Task<WorkTask?> GetAsync(int TaskID) =>
                await _UserAccountsCollection.Find(x => x.taskID == TaskID).FirstOrDefaultAsync();

            public async Task CreateAsync(WorkTask newtask) =>
                await _UserAccountsCollection.InsertOneAsync(newtask);

            public async Task UpdateAsync(int TaskID, WorkTask updatedtask) =>
                await _UserAccountsCollection.ReplaceOneAsync(x => x.taskID == TaskID, updatedtask);

            public async Task RemoveAsync(int TaskID) =>
                await _UserAccountsCollection.DeleteOneAsync(x => x.taskID == TaskID);
    }
}
