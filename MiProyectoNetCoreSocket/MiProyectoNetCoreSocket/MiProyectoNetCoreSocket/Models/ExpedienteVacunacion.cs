using System;
using System.Collections.Generic;

namespace MiProyectoNetCoreSocket.Models;

public partial class ExpedienteVacunacion
{
    public int Iidexpedientevacunacion { get; set; }

    public int? Iidpersona { get; set; }

    public DateOnly? Fechavacunacion { get; set; }

    public int? Diabetes { get; set; }

    public int? Hipertencion { get; set; }

    public string? Otropadecimiento { get; set; }

    public int? Iidmarcavacuna { get; set; }

    public int? Numerodosis { get; set; }

    public int? Lotevacuna { get; set; }

    public int? Bhabilitado { get; set; }

    public int? Edad { get; set; }

    public virtual MarcaVacuna? IidmarcavacunaNavigation { get; set; }

    public virtual Persona? IidpersonaNavigation { get; set; }
}
