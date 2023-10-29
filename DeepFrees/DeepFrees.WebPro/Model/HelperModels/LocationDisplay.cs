using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DeepFrees.WebPro.Model.HelperModels
{
    public class LocationDisplay
    {
        public int OrderIndex { get; set; } //Theorder in the route
        public int locationID { get; set; }
        public bool isSelected { get; set; }
        public string city { get; set; } = null!;
    }

    public class SavedRoute
    {
        [BsonId]
        public ObjectId? _id { get; set; }
        public string? routeName { get; set; }
        public int startLocation { get; set; }
        public string? vehicleNumber { get; set; }
        public int vehicleIndex { get; set; }
        public string? driver { get; set; }
        public string? driverID { get; set; }
        public string? comments { get; set; }
        public string? totalDistance { get; set; }
        public int[]? routeOrder { get; set; }
    }
}
