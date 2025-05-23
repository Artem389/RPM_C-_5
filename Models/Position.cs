using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Position
{
    public int IdPositions { get; set; }

    public string Positions { get; set; } = null!;

    public decimal Salary { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
