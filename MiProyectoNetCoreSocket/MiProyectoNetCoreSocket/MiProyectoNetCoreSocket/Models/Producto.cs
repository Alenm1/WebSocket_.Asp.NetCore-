using System;
using System.Collections.Generic;

namespace MiProyectoNetCoreSocket.Models;

public partial class Producto
{
    public int Iidproducto { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public int? Bhabilitado { get; set; }

    public int? Stock { get; set; }

    public byte[]? Foto { get; set; }

    public string? Nombrefoto { get; set; }

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();
}
