namespace DeepFreez.WebApp.Data
{
    public class DBSettings
    {
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";

        public string DatabaseName { get; set; } = "DeepFrees";

        public string[] ShoppinzUsersCollectionName { get; set; } = new string[] { "UserAccount", "Employee", "Call", "Employee", "CallPool" , "WorkTaskRequest", "DispJobs", "EmpJobs" };
    }
}
