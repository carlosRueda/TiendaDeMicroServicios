using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaDeMicroServicios.RabbitMQ.Bus.BusRabbit;
using TiendaDeMicroServicios.RabbitMQ.Bus.EventoQueue;

namespace TiendaDeMicroservicios.API.Autor.ManejadorRabbit
{
    public class ManejadorEventoEmail : IEventoManejador<EmailEventoQueue>
    {
        //private readonly ILogger<ManejadorEventoEmail> _logger;
        //public ManejadorEventoEmail(ILogger<ManejadorEventoEmail> logger)
        //{
        //    _logger = logger;
        //}
        public ManejadorEventoEmail()
        {

        }
        public Task Handle(EmailEventoQueue @event)
        {
            //_logger.LogInformation($"Este es el valor que consumo desde RabbitMQ = {@event.Titulo}");

            return Task.CompletedTask;
        }
    }
}
