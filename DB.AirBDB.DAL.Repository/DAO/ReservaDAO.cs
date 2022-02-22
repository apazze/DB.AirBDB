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
            IList<Booking> list = new List<Booking>();

            foreach (var item in reservas)
            {
                Booking booking = mapper.Map<Booking>(item);
                list.Add(booking);
            }

            contexto.Bookings.AddRange(list);

            var lastId = contexto.Bookings.OrderBy(i => i.BookingId).Select(i => i.BookingId).LastOrDefault();

            contexto.SaveChanges();

            foreach (var item in reservas)
            {
                item.ReservaId = ++lastId;
            }

        }
        public void Atualizar(ReservaDTO reservaDTO)
        {
            var reserva = mapper.Map<Booking>(reservaDTO);

            contexto.ChangeTracker.Clear();
            contexto.Bookings.Update(reserva);
            contexto.SaveChanges();
        }
        public IList<ReservaDTO> RecuperaListaDeReservas()
        {
            IList<ReservaDTO> list = new List<ReservaDTO>();
            var reservas = contexto.Bookings.ToList();

            foreach (var item in reservas)
            {
                var reservaDTO = mapper.Map<ReservaDTO>(item);
                list.Add(reservaDTO);
            }

            return list;
        }
        public ReservaDTO RecuperaReservaPorId(int id)
        {
            var reserva = contexto.Bookings.Find(id);

            if (reserva == null)
                throw new ArgumentException("Id da Reserva não localizado.");

            return mapper.Map<ReservaDTO>(reserva);
        }
        public void Remover(ReservaDTO reservaDTO)
        {
            var reserva = mapper.Map<Booking>(reservaDTO);

            contexto.ChangeTracker.Clear();
            contexto.Bookings.Remove(reserva);
            contexto.SaveChanges();
        }
    }
}
