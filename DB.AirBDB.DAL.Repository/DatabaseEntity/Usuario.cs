using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("DB.AirBDB.DAL.RepositoryTests")]
namespace DB.AirBDB.DAL.Repository.DatabaseEntity
{
    internal class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        [Required]
        [StringLength(20)]
        public string Login { get; set; }
        [Required]
        [StringLength(20)]
        public string Senha { get; set; }
        [Required]
        [StringLength(120)]
        public string Nome { get; set; }
        public IList<Reserva> listaReservas { get; set; }

    }
}
