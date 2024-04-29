using Probeaufgabe_WPF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Probeaufgabe_WPF;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Probeaufgabe_WPF.Models
{
    public class PersonManager
    {
        public PersonManager()
        {
        }
        public List<Person> GetPersons
        {
            get
            {
                return MainWindow._DatabaseContext.Person.ToList(); 
            }
        }
        public static void AddPerson(Person person)
        {
            MainWindow._DatabaseContext.Attach(person);
            MainWindow._DatabaseContext.SaveChanges();
        }

        public static List<Person> getPersons()
        {
            return MainWindow._DatabaseContext.Person.ToList();
        }

        public static void RemovePerson(Person person)
        {

            MainWindow._DatabaseContext.Remove(person);
            MainWindow._DatabaseContext.SaveChanges();
        }
    }
}
