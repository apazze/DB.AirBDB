using DB.AirBDB.Common.Model.DTO;
using System.Collections.Generic;

namespace DB.AirBDB.DAL.Repository.DAO
{
    public interface IUsuarioDAO
    {
        void Adicionar(IEnumerable<UsuarioDTO> usuarios);
        void Atualizar(UsuarioDTO usuario);
        void Remover(UsuarioDTO usuario);
        IList<UsuarioDTO> RecuperaListaDeUsuarios();
        UsuarioDTO RecuperaUsuarioPorId(int id);
    }
}