using System;
using System.Collections.Generic;

namespace kursachRVV.Models;

public partial class Rol
{
    public int IdRol { get; set; }

    public string Roli { get; set; } = null!;

    public virtual ICollection<Vhod> Vhods { get; set; } = new List<Vhod>();
}
