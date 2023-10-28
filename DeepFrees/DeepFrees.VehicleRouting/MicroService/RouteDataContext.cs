using Commons.DeepFrees.DatabaseConfiguration;
using DeepFrees.VehicleRouting.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DeepFrees.VehicleRouting.MicroService
{
    public class RouteDataContext
    { 
            private readonly IMongoCollection<RouteUnit> _UserAccountsCollection;

            public RouteDataContext(IOptions<MongoDBConn> deepfreesDatabaseSettings)
            {
                var mongoClient = new MongoClient(
                    MongoDBConn.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(
                    MongoDBConn.DatabaseName);

                _UserAccountsCollection = mongoDatabase.GetCollection<RouteUnit>(
                    MongoDBConn.DeepFreesDataCollections[6]);
            }

            public async Task<List<RouteUnit>> GetAsync() =>
                await _UserAccountsCollection.Find(_ => true).ToListAsync();

            public async Task<RouteUnit?> GetAsync(ObjectId _id) =>
                await _UserAccountsCollection.Find(x => x._id == _id).FirstOrDefaultAsync();

            public async Task CreateAsync(RouteUnit locations) =>
                await _UserAccountsCollection.InsertOneAsync(locations);

            public async Task UpdateAsync(ObjectId _id, RouteUnit locations) =>
                await _UserAccountsCollection.ReplaceOneAsync(x => x._id == _id, locations);

            public async Task RemoveAsync(ObjectId _id) =>
                await _UserAccountsCollection.DeleteOneAsync(x => x._id == _id);
        }

}
