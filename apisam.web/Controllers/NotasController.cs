using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
using apisam.interfaces;
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
    public class NotasController : ControllerBase
    {
        public INotas notasRepo;

        public NotasController(
            INotas notasRepository)
        {
            notasRepo = notasRepository;
        }


        [Authorize(Roles = "2")]
        [HttpPost("")]
        public IActionResult Add([FromBody] List<Notas> notas)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = notasRepo.AddNotaLista(notas);
            if (_resp.Ok) return Ok(notas);
            return BadRequest(_resp);
        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public IActionResult Update([FromBody] List<Notas> notas)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = notasRepo.UpdateNotaLista(notas);
            if (_resp.Ok) return Ok(notas);
            return BadRequest(_resp);
        }

        [Authorize(Roles = "2")]
        [HttpPut("desactivar", Name = "DesactivarNota")]
        public IActionResult DesactivarNota([FromBody] Notas nota)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = notasRepo.UpdateNota(nota);
            if (_resp.Ok) return Ok(nota);
            return BadRequest(_resp);
        }


        [Authorize(Roles = "2")]
        [HttpGet("pacienteid/{pacienteId}/doctorid/{doctorId}/preclinicaid/{preclinicaId}", Name = "GetNotas")]
        public IActionResult GetNotas([FromRoute] int pacienteId, [FromRoute] int doctorId, [FromRoute] int preclinicaId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(notasRepo.GetNotas(pacienteId, doctorId, preclinicaId));

        }

    }
}