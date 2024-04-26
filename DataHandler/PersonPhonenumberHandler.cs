using Probeaufgabe_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probeaufgabe_WPF.DataHandler
{
    internal class PersonPhonenumberHandler : PersonPhonenumber
    {
        internal PersonPhonenumberHandler(Person person, string number, string type)
        {
            this.Person = person;
            this.Number = number;
            this.Type = type;
        }
    }
}
