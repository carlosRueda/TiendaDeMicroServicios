using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TiendaDeMicroservicios.API.Autor.Aplicacion;
using TiendaDeMicroservicios.API.Autor.ManejadorRabbit;
using TiendaDeMicroservicios.API.Autor.Persistencia;
using TiendaDeMicroServicios.RabbitMQ.Bus.BusRabbit;
using TiendaDeMicroServicios.RabbitMQ.Bus.EventoQueue;
using TiendaDeMicroServicios.RabbitMQ.Bus.Implement;

namespace TiendaDeMicroservicios.API.Autor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IRabbitEventBus, RabbitEventBus>();
            services.AddTransient<IEventoManejador<EmailEventoQueue>, ManejadorEventoEmail>();

            services.AddControllers()
                .AddFluentValidation(cfg => {
                    cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>();
                });
            services.AddDbContext<ContextoAutor>(options=>
            {
                options.UseNpgsql(Configuration.GetConnectionString("ConexionDatabase"));
            });
            //no hay necesidad de hacerlo con todas las clases. solo se hace con una...y el automaticamente
            //busca todas las que dependan de mediatR
            services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
            services.AddAutoMapper(typeof(Consulta.Manejador));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var eventBus = app.ApplicationServices.GetRequiredService<IRabbitEventBus>();
            eventBus.Suscribe<EmailEventoQueue, ManejadorEventoEmail>();
        }
    }
}
