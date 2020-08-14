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
    public class PlanesController : ControllerBase
    {
        public IPlanes planesRepo;

        public PlanesController(
            IPlanes planesRepository)
        {
            planesRepo = planesRepository;
        }


        [Authorize(Roles = "1")]
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] Planes plan)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await planesRepo.AddPlan(plan);
            if (_resp.Ok) return Ok(plan);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "1")]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] Planes plan)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await planesRepo.UpdatePlan(plan);
            if (_resp.Ok) return Ok(plan);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "1")]
        [HttpGet("")]
        public async Task<IActionResult> GetPlanes()
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            var  _resp = await planesRepo.GetPlanes();
            return Ok(_resp);
          
        }


    }
}
