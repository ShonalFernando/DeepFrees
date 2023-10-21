using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.ComponentModel.DataAnnotations;

namespace DeepFrees.EmployeeService.Model
{
    public class EmployeePR
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [StringLength(12)]
        public string NIC { get; set; } = null!;

        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$")]
        public string CommonName { get; set; } = null!; //The Common Name apart from the Name Array 

        public Contact Contact { get; set; } = null!;
        public Roles Roles { get; set; } = null!;
        public SallaryData SallaryData { get; set; } = null!;
        public PersonalData PersonalData { get; set; } = null!;

        public Teams Teams { get; set; }

        public bool isRecycled { get; set; }

        public EmployeePR()
        {
            _id = ObjectId.GenerateNewId();
        }
    }

    public class Contact
    {
        [RegularExpression(@"^\+(?:\d{1,3}-)?\d{9}$")]
        public string Mobile { get; set; } = null!;

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        public string EmailAddress { get; set; } = null!;

        public string[]? PhysicalAddress { get; set; } 
    }

    public class Roles
    {
        public EmployeeRole EmployeeRole { get; set; }
        public string? Privilege { get; set; }
    }

    public enum EmployeeRole
    {
        Manager,
        Engineering,
        CallAgent
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
        public Dictionary<string, string>? Qualifications { get; set; } //Academy: Qualification
        public string? EducationLevel { get; set; } 
        public string? EducationDiscription { get; set; }
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

        public List<MonthlySallarySheet>? MonthlySallarySheets { get; set; }
        public int[]? WeekSchedule { get; set; } //Eg: 0,8,8,8,8,8,4 <- Saturday is halfday -- Optional
    }

    public class MonthlySallarySheet
    {
        public int Month { get; set; } // Assuming 1 represents January, 2 represents February, and so on
        public int Year { get; set; } //මේහෙම කරන එක ලේසී DateTime එකට වඩා, වැඩේ වෙනවනං එච්චරයි

        public double Deductions { get; set; }
        public double EmployeeFund { get; set; }
        public double Tax { get; set; }

        public List<Tuple<double, string>>? Increments { get; set; } //The List contains increments and extras with the reason

        public int NonMedicalLeaves { get; set; }
        public int MedicalLeaves { get; set; }

        public int OTHours { get; set; }

        public double NetSallary { get; set; }
    }

    public class PersonalData
    {
        //Gender, Title වගේ ඒවට int use කරලා තියන නිසා සම්පූර්ණ Application එකේ Cross Standard එකක් හදාගන්න ඕන
        //enumarations වලින් හරි... අදාල එකට අදාල value එක map කරන්න
        public string[] NameArray { get; set; } = null!;
        public string BankAccountNumber { get; set; } = null!;
        public string Bank { get; set; } = null!; //බැංකුවේ නම
        public int ReligiousPreference { get; set; } //නිවාඩූ...
        public int LanguagePreference { get; set; }
        public int Gender { get; set; } // Male : 0 , Female : 1
        public int Title { get; set; } // Mr, Mrs, Miss, Dr, Rev, Eng

        public Dictionary<string, string>? AdditionalPersonalData { get; set; } //Additional Data
    }
}
