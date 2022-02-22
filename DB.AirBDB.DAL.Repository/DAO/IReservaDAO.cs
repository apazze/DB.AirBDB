using DB.AirBDB.Common.Model.DTO;
using System.Collections.Generic;

namespace DB.AirBDB.DAL.Repository.DAO
{
    public interface IReservaDAO
    {
        void Adicionar(IEnumerable<ReservaDTO> bookingDTO);
        void Atualizar(ReservaDTO bookingDTO);
        void Remover(ReservaDTO bookingDTO);
        IList<ReservaDTO> RecuperaListaDeReservas();
        ReservaDTO RecuperaReservaPorId(int id);
    }
}
