using Probeaufgabe_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probeaufgabe_WPF.DataHandler
{
    internal class PersonHandler : Person
    {

        //*TO-DO* replace List<string> with Dictionary<string, string> (<number, Type of number (f.e. mobile, home, work etc.))
        internal PersonHandler(string name, string surname, string plz, string location, Dictionary<string, string> phonenumbers)
        {
            this.Name = name;
            this.Surname = surname;
            this.Plz = plz;
            this.Location = location;

            if (phonenumbers.Count != 0)
                foreach (KeyValuePair<string, string> phonenumber in phonenumbers)
                {
                    PersonPhonenumber personPhonenumber = new PersonPhonenumberHandler(this, phonenumber.Key, phonenumber.Value);
                    this.PersonPhonenumbers.Add(personPhonenumber);
                }
        }
    }
}
