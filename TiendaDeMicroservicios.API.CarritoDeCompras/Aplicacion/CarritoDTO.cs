using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaDeMicroservicios.API.CarritoDeCompras.Aplicacion
{
    public class CarritoDTO
    {
        //comentario para probar GIT
        public int CarritoId { get; set; }
        public DateTime? FechaDeCreacionSesion { get; set; }
        public List<CarritoDetalleDTO> ListaDeProductos { get; set; }
    }
}
