using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
using apisam.interfaces;
using apisam.web.HandleErrors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apisam.web.Controllers
{
    [EnableCors("Todos")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        public ICalendarioFecha CalendarioRepo;

        public AgendaController(ICalendarioFecha calendarrepository)
        {
            CalendarioRepo = calendarrepository;
        }

        [Authorize(Roles = "2")]
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] CalendarioFecha evento)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await CalendarioRepo.AddCalendarioFecha(evento);
            if (_resp.Ok) return Ok(evento);
            return BadRequest(new BadRequestError(_resp.Mensaje));

        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] CalendarioFecha evento)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await CalendarioRepo.UpdateCalendarioFecha(evento);
            if (_resp.Ok) return Ok(evento);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "2")]
        [HttpGet("doctorid/{id}", Name = "GetEventos")]
        public async Task<IActionResult> GetEventos([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await CalendarioRepo.GetEventos(id));
        }

        [Authorize(Roles = "2")]
        [HttpGet("movil/doctorid/{id}", Name = "GetEventosMovil")]
        public IActionResult GetEventosMovil([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(CalendarioRepo.GetEventosMovil(id));
        }





    }
}