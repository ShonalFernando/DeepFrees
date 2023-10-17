namespace DeepFrees.CallDirecting.Model
{
    public class CallAgent
    {
        public string EmployeeID { get; set; } = null!; //NIC
        public bool IsAvailable { get; set; }
        public string? Category { get; set; }
    }

    public class Call
    {
        public int CallIndex { get; set; }
        public string? CallerName { get; set; }
        public string? RequestedCategory { get; set; }
        public string? AssignedAgent { get; set; } //NIC of Call agent employee
        public CallStatus CallStatus { get; set; }
        public int ElapsedTime { get; set; } //Time in Minutes
    }

    public enum CallStatus
    {
        Waiting,
        Started,
        Ended
    }

    public class CallPool
    {
        public List<Call>? CallList { get; set; }
        public List<CallAgent>? EmpList { get; set; }
    }
}