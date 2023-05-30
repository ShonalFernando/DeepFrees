using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DFShell.Commands
{
    internal class ShellCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            MessageBox.Show("Working");
        }
    }
}
