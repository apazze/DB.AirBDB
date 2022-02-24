using AutoMapper;
using DB.AirBDB.Common.Model.DTO;
using DB.AirBDB.Common.Utils;
using DB.AirBDB.DAL.Repository;
using DB.AirBDB.DAL.Repository.DAO;
using DB.AirBDB.DAL.Repository.DatabaseEntity;
using DB.AirBDB.Services.API.Configuration;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DB.AirBDB.DAL.RepositoryTests
{
    //public class ReservaDAOTests
    //{
    //    [Fact]
    //    public void ValidarReserva_NaoDevePermitirReservaEmPeriodoJaReservadoParaMesmoLugar_RetornaTrue()
    //    {
    //        // Arrange

    //        //var mock = new Mock<IReservaDAO>();

    //        //mock.Setup(r => r.Adicionar(It.IsAny<ReservaDTO[]>()));

    //        //var repo = mock.Object;

    //        //repo.Adicionar();

    //        //Mock<AppDBContext> mockAppDBContext = new Mock<AppDBContext>();
    //        //mockAppDBContext.Setup(x => x.Reservas).Returns(MockReservas(listaDeReservasMock));

    //        //======================================================================

    //        AppDBContext contexto = new AppDBContext();

    //        var config = new MapperConfiguration(cfg => cfg.CreateMap<Reserva, ReservaDTO>().ReverseMap());
            
    //        var mapper = config.CreateMapper();

    //        var reservasConfig = new ReservasConfiguration();
    //        var validadorDatas = new ValidadorDeDatas();


    //        List<Reserva> listaDeReservasMock = new List<Reserva>();


    //        IReservaDAO reserva = new ReservaDAO(contexto, mapper, reservasConfig, validadorDatas);

    //        IEnumerable<ReservaDTO> primeiraReserva = new List<ReservaDTO>()
    //        {
    //                new ReservaDTO
    //                {
    //                    UsuarioId = 1,
    //                    LugarId = 1,
    //                    DataInicio = new DateTime(2022, 2, 25, 15, 30, 30),
    //                    DataFim = new DateTime(2022, 3, 25, 15, 30, 30)
    //                }
    //        };

    //        reserva.Adicionar(primeiraReserva);

    //        //IEnumerable<ReservaDTO> segundaReserva = new List<ReservaDTO>()
    //        //{
    //        //        new ReservaDTO
    //        //        {
    //        //            ReservaId = 11,
    //        //            UsuarioId = 1,
    //        //            LugarId = 1,
    //        //            DataInicio = new DateTime(2022, 2, 30, 15, 30, 30),
    //        //            DataFim = new DateTime(2022, 3, 30, 15, 30, 30),
    //        //        }
    //        //};

    //        //IEnumerable<ReservaDTO> segundaReserva = new List<ReservaDTO>();
    //        ReservaDTO segundaReserva = null;

    //        // Act
    //        bool result = reserva.VerificaDisponibilidadeDoLugarNoPeriodo(segundaReserva);
            
    //        // Assert
    //        result.Should().BeTrue();
    //    }

    //    //private static DbSet<Reserva> MockReservas<Reserva>(IEnumerable<Reserva> listaDeReservas) where Reserva : class
    //    //{
    //    //    IQueryable<Reserva> listaQueryable = listaDeReservas.AsQueryable();
    //    //    Mock<DbSet<Reserva>> dbSetMock = new Mock<DbSet<Reserva>>();
    //    //    dbSetMock.Setup(x => x.Select(x => x)).Returns(listaQueryable);

    //    //    return dbSetMock.Object;
    //    //}
    //}
}
