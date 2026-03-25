using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace SistemaStock.Models;

public partial class SistemaStockContext : DbContext
{
    public SistemaStockContext()
    {
    }

    public SistemaStockContext(DbContextOptions<SistemaStockContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Producto> Productos { get; set; }
    public virtual DbSet<Categoria> Categorias { get; set; }
    public virtual DbSet<Usuarios> Usuarios { get; set; }
    public virtual DbSet<Rol> Roles { get; set; }
    public virtual DbSet<Movimiento> Movimientos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=SistemaStock;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__Producto__A430AEA393E88D99");
            entity.HasIndex(e => e.Codigo, "UQ__Producto__06370DAC4D1B1FF2").IsUnique();
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StockMinimo).HasDefaultValue(5);

            entity.HasOne(p => p.Categoria)
                  .WithMany(c => c.Productos)
                  .HasForeignKey(p => p.CategoriaId)
                  .HasConstraintName("FK_Productos_Categorias")
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.MovimientoId);
            entity.Property(e => e.Tipo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Observacion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(m => m.Producto)
                  .WithMany()
                  .HasForeignKey(m => m.ProductoId)
                  .HasConstraintName("FK_Movimientos_Productos")
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(m => m.Usuario)
                  .WithMany()
                  .HasForeignKey(m => m.UsuarioId)
                  .HasConstraintName("FK_Movimientos_Usuarios")
                  .OnDelete(DeleteBehavior.Restrict);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}