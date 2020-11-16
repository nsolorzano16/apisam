using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;
using apisam.interfaces;
using apisam.web.HandleErrors;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public PlanTerapeuticoController(IPlanTerapeutico consultaRepository, IMapper mapper)
        {
            PlanRepo = consultaRepository;
            _mapper = mapper;

        }


        [Authorize(Roles = "2")]
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] PlanTerapeutico plan)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await PlanRepo.AddPlanTerapeutico(plan);
            if (_resp.Ok) return Ok(plan);
            return BadRequest(new BadRequestError(_resp.Mensaje));

        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] PlanTerapeutico plan)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await PlanRepo.UpdatePlanTerapeutico(plan);
            if (_resp.Ok) return Ok(plan);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "2")]
        [HttpPut("edit")]
        public async Task<IActionResult> UpdatePlan([FromBody] PlanTerapeuticoViewModel plan)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            var _x = _mapper.Map<PlanTerapeuticoViewModel, PlanTerapeutico>(plan);
            RespuestaMetodos _resp = await PlanRepo.UpdatePlanTerapeutico(_x);
            if (_resp.Ok) return Ok(await PlanRepo.GetPlanInfo(_x.PlanTerapeuticoId));
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "2")]
        [HttpGet("pacienteid/{pacienteId}/doctorid/{doctorId}/preclinicaid/{preclinicaId}", Name = "GetPlanTerapeutico")]
        public async Task<IActionResult> GetPlanTerapeutico([FromRoute] int pacienteId, [FromRoute] string doctorId, [FromRoute] int preclinicaId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await PlanRepo.GetPlanTerapeutico(pacienteId, doctorId, preclinicaId));



        }

        [Authorize(Roles = "2")]
        [HttpGet("listar/pacienteid/{pacienteId}/doctorid/{doctorId}/preclinicaid/{preclinicaId}", Name = "GetPlanes")]
        public async Task<IActionResult> GetPlanes([FromRoute] int pacienteId, [FromRoute] string doctorId, [FromRoute] int preclinicaId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await PlanRepo.GetPlanes(pacienteId, doctorId, preclinicaId));

        }

        [Authorize(Roles = "2")]
        [HttpGet("movil/listar/pacienteid/{pacienteId}/doctorid/{doctorId}/preclinicaid/{preclinicaId}", Name = "GetPlanesLista")]
        public async Task<IActionResult> GetPlanesLista([FromRoute] int pacienteId, [FromRoute] string doctorId, [FromRoute] int preclinicaId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await PlanRepo.GetPlanesLista(pacienteId, doctorId, preclinicaId));

        }
    }
}