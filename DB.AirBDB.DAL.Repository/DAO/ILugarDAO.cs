using DB.AirBDB.Common.Model.DTO;
using System.Collections.Generic;

namespace DB.AirBDB.DAL.Repository.DAO
{
    public interface ILugarDAO
    {
        void Adicionar(IEnumerable<LugarDTO> place);
        void Atualizar(LugarDTO place);
        void Remover(LugarDTO place);
        LugarDTO RecuperaLugarPorId(int id);
        public IEnumerable<LugarDTO> Filtrar(LugaresFiltroDTO filtro);
    }
}
