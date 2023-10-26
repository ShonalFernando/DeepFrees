using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DeepFrees.TaskService.Model
{
    public class WorkTask
    {
        [BsonId]
        public ObjectId? _id { get; set; }
        public bool isAvailable { get; set; }
        public int TaskID { get; set; }
        public string? taskName { get; set; }
        public TaskCategory? taskCategory { get; set; }

        public int DateDay { get; set; }
        public int DateMonth { get; set; }
        public int taskWeigth { get; set; }
        public int taskLengthInDays { get; set; }
        public int taskLengthInHours { get; set; }
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
