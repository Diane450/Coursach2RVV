using System;
using System.Collections.Generic;

namespace kursachRVV.Models;

public partial class Zayavki
{
    public int IdZayavki { get; set; }

    public string Opisanie { get; set; } = null!;

    public int Srochnost { get; set; }

    public string Raspolozenie { get; set; } = null!;

    public DateOnly DateAndTime { get; set; }

    public int? Stastus { get; set; }

    public int? Ispolnitel { get; set; }

    public virtual Ispolnitel? IspolnitelNavigation { get; set; }

    public virtual Srochnost SrochnostNavigation { get; set; } = null!;

    public virtual Status? StastusNavigation { get; set; }
}
