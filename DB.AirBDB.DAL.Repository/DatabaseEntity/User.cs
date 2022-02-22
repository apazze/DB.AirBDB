using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DB.AirBDB.DAL.Repository.DatabaseEntity
{
    internal class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        public IList<Booking> listaReservas { get; set; }

    }
}
