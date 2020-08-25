using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaDeMicroservicios.API.Libro.Modelo;
using TiendaDeMicroservicios.API.Libro.Persistencia;

namespace TiendaDeMicroservicios.API.Libro.Aplicacion
{
    public class ConsultaFiltro
    {
        public class LibroUnico : IRequest<LibroMaterialDTO>
        {
            public Guid? LibroId { get; set; }
        }

        public class Manejador : IRequestHandler<LibroUnico, LibroMaterialDTO>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<LibroMaterialDTO> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _contexto.LibreriaMaterial
                    .Where(x => x.LibreriaMaterialId == request.LibroId)
                    .FirstOrDefaultAsync();
                if (libro == null)
                    throw new Exception("No se encontró el Libro");

                var libroDto = _mapper.Map<LibreriaMaterial, LibroMaterialDTO>(libro);
                return libroDto;
            }
        }
    }
}
