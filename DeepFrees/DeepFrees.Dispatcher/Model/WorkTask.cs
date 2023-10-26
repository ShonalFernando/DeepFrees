using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DeepFrees.Dispatcher.Model
{
    public class WorkTask
    {
        [BsonId]
        public ObjectId? _id { get; set; }

        public bool isAvailable{ get; set; }

        public int taskID { get; set; }
        public string? taskName { get; set; }
        public TaskCategory? taskCategory { get; set; }

        public int dateDay { get; set; }
        public int dateMonth { get; set; }
        public int taskWeigth { get; set; }
        public int taskLengthInDays { get; set; }
        public int taskLengthInHours { get; set; }

        public WorkTask()
        {
            _id = ObjectId.GenerateNewId();
        }
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
