using AutoMapper;
using DB.AirBDB.Common.Model.DTO;
using DB.AirBDB.DAL.Repository.DatabaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
                if (UsuarioValidations(item))
                {
                    Usuario usuario = mapper.Map<Usuario>(item);
                    list.Add(usuario);
                }
            }

            contexto.ChangeTracker.Clear();
            contexto.Usuarios.AddRange(list);

            var lastId = contexto.Usuarios.OrderBy(i => i.UsuarioId).Select(i => i.UsuarioId).LastOrDefault();

            contexto.SaveChanges();

            foreach (var item in usuarios)
            {
                item.UsuarioId = ++lastId;
            }

        }
        private bool UsuarioValidations(UsuarioDTO item)
        {
            return ValidaAlfanumericos(item) &&
                !LoginJaExiste(item);
        }

        public void Atualizar(UsuarioDTO usuarioDTO)
        {
            Usuario usuario = mapper.Map<Usuario>(usuarioDTO);

            contexto.ChangeTracker.Clear();
            if (UsuarioValidations(usuarioDTO))
            {
                contexto.Usuarios.Update(usuario);
                contexto.SaveChanges();
            }
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

        public bool ValidaAlfanumericos(UsuarioDTO usuarioDTO)
        {
            if (!EhAlfaNumerico(usuarioDTO.Login))
            {
                throw new ArgumentException("Somente letras e números são permitidos no Login.");
            }
            return true;
        }

        public static bool EhAlfaNumerico(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9\s,]*$");
            return rg.IsMatch(strToCheck);
        }

        public bool LoginJaExiste(UsuarioDTO usuario)
        {
            var buscaUsuarios = 
                contexto.Usuarios.Where(u => u.Login == usuario.Login)
                    .FirstOrDefault();

            if (buscaUsuarios != null)
            {
                throw new ArgumentException("Login indisponível para o cadastro.");
            }

            return false;
        }
    }
}
