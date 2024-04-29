using Probeaufgabe_WPF.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Probeaufgabe_WPF.Models;

public partial class PersonPhonenumber : INotifyPropertyChanged
{
    private int id;
    private int personId;
    private string? number;
    private string? type;
    public virtual Person? Person { get; set; }

    public int Id
    {

        get => id; set
        {
            id = value;
            onPropertyChanged(nameof(Id));
        }
    }

    public int PersonId
    {

        get => personId; set
        {
            personId = value;
            onPropertyChanged(nameof(PersonId));
        }
    }

    public string Number
    {

        get => number; set
        {
            number = value;
            onPropertyChanged(nameof(Number));
        }
    }

    public string Type
    {

        get => type; set
        {
            type = value;
            onPropertyChanged(nameof(Type));
        }
    }

    private Person getPerson(int personID)
    {
        PersonEntities entities = new PersonEntities();
        List<Person> persons = entities.Person.ToList();
        return persons.FirstOrDefault(x => x.Id == personID); ;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void onPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
