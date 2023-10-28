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

    public class Job : IEnumerable<JobTask>
    {
        public List<JobTask> Tasks { get; set; } = new List<JobTask>();

        public IEnumerator<JobTask> GetEnumerator()
        {
            return Tasks.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class JobScheduleRequest
    {
        public List<Job> AllJobs { get; set; } = new List<Job>();
    }
}
