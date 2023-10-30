using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DeepFrees.Scheduler.Model
{
    public class JobTask
    {
        [BsonId]
        public ObjectId? _id { get; set; }
        public int weekID { get; set; }
        public bool isAvailable { get; set; }
        public int team { get; set; }
        public int duration { get; set; }
    }

    public class Job
    {
        public List<JobTask> tasks { get; set; } = new List<JobTask>();
    }

    public class JobScheduleRequest
    {
        public List<Job> allJobs { get; set; } = new List<Job>();
    }
}
