using System.Collections.Generic;
using DB.AirBDB.Common.Utils.Enums;

namespace DB.AirBDB.DAL.Repository.DatabaseEntity
{
    internal class Place
    {
        public int PlaceId { get; set; }
        public string Description { get; set; }
        public string AccomodationType { get; set; }
        public string City { get; set; }
        public double Value { get; set; }
        public PlaceStatusLocacao StatusLocacao { get; set; } = PlaceStatusLocacao.Disponivel;
        public bool Ativo { get; set; } = true;
        public IList<Booking> ListaReservas { get; set; }

    }
}
