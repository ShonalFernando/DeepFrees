namespace DeepFreez.WebApp.Model
{
    public class DispatchRequest
    {
        public int EmpID { get; set; }
        public int[]? CatAvailableSlots { get; set; }
    }

    public class DispatchRequestList
    {
        public List<DispatchRequest> dpList { get; set; }
        public int MaxCat { get; set; }
    }

    public class DispatchSolutions
    {
        public int EmpID { get; set; }
        public int TaskID { get; set; }
    }
}
