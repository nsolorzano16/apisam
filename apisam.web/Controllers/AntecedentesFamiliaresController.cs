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
    public class AntecedentesFamiliaresController : ControllerBase
    {

        public IAntecedentesFamiliaresPersonales AntecedentesRepo;

        public AntecedentesFamiliaresController(
            IAntecedentesFamiliaresPersonales antecedentesRepository)
        {
            AntecedentesRepo = antecedentesRepository;
        }


        [Authorize(Roles = "2")]
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] AntecedentesFamiliaresPersonales antecedentes)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await AntecedentesRepo.AddAntecedentes(antecedentes);
            if (_resp.Ok) return Ok(antecedentes);
            return BadRequest(new BadRequestError(_resp.Mensaje));

        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] AntecedentesFamiliaresPersonales antecedentes)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await AntecedentesRepo.UpdateAntecedentes(antecedentes);
            if (_resp.Ok) return Ok(antecedentes);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "2")]
        [HttpGet("pacienteId/{pacienteId}", Name = "GetAntecedente")]
        public async Task<IActionResult> GetAntecedente([FromRoute] int pacienteId)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            return Ok(await AntecedentesRepo.GetAntecedente(pacienteId));
        }





    }
}