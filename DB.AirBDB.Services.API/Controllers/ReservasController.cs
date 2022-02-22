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
    public class ReservasController : ControllerBase
    {

        private readonly IReservaDAO _repoReserva;

        public ReservasController(IReservaDAO repoReserva)
        {
            _repoReserva = repoReserva;
        }

        [SwaggerOperation(Summary = "Recupera a lista de Reservas.", Tags = new[] { "Reservas" })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReservaDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult GetListaReservas()
        {
            IList<ReservaDTO> lista;

            try
            {
                lista = _repoReserva.RecuperaListaDeReservas();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return Ok(lista);
        }

        [SwaggerOperation(Summary = "Recupera a Reserva identificado por seu {id}.", Tags = new[] { "Reservas" })]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ReservaDTO model;

            try
            {
                model = _repoReserva.RecuperaReservaPorId(id);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return Ok(model);
        }

        [SwaggerOperation(Summary = "Registra uma lista de Reservas na base.", Tags = new[] { "Reservas" })]
        [HttpPost]
        public IActionResult Post([FromBody] IEnumerable<ReservaDTO> model)
        {
            if (ModelState.IsValid)
            {
                _repoReserva.Adicionar(model);

                foreach (var item in model)
                {
                    var uri = Url.Action("Get", new { id = item.ReservaId });
                    return Created(uri, model);
                }
            }

            return BadRequest();
        }

        [SwaggerOperation(Summary = "Atualiza a Reserva identificado por seu {id}.", Tags = new[] { "Reservas" })]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ReservaDTO model)
        {

            if (ModelState.IsValid)
            {
                ReservaDTO reserva;

                try
                {
                    reserva = _repoReserva.RecuperaReservaPorId(id);
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }

                reserva.UserId = model.UserId;
                reserva.LugarId = model.LugarId;
                reserva.DataInicio = model.DataInicio;
                reserva.DataFim = model.DataFim;

                _repoReserva.Atualizar(reserva);

                return Ok();
            }
            return BadRequest();
        }

        [SwaggerOperation(Summary = "Remove a Reserva identificado por seu {id}.", Tags = new[] { "Reservas" })]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                ReservaDTO reserva;

                try
                {
                    reserva = _repoReserva.RecuperaReservaPorId(id);
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }

                _repoReserva.Remover(reserva);
                return NoContent();
            }

            return BadRequest();
        }
    }
}
