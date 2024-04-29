using Microsoft.Win32;
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
    /// Interaction logic for AddPerson.xaml
    /// </summary>
    public partial class AddPersonWindow : Window
    {
        public AddPersonWindow()
        {
            personEntities = new PersonEntities();
            InitializeComponent();
        }

        PersonEntities personEntities;




        private void Send_PersonData_To_DB(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> phonenumbers = getAllPhonenumbersFromXAMLContainers();
            if (phonenumbers != null)
            {
                Person person = new Person(Name_TB.Text, Surname_TB.Text, PLZ_TB.Text, Location_TB.Text, null, phonenumbers);
                personEntities.Person.Add(person);

                if (phonenumbers.Count > 0)
                    foreach (KeyValuePair<string, string> phonenumber in phonenumbers)
                    {
                        PersonPhonenumber personPhonenumber = new PersonPhonenumber();
                        personPhonenumber.Id = person.Id;
                        personPhonenumber.Number = phonenumber.Key;
                        personPhonenumber.Type = phonenumber.Value;
                        personEntities.PersonPhonenumbers.Add(personPhonenumber);
                    }

                personEntities.SaveChanges();

                MessageBox.Show("You added successfully a new person to the Database!");

                this.Close();
            }
            else
                MessageBox.Show("Adding to the Database failed!");
        }

        private Dictionary<string, string> getAllPhonenumbersFromXAMLContainers()
        {
            Dictionary<string, string> phonenumbers = new Dictionary<string, string>();
            foreach (object stackPanel in ParentPhoneNumbers_SP.Children)
            {
                if (stackPanel is StackPanel innerStackpanel && stackpanel != null)
                {
                    string? number = null;
                    string? type = null;

                    foreach (object child in innerStackpanel.Children)
                    {
                        if (child is TextBox textbox)
                            number = textbox.Text;
                        if (child is ComboBox comboBox)
                        {
                            ComboBoxItem? comboBoxItem = comboBox.SelectedItem as ComboBoxItem;
                            if (comboBoxItem != null)
                                type = comboBoxItem.Content.ToString();
                        }
                    }
                    try
                    {
                        if (number != null && number != "" && type != null)
                            phonenumbers.Add(number, type);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Please use different telephone numbers.");
                        return null;
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
                    foreach (ComboBoxItem comboBoxItem in comboBox.Items)
                    {
                        ComboBoxItem newComboBoxItem = new();
                        newComboBoxItem.Content = comboBoxItem.Content;
                        newComboBoxItem.IsSelected = comboBoxItem.IsSelected;
                        newChildcomboBox.Items.Add(newComboBoxItem);
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

        private void Picture_Upload_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files|*.bmp;*.jpg;*png";
            openDialog.FilterIndex = 1;
            if (openDialog.ShowDialog() == true)
            {
                imagePicture.Source = new BitmapImage(new Uri(openDialog.FileName));
            }
        }

    }
}
