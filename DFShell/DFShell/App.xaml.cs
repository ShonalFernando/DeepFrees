using DFShell.View;
using DFShell.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DFShell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //MainWindow = new Shell()
            //{
            //    DataContext = new MainViewModel()
            //};
            //MainWindow.Show();

            MainWindow = new AuthWindow();
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
