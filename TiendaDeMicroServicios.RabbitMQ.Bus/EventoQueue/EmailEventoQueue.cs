using System;
using System.Collections.Generic;
using System.Text;
using TiendaDeMicroServicios.RabbitMQ.Bus.Eventos;

namespace TiendaDeMicroServicios.RabbitMQ.Bus.EventoQueue
{
    public class EmailEventoQueue : Evento
    {
        public string Destinatario { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public EmailEventoQueue(string destinatario, string titulo, string contenido)
        {
            Destinatario = destinatario;
            Titulo = titulo;
            Contenido = contenido;
        }
    }
}
