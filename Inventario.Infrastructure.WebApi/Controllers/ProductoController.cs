using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventario.Application;
using Inventario.Domain;
using Inventario.Domain.Contracts;
using Inventario.Infrastructure.Data.ObjectMother;
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
            if (!_productoRepository.consultarProductoCompuestos().Any()){
                _productoRepository.Add(ProductoCompuestoMother.CrearProducto());
                unitOfWork.Commit();
            }
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
        [HttpGet("Simple")]
        public List<ProductoSimple> ConsultarProductosSimple() 
        {
            var service = new ConsultarProductoSimpleService(_unitOfWork, _productoRepository);
            var response = service.Handle();
            return response;
        }
        [HttpGet("Compuesto")]
        public List<ProductoCompuesto> ConsultarProductosCompuestos() 
        {
            var service = new ConsultarProductoCompuestoService(_unitOfWork, _productoRepository);
            var response = service.ConsultarTodos();
            return response;
        }
        
        [HttpGet("Compuesto/codigo")]
        public ProductoCompuesto ConsultarProductosCompuestos(string codigo) 
        {
            var service = new ConsultarProductoCompuestoService(_unitOfWork, _productoRepository);
            var response = service.Consultar(codigo);
            return response;
        }
    }
}