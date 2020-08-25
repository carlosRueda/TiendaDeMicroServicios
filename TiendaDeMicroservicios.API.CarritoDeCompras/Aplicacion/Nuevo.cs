using FluentValidation;
using MediatR;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaDeMicroservicios.API.CarritoDeCompras.Modelo;
using TiendaDeMicroservicios.API.CarritoDeCompras.Persistencia;

namespace TiendaDeMicroservicios.API.CarritoDeCompras.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaDeCreacionSesion { get; set; }
            public List<string> ListaDeProductos { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.FechaDeCreacionSesion).NotEmpty();
                //RuleFor(x => x.Apellido).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly CarritoContexto _contexto;
            public Manejador(CarritoContexto contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSession = new CarritoSesion
                {
                    FechaCreacion = request.FechaDeCreacionSesion,
                };

                _contexto.CarritoSesion.Add(carritoSession);
                var valor = await _contexto.SaveChangesAsync();

                if (valor == 0)
                    throw new Exception("Errores en la inserción de la sesión del carrito.");

                foreach (var producto in request.ListaDeProductos)
                {
                    var carritoSessionDetalle = new CarritoSesionDetalle
                    {
                        CarritoSesionId = carritoSession.CarritoSesionId,
                        ProductoSeleccionado = producto,
                        FechaCreacion = DateTime.Now,
                    };

                    _contexto.CarritoSesionDetalle.Add(carritoSessionDetalle);
                }
                valor = await _contexto.SaveChangesAsync();

                if (valor > 0)
                    return Unit.Value;

                throw new Exception("No se pudo insertar el detalle del carrito de compras.");

            }
        }
    }
}
