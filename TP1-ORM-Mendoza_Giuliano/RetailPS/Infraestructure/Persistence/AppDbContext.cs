using Domain.Entities;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Caregory> Caregory { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Sale> Sale {  get; set; }
        public DbSet<SaleProduct> SaleProduct { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=Ventas;Trusted_Connection=True;TrustServerCertificate=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sale");   
                entity.HasKey(s => s.SaleId);
                entity.Property(s => s.TotalPay).IsRequired();
                entity.Property(s => s.Subtotal).IsRequired();
                entity.Property(s => s.TotalDiscount).IsRequired();
                entity.Property(s => s.Taxes).IsRequired();
                entity.Property(s => s.Date).IsRequired();
                //Relación con SaleProduct
                entity.HasMany<SaleProduct>(s => s.SaleProducts).WithOne(sp => sp.Venta).HasForeignKey(sp => sp.Sale).IsRequired();
            });

            modelBuilder.Entity<Caregory>(entity =>
            {
                entity.ToTable("Caregory");
                entity.HasKey(c => c.CategoryId);
                entity.Property(c => c.Name).HasMaxLength(100).IsRequired();
                //Relación con Product
                entity.HasMany<Product>(c => c.Products).WithOne(p => p.Categoria).HasForeignKey(p => p.Category).IsRequired();
            });
            modelBuilder.ApplyConfiguration(new CategoryData());

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(p => p.ProductId);
                entity.Property(p => p.Name).HasMaxLength(100).IsRequired();
                entity.Property(p => p.Description);
                entity.Property(p => p.Price).HasConversion<decimal>().IsRequired();
                entity.Property(p => p.Discount);
                entity.Property(p => p.ImageUrl);
                //Relación con Categoria
                entity.HasOne(p => p.Categoria).WithMany(c => c.Products).HasForeignKey(p => p.Category).IsRequired();
                //Relación con Saleproduct
                entity.HasMany<SaleProduct>(p => p.SaleProducts).WithOne(sp => sp.Producto).HasForeignKey(sp => sp.Product).IsRequired();
            });
            modelBuilder.ApplyConfiguration(new ProductData());

            modelBuilder.Entity<SaleProduct>(entity =>
            {
                entity.ToTable("SaleProduct");
                entity.HasKey(sp => sp.ShoppingCartId);
                entity.Property(sp => sp.Quantity).IsRequired();
                entity.Property(sp => sp.Price).IsRequired();
                entity.Property(sp => sp.Discount);
            }); 
        }
    }
}
