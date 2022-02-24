using AutoMapper;
using DB.AirBDB.Common.Model.DTO;
using DB.AirBDB.Common.Utils;
using DB.AirBDB.DAL.Repository.DatabaseEntity;
using DB.AirBDB.Services.API.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DB.AirBDB.DAL.Repository.DAO
{
    public class ReservaDAO : IReservaDAO
    {
        private readonly AppDBContext contexto;
        private readonly IMapper mapper;
        private readonly ReservasConfiguration reservasConfiguration;
        private readonly ValidadorDeDatas validadorDeDatas;
        public TimeSpan IntervaloPermitido { get; }
        public ReservaDAO(AppDBContext contexto, IMapper mapper, ReservasConfiguration reservasConfiguration, ValidadorDeDatas validadorDeDatas)
        {
            this.contexto = contexto;
            this.mapper = mapper;
            this.reservasConfiguration = reservasConfiguration;
            IntervaloPermitido = new TimeSpan(reservasConfiguration.IntervaloMinimoPermitidoEmDias, 0, 0, 0);
            this.validadorDeDatas = validadorDeDatas;
        }
        public void Adicionar(IEnumerable<ReservaDTO> reservas)
        {
            IList<Reserva> list = new List<Reserva>();

            foreach (var item in reservas)
            {
                if (ValidarDatas(item))
                {
                    if (VerificaDisponibilidadeDoLugarNoPeriodo(item))
                    {
                        Reserva reserva = mapper.Map<Reserva>(item);
                        list.Add(reserva);
                    }
                }
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

            if (ValidarDatas(reservaDTO))
            {
                contexto.Reservas.Update(reserva);
                contexto.SaveChanges();
            }
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
        private bool ValidarDatas(ReservaDTO reservaDTO)
        {
            if(!validadorDeDatas.DataEhAtualOuFutura(reservaDTO.DataInicio, reservaDTO.DataFim))
            {
                throw new ArgumentException("Data incoerente.");
            }

            if (!validadorDeDatas.ValidaIntervaloMinimo(reservaDTO.DataInicio, reservaDTO.DataFim, IntervaloPermitido))
            {
                throw new ArgumentException($"Data fim deve ser superior em {reservasConfiguration.IntervaloMinimoPermitidoEmDias} dia(s).");
            }

            return true;
        }
        
        public bool VerificaDisponibilidadeDoLugarNoPeriodo(ReservaDTO reserva)
        {
            var buscaReservas = contexto.Reservas
                .Where(r => r.LugarId == reserva.LugarId)
                .Where(r => reserva.DataInicio >= r.DataInicio && reserva.DataInicio <= r.DataFim || reserva.DataFim >= r.DataInicio && reserva.DataFim <= r.DataFim).FirstOrDefault();

            if(buscaReservas != null)
            {
                throw new ArgumentException("Data indisponível para a reserva.");
            }

            return true;
        }
    }
}
