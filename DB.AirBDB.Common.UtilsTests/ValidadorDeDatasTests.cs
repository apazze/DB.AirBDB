using DB.AirBDB.Common.Utils;
using FluentAssertions;
using System;
using Xunit;

namespace DB.AirBDB.Common.UtilsTests
{
    public class ValidadorDeDatasTests
    {
        [Fact]
        public void ValidarData_DatasSaoAtuaisOuFuturas_RetornaTrue()
        {
            // Arrange
            DateTime dataInicial = new DateTime(2030, 02, 23, 10, 09, 00);
            DateTime dataFinal = new DateTime(2030, 02, 25, 10, 09, 00);

            ValidadorDeDatas sut = new ValidadorDeDatas();

            //Act
            bool result = sut.DataEhAtualOuFutura(dataInicial, dataFinal);

            //Assert
            result.Should().BeTrue();

        }
        [Fact]
        public void ValidarData_DatasSaoAtuaisOuFuturas_RetornaFalse()
        {
            // Arrange
            DateTime dataInicial = new DateTime(2030, 02, 25, 10, 09, 00);
            DateTime dataFinal = new DateTime(2022, 02, 22, 10, 09, 00);

            ValidadorDeDatas sut = new ValidadorDeDatas();

            //Act
            bool result = sut.DataEhAtualOuFutura(dataInicial, dataFinal);

            //Assert
            result.Should().BeFalse();

        }

        [Fact]
        public void ValidarData_DatasAlemDoIntervaloMinimo_RetornaTrue()
        {
            // Arrange
            DateTime dataInicial = new DateTime(2022, 02, 26, 10, 09, 00);
            DateTime dataFinal = new DateTime(2022, 02, 27, 10, 09, 00);
            TimeSpan intervaloPermitido = new TimeSpan(1, 0, 0, 0);
            ValidadorDeDatas sut = new ValidadorDeDatas();

            //Act
            bool result = sut.ValidaIntervaloMinimo(dataInicial, dataFinal, intervaloPermitido);

            //Assert
            result.Should().BeTrue();
        }
        
        [Fact]
        public void ValidarData_DatasAlemDoIntervaloMinimo_RetornaFalse()
        {
            // Arrange
            DateTime dataInicial = new DateTime(2022, 02, 22, 10, 09, 00);
            DateTime dataFinal = new DateTime(2022, 02, 23, 10, 08, 59);
            TimeSpan intervaloPermitido = new TimeSpan(1, 0, 0, 0);
            ValidadorDeDatas sut = new ValidadorDeDatas();

            //Act
            bool result = sut.ValidaIntervaloMinimo(dataInicial, dataFinal, intervaloPermitido);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ValidarData_DatasAlemDoIntervaloMaximo_RetornaTrue()
        {
            // Arrange
            DateTime dataInicial = new DateTime(2022, 12, 22, 10, 09, 00);
            DateTime dataFinal = new DateTime(2022, 12, 23, 10, 08, 59);
            TimeSpan intervaloPermitido = new TimeSpan(365, 0, 0, 0);
            ValidadorDeDatas sut = new ValidadorDeDatas();

            //Act
            bool result = sut.ValidaDataFuturaMaxima(dataInicial, dataFinal, intervaloPermitido);

            //Assert
            result.Should().BeTrue();
        }
        [Fact]
        public void ValidarData_DatasAlemDoIntervaloMaximo_RetornaFalse()
        {
            // Arrange
            DateTime dataInicial = new DateTime(2023, 12, 22, 10, 09, 00);
            DateTime dataFinal = new DateTime(2023, 12, 23, 10, 08, 59);
            TimeSpan intervaloPermitido = new TimeSpan(365, 0, 0, 0);
            ValidadorDeDatas sut = new ValidadorDeDatas();

            //Act
            bool result = sut.ValidaDataFuturaMaxima(dataInicial, dataFinal, intervaloPermitido);

            //Assert
            result.Should().BeFalse();
        }
    }
}
