using AutoMapper;
using DB.AirBDB.Common.Model.DTO;
using DB.AirBDB.DAL.Repository.DatabaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DB.AirBDB.DAL.Repository.DAO
{
    public class UsuarioDAO : IUsuarioDAO
    {
        private readonly AppDBContext contexto;
        private readonly IMapper mapper;
        public UsuarioDAO(AppDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }
        public void Adicionar(IEnumerable<UsuarioDTO> users)
        {
            IList<User> list = new List<User>();

            foreach (var item in users)
            {
                User usuario = mapper.Map<User>(item);

                list.Add(usuario);
            }

            contexto.Users.AddRange(list);

            var lastId = contexto.Users.OrderBy(i => i.UserId).Select(i => i.UserId).LastOrDefault();

            contexto.SaveChanges();

            foreach (var item in users)
            {
                item.UserId = ++lastId;
            }

        }
        public void Atualizar(UsuarioDTO usuarioDTO)
        {
            User usuario = mapper.Map<User>(usuarioDTO);

            contexto.ChangeTracker.Clear();
            contexto.Users.Update(usuario);
            contexto.SaveChanges();
        }
        public IList<UsuarioDTO> RecuperaListaDeUsuarios()
        {
            IList<UsuarioDTO> list = new List<UsuarioDTO>();
            var usuarios = contexto.Users.ToList();

            foreach (var item in usuarios)
            {
                var usuarioDTO = mapper.Map<UsuarioDTO>(item);
                usuarioDTO.Password = null;
                list.Add(usuarioDTO);
            }

            return list;
        }
        public UsuarioDTO RecuperaUsuarioPorId(int id)
        {
            var usuario = contexto.Users.Find(id);

            if (usuario == null)
                throw new ArgumentException("Id do Usuário não localizado.");

            return mapper.Map<UsuarioDTO>(usuario);
        }
        public void Remover(UsuarioDTO usuarioDTO)
        {
            User usuario = mapper.Map<User>(usuarioDTO);

            contexto.ChangeTracker.Clear();
            contexto.Users.Remove(usuario);
            contexto.SaveChanges();
        }
    }
}
