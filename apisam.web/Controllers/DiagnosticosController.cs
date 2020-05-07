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
    public class DiagnosticosController : ControllerBase
    {
        public IDiagnosticos diagnosticosRepo;

        public DiagnosticosController(
            IDiagnosticos diagnosticosRepository)
        {
            diagnosticosRepo = diagnosticosRepository;
        }


        [Authorize(Roles = "2")]
        [HttpPost("")]
        public IActionResult Add([FromBody] List<Diagnosticos> diagnosticos)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = diagnosticosRepo.AddDiagnosticoLista(diagnosticos);
            if (_resp.Ok) return Ok(diagnosticos);
            return BadRequest(_resp);
        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public IActionResult Update([FromBody] List<Diagnosticos> diagnosticos)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = diagnosticosRepo.UpdateDiagnosticoLista(diagnosticos);
            if (_resp.Ok) return Ok(diagnosticos);
            return BadRequest(_resp);
        }

        [Authorize(Roles = "2")]
        [HttpPut("desactivar", Name = "DesactivarDiagnostico")]
        public IActionResult DesactivarDiagnostico([FromBody] Diagnosticos diagnostico)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = diagnosticosRepo.UpdateDiagnostico(diagnostico);
            if (_resp.Ok) return Ok(diagnostico);
            return BadRequest(_resp);
        }


        [Authorize(Roles = "2")]
        [HttpGet("pacienteid/{pacienteId}/doctorid/{doctorId}/preclinicaid/{preclinicaId}", Name = "GetDiagnosticos")]
        public IActionResult GetDiagnosticos([FromRoute] int pacienteId, [FromRoute] int doctorId, [FromRoute] int preclinicaId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(diagnosticosRepo.GetDiagnosticos(pacienteId, doctorId, preclinicaId));

        }
    }
}