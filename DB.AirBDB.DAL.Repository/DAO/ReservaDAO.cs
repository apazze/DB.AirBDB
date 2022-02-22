using AutoMapper;
using DB.AirBDB.Common.Model.DTO;
using DB.AirBDB.DAL.Repository.DatabaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DB.AirBDB.DAL.Repository.DAO
{
    public class ReservaDAO : IReservaDAO
    {
        private readonly AppDBContext contexto;
        private readonly IMapper mapper;
        public ReservaDAO(AppDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }
        public void Adicionar(IEnumerable<ReservaDTO> reservas)
        {
            IList<Reserva> list = new List<Reserva>();

            foreach (var item in reservas)
            {
                Reserva reserva = mapper.Map<Reserva>(item);
                list.Add(reserva);
            }

            contexto.Reservas.AddRange(list);

            var lastId = contexto.Reservas.OrderBy(i => i.ReservaId).Select(i => i.ReservaId).LastOrDefault();

            contexto.SaveChanges();

            foreach (var item in reservas)
            {
                item.ReservaId = ++lastId;
            }

        }
        public void Atualizar(ReservaDTO reservaDTO)
        {
            var reserva = mapper.Map<Reserva>(reservaDTO);

            contexto.ChangeTracker.Clear();
            contexto.Reservas.Update(reserva);
            contexto.SaveChanges();
        }
        public IList<ReservaDTO> RecuperaListaDeReservas()
        {
            IList<ReservaDTO> list = new List<ReservaDTO>();
            var reservas = contexto.Reservas.ToList();

            foreach (var item in reservas)
            {
                var reservaDTO = mapper.Map<ReservaDTO>(item);
                list.Add(reservaDTO);
            }

            return list;
        }
        public ReservaDTO RecuperaReservaPorId(int id)
        {
            var reserva = contexto.Reservas.Find(id);

            if (reserva == null)
                throw new ArgumentException("Id da Reserva não localizado.");

            return mapper.Map<ReservaDTO>(reserva);
        }
        public void Remover(ReservaDTO reservaDTO)
        {
            var reserva = mapper.Map<Reserva>(reservaDTO);

            contexto.ChangeTracker.Clear();
            contexto.Reservas.Remove(reserva);
            contexto.SaveChanges();
        }
    }
}
