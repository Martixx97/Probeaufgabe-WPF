using Probeaufgabe_WPF.Commands;
using Probeaufgabe_WPF.Data;
using Probeaufgabe_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Probeaufgabe_WPF.ViewModel
{
    internal class AddPersonViewModel :INotifyPropertyChanged
    {

        PersonEntities personEntities;
        private const bool V = true;
        public AddPersonViewModel()
        {
            personEntities = new PersonEntities();
            AddPersonCommand = new Command((s) => V, AddPerson);
        }
        private void AddPerson(object obj)
        {
            personEntities.Person.Add(Person);
            personEntities.SaveChanges();
            Person = new Person();
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

        public ICommand AddPersonCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }



}
