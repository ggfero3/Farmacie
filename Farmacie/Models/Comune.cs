using System;
using System.Collections.Generic;

namespace Farmacie.Models;

public partial class Comune
{
    public int Id { get; set; }

    public int? IdProvincia { get; set; }

    public string? Denominazione { get; set; }

    public virtual ICollection<Farmacia> Farmacies { get; set; } = new List<Farmacia>();

    public virtual ICollection<Frazione> Fraziones { get; set; } = new List<Frazione>();

    public virtual Provincia? IdProvinciaNavigation { get; set; }
}
