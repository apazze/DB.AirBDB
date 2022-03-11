using DB.AirBDB.DAL.Repository.DatabaseEntity;
using System;
using System.Collections.Generic;

namespace DB.AirBDB.DAL.RepositoryTests
{
    class ListasFake
    {
        private IEnumerable<Lugar> listaLugares = new List<Lugar>()
        {
            new Lugar
            {
                LugarId = 1,
                Descricao = "Lindo AP",
                Cidade = "Porto Alegre",
                Ativo = true,
                TipoDeAcomodacao = "Apartamento",
                StatusLocacao = Common.Utils.Enums.LugarStatusLocacao.Disponivel,
                Valor = 500
            },

            new Lugar
            {
                LugarId = 2,
                Descricao = "Linda Casa",
                Cidade = "Gravataí",
                Ativo = true,
                TipoDeAcomodacao = "Casa",
                StatusLocacao = Common.Utils.Enums.LugarStatusLocacao.Disponivel,
                Valor = 600
            },

            new Lugar
            {
                LugarId = 3,
                Descricao = "Lindo Quarto",
                Cidade = "Canoas",
                Ativo = true,
                TipoDeAcomodacao = "Quarto",
                StatusLocacao = Common.Utils.Enums.LugarStatusLocacao.Disponivel,
                Valor = 200
            },

            new Lugar
            {
                LugarId = 4,
                Descricao = "Lindo Quarto",
                Cidade = "Porto Alegre",
                Ativo = true,
                TipoDeAcomodacao = "Quarto",
                StatusLocacao = Common.Utils.Enums.LugarStatusLocacao.Disponivel,
                Valor = 200
            },
        };
        public IEnumerable<Lugar> Lugares { get { return listaLugares; } }

        private IEnumerable<Usuario> listaUsuarios = new List<Usuario>()
        {
            new Usuario
           {
               UsuarioId = 1,
               Login = "Bruce_Kent",
               Nome = "Bruce Kent",
               Senha = "123456"
           },

           new Usuario
           {
               UsuarioId = 2,
               Login = "Marta_Silva",
               Nome = "Marta Silva",
               Senha = "654321"
           },

           new Usuario
           {
               UsuarioId = 3,
               Login = "Hélio_Lopes",
               Nome = "Hélio Lopes",
               Senha = "789456"
           },

        };
        public IEnumerable<Usuario> Usuarios { get { return listaUsuarios; } }

        private IEnumerable<Reserva> listaReservas = new List<Reserva>()
        {
            new Reserva
            {
                ReservaId = 1,
                LugarId = 1,
                UsuarioId = 1,
                DataInicio = new DateTime(2022, 5, 1, 8, 0, 0),
                DataFim = new DateTime(2022, 5, 20, 18, 0, 0)
            },
            new Reserva
            {
                ReservaId = 2,
                LugarId = 2,
                UsuarioId = 2,
                DataInicio = new DateTime(2022, 9, 1, 8, 0, 0),
                DataFim = new DateTime(2022, 9, 20, 18, 0, 0)
            },
            new Reserva
            {
                ReservaId = 3,
                LugarId = 3,
                UsuarioId = 3,
                DataInicio = new DateTime(2022, 12, 1, 8, 0, 0),
                DataFim = new DateTime(2022, 12, 20, 18, 0, 0)
            },
        };
        public IEnumerable<Reserva> Reservas { get { return listaReservas; } }

    }

}