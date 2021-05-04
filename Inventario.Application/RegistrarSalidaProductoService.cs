using System;
using System.Collections.Generic;
using System.Linq;
using Inventario.Domain;
using Inventario.Domain.Contracts;

namespace Inventario.Application
{
    public class RegistrarSalidaProductoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductoRepository _productoRepository;

        public RegistrarSalidaProductoService(
            IUnitOfWork unitOfWork,
            IProductoRepository productoRepository
        )
        {
            _unitOfWork = unitOfWork;
            _productoRepository = productoRepository;

        }

        public string Handle(SalidaProductoRequest request)
        {
            var producto =_productoRepository.FindFirstOrDefault(p => p.Codigo == request.Codigo);
            if (producto == null)
                return "No existe el producto";
            if (request.Tipo=="Compuesto")
            {
                
                return RegistrarSalidaProductoCompuesto(request, producto);
            }
            
            var response = "";
            response = producto.RegistrarSalidaProducto(request.Cantidad);
            try
            {
                _productoRepository.Update(producto);
            }
            catch (Exception e)
            {
                return "no se pudo guardar";
            }
            _unitOfWork.Commit();
            return response;
        }

        private string RegistrarSalidaProductoCompuesto(SalidaProductoRequest request, Producto producto)
        {
            ProductoCompuesto productoCompuesto;
            try
            {
                productoCompuesto = (ProductoCompuesto) producto;
            }
            catch (Exception e)
            {
                return "No es un producto Compuesto";
            }
            var productosEnInventario = new List<Producto>();
            foreach (var p in productoCompuesto.Ingredientes)
            {
                var auxProductos = _productoRepository.FindBy(c => c.Codigo == p.Codigo);
                foreach (var s in auxProductos)
                {
                    Console.WriteLine($"nombre {s.Nombre} foregn {s.ProductoCompuestoId}");
                }

                Producto auxProducto;
                if (p.GetType() == typeof(ProductoCompuesto))
                {
                    auxProducto = auxProductos.FirstOrDefault();
                }
                else
                {
                    auxProducto = auxProductos.FirstOrDefault(x => x.ProductoCompuestoId == null);

                }
                if (auxProducto == null)
                    return "No se encontro el producto";
                productosEnInventario.Add(auxProducto);
            }
            productoCompuesto.ProductosEnInventario = productosEnInventario;
            var response=productoCompuesto.RegistrarSalidaProducto(request.Cantidad);
            if (response.Contains("Error"))
                return response;
            try
            {
                foreach (var p in productoCompuesto.ProductosEnInventario)
                {
                    _productoRepository.Update(p);
                }
            }
            catch (Exception e)
            {
                return "no se puede actualizar";
            }

            _unitOfWork.Commit();
            return $"se registro la salida de {productoCompuesto.Cantidad} {productoCompuesto.Nombre}";
        }
    }

    public record SalidaProductoRequest(string Tipo,string Codigo, int Cantidad);
}