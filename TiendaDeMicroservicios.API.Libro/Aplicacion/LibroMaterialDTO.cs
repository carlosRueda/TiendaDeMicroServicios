using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaDeMicroservicios.API.Libro.Aplicacion
{
    public class LibroMaterialDTO
    {
        public Guid? LibreriaMaterialId { get; set; }
        public string Titulo { get; set; }
        public DateTime? FechaDePublicacion { get; set; }
        public Guid? AutorLibro { get; set; }
    }
}
