﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DB.AirBDB.DAL.Repository.DatabaseEntity
{
    internal class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        [StringLength(128)]
        public string Nome { get; set; }
        public IList<Reserva> listaReservas { get; set; }

    }
}