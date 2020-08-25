using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaDeMicroservicios.API.Autor.Modelo;
using TiendaDeMicroservicios.API.Autor.Persistencia;

namespace TiendaDeMicroservicios.API.Autor.Aplicacion
{
    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorDTO>
        {
            public string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDTO>
        {
            private readonly ContextoAutor _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<AutorDTO> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _contexto.AutorLibro
                    .Where(x => x.AutorLibroGuid == request.AutorGuid)
                    .FirstOrDefaultAsync();
                if (autor == null)
                    throw new Exception("No se encontró el autor");

                var autorDto = _mapper.Map<AutorLibro, AutorDTO>(autor);
                return autorDto;
            }
        }
    }
}
