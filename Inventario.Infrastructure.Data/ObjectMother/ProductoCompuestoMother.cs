using System.Collections.Generic;
using Inventario.Domain;

namespace Inventario.Infrastructure.Data.ObjectMother
{
    public static class ProductoCompuestoMother
    {
        public static ProductoCompuesto CrearProducto()
        {
            var ingredientes = new List<Producto>();
            var productosEnInventario = new List<Producto>();
            var tomateInv = new ProductoSimple("123a", "tomate", 5, 1300,1500 );
            var lechugaInv = new ProductoSimple("123b", "lechuga", 5, 1000, 1500);
            //var gaseosaInv = new ProductoSimple("123c", "Gaseosa", 5, 1000, 1500);
            var tomate = new ProductoSimple("123a", "tomate", 1, 1300,1500 );
            var lechuga = new ProductoSimple("123b", "lechuga", 1, 1000, 1500);
            //var gaseosa = new ProductoSimple("123c", "Gaseosa", 1, 1000, 1500);
            productosEnInventario.Add(tomateInv);
            productosEnInventario.Add(lechugaInv);
            ingredientes.Add(tomate);
            ingredientes.Add(lechuga);
            var ensalada = new ProductoCompuesto("123d", "Ensalada", 1,4000 , ingredientes,productosEnInventario);
            return ensalada;
        }
        
    }
}