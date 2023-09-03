namespace DeepFreez.WebApp.Model
{
    public class CallTechModel
    {
        public class CallCenterEmployee
        {
            public double EmpID { get; set; }
            public string Name { get; set; }
            public bool IsAvailable { get; set; }
            public string Category { get; set; }
        }

        public class Call
        {
            public int CallID { get; set; }
            public string? CallerName { get; set; }
            public string RequestedCategory { get; set; }
        }

        public class CallPool
        {
            public List<Call> CallList { get; set; }
            public List<CallCenterEmployee> EmpList { get; set; }
        }

        public class CallPoolSolution
        {
            public double EmpID { get; set; }
            public int CallID { get; set; }
        }
    }
}
