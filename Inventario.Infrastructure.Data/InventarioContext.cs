using Inventario.Domain;
using Inventario.Infrastructure.Data.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Inventario.Domain;

namespace Inventario.Infrastructure.Data
{
    public class BancoContext: DbContextBase
    {
        
        public BancoContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ProductoSimple> ProductoSimples { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductoSimple>().HasKey(c => c.Id);
        }
    }
}
