using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaDeMicroservicios.API.Libro.Modelo;
using TiendaDeMicroservicios.API.Libro.Persistencia;
using TiendaDeMicroServicios.RabbitMQ.Bus.BusRabbit;
using TiendaDeMicroServicios.RabbitMQ.Bus.EventoQueue;

namespace TiendaDeMicroservicios.API.Libro.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Titulo { get; set; }
            public DateTime? FechaDePublicacion { get; set; }
            public Guid? AutorLibro { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.FechaDePublicacion).NotEmpty();
                RuleFor(x => x.AutorLibro).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IRabbitEventBus _eventBus;
            public Manejador(ContextoLibreria contexto, IRabbitEventBus eventBus)
            {
                _contexto = contexto;
                _eventBus = eventBus;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libro = new LibreriaMaterial
                {
                    Titulo = request.Titulo,
                    AutorLibro = request.AutorLibro,
                    FechaDePublicacion = request.FechaDePublicacion,
                    //LibreriaMaterialId = Guid.NewGuid()
                };

                _contexto.LibreriaMaterial.Add(libro);
                var valor = await _contexto.SaveChangesAsync();

                _eventBus.Publish(new EmailEventoQueue("Carlosnake.182@gmail.com", request.Titulo, "Este contenido es un ejemplo"));
                
                if (valor > 0)
                    return Unit.Value;

                throw new Exception("No se pudo insertar el libro");
            }
        }
    }
}
