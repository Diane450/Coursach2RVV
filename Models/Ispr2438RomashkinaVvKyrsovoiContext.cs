using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace kursachRVV.Models;

public partial class Ispr2438RomashkinaVvKyrsovoiContext : DbContext
{
    public Ispr2438RomashkinaVvKyrsovoiContext()
    {
    }

    public Ispr2438RomashkinaVvKyrsovoiContext(DbContextOptions<Ispr2438RomashkinaVvKyrsovoiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dolznosti> Dolznostis { get; set; }

    public virtual DbSet<Ispolnitel> Ispolnitels { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Srochnost> Srochnosts { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<TexnicheskiOtdel> TexnicheskiOtdels { get; set; }

    public virtual DbSet<Vhod> Vhods { get; set; }

    public virtual DbSet<Zayavki> Zayavkis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=cfif31.ru;database=ISPr24-38_RomashkinaVV_kyrsovoi;uid=ISPr24-38_RomashkinaVV;pwd=ISPr24-38_RomashkinaVV", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Dolznosti>(entity =>
        {
            entity.HasKey(e => e.IdDolznosti).HasName("PRIMARY");

            entity.ToTable("dolznosti");

            entity.Property(e => e.IdDolznosti).HasColumnName("id_dolznosti");
            entity.Property(e => e.Dolznost)
                .HasMaxLength(100)
                .HasColumnName("dolznost");
        });

        modelBuilder.Entity<Ispolnitel>(entity =>
        {
            entity.HasKey(e => e.IdIspolnitel).HasName("PRIMARY");

            entity.ToTable("ispolnitel");

            entity.HasIndex(e => e.TexOtSotrydnik, "FK_ispolnitel_texOtd_idx");

            entity.Property(e => e.IdIspolnitel)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_ispolnitel");
            entity.Property(e => e.TexOtSotrydnik).HasColumnName("Tex_ot_sotrydnik");

            entity.HasOne(d => d.IdIspolnitelNavigation).WithOne(p => p.IspolnitelIdIspolnitelNavigation)
                .HasForeignKey<Ispolnitel>(d => d.IdIspolnitel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("zxcb");

            entity.HasOne(d => d.TexOtSotrydnikNavigation).WithMany(p => p.IspolnitelTexOtSotrydnikNavigations)
                .HasForeignKey(d => d.TexOtSotrydnik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ispolnitel_texOtd");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Roli)
                .HasMaxLength(45)
                .HasColumnName("roli");
        });

        modelBuilder.Entity<Srochnost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("srochnost");

            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatys).HasName("PRIMARY");

            entity.ToTable("status");

            entity.Property(e => e.IdStatys).HasColumnName("id_statys");
            entity.Property(e => e.Statys)
                .HasMaxLength(70)
                .HasColumnName("statys");
        });

        modelBuilder.Entity<TexnicheskiOtdel>(entity =>
        {
            entity.HasKey(e => e.IdTexnicheskiOtdel).HasName("PRIMARY");

            entity.ToTable("texnicheski_otdel");

            entity.HasIndex(e => e.Doljnost, "ghj_idx");

            entity.Property(e => e.IdTexnicheskiOtdel).HasColumnName("id_texnicheski_otdel");
            entity.Property(e => e.DataRozden).HasColumnName("data_rozden");
            entity.Property(e => e.Doljnost).HasColumnName("doljnost");
            entity.Property(e => e.Familia)
                .HasMaxLength(45)
                .HasColumnName("familia");
            entity.Property(e => e.FamilyStatys)
                .HasMaxLength(45)
                .HasColumnName("family_statys");
            entity.Property(e => e.Gender)
                .HasMaxLength(30)
                .HasColumnName("gender");
            entity.Property(e => e.IdIspolnitel).HasColumnName("id_ispolnitel");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.Otchestvo)
                .HasMaxLength(45)
                .HasColumnName("otchestvo");
            entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");

            entity.HasOne(d => d.DoljnostNavigation).WithMany(p => p.TexnicheskiOtdels)
                .HasForeignKey(d => d.Doljnost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ewrt");
        });

        modelBuilder.Entity<Vhod>(entity =>
        {
            entity.HasKey(e => e.IdVhod).HasName("PRIMARY");

            entity.ToTable("Vhod");

            entity.HasIndex(e => e.TexOt, "qwefga_idx");

            entity.HasIndex(e => e.Rol, "qwert_idx");

            entity.Property(e => e.IdVhod).HasColumnName("idVhod");
            entity.Property(e => e.Login).HasMaxLength(45);
            entity.Property(e => e.Password).HasMaxLength(45);
            entity.Property(e => e.TexOt).HasColumnName("Tex_ot");

            entity.HasOne(d => d.RolNavigation).WithMany(p => p.Vhods)
                .HasForeignKey(d => d.Rol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("qwert");

            entity.HasOne(d => d.TexOtNavigation).WithMany(p => p.Vhods)
                .HasForeignKey(d => d.TexOt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("erty");
        });

        modelBuilder.Entity<Zayavki>(entity =>
        {
            entity.HasKey(e => e.IdZayavki).HasName("PRIMARY");

            entity.ToTable("zayavki");

            entity.HasIndex(e => e.Srochnost, "FK_zayavki_srochnostId_idx");

            entity.HasIndex(e => e.Ispolnitel, "asdfhg_idx");

            entity.HasIndex(e => e.Stastus, "dafh_idx");

            entity.Property(e => e.IdZayavki).HasColumnName("id_zayavki");
            entity.Property(e => e.DateAndTime).HasColumnName("date_and_time");
            entity.Property(e => e.Opisanie)
                .HasMaxLength(200)
                .HasColumnName("opisanie");
            entity.Property(e => e.Raspolozenie)
                .HasMaxLength(200)
                .HasColumnName("raspolozenie");
            entity.Property(e => e.Srochnost).HasColumnName("srochnost");
            entity.Property(e => e.Stastus).HasColumnName("stastus");

            entity.HasOne(d => d.IspolnitelNavigation).WithMany(p => p.Zayavkis)
                .HasForeignKey(d => d.Ispolnitel)
                .HasConstraintName("asdfhg");

            entity.HasOne(d => d.SrochnostNavigation).WithMany(p => p.Zayavkis)
                .HasForeignKey(d => d.Srochnost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_zayavki_srochostId");

            entity.HasOne(d => d.StastusNavigation).WithMany(p => p.Zayavkis)
                .HasForeignKey(d => d.Stastus)
                .HasConstraintName("ghjk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
