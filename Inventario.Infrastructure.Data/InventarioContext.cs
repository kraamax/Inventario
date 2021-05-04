using Inventario.Domain;
using Inventario.Infrastructure.Data.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario.Infrastructure.Data
{
    public class InventarioContext: DbContextBase
    {
        
        public InventarioContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoSimple> ProductosSimples { get; set; }
        public DbSet<ProductoCompuesto> ProductosCompuestos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Producto>().HasKey(c => c.Id);
            modelBuilder.Entity<ProductoCompuesto>().Ignore(c=>c.ProductosEnInventario);
            
        }
    }
}
