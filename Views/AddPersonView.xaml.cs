using Microsoft.Win32;
using Probeaufgabe_WPF.Data;
using Probeaufgabe_WPF.ViewModel;
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

namespace Probeaufgabe_WPF.Views
{
    /// <summary>
    /// Interaction logic for AddPersonView.xaml
    /// </summary>
    public partial class AddPersonView : Window
    {
        public AddPersonView()
        {
            InitializeComponent();
            this.DataContext = new AddPersonViewModel();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            (DataContext as AddPersonViewModel)?.OnDialogClosed();
        }
    }
}
