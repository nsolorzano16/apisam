using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    //[Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        public IConsulta ConsultaRepo;

        public ConsultaController(IConsulta consultaRepository)
        {
            ConsultaRepo = consultaRepository;

        }

        [Authorize(Roles = "2")]
        [HttpGet("pacienteid/{pacienteId}/doctorId/{doctorId}/preclinicaId/{preclinicaId}", Name = "GetDetalleConsulta")]
        public IActionResult GetDetalleConsulta([FromRoute] int pacienteId, [FromRoute] int doctorId, [FromRoute] int preclinicaId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var resp = ConsultaRepo.GetDetalleConsulta(doctorId, pacienteId, preclinicaId);
            return Ok(resp);
        }



    }
}