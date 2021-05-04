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
            {
                response= producto.RegistrarEntradaProducto(request.Cantidad);
                _productoRepository.Update(producto);
                _unitOfWork.Commit();
                return response;
            }
            producto = TipoProducto.CrearProducto(request);
            response = producto.RegistrarEntradaProducto(request.Cantidad);
            if (response.Contains("Error"))
                return response;
            try
            {
                _productoRepository.Add(producto);
                response= $"Se registro la entrada del producto {producto.Nombre} con una cantidad de {producto.Cantidad}";
            }
            catch (Exception e)
            {
                return "no se pudo guardar";
            }
            _unitOfWork.Commit();
            return response;
        }
    }

    public record EntradaProductoRequest(string Tipo,string Codigo, string Nombre, int Cantidad, decimal Costo, decimal Precio, List<Producto> Productos);

    public class TipoProducto
    {
        public static Producto CrearProducto(EntradaProductoRequest request)
        {
            if (request.Tipo.Equals("Simple"))
                return new ProductoSimple(request.Codigo,request.Nombre,0,request.Costo,request.Precio);
            return new ProductoCompuesto(request.Codigo, request.Nombre, 0, 
                request.Precio, request.Productos,null);
        }
    }
}