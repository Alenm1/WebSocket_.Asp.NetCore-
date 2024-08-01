using System;
using System.Collections.Generic;

namespace MiProyectoNetCoreSocket.Models;

public partial class Sexo
{
    public int Iidsexo { get; set; }

    public string? Nombre { get; set; }

    public int? Bhabilitado { get; set; }

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
