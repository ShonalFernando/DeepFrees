using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DeepFrees.Scheduler.Model
{
    public class NewJobModel
    {
        public int machine { get; set; } 
        public int duration { get; set; }
    }

    public class JobCollection
    {
        [BsonId]
        public ObjectId? _id { get; set; }
        public List<NewJobModel>? WeekJobs { get; set; }
        public int DateDay { get; set; }
        public int DateYear { get; set; }
    }

    public class AssignedJobs
    {
        [BsonId]
        public ObjectId? _id { get; set; }
        public int teamID {get;set;}
        public int taskID {get;set;}
        public int jobID {get;set;}
        public int duration {get;set;}
    }
}
