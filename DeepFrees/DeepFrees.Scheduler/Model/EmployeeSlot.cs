namespace DeepFrees.Scheduler.Model
{
    public class EmployeeSlot
    {
        public int ID { get; set; }
        public List<int> TimeAvailableAtWeek { get; set; }
        public string Category { get; set; }
    }
}
