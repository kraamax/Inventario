using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Inventario.Domain
{
    public class ProductoCompuesto : Producto
    {

        public List<Producto> Productos { get; private set; }

        public ProductoCompuesto(string codigo, string nombre, int cantidad, decimal precio) : base(codigo, nombre, cantidad, precio)
        {
        }
        public ProductoCompuesto(string codigo, string nombre, int cantidad,  decimal precio, List<Producto> productos) : base(codigo, nombre, cantidad,  precio)
        {
            Productos = productos;
            Costo = CalcularCosto();
        }

        public List<ProductoSimple> Descomponer()
        {
            List<ProductoSimple> pSimple = new List<ProductoSimple>();
            foreach (var item in Productos)
            {
                if (item.GetType().Equals(typeof(ProductoCompuesto)))
                {
                    var newItem = (ProductoCompuesto)item;
                    foreach (var p in newItem.Descomponer())
                    {
                        pSimple.Add(p);
                    }
                }
                else
                {
                        pSimple.Add((ProductoSimple)item);
                }
            }
            return pSimple;
        }
        public decimal CalcularCosto()
        {
            decimal costo = 0;
            foreach (var item in Productos)
            {
                if (item.GetType().Equals(typeof(ProductoCompuesto)))
                {
                    var newItem = (ProductoCompuesto)item;
                    foreach (var p in newItem.Descomponer())
                    {
                        Console.WriteLine(p.Costo);
                        costo = costo +(p.Costo*p.Cantidad);
                    }
                    Console.WriteLine(Costo);
                }
                else
                {
                    Console.WriteLine(item.Costo);
                    costo = costo + (item.Costo * item.Cantidad); 
                }
            }
            return costo;
        }

        public override string RegistrarEntradaProducto(int cantidad)
        {
             if (cantidad <= 0) {
                return "La cantidad de la entrada de debe ser mayor a 0";
            }
            Cantidad = Cantidad + cantidad;
            return $"La nueva cantidad del producto {Nombre} es {Cantidad}";
        }

        public override string RegistrarSalidaProducto(int cantidad)
        {
            foreach (var item in Productos)
            {
                    item.RegistrarSalidaProducto(cantidad);
            }
            return $"Salida registrada {Nombre}, cantidad {cantidad} precio {Precio*cantidad}";
        }
    }
}
