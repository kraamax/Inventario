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

        public DbSet<ProductoSimple> ProductoSimples { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductoSimple>().HasKey(c => c.Id);
        }
    }
}
