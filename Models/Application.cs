using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Application
{
    public int IdApplications { get; set; }

    public DateOnly DateOfApplicationSubmission { get; set; }

    public string ApplicationStatus { get; set; } = null!;

    public int MebelId { get; set; }

    public int ClientsId { get; set; }

    public int StaffId { get; set; }

    public virtual Client Clients { get; set; } = null!;

    public virtual Mebel Mebel { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}
