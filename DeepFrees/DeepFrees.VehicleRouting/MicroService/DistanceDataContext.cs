using Commons.DeepFrees.DatabaseConfiguration;
using DeepFrees.VehicleRouting.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DeepFrees.VehicleRouting.MicroService
{
    public class DistanceDataContext
    {
        private readonly IMongoCollection<DistanceModel> _UserAccountsCollection;

        public DistanceDataContext(IOptions<MongoDBConn> deepfreesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                MongoDBConn.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                MongoDBConn.DatabaseName);

            _UserAccountsCollection = mongoDatabase.GetCollection<DistanceModel>(
                MongoDBConn.DeepFreesDataCollections[8]);
        }

        public async Task<List<DistanceModel>> GetAsync() =>
            await _UserAccountsCollection.Find(_ => true).ToListAsync();

        public async Task<DistanceModel?> GetAsync(int _id) =>
            await _UserAccountsCollection.Find(x => x.locationID == _id).FirstOrDefaultAsync();

        public async Task CreateAsync(DistanceModel locations) =>
            await _UserAccountsCollection.InsertOneAsync(locations);

        public async Task UpdateAsync(ObjectId? _id, DistanceModel locations) =>
            await _UserAccountsCollection.ReplaceOneAsync(x => x._id == _id, locations);

        public async Task RemoveAsync(int _id) =>
            await _UserAccountsCollection.DeleteOneAsync(x => x.locationID == _id);
    }

}
