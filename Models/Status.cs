using System;
using System.Collections.Generic;

namespace kursachRVV.Models;

public partial class Status
{
    public int IdStatys { get; set; }

    public string Statys { get; set; } = null!;

    public virtual ICollection<Zayavki> Zayavkis { get; set; } = new List<Zayavki>();
}
