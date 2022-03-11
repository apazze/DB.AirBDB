using DB.AirBDB.DAL.Repository.DatabaseEntity;
using System.Collections.Generic;

namespace DB.AirBDB.DAL.RepositoryTests
{
    class ListaDeLugares
    {
        private IEnumerable<Lugar> lista = new List<Lugar>()
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
            }
        };
        public IEnumerable<Lugar> Lugares { get { return lista; } }

    }

}