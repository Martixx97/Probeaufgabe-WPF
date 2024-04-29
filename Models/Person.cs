using System;
using System.Collections.Generic;
using System.Xaml.Schema;

namespace Probeaufgabe_WPF.Models;

public partial class Person
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Plz { get; set; }

    public string? Location { get; set; }

    public byte[]? Picture { get; set; }

    public virtual ICollection<PersonPhonenumber> PersonPhonenumbers { get; set; } = new List<PersonPhonenumber>();

    internal Person(string name, string surname, string plz, string location, byte[] picture)
    {
        this.Name = name;
        this.Surname = surname;
        this.Plz = plz;
        this.Location = location;
        this.Picture = picture;
    }

    internal Person(string name, string surname, string plz, string location, byte[]? picture , Dictionary<string, string> phonenumbers)
    {
        this.Name = name;
        this.Surname = surname;
        this.Plz = plz;
        this.Location = location;

        if (phonenumbers.Count != 0)
            foreach (KeyValuePair<string, string> phonenumber in phonenumbers)
            {
                PersonPhonenumber personPhonenumber = new PersonPhonenumber(this, phonenumber.Key, phonenumber.Value);
                this.PersonPhonenumbers.Add(personPhonenumber);
            }
    }
}
