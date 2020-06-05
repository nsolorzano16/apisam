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
    public class EscolaridadController : ControllerBase
    {

        public IEscolaridad EscolaridadRepo;

        public EscolaridadController(IEscolaridad escolaridadRepository)
        {
            EscolaridadRepo = escolaridadRepository;
        }


        [HttpGet("")]
        public async Task<IActionResult> GetEscolaridades()
        {
            return Ok(await EscolaridadRepo.Escolaridades());
        }

        [HttpGet("{id}", Name = "GetEscolaridad")]
        public async Task<IActionResult> GetEscolaridad(int id)
        {
            return Ok(await EscolaridadRepo.GetEscolaridadById(id));
        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] Escolaridad escolaridad)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await EscolaridadRepo.Add(escolaridad);
            if (_resp.Ok) return Ok(escolaridad);
            return BadRequest(new BadRequestError(_resp.Mensaje));

        }

        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] Escolaridad escolaridad)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await EscolaridadRepo.Update(escolaridad);
            if (_resp.Ok) return Ok(escolaridad);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }
    }
}