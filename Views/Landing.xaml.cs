using Probeaufgabe_WPF.Data;
using Probeaufgabe_WPF.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Probeaufgabe_WPF.Views
{
    /// <summary>
    /// Interaction logic for Landing.xaml
    /// </summary>
    public partial class Landing : Page
    {
        public static DatabaseContext _DatabaseContext;
        public Landing()
        {
            var context = new DatabaseContext();
            _DatabaseContext = context;
            List<Person> persons = context.Person.ToList();
            foreach (Person person in persons)
                Console.WriteLine(person.Name);
            context.PersonPhonenumbers.ToList();
            InitializeComponent();
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            //var ClickedButton = e.OriginalSource as NavButton;
            //NavigationService.Navigate(ClickedButton.NavUri);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OverviewPerson overviewPerson = new OverviewPerson();
            overviewPerson.Show();
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            AddPersonWindow  addPerson = new AddPersonWindow();
            addPerson.Show();
        }
    }
}
