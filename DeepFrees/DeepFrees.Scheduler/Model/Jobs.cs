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

    public class SolutionsModel
    {
        public string Team { get; set; }
        public string JobTask { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }


    public class Job
    {
        public List<JobTask> Tasks { get; set; } = new List<JobTask>();
    }

    public class JobScheduleRequest
    {
        public List<Job> AllJobs { get; set; } = new List<Job>();
    }
}
