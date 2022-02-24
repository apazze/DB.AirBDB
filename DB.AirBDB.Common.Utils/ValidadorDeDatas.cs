using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
