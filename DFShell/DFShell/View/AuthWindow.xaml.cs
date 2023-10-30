using DFShell.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DFShell.View
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }


        //Remove this event handler after bindings
        //For testing purposes only
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserAccsConsumer ucc = new();
            var acc = ucc.GetAccs(UsernameT.Text).Result;
            if(acc.Find(a => a.UserName.Equals(UsernameT.Text)).Password == PasswordT.Text)
            {
                Shell MainWindow = new Shell();
                MainWindow.Show();
                this.Hide();
            }

        }
    }
}
