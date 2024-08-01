using System;
using System.Collections.Generic;

namespace MiProyectoNetCoreSocket.Models;

public partial class Persona
{
    public int Iidpersona { get; set; }

    public string? Numeroidentificacion { get; set; }

    public string? Nombre { get; set; }

    public string? Appaterno { get; set; }

    public string? Apmaterno { get; set; }

    public int? Iidsexo { get; set; }

    public string? Correo { get; set; }

    public string? Telefonoocelular1 { get; set; }

    public string? Telefonoocelular2 { get; set; }

    public string? Calle { get; set; }

    public string? Nexterior { get; set; }

    public string? Ninterior { get; set; }

    public string? Colonia { get; set; }

    public string? Cp { get; set; }

    public string? Municipiopais { get; set; }

    public string Estadopais { get; set; } = null!;

    public int? Iidtipodocumento { get; set; }

    public string? Numeroregistrounicocontribuyente { get; set; }

    public string? Nombrefoto { get; set; }

    public int? Bhabilitado { get; set; }

    public virtual ICollection<EnfermedadesIntervencionesPersona> EnfermedadesIntervencionesPersonas { get; set; } = new List<EnfermedadesIntervencionesPersona>();

    public virtual ICollection<ExpedienteVacunacion> ExpedienteVacunacions { get; set; } = new List<ExpedienteVacunacion>();

    public virtual ICollection<FichaMedica> FichaMedicas { get; set; } = new List<FichaMedica>();

    public virtual ICollection<HoraDiaActividadPersona> HoraDiaActividadPersonas { get; set; } = new List<HoraDiaActividadPersona>();

    public virtual Sexo? IidsexoNavigation { get; set; }

    public virtual TipoDocumentoIdentificacion? IidtipodocumentoNavigation { get; set; }

    public virtual ICollection<MedicamentoConsumoPersona> MedicamentoConsumoPersonas { get; set; } = new List<MedicamentoConsumoPersona>();

    public virtual ICollection<MedicoTelefonoPersona> MedicoTelefonoPersonas { get; set; } = new List<MedicoTelefonoPersona>();

    public virtual ICollection<ObservacionesPersona> ObservacionesPersonas { get; set; } = new List<ObservacionesPersona>();

    public virtual ICollection<TratamientoMedicoPersona> TratamientoMedicoPersonas { get; set; } = new List<TratamientoMedicoPersona>();
}
