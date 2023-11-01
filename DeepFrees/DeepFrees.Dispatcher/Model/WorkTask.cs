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
        public bool isCompleted{ get; set; }

        public int taskID { get; set; }
        public string? taskName { get; set; }

        public TaskCategory? taskCategory { get; set; } //This is a enumeration

        public int dateDay { get; set; }
        public int dateMonth { get; set; }
        public int taskWeigth { get; set; } //Used to calculate and assign points to an employee
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
        Installation,
        Delivery,
        Assembly
    }
}
