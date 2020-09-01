using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaDeMicroServicios.API.Gateway.LibroRemote;

namespace TiendaDeMicroServicios.API.Gateway.InterfaceRemote
{
    public interface IAutorRemote
    {
        Task<(bool resultado, AutorModeloRemoto autor, string ErrorMessage)> GetAutor(Guid AutorId);
    }
}
