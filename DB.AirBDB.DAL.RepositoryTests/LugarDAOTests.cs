using AutoMapper;
using DB.AirBDB.Common.Model.DTO;
using DB.AirBDB.DAL.Repository;
using DB.AirBDB.DAL.Repository.DAO;
using DB.AirBDB.DAL.Repository.DatabaseEntity;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DB.AirBDB.DAL.RepositoryTests
{
    public class LugarDAOTests
    {
        [Fact]
        public void RecuperaLugares_FiltroPorCidade_RetornaRegistros()
        {
            //Assert
            var lista = new ListasFake();

            var contextMock = new Mock<AppDBContext>();
            contextMock.Object.Lugares = CreateDbSetMock(lista.Lugares).Object;

            var configLugar = new MapperConfiguration(cfg => cfg.CreateMap<Lugar, LugarDTO>().ReverseMap());
            var mapperLugar = configLugar.CreateMapper();

            ILugarDAO sut = new LugarDAO(contextMock.Object, mapperLugar);

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

        [Fact]
        public void RecuperaLugares_FiltroPorCidade_RetornaListaVazia()
        {
            //Assert
            var lista = new ListasFake();

            var contextMock = new Mock<AppDBContext>();
            contextMock.Object.Lugares = CreateDbSetMock(lista.Lugares).Object;

            var configLugar = new MapperConfiguration(cfg => cfg.CreateMap<Lugar, LugarDTO>().ReverseMap());
            var mapperLugar = configLugar.CreateMapper();

            ILugarDAO sut = new LugarDAO(contextMock.Object, mapperLugar);

            LugarFiltroDTO filtro = new LugarFiltroDTO() { Cidade = "Porto Alegres" };

            //Act
            var resultado = sut.Filtrar(filtro);

            //Assert
            using (new AssertionScope())
            {
                resultado.Should().BeEmpty();
            }
        }

        [Fact]
        public void RecuperaLugares_FiltroPorCidadeEDatasDisponiveis_RetornaRegistros()
        {
            //Assert
            var lista = new ListasFake();

            var contextMock = new Mock<AppDBContext>();
            contextMock.Object.Lugares = CreateDbSetMock(lista.Lugares).Object;
            contextMock.Object.Reservas = CreateDbSetMock(lista.Reservas).Object;
            contextMock.Object.Usuarios = CreateDbSetMock(lista.Usuarios).Object;

            var configLugar = new MapperConfiguration(cfg => cfg.CreateMap<Lugar, LugarDTO>().ReverseMap());
            var mapperLugar = configLugar.CreateMapper();

            ILugarDAO sut = new LugarDAO(contextMock.Object, mapperLugar);

            LugarFiltroDTO filtro = new LugarFiltroDTO()
            {
                Cidade = "Porto Alegre",
                DataInicio = new DateTime(2022, 6, 1, 8, 0, 0).ToString(),
                DataFim = new DateTime(2022, 6, 20, 18, 0, 0).ToString(),
            };

            //Act
            var resultado = sut.Filtrar(filtro);

            //Assert
            using (new AssertionScope())
            {
                resultado.Should().NotBeNull();
                resultado.Count().Should().Be(2);
            }
        }

        [Fact]
        public void RecuperaLugares_FiltroPorCidadeEDatasOcupadas_RetornaVazio()
        {
            //Assert
            var lista = new ListasFake();

            var contextMock = new Mock<AppDBContext>();
            contextMock.Object.Lugares = CreateDbSetMock(lista.Lugares).Object;
            contextMock.Object.Reservas = CreateDbSetMock(lista.Reservas).Object;
            contextMock.Object.Usuarios = CreateDbSetMock(lista.Usuarios).Object;

            var configLugar = new MapperConfiguration(cfg => cfg.CreateMap<Lugar, LugarDTO>().ReverseMap());
            var mapperLugar = configLugar.CreateMapper();

            ILugarDAO sut = new LugarDAO(contextMock.Object, mapperLugar);

            LugarFiltroDTO filtro = new LugarFiltroDTO()
            {
                Cidade = "Gravataí",
                DataInicio = new DateTime(2022, 9, 1, 8, 0, 0).ToString(),
                DataFim = new DateTime(2022, 9, 20, 18, 0, 0).ToString(),
            };

            //Act
            var resultado = sut.Filtrar(filtro);

            //Assert
            using (new AssertionScope())
            {
                resultado.Should().BeEmpty();
            }
        }

        private static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            return dbSetMock;
        }
    }
}
