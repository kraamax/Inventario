﻿using System;
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
            if (request.Productos!=null)
            {
                var productoCompuesto = (ProductoCompuesto) producto;
                var productosEnInventario = new List<Producto>();
                foreach (var p in request.Productos)
                {
                    var auxProductos = _productoRepository.FindBy(c => c.Codigo == p.Codigo);
                    var auxProducto =auxProductos.FirstOrDefault(x => x.ProductoCompuestoId == 0);
                    productosEnInventario.Add(auxProducto);
                }

                productoCompuesto.ProductosEnInventario = productosEnInventario;
                producto.RegistrarSalidaProducto(request.Cantidad);
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
                return "se registro la salida";
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
            return $"Se registro la salida del producto {producto.Nombre} con una cantidad de {producto.Cantidad}";
        }
    }

    public record SalidaProductoRequest(List<Producto> Productos,string Codigo, int Cantidad);
}