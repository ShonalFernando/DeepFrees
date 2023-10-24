using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.ComponentModel.DataAnnotations;

namespace DeepFrees.EmployeeService.Model
{
    public class Employee
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string? nic { get; set; }
        public string? commonName { get; set; }
        public Contact? contact { get; set; }
        public Roles? roles { get; set; }
        public SallaryData? sallaryData { get; set; }
        public PersonalData? personalData { get; set; }
        public Education? education { get; set; }
        public int teams { get; set; }
        public bool isRecycled { get; set; }
    }

    public class Contact
    {
        [RegularExpression(@"^\+(?:\d{1,3}-)?\d{9}$")]
        public string mobile { get; set; } = null!;

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        public string emailAddress { get; set; } = null!;

        public string[]? physicalAddress { get; set; }
    }

    public class Roles
    {
        public EmployeeRole employeeRole { get; set; }
        public string? privilege { get; set; }
    }

    public enum EmployeeRole
    {
        Manager,
        Engineering,
        CallAgent
    }

    public enum Title
    {
        Mr,
        Mrs,
        Miss,
        Dr,
        Eng
    }

    public enum Teams
    {
        //Manager
        Operations,
        Project_Management,
        Customer_Support,
        Human_Resource,

        //Engineering
        Electronics_Design,
        Mechanical_Design,
        Electrical_Design,
        Software_Development,
        Software_QA,
        Electronics_QA,
        Electrical_QA,
        DevOps,

        //Finance
        Accounting,
        Planning,
        Sales,
        Audit,
        Analysis_and_Reporting
    }

    public class Education
    {
        public Dictionary<string, string>? qualifications { get; set; } //Academy: Qualification
        public string? educationLevel { get; set; }
        public string? educationDiscription { get; set; }
    }

    public class SallaryData
    {
        public int baseSallary { get; set; }
        public int allocatedYearlyPaidLeaves { get; set; }
        public int allocatedYearlyPaidMedicalLeaves { get; set; }
        public int monthlyCutOff { get; set; }
        public int fineTune { get; set; }
        public int otRatePerHour { get; set; }
        public int noPayRatePerDay { get; set; }
        public int cumulativeLeaves { get; set; }
        public int cumulativeMedicalLeaves { get; set; }
        public List<MonthlySallarySheet>? monthlySallarySheets { get; set; }
        public List<int>? weekSchedule { get; set; }
    }

    public class MonthlySallarySheet
    {
        public int month { get; set; }
        public int year { get; set; }
        public int deductions { get; set; }
        public int employeeFund { get; set; }
        public int tax { get; set; }
        public int nonMedicalLeaves { get; set; }
        public int medicalLeaves { get; set; }
        public int otHours { get; set; }
        public int netSallary { get; set; }
        public int[]? increments { get; set; }
    }

    public class PersonalData
    {
        public List<string>? nameArray { get; set; }
        public string? bankAccountNumber { get; set; }
        public string? bank { get; set; }
        public int religiousPreference { get; set; }
        public int languagePreference { get; set; }
        public int gender { get; set; }
        public int title { get; set; }
        public Dictionary<string, string>? additionalPersonalData { get; set; } //Additional Data
    }
}
