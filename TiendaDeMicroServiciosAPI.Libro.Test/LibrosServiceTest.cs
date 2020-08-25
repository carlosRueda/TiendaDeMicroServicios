using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.Xml;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TiendaDeMicroservicios.API.Libro.Aplicacion;
using TiendaDeMicroservicios.API.Libro.Modelo;
using TiendaDeMicroservicios.API.Libro.Persistencia;
using Xunit;

namespace TiendaDeMicroServiciosAPI.Libro.Test
{
    public class LibrosServiceTest
    {
        private List<LibreriaMaterial> ObtenerDataPrueba()
        {
            //este metodo es para llenar con data de genfu
            A.Configure<LibreriaMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle()
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibreriaMaterial>(30);
            lista[0].LibreriaMaterialId = Guid.Empty;

            return lista;
        }

        private Mock<ContextoLibreria> CrearContexto()
        {
            var dataPrueba = ObtenerDataPrueba().AsQueryable();
            var dbSet = new Mock<DbSet<LibreriaMaterial>>();
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibreriaMaterial>(dataPrueba.GetEnumerator()));

            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider)
                .Returns(new AsyncQueryProvider<LibreriaMaterial>(dataPrueba.Provider));

            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);
            return contexto;
        }

        [Fact]
        public async void GetLibroPorId()
        {
            var mockContexto = CrearContexto();
            var mapper = CrearMapper();

            var manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mapper);
            var request = new ConsultaFiltro.LibroUnico() {
                LibroId = Guid.Empty
            };

            var libro = await manejador.Handle(request, new CancellationToken());
            Assert.NotNull(libro);
            Assert.True(libro.LibreriaMaterialId == Guid.Empty);
        }

        [Fact]
        public async void GetLibros()
        {
            ///que metodo en mi microservices se encarga de la consulta de libros de la bd??
            ///1. emular la instancia de EF Core
            /// Para hacer esa emulacion dentro de un ambiente de test usamos un Mock
            var mockContexto = CrearContexto();
            ///2. emular el mapper
            var mapConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingTest());
            });
            var mapper = mapConfig.CreateMapper();
            ///3. Intanciar el manejador y pasarle como parametros los mocks.
            var manejador = new Consulta.Manejador(mockContexto.Object, mapper);
            var request = new Consulta.Ejecuta();

            var lista = await manejador.Handle(request, new CancellationToken());
            Assert.True(lista.Any());
        }

        [Fact]
        public async void GuardarLibro()
        {
            var options = new DbContextOptionsBuilder<ContextoLibreria>()
                .UseInMemoryDatabase(databaseName: "BaseDatosLibro")
                .Options;

            var contexto = new ContextoLibreria(options);
            var manejador = new Nuevo.Manejador(contexto);
            var request = new Nuevo.Ejecuta()
            {
                Titulo = "Libro de Microservicios",
                AutorLibro = Guid.Empty,
                FechaDePublicacion = DateTime.Now
            };

            var libro = await manejador.Handle(request, new CancellationToken());
            Assert.True(libro!=null);

        }

        private IMapper CrearMapper()
        {
            var mapConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingTest());
            });
            return mapConfig.CreateMapper();
        }

    }
}
