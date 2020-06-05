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
    public class HabitosController : ControllerBase
    {
        public IHabitos HabitosRepo;
        public HabitosController(IHabitos habitosrepository)
        {
            HabitosRepo = habitosrepository;
        }

        [Authorize(Roles = "2")]
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] Habitos habitos)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await HabitosRepo.AddAHabito(habitos);
            if (_resp.Ok) return Ok(habitos);
            return BadRequest(new BadRequestError(_resp.Mensaje));

        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] Habitos habitos)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await HabitosRepo.UpdateAHabito(habitos);
            if (_resp.Ok) return Ok(habitos);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }


        [Authorize(Roles = "2")]
        [HttpGet("pacienteId/{pacienteId}/doctorId/{doctorId}", Name = "GetHabito")]
        public async Task<IActionResult> GetHabito([FromRoute] int pacienteId, int doctorId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await HabitosRepo.GetHabito(pacienteId, doctorId));


        }


    }
}