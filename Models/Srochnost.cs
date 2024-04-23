using System;
using System.Collections.Generic;

namespace kursachRVV.Models;

public partial class Srochnost
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Zayavki> Zayavkis { get; set; } = new List<Zayavki>();
}
