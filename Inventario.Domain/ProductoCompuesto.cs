using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Inventario.Domain
{
    public class ProductoCompuesto : Producto
    {

        public List<Producto> Ingredientes { get; private set; }
        public List<Producto> ProductosEnInventario { get;  set; }

        public ProductoCompuesto(string codigo, string nombre, int cantidad, decimal precio) : base(codigo, nombre, cantidad, precio)
        {
        }

     

        public ProductoCompuesto(string codigo, string nombre, int cantidad,  decimal precio, List<Producto> ingredientes, List<Producto> productosEnInventario) : base(codigo, nombre, cantidad,  precio)
        {
            Ingredientes = ingredientes;
            ProductosEnInventario = productosEnInventario;
            Costo = CalcularCosto();
        }

        public List<ProductoSimple> Descomponer()
        {
            List<ProductoSimple> pSimple = new List<ProductoSimple>();
            foreach (var item in Ingredientes)
            {
                if (item.GetType() == typeof(ProductoCompuesto))
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
            foreach (var item in Ingredientes)
            {
                if (item.GetType().Equals(typeof(ProductoCompuesto)))
                {
                    var newItem = (ProductoCompuesto)item;
                    foreach (var p in newItem.Descomponer())
                    {
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
             if (cantidad <= 0)
                 return "Error: La cantidad de la entrada de debe ser mayor a 0";
             Cantidad = Cantidad + cantidad;
            return $"La nueva cantidad del producto {Nombre} es {Cantidad}";
        }

        public override string RegistrarSalidaProducto(int cantidad)
        {
            foreach (var item in ProductosEnInventario)
            {
                foreach (var ingrediente in Ingredientes)
                {
                    if (item.Codigo.Equals(ingrediente.Codigo))
                    {
                        var mensaje=item.RegistrarSalidaProducto(ingrediente.Cantidad*cantidad);
                        if (mensaje.Contains("Error"))
                            return mensaje;
                    }
                }
            }
            return $"Salida registrada {Nombre}, cantidad {cantidad} precio {Precio*cantidad}";
        }
    }
}
