using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaDeMicroServicios.API.Gateway.LibroRemote
{
    public class LibroModeloRemoto
    {
        public Guid? LibreriaMaterialId { get; set; }
        public string Titulo { get; set; }
        public DateTime? FechaDePublicacion { get; set; }
        public Guid? AutorLibro { get; set; }
        public AutorModeloRemoto AutorData { get; set; }
    }
}
