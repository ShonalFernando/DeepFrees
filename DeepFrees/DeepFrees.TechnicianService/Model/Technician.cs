using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DeepFrees.TechnicianService.Model
{
    public class Technician
    {
        [BsonId]
        public ObjectId? _id { get; set; }
        public string? NIC { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Qualification { get; set; }

        public List<WorkTaskPoints>? WorkTaskPointTable { get; set; }
        public List<AssignedTask>? AssignedTasks { get; set; }

        public Technician()
        {
            this._id = ObjectId.GenerateNewId();
        }
    }

    public class AssignedTask
    {
        public string? TaskID { get; set; }
        public string? TaskName { get; set; }
    }

    public class WorkTaskPoints
    {
        public int TaskCategory { get; set; } //TaskCatID
        public double? TaskCategoryPoints { get; set; }
    }

    public enum TaskCategory
    {
        Repairs,
        Maintainance,
        Delivery,
        Assembly,
        Troubleshooting,
        Installation,
        Callibration,
        Sanitary
    }
}
