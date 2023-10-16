namespace DeepFrees.CallDirecting.Model
{
    public class CallCenterEmployee
    {
        public double EmpID { get; set; }
        public bool IsAvailable { get; set; }
        public string? Category { get; set; }
    }

    public class Call
    {
        public int CallIndex { get; set; }
        public string? CallerName { get; set; }
        public string? RequestedCategory { get; set; }
        public string? AssignedAgent { get; set; }
        public bool isEnded { get; set; }
    }

    public class CallPool
    {
        public List<Call>? CallList { get; set; }
        public List<CallCenterEmployee>? EmpList { get; set; }
    }

    public class CallPoolSolution
    {
        public double EmpID { get; set; }
        public int CallID { get; set; }
    }
}