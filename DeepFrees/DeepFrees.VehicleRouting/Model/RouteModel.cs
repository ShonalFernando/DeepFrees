using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeepFrees.VehicleRouting.Model
{
    public class RouteModel
    {
        public List<DistanceMatrixModel> DistanceMatrices { get; set; } = null!;
        public int VehicleNumber { get; set; }
        public int Depot { get; set; }
    }

    public class DistanceMatrixModel
    {
        public string? StartLocation { get; set; }
        public string? Destination { get; set; }

        public int LocationFrom { get; set; }
        public int LocationTo { get; set; }
        public long Distance { get; set; }
    }

    public class Locations
    {
        [BsonId]
        public ObjectId? _id { get; set; }
        public int LocationID { get; set; }
        public string? StartLocation { get; set; }
        public string? Destination { get; set; }

        public int LocationFrom { get; set; }
        public int LocationTo { get; set; }
        public long Distance { get; set; }

        public Locations()
        {
            this._id = ObjectId.GenerateNewId();
        }
    }

    public class SolutionMatrixModel
    {
        public List<int>? RouteOrder { get; set; }
        public long TotalDistance { get; set; }
    }
}
