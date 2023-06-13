using MongoDB.Bson.Serialization.Attributes;

namespace DeepFreez.WebApp.Model
{
    public class Employee
    {

        public string _id { get; set; } = null!;

        public string Namestring { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string JobID { get; set; } = null!;
        public string? ScheduleID { get; set; }
        public string MobileNumber { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
    }
}
