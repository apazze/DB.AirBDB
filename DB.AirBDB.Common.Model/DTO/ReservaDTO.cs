﻿using System;

namespace DB.AirBDB.Common.Model.DTO
{
    public class ReservaDTO
    {
        public int ReservaId { get; set; }
        public int UsuarioId { get; set; }
        public int LugarId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
