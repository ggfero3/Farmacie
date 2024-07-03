using System;
using System.Collections.Generic;

namespace Farmacie.Models;

public partial class Regione
{
    public int Id { get; set; }

    public string? Denominazione { get; set; }

    public virtual ICollection<Provincia> Provincia { get; set; } = new List<Provincia>();
}
