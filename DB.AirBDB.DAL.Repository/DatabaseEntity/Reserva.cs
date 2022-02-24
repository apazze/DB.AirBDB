using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("DB.AirBDB.DAL.RepositoryTests")]
namespace DB.AirBDB.DAL.Repository.DatabaseEntity
{
    internal class Reserva
    {
        public int ReservaId { get; set; }
        public int UsuarioId { get; set; } = 1;
        public Usuario Usuario { get; set; }
        public int LugarId { get; set; } = 1;
        public Lugar Lugar { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

    }
}