using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaDeMicroservicios.API.CarritoDeCompras.Persistencia;
using TiendaDeMicroservicios.API.CarritoDeCompras.RemoteInterface;

namespace TiendaDeMicroservicios.API.CarritoDeCompras.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDTO>
        {
            public int CarritoSessionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDTO>
        {
            public readonly CarritoContexto _contexto;
            public readonly ILibrosService _libroService;
            public Manejador(CarritoContexto contexto, ILibrosService librosService)
            {
                _contexto = contexto;
                _libroService = librosService;
            }

            public async  Task<CarritoDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await _contexto.CarritoSesion.
                    FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSessionId);

                var carritoSesionDetalle = await _contexto.CarritoSesionDetalle
                    .Where(x => x.CarritoSesionId == request.CarritoSessionId).ToListAsync();

                var listaCarritoDto = new List<CarritoDetalleDTO>();

                foreach(var libro in carritoSesionDetalle)
                {
                    var response = await _libroService.GetLibro(new Guid(libro.ProductoSeleccionado));
                    if (response.resultado)
                    {
                        var objetoLibro = response.Libro;
                        var carritoDetalle = new CarritoDetalleDTO
                        {
                            TituloLibro = objetoLibro.Titulo,
                            FechaDePublicacion = objetoLibro.FechaDePublicacion,
                            LibroId = objetoLibro.LibreriaMaterialId
                        };
                        listaCarritoDto.Add(carritoDetalle);
                    }
                }

                var carritoSesionDto = new CarritoDTO
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaDeCreacionSesion = carritoSesion.FechaCreacion,
                    ListaDeProductos = listaCarritoDto
                };

                return carritoSesionDto;
            }
        }
    }
}
