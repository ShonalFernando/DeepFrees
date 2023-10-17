namespace DeepFrees.Dispatcher.Model
{
    public class DispatchRequest
    {
        public string? EmployeeID { get; set; } //NIC
        public int TaskCategoryID { get; set; }
        public int TaskPoints { get; set; }
    }

    public class DispatchSolution
    {
        public string? EmployeeID { get; set; } //NIC
        public int TaskID { get; set; }
    }
}
