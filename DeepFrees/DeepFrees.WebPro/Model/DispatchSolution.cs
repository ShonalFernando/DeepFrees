namespace DeepFrees.WebPro.Model
{
    public class DispatchRequest
    {
        public string? EmployeeID { get; set; } //NIC of Technician
        public int TaskCategoryID { get; set; }
        public int TaskPoints { get; set; }
    }

    public class DispatchSolution
    {
        public string? EmployeeID { get; set; } //NIC
        public int TaskID { get; set; }
    }

    public class DispatchSolutionView
    {
        public int DSMonth { get; set; }
        public int DSYear { get; set; }
        public string? TechnicianName { get; set; }
        public int TaskID { get; set; }
        public string? TaskName { get; set; }
        public int TaskLength { get; set; }
    }
}
