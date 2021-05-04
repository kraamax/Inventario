using System;
using Inventario.Domain.Base;

namespace Inventario.Domain
{
    public abstract class Producto:Entity<int>, IAggregateRoot
    {
        protected Producto(string codigo, string nombre, int cantidad, decimal precio)
        {
            Codigo = codigo;
            Nombre = nombre;
            Cantidad = cantidad;
            Precio = precio;
        }

        public string Codigo { get; set; }
        public string Nombre { get; private set; }
        public int Cantidad { get; protected set; }
        public decimal Costo { get; protected set; }
        public decimal Precio { get; protected set; }
        public int? ProductoCompuestoId { get; set; }
        public abstract string RegistrarEntradaProducto(int cantidad);
        public abstract string RegistrarSalidaProducto(int cantidad);



    }
}
