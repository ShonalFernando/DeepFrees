using MongoDB.Bson.Serialization.Attributes;

namespace PayrollServices.Model
{
    public class SallaryModel
    {
        [BsonId]
        public string? AccountID { get; set; }
        public string BasePay { get; set; } = null!;
        public string EmployeeID { get; set; } = null!;
        public string Bonus { get; set; } = null!;
        public string Cutoff { get; set; } = null!;
    }
}
