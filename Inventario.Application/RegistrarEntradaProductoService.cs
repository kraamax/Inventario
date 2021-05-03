using System;
using Inventario.Domain.Contracts;

namespace Inventario.Application
{
    public class RegistrarEntradaProductoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductoRepository _productoRepository;
        private readonly IMailServer _emailServer;

        public RegistrarEntradaProductoService(
            IUnitOfWork unitOfWork,
            IProductoRepository productoRepository,
            IMailServer emailServer
        )
        {
            _unitOfWork = unitOfWork;
            _productoRepository = productoRepository;
            _emailServer = emailServer;

        }

        public string Handle(ProductoRequest request)
        {
            return "";
        }
    }

    public record ProductoRequest(string codigo, string nombre, int cantidad, decimal costo, decimal precio);
}