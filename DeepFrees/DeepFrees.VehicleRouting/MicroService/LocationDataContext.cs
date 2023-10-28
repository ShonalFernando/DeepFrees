using Commons.DeepFrees.DatabaseConfiguration;
using DeepFrees.VehicleRouting.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DeepFrees.VehicleRouting.MicroService
{
    public class LocationDataContext
    {
        private readonly IMongoCollection<Location> _UserAccountsCollection;

        public LocationDataContext(IOptions<MongoDBConn> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                MongoDBConn.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                MongoDBConn.DatabaseName);

            _UserAccountsCollection = mongoDatabase.GetCollection<Location>(
                MongoDBConn.DeepFreesDataCollections[7]);
        }

        public async Task<List<Location>> GetAsync() =>
            await _UserAccountsCollection.Find(_ => true).ToListAsync();

        public async Task<Location?> GetAsync(ObjectId _id) =>
            await _UserAccountsCollection.Find(x => x._id == _id).FirstOrDefaultAsync();

        public async Task CreateAsync(Location locations) =>
            await _UserAccountsCollection.InsertOneAsync(locations);

        public async Task UpdateAsync(ObjectId _id, Location locations) =>
            await _UserAccountsCollection.ReplaceOneAsync(x => x._id == _id, locations);

        public async Task RemoveAsync(ObjectId _id) =>
            await _UserAccountsCollection.DeleteOneAsync(x => x._id == _id);
    }
}
