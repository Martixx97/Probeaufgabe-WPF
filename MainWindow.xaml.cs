using Probeaufgabe_WPF.Data;
using Probeaufgabe_WPF.DataHandler;
using Probeaufgabe_WPF.Models;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Reflection.Emit;

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

            //System.Windows.Controls.Button button = new Button();
            //button.Content = "OK";
            //System.Windows.Controls.StackPanel stackpanel2 = new StackPanel();
            //stackpanel2.Orientation = Orientation.Horizontal;
            //stackpanel2.Children.Add(button);
            //TextBox textBox1 = new TextBox();
            //stackpanel2.Children.Add(textBox1);
            //stackpanel.Children.Add(stackpanel2);



        }
        private void Send_PersonData_To_DB(object sender, RoutedEventArgs e)
        {
            List<string> phonenumbers = getAllPhonenumbersFromXAMLContainers();

            PersonHandler newPerson = new PersonHandler(Name_TB.Text, Surname_TB.Text, PLZ_TB.Text, Location_TB.Text, phonenumbers);

            var context = new ProbeaufgabeWpfContext();

            //var transaction = context.Database.BeginTransaction;

            context.Attach(newPerson);
            context.SaveChanges();

            //context.Remove(person1);
            //context.SaveChanges();

            //transaction.Commit();

            MessageBox.Show("You added successfully a new person to the Database!");

            //InfoPopup infowindow = new InfoPopup();
            //infowindow.Show();


            List<Person> persons = context.Person.ToList();
            foreach (Person person in persons)
                Console.WriteLine(person.Name);
            context.PersonPhonenumbers.ToList();
        }

        private List<string> getAllPhonenumbersFromXAMLContainers()
        {
            List<string> phonenumbers = new List<string>();
            foreach (object stackPanel in ParentPhoneNumbers_SP.Children)
            {
                if (stackPanel is StackPanel innerStackpanel && stackpanel != null)
                {
                    foreach (object child in innerStackpanel.Children)
                    {
                        if (child is TextBox textbox)
                        {
                            phonenumbers.Add(textbox.Text);
                        }
                    }
                }
            }

            return phonenumbers;
        }

        private List<string> phoneNumberTextboxes = new();
        private void Add_Number_Button_Click(object sender, RoutedEventArgs e)
        {
            StackPanel newPhonenumber_SP = CopyXAMLContainer();
            ParentPhoneNumbers_SP.Children.Add(newPhonenumber_SP);
        }

        private StackPanel CopyXAMLContainer()
        {
            StackPanel newPhonenumber_SP = new StackPanel();

            foreach (object child in PhoneNumber_SP.Children)
            {
                if (child is System.Windows.Controls.Label label)
                {
                    System.Windows.Controls.Label newChildLabel = new();
                    newChildLabel.Width = label.Width;
                    newChildLabel.Content = label.Content;
                    newChildLabel.HorizontalContentAlignment = label.HorizontalContentAlignment;
                    newChildLabel.Margin = label.Margin;
                    newPhonenumber_SP.Children.Add(newChildLabel);
                }                
                if (child is System.Windows.Controls.ComboBox comboBox)
                {
                    System.Windows.Controls.ComboBox newChildcomboBox = new();
                    newChildcomboBox.Width = comboBox.Width;
                    newChildcomboBox.Margin = comboBox.Margin;
                    foreach(ComboBoxItem comboBoxItem in comboBox.Items)
                    {
                        newChildcomboBox.Items.Add(comboBoxItem);
                    }
                    newPhonenumber_SP.Children.Add(newChildcomboBox);
                }
                if (child is System.Windows.Controls.TextBox textbox)
                {
                    phoneNumberTextboxes.Add(textbox.Name);
                    System.Windows.Controls.TextBox newChildTextBox = new();
                    newChildTextBox.Width = textbox.Width;
                    newChildTextBox.Name = "Phonenumber_TB" + phoneNumberTextboxes.Count();
                    newPhonenumber_SP.Children.Add(newChildTextBox);
                }
            }
            newPhonenumber_SP.Orientation = PhoneNumber_SP.Orientation;
            return newPhonenumber_SP;
        }
    }
}