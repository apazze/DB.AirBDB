using System;

namespace DB.AirBDB.Common.Utils
{
    public class ValidadorDeDatas
    {
        public DateTime DataAtual { get; }

        public ValidadorDeDatas()
        {
            DataAtual = DateTime.Now;
        }

        public bool DataEhAtualOuFutura(DateTime inicio, DateTime fim)
        {
            return (inicio >= DataAtual) && (fim >= DataAtual);
        }

        public bool ValidaIntervaloMinimo(DateTime dataInicial, DateTime dataFinal, TimeSpan intervaloPermitido)
        {
            return (dataFinal - dataInicial) >= intervaloPermitido;
        }

        public bool ValidaDataFuturaMaxima(DateTime dataInicio, DateTime dataFim, TimeSpan intervaloMaximo)
        {
            return (dataInicio - DataAtual <= intervaloMaximo) && (dataFim - DataAtual <= intervaloMaximo);
        }
    }
}
