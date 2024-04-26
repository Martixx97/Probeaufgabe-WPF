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
}
