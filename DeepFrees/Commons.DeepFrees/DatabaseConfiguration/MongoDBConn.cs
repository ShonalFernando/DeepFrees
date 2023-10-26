using Commons.DeepFrees.NetworkConfiguration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.DeepFrees.DatabaseConfiguration
{
    public class MongoDBConn
    {
        public static readonly string ConnectionString = "mongodb://localhost:" + Ports.DatabasePort;

        public static readonly string DatabaseName  = "DeepFrees";

        public static readonly string[] DeepFreesDataCollections = new string[] { "UserAccount", "Employee", "DeepFreesPay", "Employee", "WorkTasks" , "Technicians", "Locations"};

    }
}
