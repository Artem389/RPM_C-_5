using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Categori
{
    public int IdCategori { get; set; }

    public string NameCategori { get; set; } = null!;

    public virtual ICollection<Mebel> Mebels { get; set; } = new List<Mebel>();
}
