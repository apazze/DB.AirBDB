using DB.AirBDB.Common.Utils.Enums;
using System.Collections.Generic;

namespace DB.AirBDB.Common.Model.DTO
{
    public class LugarDTO
    {
        public int LugarId { get; set; }
        public string Descricao { get; set; }
        public string TipoDeAcomodacao { get; set; }
        public string Cidade { get; set; }
        public double Valor { get; set; }
        public PlaceStatusLocacao EstadoDaLocacao { get; set; } = PlaceStatusLocacao.Disponivel;
        public bool Ativo { get; set; } = true;
        public IList<ReservaDTO> ListaReservas { get; set; }
    }
}
