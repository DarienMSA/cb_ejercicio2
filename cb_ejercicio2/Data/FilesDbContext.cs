using System;
using System.Collections.Generic;
using cb_ejercicio2.Models;
using Microsoft.EntityFrameworkCore;

namespace cb_ejercicio2.Data;

public partial class FilesDbContext : DbContext
{
    public FilesDbContext()
    {
    }

    public FilesDbContext(DbContextOptions<FilesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CbtProcesodeanalisi> CbtProcesodeanalises { get; set; }

    public virtual DbSet<CbtProcesodeanalisisCarga> CbtProcesodeanalisisCargas { get; set; }

    public virtual DbSet<CbtProcesodecarga> CbtProcesodecargas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=FilesDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CbtProcesodeanalisi>(entity =>
        {
            entity.HasKey(e => e.ProcesoDeAnalisisId).HasName("PK__cbt_proc__0EED5057ED8C4289");

            entity.ToTable("cbt_procesodeanalisis");

            entity.Property(e => e.Comentarios)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CbtProcesodeanalisisCarga>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("cbt_procesodeanalisis_carga");

            entity.Property(e => e.ProcesoId).HasColumnName("ProcesoID");

            entity.HasOne(d => d.ProcesoDeAnalisis).WithMany()
                .HasForeignKey(d => d.ProcesoDeAnalisisId)
                .HasConstraintName("FK_procesodeanalisis");

            entity.HasOne(d => d.Proceso).WithMany()
                .HasForeignKey(d => d.ProcesoId)
                .HasConstraintName("FK_procesodecarga");
        });

        modelBuilder.Entity<CbtProcesodecarga>(entity =>
        {
            entity.HasKey(e => e.ProcesoId).HasName("PK__cbt_proc__1C00FFF0C1B69932");

            entity.ToTable("cbt_procesodecarga");

            entity.Property(e => e.ProcesoId).HasColumnName("ProcesoID");
            entity.Property(e => e.NombreArchivoRespaldo)
                .HasMaxLength(300)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
