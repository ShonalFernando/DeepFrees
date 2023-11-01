using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DeepFrees.TechnicianService.Model
{
    public class Technician
    {
        [BsonId]
        public ObjectId? _id { get; set; }
        public string? nic { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? qualification { get; set; }

        public List<WorkTaskPoints>? workTaskPointTable { get; set; }
        public List<AssignedTask>? assignedTasks { get; set; }


    }

    public class AssignedTask
    {
        public int taskID { get; set; }
        public int dateDay { get; set; }
        public int dateMonth { get; set; }
    }

    public class WorkTaskPoints
    {
        public int taskCategory { get; set; } //TaskCatID
        public int taskCategoryPoints { get; set; }
    }

    public enum TaskCategory
    {
        Repairs,
        Installation,
        Delivery,
        Assembly
    }
}
