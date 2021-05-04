using System;
using System.Collections.Generic;
using Inventario.Domain;
using Inventario.Domain.Contracts;

namespace Inventario.Application
{
    public class ConsultarProductoSimpleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductoRepository _productoRepository;

        public ConsultarProductoSimpleService(
            IUnitOfWork unitOfWork,
            IProductoRepository productoRepository
        )
        {
            _unitOfWork = unitOfWork;
            _productoRepository = productoRepository;

        }

        public List<ProductoSimple> Handle()
        {
           return _productoRepository.ConsultarProductoSimple();
        }
    }

}