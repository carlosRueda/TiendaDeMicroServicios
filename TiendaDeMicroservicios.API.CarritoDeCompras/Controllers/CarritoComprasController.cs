using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaDeMicroservicios.API.CarritoDeCompras.Aplicacion;

namespace TiendaDeMicroservicios.API.CarritoDeCompras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoComprasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarritoComprasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoDTO>> GetCarrito(int id)
        {
            return await _mediator.Send(new Consulta.Ejecuta { CarritoSessionId = id });
        }

        //[HttpGet]
        //public async Task<ActionResult<List<AutorDTO>>> GetAutores()
        //{
        //    return await _mediator.Send(new Consulta.ListaAutor());
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<AutorDTO>> GetAutorLibro(string id)
        //{
        //    return await _mediator.Send(new ConsultaFiltro.AutorUnico() { AutorGuid = id });
        //}
    }
}
