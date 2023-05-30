using DFShell.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DFShell.ViewModel
{
    internal class MainViewModel:ViewModelBase
    {
        public ICommand OpenFlyoutCommand { get; }

        public MainViewModel()
        {
            OpenFlyoutCommand = new ShellCommand();
        }
    }
}
