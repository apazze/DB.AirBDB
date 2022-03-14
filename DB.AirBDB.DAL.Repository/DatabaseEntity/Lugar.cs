using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using DB.AirBDB.Common.Utils.Enums;

[assembly: InternalsVisibleToAttribute("DB.AirBDB.DAL.RepositoryTests")]
namespace DB.AirBDB.DAL.Repository.DatabaseEntity
{
    internal class Lugar
    {
        [Key]
        public int LugarId { get; set; }
        [Required]
        [StringLength(500)]
        public string Descricao { get; set; }
        [Required]
        [StringLength(20)]
        public string TipoDeAcomodacao { get; set; }
        [Required]
        [StringLength(50)]
        public string Cidade { get; set; }
        public double Valor { get; set; }
        public LugarStatusLocacao StatusLocacao { get; set; } = LugarStatusLocacao.Disponivel;
        public bool Ativo { get; set; } = true;
        public IList<Reserva> ListaReservas { get; set; }

    }
}
