using AutoMapper;
using DB.AirBDB.Common.Model.DTO;
using DB.AirBDB.DAL.Repository;
using DB.AirBDB.DAL.Repository.DAO;
using DB.AirBDB.DAL.Repository.DatabaseEntity;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DB.AirBDB.DAL.RepositoryTests
{
    public class LugarDAOTests
    {
        [Fact]
        public void RecuperaLugares_FiltroPorCidade()
        {
            //Assert
            var lugares = new ListaDeLugares();

            var contextMock = new Mock<AppDBContext>();
            contextMock.Object.Lugares = CreateDbSetMock(lugares.Lugares).Object;

            var configReserva = new MapperConfiguration(cfg => cfg.CreateMap<Lugar, LugarDTO>().ReverseMap());
            var mapperReserva = configReserva.CreateMapper();

            ILugarDAO sut = new LugarDAO(contextMock.Object, mapperReserva);

            LugarFiltroDTO filtro = new LugarFiltroDTO() { Cidade = "Porto Alegre" };

            //Act
            var resultado = sut.Filtrar(filtro);

            //Assert
            using (new AssertionScope())
            {
                resultado.Should().NotBeNull();
                resultado.Count().Should().Be(2);
            }
        }

        private static Mock<DbSet<Lugar>> CreateDbSetMock<Lugar>(IEnumerable<Lugar> elements) where Lugar : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<Lugar>>();

            dbSetMock.As<IQueryable<Lugar>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<Lugar>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<Lugar>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<Lugar>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            return dbSetMock;
        }
    }
}
