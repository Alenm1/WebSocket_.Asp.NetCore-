using System;
using System.Collections.Generic;

namespace MiProyectoNetCoreSocket.Models;

public partial class MedicamentoConsumoPersona
{
    public int Iidmedicamentoconsumopersona { get; set; }

    public int? Iidpersona { get; set; }

    public string? Descripcion { get; set; }

    public int? Bhabilitado { get; set; }

    public virtual Persona? IidpersonaNavigation { get; set; }
}
