using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Staff
{
    public int IdStaff { get; set; }

    public string Suname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Fatherland { get; set; }

    public int PassportId { get; set; }

    public int PositionsId { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual Pasport Passport { get; set; } = null!;

    public virtual Position Positions { get; set; } = null!;
}
