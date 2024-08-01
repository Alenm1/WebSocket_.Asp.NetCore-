using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MiProyectoNetCoreSocket.Models;

public partial class DbAab78eBdreportesContext : DbContext
{
    public DbAab78eBdreportesContext()
    {
    }

    public DbAab78eBdreportesContext(DbContextOptions<DbAab78eBdreportesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Compañium> Compañia { get; set; }

    public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }

    public virtual DbSet<Dium> Dia { get; set; }

    public virtual DbSet<EnfermedadesIntervencionesPersona> EnfermedadesIntervencionesPersonas { get; set; }

    public virtual DbSet<ExpedienteVacunacion> ExpedienteVacunacions { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FichaMedica> FichaMedicas { get; set; }

    public virtual DbSet<GrupoSanguineo> GrupoSanguineos { get; set; }

    public virtual DbSet<Hora> Horas { get; set; }

    public virtual DbSet<HoraDiaActividadPersona> HoraDiaActividadPersonas { get; set; }

    public virtual DbSet<MarcaVacuna> MarcaVacunas { get; set; }

    public virtual DbSet<MedicamentoConsumoPersona> MedicamentoConsumoPersonas { get; set; }

    public virtual DbSet<MedicoTelefonoPersona> MedicoTelefonoPersonas { get; set; }

    public virtual DbSet<ObservacionesPersona> ObservacionesPersonas { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Registro> Registros { get; set; }

    public virtual DbSet<Sexo> Sexos { get; set; }

    public virtual DbSet<SistemaSalud> SistemaSaluds { get; set; }

    public virtual DbSet<TipoAlergium> TipoAlergia { get; set; }

    public virtual DbSet<TipoDocumentoIdentificacion> TipoDocumentoIdentificacions { get; set; }

    public virtual DbSet<TratamientoMedicoPersona> TratamientoMedicoPersonas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=SQL8005.site4now.net;database=db_aab78e_bdreportes;uid=db_aab78e_bdreportes_admin;pwd=Jose_933530480");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Idmensaje).HasName("PK__chat__4EF3BE44AA253708");

            entity.ToTable("chat");

            entity.Property(e => e.Bhabilitado).HasDefaultValue(1);
            entity.Property(e => e.Nombreusuario).HasMaxLength(255);
        });

        modelBuilder.Entity<Compañium>(entity =>
        {
            entity.HasKey(e => e.Iidcompania);

            entity.Property(e => e.Iidcompania).HasColumnName("IIDCOMPANIA");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHABILITADO");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CIUDAD");
            entity.Property(e => e.Codigopostal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CODIGOPOSTAL");
            entity.Property(e => e.Direccion)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("DIRECCION");
            entity.Property(e => e.Estado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ESTADO");
            entity.Property(e => e.Nombrecompañia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRECOMPAÑIA");
            entity.Property(e => e.Personaafacturar)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PERSONAAFACTURAR");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");
        });

        modelBuilder.Entity<DetalleFactura>(entity =>
        {
            entity.HasKey(e => e.Iiddetallefactura).HasName("PK_DetalleFacturas");

            entity.ToTable("DetalleFactura");

            entity.Property(e => e.Iiddetallefactura).HasColumnName("IIDDETALLEFACTURA");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHABILITADO");
            entity.Property(e => e.Iidfactura).HasColumnName("IIDFACTURA");
            entity.Property(e => e.Iidproducto).HasColumnName("IIDPRODUCTO");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("PRECIO");

            entity.HasOne(d => d.IidfacturaNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.Iidfactura)
                .HasConstraintName("FK_DetalleFacturas_Factura");

            entity.HasOne(d => d.IidproductoNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.Iidproducto)
                .HasConstraintName("FK_DetalleFacturas_Producto");
        });

        modelBuilder.Entity<Dium>(entity =>
        {
            entity.HasKey(e => e.Iiddia);

            entity.Property(e => e.Iiddia).HasColumnName("IIDDIA");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHABILITADO");
            entity.Property(e => e.Nombredia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBREDIA");
        });

        modelBuilder.Entity<EnfermedadesIntervencionesPersona>(entity =>
        {
            entity.HasKey(e => e.Iidenfermedadesintervenciones).HasName("PK_EnfermedadesIntervenciones");

            entity.ToTable("EnfermedadesIntervencionesPersona");

            entity.Property(e => e.Iidenfermedadesintervenciones).HasColumnName("IIDENFERMEDADESINTERVENCIONES");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHABILITADO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Iidpersona).HasColumnName("IIDPERSONA");

            entity.HasOne(d => d.IidpersonaNavigation).WithMany(p => p.EnfermedadesIntervencionesPersonas)
                .HasForeignKey(d => d.Iidpersona)
                .HasConstraintName("FK_EnfermedadesIntervencionesPersona_EnfermedadesIntervencionesPersona");
        });

        modelBuilder.Entity<ExpedienteVacunacion>(entity =>
        {
            entity.HasKey(e => e.Iidexpedientevacunacion);

            entity.ToTable("ExpedienteVacunacion");

            entity.Property(e => e.Iidexpedientevacunacion).HasColumnName("IIDEXPEDIENTEVACUNACION");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHABILITADO");
            entity.Property(e => e.Diabetes).HasColumnName("DIABETES");
            entity.Property(e => e.Edad).HasColumnName("EDAD");
            entity.Property(e => e.Fechavacunacion).HasColumnName("FECHAVACUNACION");
            entity.Property(e => e.Hipertencion).HasColumnName("HIPERTENCION");
            entity.Property(e => e.Iidmarcavacuna).HasColumnName("IIDMARCAVACUNA");
            entity.Property(e => e.Iidpersona).HasColumnName("IIDPERSONA");
            entity.Property(e => e.Lotevacuna).HasColumnName("LOTEVACUNA");
            entity.Property(e => e.Numerodosis).HasColumnName("NUMERODOSIS");
            entity.Property(e => e.Otropadecimiento)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("OTROPADECIMIENTO");

            entity.HasOne(d => d.IidmarcavacunaNavigation).WithMany(p => p.ExpedienteVacunacions)
                .HasForeignKey(d => d.Iidmarcavacuna)
                .HasConstraintName("FK_ExpedienteVacunacion_MarcaVacuna");

            entity.HasOne(d => d.IidpersonaNavigation).WithMany(p => p.ExpedienteVacunacions)
                .HasForeignKey(d => d.Iidpersona)
                .HasConstraintName("FK_ExpedienteVacunacion_Persona");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.Iidfactura).HasName("PK_Facturas");

            entity.ToTable("Factura");

            entity.Property(e => e.Iidfactura).HasColumnName("IIDFACTURA");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHABILITADO");
            entity.Property(e => e.Iidcompania).HasColumnName("IIDCOMPANIA");
            entity.Property(e => e.Impuesto)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("IMPUESTO");
            entity.Property(e => e.Numerofactura).HasColumnName("NUMEROFACTURA");
            entity.Property(e => e.Otro)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("OTRO");
            entity.Property(e => e.Subtotalfactura)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("SUBTOTALFACTURA");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TOTAL");

            entity.HasOne(d => d.IidcompaniaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.Iidcompania)
                .HasConstraintName("FK_Facturas_Compañia");
        });

        modelBuilder.Entity<FichaMedica>(entity =>
        {
            entity.HasKey(e => e.Iidfichamedica);

            entity.ToTable("FichaMedica");

            entity.Property(e => e.Iidfichamedica).HasColumnName("IIDFICHAMEDICA");
            entity.Property(e => e.Descripcionalergia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCIONALERGIA");
            entity.Property(e => e.Enfermedadcronica)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ENFERMEDADCRONICA");
            entity.Property(e => e.Iidgruposanguineo).HasColumnName("IIDGRUPOSANGUINEO");
            entity.Property(e => e.Iidpersona).HasColumnName("IIDPERSONA");
            entity.Property(e => e.Iidsistemasalud).HasColumnName("IIDSISTEMASALUD");
            entity.Property(e => e.Iidtipoalergia).HasColumnName("IIDTIPOALERGIA");
            entity.Property(e => e.Medicoatiende)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MEDICOATIENDE");
            entity.Property(e => e.Nombresistemasalud)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRESISTEMASALUD");

            entity.HasOne(d => d.IidgruposanguineoNavigation).WithMany(p => p.FichaMedicas)
                .HasForeignKey(d => d.Iidgruposanguineo)
                .HasConstraintName("FK_FichaMedica_GrupoSanguineo");

            entity.HasOne(d => d.IidpersonaNavigation).WithMany(p => p.FichaMedicas)
                .HasForeignKey(d => d.Iidpersona)
                .HasConstraintName("FK_FichaMedica_Persona");

            entity.HasOne(d => d.IidsistemasaludNavigation).WithMany(p => p.FichaMedicas)
                .HasForeignKey(d => d.Iidsistemasalud)
                .HasConstraintName("FK_FichaMedica_SistemaSalud");

            entity.HasOne(d => d.IidtipoalergiaNavigation).WithMany(p => p.FichaMedicas)
                .HasForeignKey(d => d.Iidtipoalergia)
                .HasConstraintName("FK_FichaMedica_TipoAlergia");
        });

        modelBuilder.Entity<GrupoSanguineo>(entity =>
        {
            entity.HasKey(e => e.Iidgruposanguineo);

            entity.ToTable("GrupoSanguineo");

            entity.Property(e => e.Iidgruposanguineo).HasColumnName("IIDGRUPOSANGUINEO");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHABILITADO");
            entity.Property(e => e.Nombresanguineo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRESANGUINEO");
        });

        modelBuilder.Entity<Hora>(entity =>
        {
            entity.HasKey(e => e.Iidhora).HasName("PK_Horass");

            entity.ToTable("Hora");

            entity.Property(e => e.Iidhora).HasColumnName("IIDHORA");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHABILITADO");
            entity.Property(e => e.Hora1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("HORA");
        });

        modelBuilder.Entity<HoraDiaActividadPersona>(entity =>
        {
            entity.HasKey(e => e.Iidhoradiaactividadpersona);

            entity.ToTable("HoraDiaActividadPersona");

            entity.Property(e => e.Iidhoradiaactividadpersona).HasColumnName("IIDHORADIAACTIVIDADPERSONA");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHABILITADO");
            entity.Property(e => e.Iiddia).HasColumnName("IIDDIA");
            entity.Property(e => e.Iidhora).HasColumnName("IIDHORA");
            entity.Property(e => e.Iidpersona).HasColumnName("IIDPERSONA");
            entity.Property(e => e.Nombreactividad)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("NOMBREACTIVIDAD");

            entity.HasOne(d => d.IiddiaNavigation).WithMany(p => p.HoraDiaActividadPersonas)
                .HasForeignKey(d => d.Iiddia)
                .HasConstraintName("FK_HoraDiaActividadPersona_Dia");

            entity.HasOne(d => d.IidhoraNavigation).WithMany(p => p.HoraDiaActividadPersonas)
                .HasForeignKey(d => d.Iidhora)
                .HasConstraintName("FK_HoraDiaActividadPersona_Hora");

            entity.HasOne(d => d.IidpersonaNavigation).WithMany(p => p.HoraDiaActividadPersonas)
                .HasForeignKey(d => d.Iidpersona)
                .HasConstraintName("FK_HoraDiaActividadPersona_Persona");
        });

        modelBuilder.Entity<MarcaVacuna>(entity =>
        {
            entity.HasKey(e => e.Iidmarcavacuna);

            entity.ToTable("MarcaVacuna");

            entity.Property(e => e.Iidmarcavacuna).HasColumnName("IIDMARCAVACUNA");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHABILITADO");
            entity.Property(e => e.Nombremarca)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBREMARCA");
        });

        modelBuilder.Entity<MedicamentoConsumoPersona>(entity =>
        {
            entity.HasKey(e => e.Iidmedicamentoconsumopersona);

            entity.ToTable("MedicamentoConsumoPersona");

            entity.Property(e => e.Iidmedicamentoconsumopersona).HasColumnName("IIDMEDICAMENTOCONSUMOPERSONA");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHABILITADO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Iidpersona).HasColumnName("IIDPERSONA");

            entity.HasOne(d => d.IidpersonaNavigation).WithMany(p => p.MedicamentoConsumoPersonas)
                .HasForeignKey(d => d.Iidpersona)
                .HasConstraintName("FK_MedicamentoConsumoPersona_Persona");
        });

        modelBuilder.Entity<MedicoTelefonoPersona>(entity =>
        {
            entity.HasKey(e => e.Iidmedicotelefono);

            entity.ToTable("MedicoTelefonoPersona");

            entity.HasIndex(e => e.Iidmedicotelefono, "IX_MedicoTelefonoPersona");

            entity.Property(e => e.Iidmedicotelefono).HasColumnName("IIDMEDICOTELEFONO");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHABILITADO");
            entity.Property(e => e.Iidpersona).HasColumnName("IIDPERSONA");
            entity.Property(e => e.Numerotelefonicomedico)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("NUMEROTELEFONICOMEDICO");

            entity.HasOne(d => d.IidpersonaNavigation).WithMany(p => p.MedicoTelefonoPersonas)
                .HasForeignKey(d => d.Iidpersona)
                .HasConstraintName("FK_MedicoTelefonoPersona_Persona");
        });

        modelBuilder.Entity<ObservacionesPersona>(entity =>
        {
            entity.HasKey(e => e.Iidobservacionespersona);

            entity.ToTable("ObservacionesPersona");

            entity.Property(e => e.Iidobservacionespersona).HasColumnName("IIDOBSERVACIONESPERSONA");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHABILITADO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Iidpersona).HasColumnName("IIDPERSONA");

            entity.HasOne(d => d.IidpersonaNavigation).WithMany(p => p.ObservacionesPersonas)
                .HasForeignKey(d => d.Iidpersona)
                .HasConstraintName("FK_ObservacionesPersona_Persona");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Iidpersona);

            entity.ToTable("Persona");

            entity.Property(e => e.Iidpersona).HasColumnName("IIDPERSONA");
            entity.Property(e => e.Apmaterno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("APMATERNO");
            entity.Property(e => e.Appaterno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("APPATERNO");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHABILITADO");
            entity.Property(e => e.Calle)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CALLE");
            entity.Property(e => e.Colonia)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("COLONIA");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CORREO");
            entity.Property(e => e.Cp)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("CP");
            entity.Property(e => e.Estadopais)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ESTADOPAIS");
            entity.Property(e => e.Iidsexo).HasColumnName("IIDSEXO");
            entity.Property(e => e.Iidtipodocumento).HasColumnName("IIDTIPODOCUMENTO");
            entity.Property(e => e.Municipiopais)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MUNICIPIOPAIS");
            entity.Property(e => e.Nexterior)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("NEXTERIOR");
            entity.Property(e => e.Ninterior)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("NINTERIOR");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Nombrefoto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBREFOTO");
            entity.Property(e => e.Numeroidentificacion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NUMEROIDENTIFICACION");
            entity.Property(e => e.Numeroregistrounicocontribuyente)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("NUMEROREGISTROUNICOCONTRIBUYENTE");
            entity.Property(e => e.Telefonoocelular1)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("TELEFONOOCELULAR1");
            entity.Property(e => e.Telefonoocelular2)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("TELEFONOOCELULAR2");

            entity.HasOne(d => d.IidsexoNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.Iidsexo)
                .HasConstraintName("FK_Persona_Sexo1");

            entity.HasOne(d => d.IidtipodocumentoNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.Iidtipodocumento)
                .HasConstraintName("FK_Persona_Sexo");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Iidproducto);

            entity.ToTable("Producto");

            entity.Property(e => e.Iidproducto).HasColumnName("IIDPRODUCTO");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHABILITADO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Foto).HasColumnName("FOTO");
            entity.Property(e => e.Nombrefoto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBREFOTO");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("PRECIO");
            entity.Property(e => e.Stock).HasColumnName("stock");
        });

        modelBuilder.Entity<Registro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Registro__3214EC078B7522B3");

            entity.ToTable("Registro");

            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.NumeroDocumento).HasMaxLength(50);
        });

        modelBuilder.Entity<Sexo>(entity =>
        {
            entity.HasKey(e => e.Iidsexo);

            entity.ToTable("Sexo");

            entity.Property(e => e.Iidsexo).HasColumnName("IIDSEXO");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHABILITADO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<SistemaSalud>(entity =>
        {
            entity.HasKey(e => e.Iidsistemasalud);

            entity.ToTable("SistemaSalud");

            entity.Property(e => e.Iidsistemasalud).HasColumnName("IIDSISTEMASALUD");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHABILITADO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<TipoAlergium>(entity =>
        {
            entity.HasKey(e => e.Iidtipoalergia);

            entity.Property(e => e.Iidtipoalergia).HasColumnName("IIDTIPOALERGIA");
            entity.Property(e => e.Bhabilitado)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("BHABILITADO");
            entity.Property(e => e.Nombretipoalergia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRETIPOALERGIA");
        });

        modelBuilder.Entity<TipoDocumentoIdentificacion>(entity =>
        {
            entity.HasKey(e => e.Iidtipodocumento);

            entity.ToTable("TipoDocumentoIdentificacion");

            entity.Property(e => e.Iidtipodocumento).HasColumnName("IIDTIPODOCUMENTO");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHABILITADO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<TratamientoMedicoPersona>(entity =>
        {
            entity.HasKey(e => e.Iidtratamientomedicopersona);

            entity.ToTable("TratamientoMedicoPersona");

            entity.Property(e => e.Iidtratamientomedicopersona).HasColumnName("IIDTRATAMIENTOMEDICOPERSONA");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHABILITADO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Iidpersona).HasColumnName("IIDPERSONA");

            entity.HasOne(d => d.IidpersonaNavigation).WithMany(p => p.TratamientoMedicoPersonas)
                .HasForeignKey(d => d.Iidpersona)
                .HasConstraintName("FK_TratamientoMedicoPersona_Persona");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
