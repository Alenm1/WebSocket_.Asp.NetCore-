using System;
using System.Collections.Generic;

namespace MiProyectoNetCoreSocket.Models;

public partial class Registro
{
    public int Id { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }
}
