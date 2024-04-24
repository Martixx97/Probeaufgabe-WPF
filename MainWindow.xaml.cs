using Probeaufgabe_WPF.Data;
using Probeaufgabe_WPF.Models;
using System.Windows;

namespace Probeaufgabe_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Person person1 = new Person();
            person1.Name = "Testvorname2";
            var context = new ProbeaufgabeWpfContext();

//            var transaction = context.Database.BeginTransaction();
            context.Attach(person1);
            context.SaveChanges();

            context.Remove(person1);
            context.SaveChanges();

            //transaction.Commit();



        }
    }
}