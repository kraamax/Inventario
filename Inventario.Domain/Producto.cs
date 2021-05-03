using System;

namespace Inventario.Domain
{
    public abstract class Producto
    {
        public Producto(string id, int cantidad, string nombre, decimal precio)
        {
            Id = id;
            Cantidad = cantidad;
            Nombre = nombre;
            Precio = precio;
        }
        public string Id { get; set; }
       
        public int Cantidad { get; protected set; }
        public string Nombre { get; private set; }
        public decimal Costo { get; protected set; }
        public decimal Precio { get; protected set; }
        public abstract string RegistrarEntradaProducto(int cantidad);
        public abstract string RegistrarSalidaProducto(int cantidad);



    }
}
