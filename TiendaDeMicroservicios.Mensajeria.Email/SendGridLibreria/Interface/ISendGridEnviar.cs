using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TiendaDeMicroservicios.Mensajeria.Email.SendGridLibreria.Modelo;

namespace TiendaDeMicroservicios.Mensajeria.Email.SendGridLibreria.Interface
{
    public interface ISendGridEnviar
    {
        Task<(bool resultado, string errorMessage)> EnviarEmail(SendGridData data);
    }
}
