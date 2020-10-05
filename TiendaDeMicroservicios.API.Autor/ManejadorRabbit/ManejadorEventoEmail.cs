using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaDeMicroservicios.Mensajeria.Email.SendGridLibreria.Interface;
using TiendaDeMicroservicios.Mensajeria.Email.SendGridLibreria.Modelo;
using TiendaDeMicroServicios.RabbitMQ.Bus.BusRabbit;
using TiendaDeMicroServicios.RabbitMQ.Bus.EventoQueue;

namespace TiendaDeMicroservicios.API.Autor.ManejadorRabbit
{
    public class ManejadorEventoEmail : IEventoManejador<EmailEventoQueue>
    {
        public readonly ILogger<ManejadorEventoEmail> _logger;
        public readonly ISendGridEnviar _sendGridEnviar;
        public readonly IConfiguration _configuration;

        public ManejadorEventoEmail(ILogger<ManejadorEventoEmail> logger, ISendGridEnviar sendGridEnviar, IConfiguration configuration)
        {
            _logger = logger;
            _sendGridEnviar = sendGridEnviar;
            _configuration = configuration;
        }
        public async Task Handle(EmailEventoQueue @event)
        {
            _logger.LogInformation($"el evento que se encontró es: {@event.Titulo}");
            var objData = new SendGridData()
            {
                Contenido = @event.Contenido,
                Titulo = @event.Titulo,
                EmailDestinatario = @event.Destinatario,
                NombreDestinatario = @event.Destinatario,
                SendGridAPIKey = _configuration["SendGrid:ApiKey"]
            };

            var result = await _sendGridEnviar.EnviarEmail(objData);

            if (result.resultado)
            {
                await Task.CompletedTask;
                return;
            }
        }
    }
}
