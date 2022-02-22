using System.Collections.Generic;
using DB.AirBDB.Common.Utils.Enums;

namespace DB.AirBDB.DAL.Repository.DatabaseEntity
{
    internal class Lugar
    {
        public int LugarId { get; set; }
        public string Descricao { get; set; }
        public string TipoDeAcomodacao { get; set; }
        public string Cidade { get; set; }
        public double Valor { get; set; }
        public LugarStatusLocacao StatusLocacao { get; set; } = LugarStatusLocacao.Disponivel;
        public bool Ativo { get; set; } = true;
        public IList<Reserva> ListaReservas { get; set; }

    }
}
