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
                return "Error: La cantidad de la entrada de debe ser mayor a 0";
            }
            Cantidad = Cantidad + cantidad;
            return $"La nueva cantidad del producto {Nombre} es {Cantidad}";
        }

        public override string RegistrarSalidaProducto(int cantidad)
        {
            if (cantidad <= 0)
            {
                return "Error: La cantidad de la salida de debe ser mayor a 0";
            }

            if (cantidad > Cantidad)
                return $"Error {Nombre}: La cantidad de la salida no puede ser mayor a la disponible";
            Cantidad = Cantidad - cantidad;
            return $"La nueva cantidad del producto {Nombre} es {Cantidad}";
        }
    }
}
