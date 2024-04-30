using Probeaufgabe_WPF.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xaml.Schema;

namespace Probeaufgabe_WPF.Models;

public partial class Person : INotifyPropertyChanged
{
    private int id;
    private string? name;
    private string? surname;
    private string? street;
    private string? plz;
    private string? location;
    private byte[]? picture;
    private string firstNumber;
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
    public string? Street
    {

        get => street; set
        {
            street = value;
            onPropertyChanged(nameof(Street));
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

    public string Picture
    {

        get => Encoding.ASCII.GetString(picture); set
        {
            picture = Encoding.ASCII.GetBytes(value);
            onPropertyChanged(nameof(Picture));
        }
    }
    public string? FirstNumber
    {

        get
        {
            PersonEntities entities = new PersonEntities();
            StringBuilder sb = new StringBuilder();
            List<PersonPhonenumber> phonenumbers = entities.PersonPhonenumbers.Where(x => x.PersonId == Id).ToList();
            if (phonenumbers.Count() > 0)
            {
                PersonPhonenumber pPN = phonenumbers.FirstOrDefault(x => x.PersonId == Id);

                return sb.Append(pPN.Type + ": ").Append(pPN.Number).ToString();
            }
            return "No number assigned.";
        }
        //set
        //{
        //    firstNumber = value;
        //    onPropertyChanged(nameof(FirstNumber));
        //}
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
