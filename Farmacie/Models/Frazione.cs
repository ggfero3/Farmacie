using System;
using System.Collections.Generic;

namespace Farmacie.Models;

public partial class Frazione
{
    public int Id { get; set; }

    public int? IdComune { get; set; }

    public string? Denominazione { get; set; }

    public virtual Comune? IdComuneNavigation { get; set; } = null!;
}
