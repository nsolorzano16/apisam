using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
using apisam.interfaces;
using apisam.web.HandleErrors;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apisam.web.Controllers
{
    [EnableCors("Todos")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesionController : ControllerBase
    {

        public IProfesion ProfesionRepo;

        public ProfesionController(IProfesion profesionRepository)
        {
            ProfesionRepo = profesionRepository;
        }


        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await ProfesionRepo.Profesiones());
        }

        [HttpGet("{id}", Name = "GetProfesion")]
        public async Task<IActionResult> GetProfesion(int id)
        {
            return Ok(await ProfesionRepo.GetProfesionById(id));
        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] Profesion profesion)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await ProfesionRepo.Add(profesion);
            if (_resp.Ok) return Ok(profesion);
            return BadRequest(new BadRequestError(_resp.Mensaje));

        }

        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] Profesion profesion)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await ProfesionRepo.Update(profesion);
            if (_resp.Ok) return Ok(profesion);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }
    }
}