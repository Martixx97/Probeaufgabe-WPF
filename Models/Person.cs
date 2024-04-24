using System;
using System.Collections.Generic;

namespace Probeaufgabe_WPF.Models;

public partial class Person
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Plz { get; set; }

    public string? Location { get; set; }

    public virtual ICollection<PersonPhonenumber> PersonPhonenumbers { get; set; } = new List<PersonPhonenumber>();

}
