using System;
using System.Collections.Generic;

namespace MiProyectoNetCoreSocket.Models;

public partial class TratamientoMedicoPersona
{
    public int Iidtratamientomedicopersona { get; set; }

    public int? Iidpersona { get; set; }

    public string? Descripcion { get; set; }

    public int? Bhabilitado { get; set; }

    public virtual Persona? IidpersonaNavigation { get; set; }
}
