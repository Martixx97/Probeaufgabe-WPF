using System;
using System.Collections.Generic;

namespace Probeaufgabe_WPF.Models;

public partial class PersonPhonenumber
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public string? Number { get; set; }

    public string? Type { get; set; }

    public virtual Person Person { get; set; } = null!;

    internal PersonPhonenumber(Person person, string number, string type)
    {
        this.Person = person;
        this.Number = number;
        this.Type = type;
    }
    internal PersonPhonenumber(int personId, string number, string type)
    {
        this.PersonId = personId;
        this.Number = number;
        this.Type = type;
    }
}
