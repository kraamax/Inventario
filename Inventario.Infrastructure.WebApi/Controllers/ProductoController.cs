using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventario.Application;
using Inventario.Domain.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Infrastructure.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductoRepository _productoRepository;
        public ProductoController
        (
            IUnitOfWork unitOfWork,
            IProductoRepository productoRepository)
        {

            _unitOfWork = unitOfWork; 
            _productoRepository = productoRepository;
        }
        [HttpPost]
        public string PostRegistrarEntrada(EntradaProductoRequest request) 
        {
            var service = new RegistrarEntradaProductoService(_unitOfWork, _productoRepository);
            var response = service.Handle(request);
            return response;
        }
        [HttpPost("Salida")]
        public string PostRegistrarSalida(SalidaProductoRequest request) 
        {
            var service = new RegistrarSalidaProductoService(_unitOfWork, _productoRepository);
            var response = service.Handle(request);
            return response;
        }
    }
}