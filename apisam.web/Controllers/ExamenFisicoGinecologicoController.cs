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
    public class ExamenFisicoGinecologicoController : ControllerBase
    {
        public IExamenFisicoGinecologico ExamenGinecologicoRepo;

        public ExamenFisicoGinecologicoController(
            IExamenFisicoGinecologico examenRepository)
        {
            ExamenGinecologicoRepo = examenRepository;
        }


        [Authorize(Roles = "2")]
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] ExamenFisicoGinecologico examen)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = await ExamenGinecologicoRepo.AddExamenFisicoGinecologico(examen);
            if (_resp.Ok) return Ok(examen);
            return BadRequest(_resp);

        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] ExamenFisicoGinecologico examen)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = await ExamenGinecologicoRepo.UpdateExamenFisicoGinecologico(examen);
            if (_resp.Ok) return Ok(examen);
            return BadRequest(_resp);
        }

        [Authorize(Roles = "2")]
        [HttpGet("pacienteid/{pacienteId}/doctorid/{doctorId}/preclinicaid/{preclinicaId}", Name = "GetExamenFisicoGinecologico")]
        public async Task<IActionResult> GetExamenFisicoGinecologico([FromRoute] int pacienteId,
            [FromRoute] int doctorId, [FromRoute] int preclinicaId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var _examen = await ExamenGinecologicoRepo.GetExamenGinecologico(pacienteId, doctorId, preclinicaId);
            if (_examen != null) return Ok(_examen);

            return Ok();

        }


    }
}