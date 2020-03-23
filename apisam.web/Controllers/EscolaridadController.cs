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
    public class EscolaridadController : ControllerBase
    {

        public IEscolaridad EscolaridadRepo;

        public EscolaridadController(IEscolaridad escolaridadRepository)
        {
            EscolaridadRepo = escolaridadRepository;
        }


        [HttpGet("")]
        public IEnumerable<Escolaridad> Get()
        {
            return EscolaridadRepo.Escolaridades;
        }

        [HttpGet("{id}", Name = "GetEscolaridad")]
        public IActionResult GetEscolaridad(int id)
        {
            return Ok(EscolaridadRepo.GetEscolaridadById(id));
        }

        [HttpPost("")]
        public IActionResult Add([FromBody] Escolaridad escolaridad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (EscolaridadRepo.Add(escolaridad)) return Ok(escolaridad);
            return BadRequest("error salvando escolaridad");

        }

        [HttpPut("")]
        public IActionResult Update([FromBody] Escolaridad escolaridad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (EscolaridadRepo.Update(escolaridad)) return Ok(escolaridad);
            return BadRequest("error actualizando escolaridad");
        }
    }
}