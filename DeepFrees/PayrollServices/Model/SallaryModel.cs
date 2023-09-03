using MongoDB.Bson.Serialization.Attributes;

namespace PayrollServices.Model
{
    public class SallaryModel
    {
        public string EmpID { get; set; } = null!;
        public double BasePay { get; set; }
        public double Bonus { get; set; }
        public double Cutoff { get; set; }
        public double TotalPay { get; set; }
    }
}
