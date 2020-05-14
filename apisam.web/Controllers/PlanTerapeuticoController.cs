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
    public class PlanTerapeuticoController : ControllerBase
    {
        public IPlanTerapeutico PlanRepo;

        public PlanTerapeuticoController(IPlanTerapeutico consultaRepository)
        {
            PlanRepo = consultaRepository;

        }


        [Authorize(Roles = "2")]
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] PlanTerapeutico plan)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = await PlanRepo.AddPlanTerapeutico(plan);
            if (_resp.Ok) return Ok(plan);
            return BadRequest(_resp);

        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] PlanTerapeutico plan)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = await PlanRepo.UpdatePlanTerapeutico(plan);
            if (_resp.Ok) return Ok(plan);
            return BadRequest(_resp);
        }

        [Authorize(Roles = "2")]
        [HttpGet("pacienteid/{pacienteId}/doctorid/{doctorId}/preclinicaid/{preclinicaId}", Name = "GetPlanTerapeutico")]
        public async Task<IActionResult> GetPlanTerapeutico([FromRoute] int pacienteId, [FromRoute] int doctorId, [FromRoute] int preclinicaId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await PlanRepo.GetPlanTerapeutico(pacienteId, doctorId, preclinicaId));



        }

        [Authorize(Roles = "2")]
        [HttpGet("listar/pacienteid/{pacienteId}/doctorid/{doctorId}/preclinicaid/{preclinicaId}", Name = "GetPlanes")]
        public async Task<IActionResult> GetPlanes([FromRoute] int pacienteId, [FromRoute] int doctorId, [FromRoute] int preclinicaId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await PlanRepo.GetPlanes(pacienteId, doctorId, preclinicaId));

        }
    }
}