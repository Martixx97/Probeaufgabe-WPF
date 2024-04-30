using Microsoft.Win32;
using Probeaufgabe_WPF.Commands;
using Probeaufgabe_WPF.Data;
using Probeaufgabe_WPF.Models;
using Probeaufgabe_WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Windows;

namespace Probeaufgabe_WPF.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private const bool V = true;
        PersonEntities personEntities;

        public MainViewModel()
        {
            personEntities = new PersonEntities();
            LoadPerson();
            PhoneNumbers = new ObservableCollection<PersonPhonenumber>();
            DeleteCommand = new Command((s) => V, Delete);
            UpdateCommand = new Command((s) => V, Update);
            UpdatePersonCommand = new Command((s) => V, UpdatePerson);
            AddPersonCommand = new Command((s) => V, AddPerson);
            ReloadTableCommand = new Command((s) => V, ReloadTable);
            UploadPictureCommand = new Command((s) => V, UploadPicture);
            AddPhoneNumberCommand = new Command((s) => V, AddPhoneNumber);
            RemovePhoneNumberCommand = new Command((s) => V, RemovePhoneNumber);

        }
        #region CRUD-Methods
        public void AddPhoneNumber(object obj)
        {
            PhoneNumbers.Add(new PersonPhonenumber());
        }

        public void RemovePhoneNumber(object obj)
        {
            PhoneNumbers.Remove(obj as PersonPhonenumber);
        }
        private void AddPerson(object obj)
        {
            AddPersonView addPersonView = new AddPersonView();
            addPersonView.ShowDialog();
        }
        private void Delete(object obj)
        {
            var person = obj as Person;
            foreach (var phoneNumber in personEntities.PersonPhonenumbers.Where(x => x.PersonId == person.Id).ToList())
            {
                personEntities.PersonPhonenumbers.Remove(phoneNumber);
            }
            personEntities.Remove(person);
            LstPerson.Remove(person);
            personEntities.SaveChanges();
        }

        private void LoadPerson() //Read Person
        {
            LstPerson = new ObservableCollection<Person>(personEntities.Person);
        }
        private void UpdatePerson(object obj)
        {
            foreach (var phoneNumber in PhoneNumbers)
            {
                PersonPhonenumber nPPN = new PersonPhonenumber();
                nPPN.Person = person;
                nPPN.Number = phoneNumber.Number;
                nPPN.Type = phoneNumber.Type.Split('.').Last().Split(':').Last();

                personEntities.PersonPhonenumbers.Add(nPPN);
            }
            personEntities.SaveChanges();
            SelectedPerson_ImageSource = null;
            while (PhoneNumbers.Count > 0)
            {
                PhoneNumbers.RemoveAt(PhoneNumbers.Count - 1);
            }
            SelectedPerson = new Person();

        }
        private void Update(object obj)
        {
            while (PhoneNumbers.Count > 0)
            {
                PhoneNumbers.RemoveAt(PhoneNumbers.Count - 1);
            }

            SelectedPerson = obj as Person;

            List<PersonPhonenumber> selectedPersonPhonenumbers = personEntities.PersonPhonenumbers.Where(x => x.PersonId == SelectedPerson.Id).ToList();
            if (selectedPersonPhonenumbers.Count() > 0)
                foreach (var phonenumber in selectedPersonPhonenumbers)
                {
                    PhoneNumbers.Add(phonenumber);
                }
            if (SelectedPerson.Picture != null)
            {
                Uri newUri = new Uri(SelectedPerson.Picture);
                SelectedPerson_ImageSource = new BitmapImage(newUri);
            }
            else
            {
                SelectedPerson_ImageSource = null;
            }
        }
        #endregion

        private void ReloadTable(object obj)
        {
            LstPerson = new ObservableCollection<Person>(personEntities.Person);
        }
      
        public void UploadPicture(object obj)
        {
            if(SelectedPerson == null)
            {
                MessageBox.Show("Please select a person from the list first.");
                return;
            }    
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files|*.bmp;*.jpg;*png";
            openDialog.FilterIndex = 1;
            if (openDialog.ShowDialog() == true)
            {
                Uri uri = new Uri(openDialog.FileName);
                SelectedPerson.Picture = uri.ToString();
                SelectedPerson_ImageSource = new BitmapImage(uri);
            }
        }

        
        

        public ICommand DeleteCommand { get; set; }
        public ICommand ReloadTableCommand { get; set; }
        public ICommand UpdatePersonCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand AddPersonCommand { get; set; }
        public ICommand UploadPictureCommand { get; set; }
        public ICommand AddPhoneNumberCommand { get; }
        public ICommand RemovePhoneNumberCommand { get; }

        #region Property-Definitions
        private ImageSource _selectedPerson_imageSource;
        public ImageSource SelectedPerson_ImageSource
        {
            get { return _selectedPerson_imageSource; }
            set
            {
                _selectedPerson_imageSource = value;
                onPropertyChanged(nameof(SelectedPerson_ImageSource));
            }
        }
        private ObservableCollection<PersonPhonenumber> phoneNumbers;
        public ObservableCollection<PersonPhonenumber> PhoneNumbers
        {
            get { return phoneNumbers; }
            set
            {
                phoneNumbers = value;
                onPropertyChanged(nameof(PhoneNumbers));
            }
        }
        private Person _selectedPerson;

        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                onPropertyChanged(nameof(SelectedPerson));
            }

        }
        private Person person = new Person();

        public Person Person
        {
            get { return person; }
            set
            {
                person = value;
                onPropertyChanged(nameof(Person));
            }

        }

        private ObservableCollection<Person> _lstPerson;

        public ObservableCollection<Person> LstPerson
        {
            get { return _lstPerson; }
            set
            {
                _lstPerson = value;
                onPropertyChanged(nameof(LstPerson));
            }
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
