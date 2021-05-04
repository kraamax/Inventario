using System;
using System.Collections.Generic;
using Inventario.Domain;
using Inventario.Domain.Contracts;

namespace Inventario.Application
{
    public class ConsultarProductoCompuestoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductoRepository _productoRepository;

        public ConsultarProductoCompuestoService(
            IUnitOfWork unitOfWork,
            IProductoRepository productoRepository
        )
        {
            _unitOfWork = unitOfWork;
            _productoRepository = productoRepository;

        }

        public List<ProductoCompuesto> ConsultarTodos()
        {
           return _productoRepository.consultarProductoCompuestos();
        }
        
        public ProductoCompuesto Consultar(string codigo)
        {
            return _productoRepository.consultarProductoCompuesto(codigo);
        }
    }

}