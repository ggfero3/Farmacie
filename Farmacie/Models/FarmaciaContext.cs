using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Farmacie.Models;

public partial class FarmaciaContext : DbContext
{
    public FarmaciaContext()
    {
    }

    public FarmaciaContext(DbContextOptions<FarmaciaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comune> Comunes { get; set; }

    public virtual DbSet<Farmacia> Farmacies { get; set; }

    public virtual DbSet<Frazione> Fraziones { get; set; }

    public virtual DbSet<Provincia> Provincia { get; set; }

    public virtual DbSet<Regione> Regiones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Farmacia;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comune>(entity =>
        {
            entity.ToTable("Comune");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Comunes)
                .HasForeignKey(d => d.IdProvincia)
                .HasConstraintName("FK_Comune_Provincia");
        });

        modelBuilder.Entity<Farmacia>(entity =>
        {
            entity.ToTable("Farmacie");

            entity.Property(e => e.Cap)
                .IsUnicode(false)
                .HasColumnName("CAP");
            entity.Property(e => e.Codfarmaciaassegnatodaasl)
                .IsUnicode(false)
                .HasColumnName("CODFARMACIAASSEGNATODAASL");
            entity.Property(e => e.Codiceidentificativofarmacia)
                .IsUnicode(false)
                .HasColumnName("CODICEIDENTIFICATIVOFARMACIA");
            entity.Property(e => e.Codicetipologia)
                .IsUnicode(false)
                .HasColumnName("CODICETIPOLOGIA");
            entity.Property(e => e.Datafinevalidita)
                .IsUnicode(false)
                .HasColumnName("DATAFINEVALIDITA");
            entity.Property(e => e.Datainiziovalidita)
                .IsUnicode(false)
                .HasColumnName("DATAINIZIOVALIDITA");
            entity.Property(e => e.Descrizionefarmacia)
                .IsUnicode(false)
                .HasColumnName("DESCRIZIONEFARMACIA");
            entity.Property(e => e.Descrizionetipologia)
                .IsUnicode(false)
                .HasColumnName("DESCRIZIONETIPOLOGIA");
            entity.Property(e => e.Indirizzo)
                .IsUnicode(false)
                .HasColumnName("INDIRIZZO");
            entity.Property(e => e.Latitudine)
                .IsUnicode(false)
                .HasColumnName("LATITUDINE");
            entity.Property(e => e.Localize)
                .IsUnicode(false)
                .HasColumnName("LOCALIZE");
            entity.Property(e => e.Longitudine)
                .IsUnicode(false)
                .HasColumnName("LONGITUDINE");
            entity.Property(e => e.Partitaiva)
                .IsUnicode(false)
                .HasColumnName("PARTITAIVA");

            entity.HasOne(d => d.IdComuneNavigation).WithMany(p => p.Farmacies)
                .HasForeignKey(d => d.IdComune)
                .HasConstraintName("FK_Farmacie_Comune");
        });

        modelBuilder.Entity<Frazione>(entity =>
        {
            entity.ToTable("Frazione");

            entity.HasOne(d => d.IdComuneNavigation).WithMany(p => p.Fraziones)
                .HasForeignKey(d => d.IdComune)
                .HasConstraintName("FK_Frazione_Comune");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Sigla)
                .HasMaxLength(2)
                .IsFixedLength();

            entity.HasOne(d => d.IdRegioneNavigation).WithMany(p => p.Provincia)
                .HasForeignKey(d => d.IdRegione)
                .HasConstraintName("FK_Provincia_Regione");
        });

        modelBuilder.Entity<Regione>(entity =>
        {
            entity.ToTable("Regione");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Denominazione).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
