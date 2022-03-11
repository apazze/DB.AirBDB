using AutoMapper;
using DB.AirBDB.Common.Model.DTO;
using DB.AirBDB.Common.Utils;
using DB.AirBDB.DAL.Repository;
using DB.AirBDB.DAL.Repository.DAO;
using DB.AirBDB.DAL.Repository.DatabaseEntity;
using DB.AirBDB.Services.API.Configuration;
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
    public class ReservaDAOTests
    {
        [Fact]
        public void ValidarReserva_NaoDevePermitirReservaEmPeriodoJaReservadoParaMesmoLugar_RetornaTrue_EstaDisponivel_DatasNaoConflitam()
        {
            // Arrange

            var options = new DbContextOptionsBuilder<AppDBContext>().UseInMemoryDatabase("DubleAppDBContext_DataDisponivel").Options;

            var contexto = new AppDBContext(options);

            var configReserva = new MapperConfiguration(cfg => cfg.CreateMap<Reserva, ReservaDTO>().ReverseMap());
            
            var configUsuario = new MapperConfiguration(cfg => cfg.CreateMap<Usuario, UsuarioDTO>().ReverseMap());
            
            var configLugar = new MapperConfiguration(cfg => cfg.CreateMap<Lugar, LugarDTO>().ReverseMap());

            var mapperReserva = configReserva.CreateMapper();
            var mapperUsuario = configUsuario.CreateMapper();
            var mapperLugar = configLugar.CreateMapper();

            var reservasConfig = new ReservasConfiguration();
            reservasConfig.IntervaloMaximoPermitidoEmDias = 10000;
            var validadorDatas = new ValidadorDeDatas();

            IReservaDAO reserva = new ReservaDAO(contexto, mapperReserva, reservasConfig, validadorDatas);

            IUsuarioDAO usuario = new UsuarioDAO(contexto, mapperUsuario);
            ILugarDAO lugar = new LugarDAO(contexto, mapperLugar);

            IEnumerable<UsuarioDTO> primeiroUsuario = new List<UsuarioDTO>()
            {
                new UsuarioDTO
                {
                    UsuarioId = 1,
                    Login = "User1",
                    Senha = "Pass1",
                    Nome = "User1"
                }
            };

            usuario.Adicionar(primeiroUsuario);

            IEnumerable<LugarDTO> primeiroLugar = new List<LugarDTO>()
            {
                new LugarDTO
                {
                    LugarId = 1,
                    Descricao = "Lugar1",
                    Cidade = "Cidade1",
                    Ativo = true,
                    StatusLocacao = Common.Utils.Enums.LugarStatusLocacao.Disponivel,
                    TipoDeAcomodacao = "Quarto",
                    Valor = 250
                }
            };

            lugar.Adicionar(primeiroLugar);

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
            var options = new DbContextOptionsBuilder<AppDBContext>().UseInMemoryDatabase("DubleAppDBContext_DataOcupada").Options;

            var contexto = new AppDBContext(options);

            var configReserva = new MapperConfiguration(cfg => cfg.CreateMap<Reserva, ReservaDTO>().ReverseMap());

            var configUsuario = new MapperConfiguration(cfg => cfg.CreateMap<Usuario, UsuarioDTO>().ReverseMap());

            var configLugar = new MapperConfiguration(cfg => cfg.CreateMap<Lugar, LugarDTO>().ReverseMap());

            var mapperReserva = configReserva.CreateMapper();
            var mapperUsuario = configUsuario.CreateMapper();
            var mapperLugar = configLugar.CreateMapper();

            var reservasConfig = new ReservasConfiguration();
            reservasConfig.IntervaloMaximoPermitidoEmDias = 10000;
            var validadorDatas = new ValidadorDeDatas();

            IReservaDAO reserva = new ReservaDAO(contexto, mapperReserva, reservasConfig, validadorDatas);

            IUsuarioDAO usuario = new UsuarioDAO(contexto, mapperUsuario);
            ILugarDAO lugar = new LugarDAO(contexto, mapperLugar);

            IEnumerable<UsuarioDTO> primeiroUsuario = new List<UsuarioDTO>()
            {
                new UsuarioDTO
                {
                    UsuarioId = 1,
                    Login = "User1",
                    Senha = "Pass1",
                    Nome = "User1"
                }
            };

            usuario.Adicionar(primeiroUsuario);

            IEnumerable<LugarDTO> primeiroLugar = new List<LugarDTO>()
            {
                new LugarDTO
                {
                    LugarId = 1,
                    Descricao = "Lugar1",
                    Cidade = "Cidade1",
                    Ativo = true,
                    StatusLocacao = Common.Utils.Enums.LugarStatusLocacao.Disponivel,
                    TipoDeAcomodacao = "Quarto",
                    Valor = 250
                }
            };

            lugar.Adicionar(primeiroLugar);

            IEnumerable<ReservaDTO> primeiraReserva = new List<ReservaDTO>()
            {
                new ReservaDTO
                {
                    UsuarioId = 1,
                    LugarId = 1,
                    DataInicio = new DateTime(2022, 4, 25, 15, 30, 30),
                    DataFim = new DateTime(2022, 5, 25, 15, 30, 30)
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

            // Act
            //Assert
            foreach (var item in segundaReserva)
            {
                Assert.Throws<ArgumentException>(() => reserva.VerificaDisponibilidadeDoLugarNoPeriodo(item));
            }

        }

        [Fact]
        public void TestaObterReservasMock()
        {
            //Arrange

            var listaEmMemoriaDeObjetosMockados = new Mock<IReservaDAO>();
            var mock = listaEmMemoriaDeObjetosMockados.Object;

            //Act

            var lista = mock.RecuperaListaDeReservas();

            //Assert

            listaEmMemoriaDeObjetosMockados.Verify(r => r.RecuperaListaDeReservas());

        }

        [Fact]
        public void RecuperaReservaPorId_IdValido_RetornaReservaDTOComIdCombinando()
        {
            //Arrange
            IList<Reserva> listaDeReservas = new List<Reserva>();
            listaDeReservas.Add(new Reserva 
            {
                ReservaId = 1,
                UsuarioId = 1,
                LugarId = 1,
                DataInicio = new DateTime(2030, 1, 1, 8, 0, 0),
                DataFim = new DateTime(2031, 1, 1, 8, 0, 0),
            });

            var contextMock = new Mock<AppDBContext>();
            contextMock.Object.Reservas = MockReservaDbSet(listaDeReservas).Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(_ => _.Map<ReservaDTO>(It.IsAny<Reserva>()))
                .Returns((Reserva r) => new ReservaDTO { ReservaId = r.ReservaId });

            var reservasConfiguration = new ReservasConfiguration { IntervaloMinimoPermitidoEmDias = 1 };
            var validadorDeDatas = new ValidadorDeDatas();


            int idParaProcurar = 1;

            IReservaDAO sut = new ReservaDAO(contextMock.Object, mapperMock.Object, reservasConfiguration, validadorDeDatas);

            //Act
            var resultado = sut.RecuperaReservaPorId(idParaProcurar);

            //Assert
            using (new AssertionScope())
            {
                resultado.Should().NotBeNull();
                resultado.ReservaId.Should().Be(idParaProcurar);
            }

        }

        [Fact]
        public void RecuperaReservaPorId_IdInvalido_SoltaArgumentException()
        {
            //Arrange
            var contextMock = new Mock<AppDBContext>();
            contextMock.Object.Reservas = MockReservaDbSet().Object;
            var reservasConfiguration = new ReservasConfiguration { IntervaloMinimoPermitidoEmDias = 1 };
            var validadorDeDatas = new ValidadorDeDatas();
            int idParaProcurar = 1;
            IReservaDAO sut = new ReservaDAO(contextMock.Object, null, reservasConfiguration, validadorDeDatas);

            //Act
            Action act = () => sut.RecuperaReservaPorId(idParaProcurar);

            //Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Id da Reserva não localizado.");

        }

        private static Mock<DbSet<Reserva>> MockReservaDbSet(IEnumerable<Reserva> listaDeReservas = null)
        {
            var dbSetMock = new Mock<DbSet<Reserva>>();

            dbSetMock
                .Setup(x => x.Find(It.IsAny<int>()))
                .Returns(
                    (object[] id) => listaDeReservas == null 
                        ? null
                        : new Reserva { ReservaId = (int)id.First() });

            return dbSetMock;
        }
    }
}
