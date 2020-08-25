using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaDeMicroservicios.API.Autor.Aplicacion
{
    public class AutorDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaDeNacimiento { get; set; }
        public string AutorLibroGuid { get; set; }
    }
}
