using Commons.DeepFrees.DatabaseConfiguration;
using DeepFrees.VehicleRouting.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DeepFrees.VehicleRouting.MicroService
{
    public class RouteDataContext
    { 
            private readonly IMongoCollection<SavedRoute> _UserAccountsCollection;

            public RouteDataContext(IOptions<MongoDBConn> deepfreesDatabaseSettings)
            {
                var mongoClient = new MongoClient(
                    MongoDBConn.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(
                    MongoDBConn.DatabaseName);

                _UserAccountsCollection = mongoDatabase.GetCollection<SavedRoute>(
                    MongoDBConn.DeepFreesDataCollections[6]);
            }

            public async Task<List<SavedRoute>> GetAsync() =>
                await _UserAccountsCollection.Find(_ => true).ToListAsync();

            public async Task<SavedRoute?> GetAsync(ObjectId _id) =>
                await _UserAccountsCollection.Find(x => x._id == _id).FirstOrDefaultAsync();

            public async Task CreateAsync(SavedRoute locations) =>
                await _UserAccountsCollection.InsertOneAsync(locations);

            public async Task UpdateAsync(ObjectId _id, SavedRoute savedRoute) =>
                await _UserAccountsCollection.ReplaceOneAsync(x => x._id == _id, savedRoute);

            public async Task RemoveAsync(ObjectId _id) =>
                await _UserAccountsCollection.DeleteOneAsync(x => x._id == _id);
        }

}
