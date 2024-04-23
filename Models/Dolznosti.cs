using System;
using System.Collections.Generic;

namespace kursachRVV.Models;

public partial class Dolznosti
{
    public int IdDolznosti { get; set; }

    public string Dolznost { get; set; } = null!;

    public virtual ICollection<TexnicheskiOtdel> TexnicheskiOtdels { get; set; } = new List<TexnicheskiOtdel>();
}
