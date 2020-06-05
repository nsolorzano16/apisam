using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;
using apisam.interfaces;
using apisam.web.HandleErrors;
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
        public async Task<IActionResult> GetDetalleConsulta([FromRoute] int pacienteId, [FromRoute] int doctorId, [FromRoute] int preclinicaId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var resp = await ConsultaRepo.GetDetalleConsulta(doctorId, pacienteId, preclinicaId);
            return Ok(resp);
        }

        [Authorize(Roles = "2")]
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] ConsultaGeneral consulta)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await ConsultaRepo.AddConsultaGeneral(consulta);
            if (_resp.Ok) return Ok(consulta);
            return BadRequest(new BadRequestError(_resp.Mensaje));

        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] ConsultaGeneral consulta)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await ConsultaRepo.UpdateConsultaGeneral(consulta);
            if (_resp.Ok) return Ok(consulta);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "2")]
        [HttpGet("getconsultageneral/pacienteid/{pacienteId}/doctorid/{doctorId}/preclinicaid/{preclinicaId}", Name = "GetConsultaGeneral")]
        public async Task<IActionResult> GetConsultaGeneral([FromRoute] int pacienteId, [FromRoute] int doctorId, [FromRoute] int preclinicaId)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            return Ok(await ConsultaRepo.GetConsultaGeneral(pacienteId, doctorId, preclinicaId));


        }



    }
}