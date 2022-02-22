using AutoMapper;
using DB.AirBDB.Common.Model.DTO;
using DB.AirBDB.DAL.Repository.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace DB.AirBDB.Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioDAO _repoUsuario;
        private readonly IMapper _mapper;
        public UsuariosController(IUsuarioDAO usuarioDAO, IMapper mapper)
        {
            _repoUsuario = usuarioDAO;
            _mapper = mapper;
        }

        [SwaggerOperation(Summary = "Recupera a lista de Usuários.", Tags = new[] { "Usuários" })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult GetListaUsuarios()
        {
            IList<UsuarioDTO> lista;

            try
            {
                lista = _repoUsuario.RecuperaListaDeUsuarios();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return Ok(lista);
        }

        [SwaggerOperation(Summary = "Recupera o Usuário identificado por seu {id}.", Tags = new[] { "Usuários" })]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            UsuarioDTO model;

            try
            {
                model = _repoUsuario.RecuperaUsuarioPorId(id);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return Ok(model);
        }

        [SwaggerOperation(Summary = "Registra uma lista de Usuários na base.", Tags = new[] { "Usuários" })]
        [HttpPost]
        public IActionResult Post([FromBody] IEnumerable<UsuarioPostDTO> model)
        {
            if (ModelState.IsValid)
            {
                IList<UsuarioDTO> lista = new List<UsuarioDTO>();

                foreach (var item in model)
                {
                    var usuarioDTO = _mapper.Map<UsuarioDTO>(item);
                    lista.Add(usuarioDTO);
                }

                _repoUsuario.Adicionar(lista);

                foreach (var item in lista)
                {
                    var uri = Url.Action("Get", new { id = item.UsuarioId });
                    return Created(uri, item);
                }
            }
            return BadRequest();
        }

        [SwaggerOperation(Summary = "Atualiza o Usuário identificado por seu {id}.", Tags = new[] { "Usuários" })]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UsuarioPostDTO model)
        {
            if (ModelState.IsValid)
            {
                UsuarioDTO usuarioDTO;

                try
                {
                    usuarioDTO = _repoUsuario.RecuperaUsuarioPorId(id);
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }

                usuarioDTO = _mapper.Map<UsuarioDTO>(model);
                usuarioDTO.UsuarioId = id;

                _repoUsuario.Atualizar(usuarioDTO);

                return Ok();
            }
            return BadRequest();
        }

        [SwaggerOperation(Summary = "Remove o Usuário identificado por seu {id}.", Tags = new[] { "Usuários" })]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                UsuarioDTO usuario;

                try
                {
                    usuario = _repoUsuario.RecuperaUsuarioPorId(id);
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }

                _repoUsuario.Remover(usuario);
                return NoContent();
            }
            return BadRequest();
        }
    }
}
