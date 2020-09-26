using System;
using System.Collections.Generic;
using System.Text;

namespace TiendaDeMicroServicios.RabbitMQ.Bus.Eventos
{
    public abstract class Evento
    {
        public DateTime Timestamp { get; protected set; }

        public Evento()
        {
            Timestamp = DateTime.Now;
        }
    }
}
