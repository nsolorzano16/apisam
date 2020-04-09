using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
using apisam.interfaces;
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
        public IEnumerable<Profesion> Get()
        {
            return ProfesionRepo.Profesiones;
        }

        [HttpGet("{id}", Name = "GetProfesion")]
        public IActionResult GetProfesion(int id)
        {
            return Ok(ProfesionRepo.GetProfesionById(id));
        }

        [HttpPost("")]
        public IActionResult Add([FromBody] Profesion profesion)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = ProfesionRepo.Add(profesion);
            if (_resp.Ok) return Ok(profesion);
            return BadRequest(_resp);

        }

        [HttpPut("")]
        public IActionResult Update([FromBody] Profesion profesion)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = ProfesionRepo.Update(profesion);
            if (_resp.Ok) return Ok(profesion);
            return BadRequest(_resp);
        }
    }
}