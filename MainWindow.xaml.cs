using Probeaufgabe_WPF.Data;
using Probeaufgabe_WPF.Models;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Reflection.Emit;
using Microsoft.Win32;
using System.Windows.Media.Imaging;

namespace Probeaufgabe_WPF  
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static ProbeaufgabeWpfContext _DatabaseContext;
        public MainWindow()
        {
            var context = new ProbeaufgabeWpfContext();
            _DatabaseContext = context;
            List<Person> persons = context.Person.ToList();
            foreach (Person person in persons)
                Console.WriteLine(person.Name);
            context.PersonPhonenumbers.ToList();


            //_DatabaseContext = new ProbeaufgabeWpfContext();

            //var test = MainWindow._DatabaseContext.Person;
            InitializeComponent();


            //System.Windows.Controls.Button button = new Button();
            //button.Content = "OK";
            //System.Windows.Controls.StackPanel stackpanel2 = new StackPanel();
            //stackpanel2.Orientation = Orientation.Horizontal;
            //stackpanel2.Children.Add(button);
            //TextBox textBox1 = new TextBox();
            //stackpanel2.Children.Add(textBox1);
            //stackpanel.Children.Add(stackpanel2);



        }
    }
}