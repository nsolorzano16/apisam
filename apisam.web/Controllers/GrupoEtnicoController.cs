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
    public class GrupoEtnicoController : ControllerBase
    {

        public IGrupoEtnico GrupoEtnicoRepo;

        public GrupoEtnicoController(IGrupoEtnico grupoEtnicoRepository)
        {
            GrupoEtnicoRepo = grupoEtnicoRepository;
        }


        [HttpGet("")]
        public async Task<IActionResult> GetGruposEtnicos()
        {
            return Ok(await GrupoEtnicoRepo.GruposEtnicos());
        }

        [HttpGet("{id}", Name = "GetGrupoEtnico")]
        public async Task<IActionResult> GetGrupoEtnico(int id)
        {
            return Ok(await GrupoEtnicoRepo.GetGrupoEtnicoById(id));
        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] GrupoEtnico grupoEtnico)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = await GrupoEtnicoRepo.Add(grupoEtnico);
            if (_resp.Ok) return Ok(grupoEtnico);
            return BadRequest(_resp);

        }

        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] GrupoEtnico grupoEtnico)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = await GrupoEtnicoRepo.Update(grupoEtnico);
            if (_resp.Ok) return Ok(grupoEtnico);
            return BadRequest(_resp);
        }
    }
}