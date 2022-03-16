using AutoFixture;
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
    public class UsuarioDAOTests
    {
        [Theory]
        [InlineData("JoaoAdalberto1")]
        [InlineData("fwkgj4634564sdjg")]
        [InlineData("rgjgjpogrwAFFS02022")]
        [InlineData("353523FGSWDGfdgg3435")]
        public void CadastraUsuario_PermiteSomenteAlfaNumericos_RetornaTrue(String login)
        {
            //Assert
            var lista = new ListasFake();

            var contextMock = new Mock<AppDBContext>();
            contextMock.Object.Usuarios = CreateDbSetMock(lista.Usuarios).Object;

            var configUsuario = new MapperConfiguration(cfg => cfg.CreateMap<Usuario, UsuarioDTO>().ReverseMap());
            var mapperUsuario = configUsuario.CreateMapper();

            IUsuarioDAO sut = new UsuarioDAO(contextMock.Object, mapperUsuario);

            var fixture = new Fixture();
            var usuarioDTO = fixture.Build<UsuarioDTO>()
                .With(u => u.Login, login)
                .Create();

            //Act

            var resultado = sut.ValidaAlfanumericos(usuarioDTO);

            //Assert
            using (new AssertionScope())
            {
                resultado.Should().BeTrue();
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
