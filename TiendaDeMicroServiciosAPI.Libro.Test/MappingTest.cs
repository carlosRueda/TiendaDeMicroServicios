using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TiendaDeMicroservicios.API.Libro.Aplicacion;
using TiendaDeMicroservicios.API.Libro.Modelo;

namespace TiendaDeMicroServiciosAPI.Libro.Test
{
    public class MappingTest: Profile
    {
        public MappingTest()
        {
            CreateMap<LibreriaMaterial, LibroMaterialDTO>();
        }
    }
}
