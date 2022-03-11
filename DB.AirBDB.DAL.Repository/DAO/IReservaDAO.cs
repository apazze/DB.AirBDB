using DB.AirBDB.Common.Model.DTO;
using System.Collections.Generic;

namespace DB.AirBDB.DAL.Repository.DAO
{
    public interface IReservaDAO
    {
        void Adicionar(IEnumerable<ReservaDTO> reservaDTO);
        void Atualizar(ReservaDTO reservaDTO);
        void Remover(ReservaDTO reservaDTO);
        IList<ReservaDTO> RecuperaListaDeReservas();
        ReservaDTO RecuperaReservaPorId(int id);
        bool VerificaDisponibilidadeDoLugarNoPeriodo(ReservaDTO reservas);
    }
}
