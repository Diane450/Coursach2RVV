using System;
using System.Collections.Generic;

namespace kursachRVV.Models;

public partial class TexnicheskiOtdel
{
    public int IdTexnicheskiOtdel { get; set; }

    public int IdIspolnitel { get; set; }

    public string Familia { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Otchestvo { get; set; }

    public DateOnly DataRozden { get; set; }

    public long PhoneNumber { get; set; }

    public string Gender { get; set; } = null!;

    public string? FamilyStatys { get; set; }

    public int Doljnost { get; set; }

    public virtual Dolznosti DoljnostNavigation { get; set; } = null!;

    public virtual Ispolnitel? IspolnitelIdIspolnitelNavigation { get; set; }

    public virtual ICollection<Ispolnitel> IspolnitelTexOtSotrydnikNavigations { get; set; } = new List<Ispolnitel>();

    public virtual ICollection<Vhod> Vhods { get; set; } = new List<Vhod>();
}
