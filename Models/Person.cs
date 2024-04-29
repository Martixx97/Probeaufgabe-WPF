using Probeaufgabe_WPF.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xaml.Schema;

namespace Probeaufgabe_WPF.Models;

public partial class Person : INotifyPropertyChanged
{
    private int id;
    private string? name;
    private string? surname;
    private string? plz;
    private string? location;
    private byte[]? picture;
    public ICollection<PersonPhonenumber> personPhonenumbers = new List<PersonPhonenumber>();

    public ICollection<PersonPhonenumber> PersonPhonenumbers
    {
        get => personPhonenumbers; 
        set
        {
            personPhonenumbers = value;
            onPropertyChanged(nameof(PersonPhonenumbers));
        }
    }

    public int Id
    {

        get => id; set
        {
            id = value;
            onPropertyChanged(nameof(Id));
        }
    }

    public string? Name
    {

        get => name; set
        {
            name = value;
            onPropertyChanged(nameof(Name));
        }
    }

    public string? Surname
    {

        get => surname; set
        {
            surname = value;
            onPropertyChanged(nameof(Surname));
        }
    }
    public string? Plz
    {

        get => plz; set
        {
            plz = value;
            onPropertyChanged(nameof(Plz));
        }
    }

    public string? Location
    {

        get => location; set
        {
            location = value;
            onPropertyChanged(nameof(Location));
        }
    }

    public byte[]? Picture
    {

        get => picture; set
        {
            picture = value;
            onPropertyChanged(nameof(Picture));
        }
    }

    internal Person(string name, string surname, string plz, string location, byte[] picture)
    {
        this.Name = name;
        this.Surname = surname;
        this.Plz = plz;
        this.Location = location;
        this.Picture = picture;
    }

    internal Person(string name, string surname, string plz, string location, byte[]? picture, Dictionary<string, string> phonenumbers)
    {
        this.Name = name;
        this.Surname = surname;
        this.Plz = plz;
        this.Location = location;
       
    }
    public Person()
    {

    }
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void onPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
