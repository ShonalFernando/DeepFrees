namespace DeepFrees.Dispatcher.Model
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
        public int TaskCategoryID { get; set; }
    }

    //As the Dispatch Factory works with Task Categories, There should be another processing to allocate 
    //Tasks to Workers based on availability and randomness


    //Need Change
    public class AssignedTasks
    {
        public int DSDay { get; set; }
        public int DSMonth { get; set; }
        public string? EmployeeID { get; set; } //NIC
        public int TaskID { get; set; }
    }
}
