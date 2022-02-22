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
        public void Adicionar(IEnumerable<UsuarioDTO> usuarios)
        {
            IList<Usuario> list = new List<Usuario>();

            foreach (var item in usuarios)
            {
                Usuario usuario = mapper.Map<Usuario>(item);

                list.Add(usuario);
            }

            contexto.Usuarios.AddRange(list);

            var lastId = contexto.Usuarios.OrderBy(i => i.UsuarioId).Select(i => i.UsuarioId).LastOrDefault();

            contexto.SaveChanges();

            foreach (var item in usuarios)
            {
                item.UsuarioId = ++lastId;
            }

        }
        public void Atualizar(UsuarioDTO usuarioDTO)
        {
            Usuario usuario = mapper.Map<Usuario>(usuarioDTO);

            contexto.ChangeTracker.Clear();
            contexto.Usuarios.Update(usuario);
            contexto.SaveChanges();
        }
        public IList<UsuarioDTO> RecuperaListaDeUsuarios()
        {
            IList<UsuarioDTO> list = new List<UsuarioDTO>();
            var usuarios = contexto.Usuarios.ToList();

            foreach (var item in usuarios)
            {
                var usuarioDTO = mapper.Map<UsuarioDTO>(item);
                usuarioDTO.Senha = null;
                list.Add(usuarioDTO);
            }

            return list;
        }
        public UsuarioDTO RecuperaUsuarioPorId(int id)
        {
            var usuario = contexto.Usuarios.Find(id);

            if (usuario == null)
                throw new ArgumentException("Id do Usuário não localizado.");
            
            usuario.Senha = null;
            return mapper.Map<UsuarioDTO>(usuario);
        }
        public void Remover(UsuarioDTO usuarioDTO)
        {
            Usuario usuario = mapper.Map<Usuario>(usuarioDTO);

            contexto.ChangeTracker.Clear();
            contexto.Usuarios.Remove(usuario);
            contexto.SaveChanges();
        }
    }
}
