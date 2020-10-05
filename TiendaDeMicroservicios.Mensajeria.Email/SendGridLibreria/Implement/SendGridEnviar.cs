using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TiendaDeMicroservicios.Mensajeria.Email.SendGridLibreria.Interface;
using TiendaDeMicroservicios.Mensajeria.Email.SendGridLibreria.Modelo;

namespace TiendaDeMicroservicios.Mensajeria.Email.SendGridLibreria.Implement
{
    public class SendGridEnviar : ISendGridEnviar
    {
        //sendgrid.com->en esta página me doy de alta
        public async Task<(bool resultado, string errorMessage)> EnviarEmail(SendGridData data)
        {
            try
            {
            var sendGridCliente = new SendGridClient(data.SendGridAPIKey);
            var destinatario = new EmailAddress(data.EmailDestinatario, data.NombreDestinatario);
            var tituloEmail = data.Titulo;
            var sender = new EmailAddress("crueda@olimpiait.com", "Tu Vendedor");
            var contenidoMensaje = data.Contenido;

            var objMensaje = MailHelper.CreateSingleEmail(sender, destinatario, tituloEmail, contenidoMensaje, contenidoMensaje);
            await sendGridCliente.SendEmailAsync(objMensaje);

            return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
