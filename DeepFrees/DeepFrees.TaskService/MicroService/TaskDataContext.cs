using Commons.DeepFrees.DatabaseConfiguration;
using DeepFrees.TaskService.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography;

namespace DeepFrees.TaskService.MicroService
{
    public class TaskDataContext
    {
        private readonly IMongoCollection<WorkTask> _UserAccountsCollection;

        public TaskDataContext(IOptions<MongoDBConn> deepfreesDatabaseSettings)
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

        public async Task<WorkTask?> GetAsync(ObjectId _id) =>
            await _UserAccountsCollection.Find(x => x._id == _id).FirstOrDefaultAsync();

        public async Task CreateAsync(WorkTask newtask) =>
            await _UserAccountsCollection.InsertOneAsync(newtask);

        public async Task UpdateAsync(ObjectId _id, WorkTask updatedtask) =>
            await _UserAccountsCollection.ReplaceOneAsync(x => x._id == _id, updatedtask);

        public async Task RemoveAsync(ObjectId _id) =>
            await _UserAccountsCollection.DeleteOneAsync(x => x._id == _id);
    }
}
