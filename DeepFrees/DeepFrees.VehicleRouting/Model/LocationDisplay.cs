using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DeepFrees.VehicleRouting.Model
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
        public List<LocationDisplay>? locations { get; set; }
    }
}
