using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Postavhik
{
    public int IdPostavchik { get; set; }

    public string Name { get; set; } = null!;

    public string? Contact { get; set; }

    public virtual ICollection<Postavki> Postavkis { get; set; } = new List<Postavki>();
}
