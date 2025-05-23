using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Mebel
{
    public int IdMebel { get; set; }

    public int CategoriId { get; set; }

    public string ProductName { get; set; } = null!;

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual Categori Categori { get; set; } = null!;

    public virtual ICollection<Postavki> Postavkis { get; set; } = new List<Postavki>();

    public virtual ICollection<SostavZakaza> SostavZakazas { get; set; } = new List<SostavZakaza>();
}
