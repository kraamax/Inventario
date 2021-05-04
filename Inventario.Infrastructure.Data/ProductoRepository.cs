using System;
using System.Collections.Generic;
using System.Linq;
using Inventario.Domain;
using Inventario.Domain.Contracts;
using Inventario.Infrastructure.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Infrastructure.Data
{
    public class ProductoRepository:GenericRepository<Producto>,IProductoRepository
    {
        public ProductoRepository(IDbContext context) : base(context)
        {
        }

        public List<ProductoSimple> ConsultarProductoSimple()
        {

            var productos = _db.Set<ProductoSimple>().Where(p=>p.ProductoCompuestoId==null).ToList();
           // return productos.Where(x => x.ProductoCompuestoId == 0).ToList();
           return productos;
        }

        public List<ProductoCompuesto> consultarProductoCompuestos()
        {
            var productos = _db.Set<ProductoCompuesto>()
                .Include(c=>c.Ingredientes)
                .ToList();
            return productos.Where(x => x.ProductoCompuestoId == null).ToList();
        }

        public ProductoCompuesto consultarProductoCompuesto(string codigo)
        {
            var producto = _db.Set<ProductoCompuesto>()
                .Include(c=>c.Ingredientes)
                .FirstOrDefault(x=>x.Codigo==codigo);
            return producto;
        }
    }
}