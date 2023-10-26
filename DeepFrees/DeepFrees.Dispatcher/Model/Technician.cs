using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DeepFrees.Dispatcher.Model
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
        public int TaskID { get; set; }
        public int dateDay { get; set; }
        public int dateMonth { get; set; }
    }

    public class WorkTaskPoints
    {
        public int TaskCategory { get; set; } //TaskCatID
        public int TaskCategoryPoints { get; set; }
    }

}
