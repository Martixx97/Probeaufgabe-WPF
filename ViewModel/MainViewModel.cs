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

namespace Probeaufgabe_WPF.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public ICommand ShowWindowCommand { get; set; }

        private const bool V = true;
        PersonEntities personEntities;

        public MainViewModel()
        {
            personEntities = new PersonEntities();
            LoadPerson();
            DeleteCommand = new Command((s) => V, Delete);
            UpdateCommand = new Command((s) => V, Update);
            UpdatePersonCommand = new Command((s) => V, UpdatePerson);
            AddPersonCommand = new Command((s) => V, this.AddPerson);
        }
        private void AddPerson(object obj)
        {
            personEntities.Person.Add(Person);
            personEntities.SaveChanges();
            LstPerson.Add(Person);
            Person = new Person();
        }
        private void UpdatePerson(object obj)
        {
            personEntities.SaveChanges();
            SelectedPerson = new Person();
        }

        private void Update(object obj)
        {

            SelectedPerson = obj as Person;
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

        public ICommand DeleteCommand { get; set; }

        public ICommand UpdatePersonCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand AddPersonCommand { get; set; }

        private Person _selectedPerson;

        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set { _selectedPerson = value;
                onPropertyChanged(nameof(SelectedPerson));
            }

        }       
        private Person person = new Person();

        public Person Person
        {
            get { return person; }
            set {
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        class Command : ICommand
        {
            public event EventHandler? CanExecuteChanged;

            private Action<object> _Execute { get; set; }

            private Func<object, bool> _CanExecute { get; set; }

            public Command(Func<object, bool> CanExecuteMethod, Action<object> ExecuteMethod)
            {
                _Execute = ExecuteMethod;
                _CanExecute = CanExecuteMethod;
            }

            public bool CanExecute(object? parameter)
            {
                return _CanExecute(parameter);
            }
            public void Execute(object? parameter)
            {
                _Execute(parameter);
            }
        }
    }
}
