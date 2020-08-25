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
    public class Consulta
    {
        public class Ejecuta : IRequest<List<LibroMaterialDTO>>
        {
        }

        public class Manejador : IRequestHandler<Ejecuta, List<LibroMaterialDTO>>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<List<LibroMaterialDTO>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var lstLibros = await _contexto.LibreriaMaterial.ToListAsync();
                var lstLibrosDto = _mapper.Map<List<LibreriaMaterial>, List<LibroMaterialDTO>>(lstLibros);
                return lstLibrosDto;
            }
        }
    }
}
