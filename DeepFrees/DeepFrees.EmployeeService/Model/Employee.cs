using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DeepFrees.EmployeeService.Model
{
    public class Employee
    {
        [BsonId]
        public ObjectId? _id { get; set; }

        public string NIC { get; set; } = null!;
        public Contact Contact { get; set; } = null!;
        public Roles Roles { get; set; } = null!;
        public List<WorkTask>? WorkTasks { get; set; } = null!;

    }

    public class Contact
    {
        public string Mobile { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string? PhysicalAddress { get; set; } 
    }

    public class Roles
    {
        public string EmployeeRole { get; set; } = null!;
        public string? Privilege { get; set; } = null!;
    }

    public class WorkTask
    {
        public string? TaskName { get; set; }
        public int? TaskStart { get; set; }        
        public int? Duration { get; set; }
    }
}
