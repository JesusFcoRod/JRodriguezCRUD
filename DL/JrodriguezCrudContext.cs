using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class JrodriguezCrudContext : DbContext
{
    public JrodriguezCrudContext()
    {
    }

    public JrodriguezCrudContext(DbContextOptions<JrodriguezCrudContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Especialidad> Especialidads { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Triangulo> Triangulos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= JRodriguezCRUD; User ID=sa; TrustServerCertificate=True; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.IdDoctor).HasName("PK__Doctor__F838DB3E98BB8DE4");

            entity.ToTable("Doctor");

            entity.Property(e => e.Am)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("AM");
            entity.Property(e => e.Ap)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("AP");
            entity.Property(e => e.Nombre)
                .HasMaxLength(35)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEspecialidadNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.IdEspecialidad)
                .HasConstraintName("FK__Doctor__IdEspeci__1273C1CD");
        });

        modelBuilder.Entity<Especialidad>(entity =>
        {
            entity.HasKey(e => e.IdEspecialidad).HasName("PK__Especial__693FA0AF8AA4F70D");

            entity.ToTable("Especialidad");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PK__Paciente__C93DB49B0856F452");

            entity.ToTable("Paciente");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(35)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(35)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(35)
                .IsUnicode(false);
            entity.Property(e => e.Nss)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("NSS");
            entity.Property(e => e.Padecimiento)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.IdDoctor)
                .HasConstraintName("FK__Paciente__IdDoct__1B0907CE");
        });

        modelBuilder.Entity<Triangulo>(entity =>
        {
            entity.HasKey(e => e.IdTriangulo).HasName("PK__Triangul__3ED215AC3EF9C1D7");

            entity.ToTable("Triangulo");

            entity.Property(e => e.Area).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Lado).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Perimetro).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Tipo)
                .HasMaxLength(35)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
