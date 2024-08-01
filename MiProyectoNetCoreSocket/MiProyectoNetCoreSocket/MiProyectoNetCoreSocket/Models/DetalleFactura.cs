using System;
using System.Collections.Generic;

namespace MiProyectoNetCoreSocket.Models;

public partial class DetalleFactura
{
    public int Iiddetallefactura { get; set; }

    public int? Iidfactura { get; set; }

    public int? Iidproducto { get; set; }

    public decimal? Precio { get; set; }

    public int? Bhabilitado { get; set; }

    public virtual Factura? IidfacturaNavigation { get; set; }

    public virtual Producto? IidproductoNavigation { get; set; }
}
