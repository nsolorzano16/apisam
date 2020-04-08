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
    public class AntecedentesFamiliaresController : ControllerBase
    {

        public IAntecedentesFamiliaresPersonales AntecedentesRepo;

        public AntecedentesFamiliaresController(
            IAntecedentesFamiliaresPersonales antecedentesRepository)
        {
            AntecedentesRepo = antecedentesRepository;
        }


        [Authorize(Roles = "2,3")]
        [HttpPost("")]
        public IActionResult Add([FromBody] AntecedentesFamiliaresPersonales antecedentes)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (AntecedentesRepo.AddAntecedentes(antecedentes)) return Ok(antecedentes);
            return BadRequest("error salvando antecedentes");
        }

        [Authorize(Roles = "2,3")]
        [HttpPut("")]
        public IActionResult Update([FromBody] AntecedentesFamiliaresPersonales antecedentes)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (AntecedentesRepo.UpdateAntecedentes(antecedentes)) return Ok(antecedentes);
            return BadRequest("error salvando antecedentes");
        }


    }
}