using System;
using System.Collections.Generic;

namespace MiProyectoNetCoreSocket.Models;

public partial class Chat
{
    public int Idmensaje { get; set; }

    public string? Mensaje { get; set; }

    public string? Nombreusuario { get; set; }

    public int Bhabilitado { get; set; }
}
