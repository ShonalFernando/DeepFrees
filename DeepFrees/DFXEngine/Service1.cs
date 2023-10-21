using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DFXEngine
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //Check Interval Less Interval Consumes More resources
            timer = new Timer();
            timer.Interval = 60000; // Adjust interval as needed (1 minute in milliseconds)
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Check Todays Date and Perform Calculation
            if (DateTime.Now.Day == DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
            {
                // Run your MongoDB update logic here
                UpdateMongoDB();
            }
        }

        protected override void OnStop()
        {
        }
    }
}
