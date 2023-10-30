using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DeepFrees.SettingsService.Model
{
    public class SettingsModel
    {
        [BsonId]
        public ObjectId _id { get; set; }

        public SallaryData? sallaryData { get; set; }
        public Dictionary<string, string>? settingDefinitions {get; set;} 
    }

    public class SallaryData
    {
        public double BaseSallary { get; set; }

        public int AllocatedYearlyPaidLeaves { get; set; } //Allocated Monthly leaves with pay
        public int AllocatedYearlyPaidMedicalLeaves { get; set; } //Allocated Yearly Medical Leaves with pay
        public double MonthlyCutOff { get; set; } //Repititive Deductions for special cases
        public double FineTune { get; set; }//Repititive per month

        public double OTRatePerHour { get; set; } //Repititive per month, Per Hour
        public double NoPayRatePerDay { get; set; } //Repititive per month, Per Leave (Per Day)


        public int CumulativeLeaves { get; set; }   //TotalLeaves Taken this year, resets Jan 1st
        public int CumulativeMedicalLeaves { get; set; }   //TotalLeaves Taken this year, resets Jan 1st

        public int[]? WeekSchedule { get; set; } //Eg: 0,8,8,8,8,8,4 <- Saturday is halfday -- Optional
    }
}
