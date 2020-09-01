using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using TiendaDeMicroServicios.API.Gateway.ImplementRemote;
using TiendaDeMicroServicios.API.Gateway.InterfaceRemote;
using TiendaDeMicroServicios.API.Gateway.MessageHandler;

namespace TiendaDeMicroServicios.API.Gateway
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
            //services.AddControllers();
            //services.AddScoped<IAutorRemote, AutorRemote>();
            services.AddSingleton<IAutorRemote, AutorRemote>();//se usa este en lugar de addScope, debido a que esto es algo que se ejecuta de forma diferente por ser de un messageHandler

            services.AddOcelot().
                AddDelegatingHandler<LibroHandler>();
            services.AddHttpClient("AutorService", config =>
            {
                config.BaseAddress = new Uri(Configuration["Services:Autor"]);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            await app.UseOcelot();
        }
    }
}
