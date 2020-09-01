using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaDeMicroServicios.API.Gateway.LibroRemote
{
    public class AutorModeloRemoto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaDeNacimiento { get; set; }
        public string AutorLibroGuid { get; set; }
    }
}
