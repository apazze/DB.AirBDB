using DB.AirBDB.Common.Model.DTO;
using System.Collections.Generic;

namespace DB.AirBDB.DAL.Repository.DAO
{
    public interface ILugarDAO
    {
        void Adicionar(IEnumerable<LugarDTO> lugar);
        void Atualizar(LugarDTO lugar);
        void Remover(LugarDTO lugar);
        LugarDTO RecuperaLugarPorId(int id);
        public IEnumerable<LugarDTO> Filtrar(LugarFiltroDTO filtro);
    }
}
