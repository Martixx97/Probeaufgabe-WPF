using Microsoft.Win32;
using Probeaufgabe_WPF.Commands;
using Probeaufgabe_WPF.Data;
using Probeaufgabe_WPF.Models;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Probeaufgabe_WPF.ViewModel
{
    internal class AddPersonViewModel :INotifyPropertyChanged
    {

        PersonEntities personEntities;
        private const bool V = true;
        public AddPersonViewModel()
        {
            personEntities = new PersonEntities();
            person = new Person();
            PhoneNumbers = new ObservableCollection<PersonPhonenumber>();
            AddPersonCommand = new Command((s) => V, AddPerson);
            UploadPictureCommand = new Command((s) => V, UploadPicture);
            AddPhoneNumberCommand = new Command((s)=> V,AddPhoneNumber);
            RemovePhoneNumberCommand = new Command((s) => V,RemovePhoneNumber);

        }

        public void UploadPicture(object obj)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files|*.bmp;*.jpg;*png";
            openDialog.FilterIndex = 1;
            if (openDialog.ShowDialog() == true)
            {
                Uri uri = new Uri(openDialog.FileName);
                Person.Picture = uri.ToString();
                ImageSource = new BitmapImage(uri);
            }
        }

        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                onPropertyChanged(nameof(ImageSource));
            }
        }

        private void AddPerson(object obj)
        {
            personEntities.Person.Add(Person);
            foreach (var phoneNumber in PhoneNumbers) {
                PersonPhonenumber nPPN = new PersonPhonenumber();
                nPPN.Person = person;
                nPPN.Number = phoneNumber.Number;
                nPPN.Type = phoneNumber.Type.Split('.').Last().Split(':').Last();
                
                personEntities.PersonPhonenumbers.Add(nPPN);
            }
            personEntities.SaveChanges();
            Person = new Person();
            while (PhoneNumbers.Count > 0)
            {
                PhoneNumbers.RemoveAt(PhoneNumbers.Count - 1);
            }
        }

        public event EventHandler DialogClosed;
        public void OnDialogClosed()
        {
            DialogClosed?.Invoke(this, EventArgs.Empty);
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
        public void AddPhoneNumber(object obj)
        {
            PhoneNumbers.Add(new PersonPhonenumber());
        }

        public void RemovePhoneNumber(object obj)
        {
            PhoneNumbers.Remove(obj as PersonPhonenumber);
        }

        public ICommand AddPersonCommand { get; set; }
        public ICommand UploadPictureCommand { get; set; }

        public ICommand AddPhoneNumberCommand { get; }
        public ICommand RemovePhoneNumberCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }



}
