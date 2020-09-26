using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using System.Threading.Tasks;
using TiendaDeMicroServicios.RabbitMQ.Bus.Comandos;
using TiendaDeMicroServicios.RabbitMQ.Bus.Eventos;

namespace TiendaDeMicroServicios.RabbitMQ.Bus.BusRabbit
{
    public interface IRabbitEventBus
    {
        Task EnviarComando<T>(T comando) where T : Comando;
        void Publish<T>(T @evento) where T : Evento;
        void Suscribe<T, TH>() where T : Evento
                                where TH : IEventoManejador<T>;
    }
}
