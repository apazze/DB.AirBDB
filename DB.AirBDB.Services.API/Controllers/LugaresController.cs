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
    public class LugaresController : ControllerBase
    {
        private readonly ILugarDAO _repoLugar;
        public LugaresController(ILugarDAO repoLugar)
        {
            _repoLugar = repoLugar;
        }

        [SwaggerOperation(Summary = "Recupera a lista de Lugares.", Tags = new[] { "Lugares" })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LugarDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult GetListaLugares([FromQuery] LugaresFiltroDTO filtro)
        {
            IEnumerable<LugarDTO> lista;

            try
            {
                lista = _repoLugar.Filtrar(filtro);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return Ok(lista);
        }

        [SwaggerOperation(Summary = "Recupera o Lugar identificado por seu {id}.", Tags = new[] { "Lugares" })]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            LugarDTO model;

            try
            {
                model = _repoLugar.RecuperaLugarPorId(id);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return Ok(model);
        }

        [SwaggerOperation(Summary = "Registra uma lista de Lugares na base.", Tags = new[] { "Lugares" })]
        [HttpPost]

        public IActionResult Post([FromBody] IEnumerable<LugarDTO> model)
        {

            if (ModelState.IsValid)
            {
                _repoLugar.Adicionar(model);

                foreach (var item in model)
                {
                    var uri = Url.Action("Get", new { id = item.LugarId });
                    return Created(uri, model);
                }
            }

            return BadRequest();
        }

        [SwaggerOperation(Summary = "Atualiza o Lugar identificado por seu {id}.", Tags = new[] { "Lugares" })]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] LugarDTO model)
        {
            if (ModelState.IsValid)
            {
                LugarDTO lugar;

                try
                {
                    lugar = _repoLugar.RecuperaLugarPorId(id);
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }

                lugar.Descricao = model.Descricao;
                lugar.TipoDeAcomodacao = model.TipoDeAcomodacao;
                lugar.Cidade = model.Cidade;
                lugar.Valor = model.Valor;
                lugar.Ativo = model.Ativo;
                lugar.EstadoDaLocacao = model.EstadoDaLocacao;

                _repoLugar.Atualizar(lugar);

                return Ok();
            }

            return BadRequest();
        }

        [SwaggerOperation(Summary = "Remove o Lugar identificado por seu {id}.", Tags = new[] { "Lugares" })]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                LugarDTO lugar;

                try
                {
                    lugar = _repoLugar.RecuperaLugarPorId(id);
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }

                _repoLugar.Remover(lugar);
                return NoContent();
            }

            return BadRequest();
        }
    }
}
