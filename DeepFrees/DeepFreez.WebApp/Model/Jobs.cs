namespace DeepFreez.WebApp.Model
{
    public class WorkTask
    {
        public int Machine { get; set; }
        public int Duration { get; set; }
    }

    public class Job
    {
        public List<WorkTask> Tasks { get; set; }
    }

    public class TaskSolution
    {
        public int Machine { get; set; }
        public List<TaskSession> TaskList { get; set; } = new List<TaskSession>();
    }
    public class TaskSession
    {
        public int JobID { get; set; }
        public int TaskID { get; set; }
        public int TaskStart { get; set; }
        public int TaskEnd { get; set; }
    }
}

