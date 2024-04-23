using System;
using System.Collections.Generic;

namespace kursachRVV.Models;

public partial class Ispolnitel
{
    public int IdIspolnitel { get; set; }

    public int TexOtSotrydnik { get; set; }

    public virtual TexnicheskiOtdel IdIspolnitelNavigation { get; set; } = null!;

    public virtual TexnicheskiOtdel TexOtSotrydnikNavigation { get; set; } = null!;

    public virtual ICollection<Zayavki> Zayavkis { get; set; } = new List<Zayavki>();
}
