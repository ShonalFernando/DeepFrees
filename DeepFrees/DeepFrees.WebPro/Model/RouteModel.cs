using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeepFrees.WebPro.Model
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

    //*NEW
    public class DistanceModel //The list of DistanceModel is DistanceMatrix
    {
        [BsonId]
        public ObjectId? _id { get; set; }
        public int locationID { get; set; } //Distance From
        public Dictionary<string, long>? distances { get; set; } //Distance To, note: itself is 0 || String is the LocationID converted to string due to Mongo requirements
    }

    //*NEW
    public class Location
    {
        [BsonId]
        public ObjectId? _id { get; set; }
        public int locationID { get; set; }
        public string city { get; set; } = null!;

        public Location()
        {
            this._id = ObjectId.GenerateNewId();
        }
    }

    public class SubRoute
    {
        public int StartLocationID { get;set;}
        public int StopLocationID { get;set; }
        public long DistanceBetween { get;  set; }
    }


    public class RouteUnit
    {
        [BsonId]
        public ObjectId? _id { get; set; }
        public int LocationID { get; set; }
        public string? StartLocation { get; set; }
        public string? Destination { get; set; } //Not Needed Much

        public int LocationFrom { get; set; } //The order: LocationID of another
        public int LocationTo { get; set; } //The order: LocationID of another
        public long Distance { get; set; }

        public RouteUnit()
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
