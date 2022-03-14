using AutoMapper;
using DB.AirBDB.Common.Model.DTO;
using DB.AirBDB.DAL.Repository.DAO;
using DB.AirBDB.Services.API.Erros;
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
        private readonly IMapper _mapper;
        
        public ReservasController(IReservaDAO repoReserva, IMapper mapper)
        {
            _repoReserva = repoReserva;
            _mapper = mapper;
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
        public IActionResult Post([FromBody] IEnumerable<ReservaPostDTO> model)
        {
            if (ModelState.IsValid)
            {
                IList<ReservaDTO> lista = new List<ReservaDTO>();

                foreach (var item in model)
                {
                    var reservaDTO = _mapper.Map<ReservaDTO>(item);
                    lista.Add(reservaDTO);
                }
                try
                {
                    _repoReserva.Adicionar(lista);
                }
                catch (ArgumentException e)
                {
                    return BadRequest(e.Message);
                }

            foreach (var item in lista)
                {
                    var uri = Url.Action("Get", new { id = item.ReservaId });
                    return Created(uri, lista);
                }
            }

            return BadRequest(ErrorResponse.FromModelState(ModelState));
        }

        [SwaggerOperation(Summary = "Atualiza a Reserva identificado por seu {id}.", Tags = new[] { "Reservas" })]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ReservaPostDTO model)
        {

            if (ModelState.IsValid)
            {
                ReservaDTO reservaDTO;

                try
                {
                    reservaDTO = _repoReserva.RecuperaReservaPorId(id);
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }

                reservaDTO = _mapper.Map<ReservaDTO>(model);
                reservaDTO.ReservaId = id;

                _repoReserva.Atualizar(reservaDTO);

                return Ok();
            }
            return BadRequest(ErrorResponse.FromModelState(ModelState));
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

            return BadRequest(ErrorResponse.FromModelState(ModelState));
        }
    }
}
