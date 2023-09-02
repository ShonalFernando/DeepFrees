namespace DeepFrees.CallDirecting.Model
{
    public class CallCenterEmployee
    {
        public int EmpID { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public string Category { get; set; }
    }

    public class Call
    {
        public int CallID { get; set; }
        public string CallerName { get; set; }
        public string RequestedCategory { get; set; }
    }
}