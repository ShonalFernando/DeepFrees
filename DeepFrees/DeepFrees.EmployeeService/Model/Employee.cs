using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace DeepFrees.EmployeeService.Model
{
    public class Employee
    {
        [BsonId]
        public ObjectId _id { get; set; }

        public string NIC { get; set; } = null!;
        public string CommonName { get; set; } = null!; //The Common Name apart from the Name Array 
        public Contact Contact { get; set; } = null!;
        public Roles Roles { get; set; } = null!;
        public SallaryData SallaryData { get; set; } = null!;
        public PersonalData PersonalData { get; set; } = null!;
    }

    public class Contact
    {
        public string Mobile { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string[]? PhysicalAddress { get; set; } 
    }

    public class Roles
    {
        public string EmployeeRole { get; set; } = null!;
        public string? Privilege { get; set; }
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

        public int MonthlyPaidLeaves { get; set; } //Monthly leaves with pay
        public int YearlyPaidMedicalLeaves { get; set; } //Yearly Medical Leaves with pay
        public double MonthlyCutOff { get; set; } //Deductions for special cases
        public double FineTune { get; set; }

        public int[]? WeekSchedule { get; set; } //Eg: 0,8,8,8,8,8,4 <- Saturday is halfday -- Optional
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
        public int Gender { get; set; } // Male : 0 , Female : 1 , 💀 : 2,3,4
        public int Title { get; set; } // Mr, Mrs, Miss, Dr, Rev, Eng

        public Dictionary<string, string>? AdditionalDataDictionary { get; set; } //Additional Data
    }
}
