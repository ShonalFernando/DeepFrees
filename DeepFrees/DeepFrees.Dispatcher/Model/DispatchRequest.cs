namespace DeepFrees.Dispatcher.Model
{
    public class DispatchRequest
    {
        public int EmpID { get; set; }
        public int[]? CatAvailableSlots { get; set; }
    }

    public class DispatchRequestList
    {
        public int WeekID { get; set; }
        public List<DispatchRequest> dpList { get; set; }
        public int MaxCat { get; set; }
    }

    public class DispatchSolution
    {
        public int EmpID { get; set; }
        public int TaskID { get; set; }
    }

    public class DispatchSolutions
    {
        public int WeekID { get; set; }
        public List<DispatchSolution>? DispatchSolutionList { get; set; }
    }
}
