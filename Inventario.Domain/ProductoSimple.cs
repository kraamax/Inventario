using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Inventario.Domain
{
    public class ProductoSimple : Producto
    {
        public ProductoSimple(string codigo, string nombre, int cantidad, decimal costo, decimal precio) : base(codigo, nombre, cantidad, precio)
        {
            Costo = costo;
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
            if (cantidad <= 0)
            {
                return "La cantidad de la salida de debe ser mayor a 0";
            }
            Cantidad = Cantidad - cantidad;
            return $"La nueva cantidad del producto {Nombre} es {Cantidad}";
        }
    }
}
