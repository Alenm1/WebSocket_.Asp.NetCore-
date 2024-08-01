using System;
using System.Collections.Generic;

namespace MiProyectoNetCoreSocket.Models;

public partial class GrupoSanguineo
{
    public int Iidgruposanguineo { get; set; }

    public string? Nombresanguineo { get; set; }

    public int? Bhabilitado { get; set; }

    public virtual ICollection<FichaMedica> FichaMedicas { get; set; } = new List<FichaMedica>();
}
