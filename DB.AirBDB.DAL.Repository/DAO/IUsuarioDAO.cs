using DB.AirBDB.Common.Model.DTO;
using System.Collections.Generic;

namespace DB.AirBDB.DAL.Repository.DAO
{
    public interface IUsuarioDAO
    {
        void Adicionar(IEnumerable<UsuarioDTO> users);
        void Atualizar(UsuarioDTO user);
        void Remover(UsuarioDTO user);
        IList<UsuarioDTO> RecuperaListaDeUsuarios();
        UsuarioDTO RecuperaUsuarioPorId(int id);
    }
}