using System.Collections.Generic;
using Inventario.Domain.Base;

namespace Inventario.Domain.Contracts
{
    public interface IProductoRepository : IGenericRepository<Producto>
    {
        List<ProductoSimple> ConsultarProductoSimple();
        List<ProductoCompuesto> consultarProductoCompuestos();
        ProductoCompuesto consultarProductoCompuesto(string codigo);
    }
}