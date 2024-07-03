using System;
using System.Collections.Generic;

namespace Farmacie.Models;

public partial class Farmacia
{
    public int Id { get; set; }

    public string? Codiceidentificativofarmacia { get; set; }

    public string? Codfarmaciaassegnatodaasl { get; set; }

    public string? Indirizzo { get; set; }

    public string? Descrizionefarmacia { get; set; }

    public string? Partitaiva { get; set; }

    public string? Cap { get; set; }

    public int? IdComune { get; set; }

    public string? Datainiziovalidita { get; set; }

    public string? Datafinevalidita { get; set; }

    public string? Descrizionetipologia { get; set; }

    public string? Codicetipologia { get; set; }

    public string? Latitudine { get; set; }

    public string? Longitudine { get; set; }

    public string? Localize { get; set; }

    public virtual Comune? IdComuneNavigation { get; set; }
}
