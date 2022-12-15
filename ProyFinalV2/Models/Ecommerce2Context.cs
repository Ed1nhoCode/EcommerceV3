using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProyFinalV2.Models
{
    public partial class Ecommerce2Context : DbContext
    {
        public Ecommerce2Context()
        {
        }

        public Ecommerce2Context(DbContextOptions<Ecommerce2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
        public virtual DbSet<OrdenXproducto> OrdenXproductos { get; set; } = null!;
        public virtual DbSet<Ordene> Ordenes { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;


        // este metodo nos permite interactuar con la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.Property(e => e.Descripcion).HasMaxLength(150);

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<OrdenXproducto>(entity =>
            {
                entity.ToTable("OrdenXProductos");

                entity.Property(e => e.OrdenId).HasColumnName("OrdenID");

                entity.Property(e => e.ProductoId).HasColumnName("ProductoID");

                entity.HasOne(d => d.Orden)
                    .WithMany(p => p.OrdenXproductos)
                    .HasForeignKey(d => d.OrdenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdenXProductos_Ordenes");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.OrdenXproductos)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdenXProductos_Productos");
            });

            modelBuilder.Entity<Ordene>(entity =>
            {
                entity.Property(e => e.DireccionEnvio).HasMaxLength(250);

                entity.Property(e => e.FechaOrden).HasColumnType("date");

                entity.Property(e => e.StatusOrden).HasMaxLength(50);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Ordenes)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ordenes_Usuarios");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(e => e.Descripcion).HasMaxLength(150);

                entity.Property(e => e.Imagen).HasMaxLength(250);

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Productos_Categorias");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Correo).HasMaxLength(100);

                entity.Property(e => e.Direccion).HasMaxLength(150);

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Telefono).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
