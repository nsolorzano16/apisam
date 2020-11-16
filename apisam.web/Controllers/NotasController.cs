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
        public async Task<IActionResult> Add([FromBody] List<Notas> notas)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = await notasRepo.AddNotaLista(notas);
            if (_resp.Ok) return Ok(notas);
            return BadRequest(_resp);
        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] List<Notas> notas)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await notasRepo.UpdateNotaLista(notas);
            if (_resp.Ok) return Ok(notas);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "2")]
        [HttpPost("agregar", Name = "AddNota")]
        public async Task<IActionResult> AddNota([FromBody] Notas nota)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await notasRepo.AddNota(nota);
            if (_resp.Ok) return Ok(nota);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "2")]
        [HttpPut("editar", Name = "UpdateNota")]
        public async Task<IActionResult> UpdateNota([FromBody] Notas nota)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await notasRepo.UpdateNota(nota);
            if (_resp.Ok) return Ok(nota);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }



        [Authorize(Roles = "2")]
        [HttpPut("desactivar", Name = "DesactivarNota")]
        public async Task<IActionResult> DesactivarNota([FromBody] Notas nota)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await notasRepo.UpdateNota(nota);
            if (_resp.Ok) return Ok(nota);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }


        [Authorize(Roles = "2")]
        [HttpGet("pacienteid/{pacienteId}/doctorid/{doctorId}/preclinicaid/{preclinicaId}", Name = "GetNotas")]
        public async Task<IActionResult> GetNotas([FromRoute] int pacienteId, [FromRoute] string doctorId, [FromRoute] int preclinicaId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await notasRepo.GetNotas(pacienteId, doctorId, preclinicaId));

        }

    }
}