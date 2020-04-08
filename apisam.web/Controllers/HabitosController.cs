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
    public class HabitosController : ControllerBase
    {
        public IHabitos HabitosRepo;
        public HabitosController(IHabitos habitosrepository)
        {
            HabitosRepo = habitosrepository;
        }

        [Authorize(Roles = "2,3")]
        [HttpPost("")]
        public IActionResult Add([FromBody] Habitos habitos)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (HabitosRepo.AddAHabito(habitos)) return Ok(habitos);
            return BadRequest("error salvando habitos");
        }

        [Authorize(Roles = "2,3")]
        [HttpPut("")]
        public IActionResult Update([FromBody] Habitos habitos)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (HabitosRepo.UpdateAHabito(habitos)) return Ok(habitos);
            return BadRequest("error salvando habitos");
        }
    }
}