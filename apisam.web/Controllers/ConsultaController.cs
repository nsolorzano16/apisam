using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;
using apisam.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace apisam.web.Controllers
{
    [EnableCors("Todos")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        public IConsulta ConsultaRepo;

        public ConsultaController(IConsulta consultaRepository)
        {
            ConsultaRepo = consultaRepository;

        }

        [Authorize(Roles = "2")]
        [HttpGet("pacienteId/{pacienteId}/doctorId/{doctorId}/preclinicaId/{preclinicaId}", Name = "GetDetalleConsulta")]
        public IActionResult GetDetalleConsulta([FromRoute] int pacienteId, [FromRoute] int doctorId, [FromRoute] int preclinicaId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var resp = ConsultaRepo.GetDetalleConsulta(doctorId, pacienteId, preclinicaId);
            return Ok(resp);
        }

        [Authorize(Roles = "2")]
        [HttpPost("")]
        public IActionResult Add([FromBody] ConsultaGeneral consulta)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = ConsultaRepo.AddConsultaGeneral(consulta);
            if (_resp.Ok) return Ok(consulta);
            return BadRequest(_resp);

        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public IActionResult Update([FromBody] ConsultaGeneral consulta)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = ConsultaRepo.UpdateConsultaGeneral(consulta);
            if (_resp.Ok) return Ok(consulta);
            return BadRequest(_resp);
        }

        [Authorize(Roles = "2")]
        [HttpGet("id/{consultaId}", Name = "GetConsultaGeneralById")]
        public IActionResult GetConsultaGeneralById([FromRoute] int consultaId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var _consultaGeneral = ConsultaRepo.GetConsultaGeneralById(consultaId);
            if (_consultaGeneral != null) return Ok(_consultaGeneral);
            return Ok();

        }



    }
}