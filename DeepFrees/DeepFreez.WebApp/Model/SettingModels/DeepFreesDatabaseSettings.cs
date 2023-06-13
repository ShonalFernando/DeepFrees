namespace DeepFreez.WebApp.Model.SettingModels
{
    public class DeepFreesDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string EmployeeCollectionName { get; set; } = null!;
    }
}
