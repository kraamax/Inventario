using System;
using System.Collections.Generic;
using Inventario.Domain;
using Inventario.Domain.Contracts;

namespace Inventario.Application
{
    public class RegistrarEntradaProductoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductoRepository _productoRepository;

        public RegistrarEntradaProductoService(
            IUnitOfWork unitOfWork,
            IProductoRepository productoRepository
        )
        {
            _unitOfWork = unitOfWork;
            _productoRepository = productoRepository;

        }

        public string Handle(EntradaProductoRequest request)
        {
            var producto =_productoRepository.FindFirstOrDefault(p => p.Codigo == request.Codigo);
            var response = "";
            if (producto != null)
                return producto.RegistrarEntradaProducto(request.Cantidad);
            producto = TipoProducto.CrearProducto(request);
            try
            {
                _productoRepository.Add(producto);
            }
            catch (Exception e)
            {
                return "no se pudo guardar";
            }
            _unitOfWork.Commit();
            return $"Se registro la entrada del producto {producto.Nombre} con una cantidad de {producto.Cantidad}";
        }
    }

    public record EntradaProductoRequest(List<Producto> Productos,string Codigo, string Nombre, int Cantidad, decimal Costo, decimal Precio);

    public class TipoProducto
    {
        public static Producto CrearProducto(EntradaProductoRequest request)
        {
            if (request.Productos == null)
                return new ProductoSimple(request.Codigo,request.Nombre,request.Cantidad,request.Costo,request.Precio);
            return new ProductoCompuesto(request.Codigo, request.Nombre, request.Cantidad, 
                request.Precio, request.Productos,null);
        }
    }
}