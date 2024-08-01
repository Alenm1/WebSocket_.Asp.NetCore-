using System;
using System.Collections.Generic;

namespace MiProyectoNetCoreSocket.Models;

public partial class TipoDocumentoIdentificacion
{
    public int Iidtipodocumento { get; set; }

    public string? Nombre { get; set; }

    public int? Bhabilitado { get; set; }

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
