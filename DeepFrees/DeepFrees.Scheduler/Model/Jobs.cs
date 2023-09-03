namespace DeepFrees.Scheduler.Model
{
    public class WorkTask
    {
        public int Employee { get; set; }
        public int Duration { get; set; }
    }

    public class Job
    {
        public List<WorkTask> Tasks { get; set; }
    }

    public class WeeklyJob
    {
        public int WeekID { get; set; }
        public List<Job> JobList { get; set; }
    }

    public class TaskSolution
    {

        public int Employee { get; set; }
        public List<TaskSession> TaskList { get; set; } = new List<TaskSession>();
    }

    public class WeeklyTaskSolutions
    {

        public int WeekID { get; set; }
        public List<TaskSolution> TaskSolutions { get; set; } = new List<TaskSolution>();
    }

    public class TaskSession
    {
        public int JobID { get; set; }    
        public int TaskID { get; set; }
        public int TaskStart { get; set; }
        public int TaskEnd { get; set; }
    }
}
