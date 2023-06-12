namespace DeepFreesAccountsServices.AppSettings
{
    public class DeepFreesDatabaseSettings
    {
        //WARNING!!! HARDCODED THE CONFIGURATION STRINGS, THIS IS A BAD PRACTISE THEREFORE REFACTOR LATER
        public string ConnectionString { get; set; } = "mongodb://localhost:27017"; //null!; 

        public string DatabaseName { get; set; } = "DeepFrees"; //null!;

        public string[] ShoppinzUsersCollectionName { get; set; } = new string[] { "DeepFreesUsers" , "DeepFreesPay", "DeepFreesSchedules"}; //null!;
    }
}
