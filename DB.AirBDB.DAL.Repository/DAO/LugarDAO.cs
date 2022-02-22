using AutoMapper;
using DB.AirBDB.Common.Model.DTO;
using DB.AirBDB.DAL.Repository.DatabaseEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DB.AirBDB.DAL.Repository.DAO
{
    public class LugarDAO : ILugarDAO
    {
        private readonly AppDBContext contexto;
        private readonly IMapper mapper;

        public LugarDAO(AppDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }
        public void Adicionar(IEnumerable<LugarDTO> lugares)
        {
            IList<Place> list = new List<Place>();

            foreach (var item in lugares)
            {
                Place place = mapper.Map<Place>(item);
                list.Add(place);
            }

            contexto.Places.AddRange(list);

            var lastId = contexto.Places.OrderBy(i => i.PlaceId).Select(i => i.PlaceId).LastOrDefault();

            contexto.SaveChanges();

            foreach (var item in lugares)
            {
                item.LugarId = ++lastId;
            }
        }
        public void Atualizar(LugarDTO lugarDTO)
        {
            var lugar = mapper.Map<Place>(lugarDTO);

            contexto.ChangeTracker.Clear();
            contexto.Places.Update(lugar);
            contexto.SaveChanges();
        }
        public LugarDTO RecuperaLugarPorId(int id)
        {
            var lugar = contexto.Places.Where(p => p.PlaceId == id)
                .Include(p => p.ListaReservas)
                .SingleOrDefault();

            if (lugar == null)
                throw new ArgumentException("Id do Lugar não localizado.");

            return mapper.Map<LugarDTO>(lugar);

        }
        public void Remover(LugarDTO lugarDTO)
        {
            var lugar = mapper.Map<Place>(lugarDTO);

            contexto.ChangeTracker.Clear();
            contexto.Places.Remove(lugar);
            contexto.SaveChanges();
        }
        public IEnumerable<LugarDTO> Filtrar(LugaresFiltroDTO filtro)
        {
            if (filtro != null)
            {
                var ativos = contexto.Places.Where(p => p.Ativo == true);

                if (!string.IsNullOrEmpty(filtro.Cidade) && !string.IsNullOrEmpty(filtro.DataInicio) && !string.IsNullOrEmpty(filtro.DataFim))
                {
                    DateTime dataInicio = DateTime.Parse(filtro.DataInicio);
                    DateTime dataFim = DateTime.Parse(filtro.DataFim);
                    IList<Place> lugaresDisponiveis = new List<Place>();

                    var idxDeLugaresNaCidade = ativos
                        .Where(c => c.City.Contains(filtro.Cidade))
                        .Select(l => l.PlaceId)
                        .ToList();

                    if (idxDeLugaresNaCidade != null)
                    {
                        var idxDeLugaresReservados = contexto.Bookings
                            .Where(d => dataInicio >= d.DataInicio && dataInicio <= d.DataFim || dataFim >= d.DataInicio && dataFim <= d.DataFim)
                            .Select(b => b.PlaceId)
                            .ToList();

                        var idxDeLugaresDisponiveis = idxDeLugaresNaCidade.Except(idxDeLugaresReservados);

                        if (idxDeLugaresDisponiveis != null)
                        {
                            foreach (var item in idxDeLugaresDisponiveis)
                            {
                                lugaresDisponiveis.Add(
                                    ativos.Where(p => p.PlaceId == item)
                                    .Include(p => p.ListaReservas)
                                    .SingleOrDefault());
                            }
                        }
                    }

                    return mapper.Map<IList<LugarDTO>>(lugaresDisponiveis);
                }

                if (!string.IsNullOrEmpty(filtro.Cidade))
                {
                    var cidades = ativos
                        .Where(l => l.City.Contains(filtro.Cidade))
                        .Include(l => l.ListaReservas)
                        .ToList();

                    return mapper.Map<IList<LugarDTO>>(cidades);
                }
            }

            var tudo = contexto.Places.Include(p => p.ListaReservas).ToList();

            return mapper.Map<IList<LugarDTO>>(tudo);
        }
    }
}
