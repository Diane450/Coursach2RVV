using System;
using System.Collections.Generic;

namespace kursachRVV.Models;

public partial class Vhod
{
    public int IdVhod { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Rol { get; set; }

    public int TexOt { get; set; }

    public virtual Rol RolNavigation { get; set; } = null!;

    public virtual TexnicheskiOtdel TexOtNavigation { get; set; } = null!;
}
