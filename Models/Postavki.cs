using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Postavki
{
    public int IdPostavki { get; set; }

    public int PostavchikaId { get; set; }

    public int MebelId { get; set; }

    public DateOnly DatePostavki { get; set; }

    public decimal PricePostavki { get; set; }

    public virtual Mebel Mebel { get; set; } = null!;

    public virtual Postavhik Postavchika { get; set; } = null!;
}
