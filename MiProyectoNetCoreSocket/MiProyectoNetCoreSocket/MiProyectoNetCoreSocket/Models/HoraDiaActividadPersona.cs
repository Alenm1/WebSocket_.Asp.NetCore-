using System;
using System.Collections.Generic;

namespace MiProyectoNetCoreSocket.Models;

public partial class HoraDiaActividadPersona
{
    public int Iidhoradiaactividadpersona { get; set; }

    public int? Iiddia { get; set; }

    public int? Iidhora { get; set; }

    public string? Nombreactividad { get; set; }

    public int? Iidpersona { get; set; }

    public int? Bhabilitado { get; set; }

    public virtual Dium? IiddiaNavigation { get; set; }

    public virtual Hora? IidhoraNavigation { get; set; }

    public virtual Persona? IidpersonaNavigation { get; set; }
}
