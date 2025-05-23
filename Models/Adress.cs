using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Adress
{
    public int IdAdress { get; set; }

    public string Street { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
