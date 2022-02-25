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
    public class ReservaDAOTests
    {
        [Fact]
        public void ValidarReserva_NaoDevePermitirReservaEmPeriodoJaReservadoParaMesmoLugar_RetornaTrue_EstaDisponivel_DatasNaoConflitam()
        {
            // Arrange

            var options = new DbContextOptionsBuilder<AppDBContext>().UseInMemoryDatabase("DubleAppDBContext").Options;

            var contexto = new AppDBContext(options);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Reserva, ReservaDTO>().ReverseMap());

            var mapper = config.CreateMapper();

            var reservasConfig = new ReservasConfiguration();
            var validadorDatas = new ValidadorDeDatas();

            IReservaDAO reserva = new ReservaDAO(contexto, mapper, reservasConfig, validadorDatas);


            IEnumerable<ReservaDTO> primeiraReserva = new List<ReservaDTO>()
            {
                    new ReservaDTO
                    {
                        UsuarioId = 1,
                        LugarId = 1,
                        DataInicio = new DateTime(2022, 3, 25, 15, 30, 30),
                        DataFim = new DateTime(2022, 4, 20, 15, 30, 30)
                    }
            };

            reserva.Adicionar(primeiraReserva);

            IEnumerable<ReservaDTO> segundaReserva = new List<ReservaDTO>()
            {
                    new ReservaDTO
                    {
                        UsuarioId = 1,
                        LugarId = 1,
                        DataInicio = new DateTime(2022, 4, 25, 15, 30, 30),
                        DataFim = new DateTime(2022, 5, 25, 15, 30, 30)
                    }
            };

            bool result = false;

            // Act

            foreach (var item in segundaReserva)
            {
                result = reserva.VerificaDisponibilidadeDoLugarNoPeriodo(item);
            }
            
            // Assert

            result.Should().BeTrue();
        }

        [Fact]
        public void ValidarReserva_NaoDevePermitirReservaEmPeriodoJaReservadoParaMesmoLugar_RetornaFalse_EstaOcupado_DatasConflitam()
        {
            // Arrange

            var options = new DbContextOptionsBuilder<AppDBContext>().UseInMemoryDatabase("DubleAppDBContext").Options;

            var contexto = new AppDBContext(options);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Reserva, ReservaDTO>().ReverseMap());

            var mapper = config.CreateMapper();

            var reservasConfig = new ReservasConfiguration();
            var validadorDatas = new ValidadorDeDatas();

            IReservaDAO reserva = new ReservaDAO(contexto, mapper, reservasConfig, validadorDatas);


            IEnumerable<ReservaDTO> primeiraReserva = new List<ReservaDTO>()
            {
                    new ReservaDTO
                    {
                        UsuarioId = 1,
                        LugarId = 1,
                        DataInicio = new DateTime(2024, 4, 25, 15, 30, 30),
                        DataFim = new DateTime(2024, 5, 25, 15, 30, 30)
                    }
            };

            reserva.Adicionar(primeiraReserva);

            IEnumerable<ReservaDTO> segundaReserva = new List<ReservaDTO>()
            {
                    new ReservaDTO
                    {
                        UsuarioId = 1,
                        LugarId = 1,
                        DataInicio = new DateTime(2024, 4, 25, 15, 30, 30),
                        DataFim = new DateTime(2024, 5, 25, 15, 30, 30)
                    }
            };

            // Act
            //Assert
            foreach (var item in segundaReserva)
            {
                Assert.Throws<ArgumentException>(() => reserva.VerificaDisponibilidadeDoLugarNoPeriodo(item));
            }

        }

    }
}
