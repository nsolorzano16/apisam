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
        public IActionResult Add([FromBody] PlanTerapeutico plan)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = PlanRepo.AddPlanTerapeutico(plan);
            if (_resp.Ok) return Ok(plan);
            return BadRequest(_resp);

        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public IActionResult Update([FromBody] PlanTerapeutico plan)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = PlanRepo.UpdatePlanTerapeutico(plan);
            if (_resp.Ok) return Ok(plan);
            return BadRequest(_resp);
        }

        [Authorize(Roles = "2")]
        [HttpGet("id/{planId}", Name = "GetPlanTerapeuticoById")]
        public IActionResult GetPlanTerapeuticoById([FromRoute] int planId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var _planTerapeutico = PlanRepo.GetPlanTerapeuticoById(planId);
            if (_planTerapeutico != null) return Ok(_planTerapeutico);
            return Ok();

        }
    }
}