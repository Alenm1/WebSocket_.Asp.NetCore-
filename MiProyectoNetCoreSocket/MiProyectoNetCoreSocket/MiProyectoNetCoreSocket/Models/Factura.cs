using System;
using System.Collections.Generic;

namespace MiProyectoNetCoreSocket.Models;

public partial class Factura
{
    public int Iidfactura { get; set; }

    public int? Iidcompania { get; set; }

    public int? Numerofactura { get; set; }

    public decimal? Subtotalfactura { get; set; }

    public decimal? Impuesto { get; set; }

    public decimal? Otro { get; set; }

    public decimal? Total { get; set; }

    public int? Bhabilitado { get; set; }

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual Compañium? IidcompaniaNavigation { get; set; }
}
