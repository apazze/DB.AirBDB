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
            IList<Lugar> list = new List<Lugar>();

            foreach (var item in lugares)
            {
                Lugar lugar = mapper.Map<Lugar>(item);
                list.Add(lugar);
            }
            contexto.ChangeTracker.Clear();
            contexto.Lugares.AddRange(list);

            var lastId = contexto.Lugares.OrderBy(i => i.LugarId).Select(i => i.LugarId).LastOrDefault();

            contexto.SaveChanges();

            foreach (var item in lugares)
            {
                item.LugarId = ++lastId;
            }
        }
        public void Atualizar(LugarDTO lugarDTO)
        {
            var lugar = mapper.Map<Lugar>(lugarDTO);

            contexto.ChangeTracker.Clear();
            contexto.Lugares.Update(lugar);
            contexto.SaveChanges();
        }
        public LugarDTO RecuperaLugarPorId(int id)
        {
            var lugar = contexto.Lugares.Where(p => p.LugarId == id)
                .Include(p => p.ListaReservas)
                .SingleOrDefault();

            if (lugar == null)
                throw new ArgumentException("Id do Lugar não localizado.");

            return mapper.Map<LugarDTO>(lugar);

        }
        public void Remover(LugarDTO lugarDTO)
        {
            var lugar = mapper.Map<Lugar>(lugarDTO);

            contexto.ChangeTracker.Clear();
            contexto.Lugares.Remove(lugar);
            contexto.SaveChanges();
        }
        public IEnumerable<LugarDTO> Filtrar(LugarFiltroDTO filtro)
        {
            if (filtro != null)
            {
                var ativos = contexto.Lugares.Where(p => p.Ativo == true);

                if (!string.IsNullOrEmpty(filtro.Cidade) && !string.IsNullOrEmpty(filtro.DataInicio) && !string.IsNullOrEmpty(filtro.DataFim))
                {
                    DateTime dataInicio = DateTime.Parse(filtro.DataInicio);
                    DateTime dataFim = DateTime.Parse(filtro.DataFim);
                    IList<Lugar> lugaresDisponiveis = new List<Lugar>();

                    var idxDeLugaresNaCidade = ativos
                        .Where(c => c.Cidade.Contains(filtro.Cidade))
                        .Select(l => l.LugarId)
                        .ToList();

                    if (idxDeLugaresNaCidade != null)
                    {
                        var idxDeLugaresReservados = contexto.Reservas
                            .Where(d => dataInicio >= d.DataInicio && dataInicio <= d.DataFim || dataFim >= d.DataInicio && dataFim <= d.DataFim)
                            .Select(b => b.LugarId)
                            .ToList();

                        var idxDeLugaresDisponiveis = idxDeLugaresNaCidade.Except(idxDeLugaresReservados);

                        if (idxDeLugaresDisponiveis != null)
                        {
                            foreach (var item in idxDeLugaresDisponiveis)
                            {
                                lugaresDisponiveis.Add(
                                    ativos.Where(p => p.LugarId == item)
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
                        .Where(l => l.Cidade.Contains(filtro.Cidade))
                        .Include(l => l.ListaReservas)
                        .ToList();

                    return mapper.Map<IList<LugarDTO>>(cidades);
                }
            }

            var tudo = contexto.Lugares.Include(p => p.ListaReservas).ToList();

            return mapper.Map<IList<LugarDTO>>(tudo);
        }
    }
}
