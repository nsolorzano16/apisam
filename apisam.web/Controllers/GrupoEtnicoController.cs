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
        public IEnumerable<GrupoEtnico> Get()
        {
            return GrupoEtnicoRepo.GruposEtnicos;
        }

        [HttpGet("{id}", Name = "GetGrupoEtnico")]
        public IActionResult GetGrupoEtnico(int id)
        {
            return Ok(GrupoEtnicoRepo.GrupoEtnicoById(id));
        }

        [HttpPost("")]
        public IActionResult Add([FromBody] GrupoEtnico grupoEtnico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (GrupoEtnicoRepo.Add(grupoEtnico)) return Ok(grupoEtnico);
            return BadRequest("error salvando grupoEtnico");

        }

        [HttpPut("")]
        public IActionResult Update([FromBody] GrupoEtnico grupoEtnico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (GrupoEtnicoRepo.Update(grupoEtnico)) return Ok(grupoEtnico);
            return BadRequest("error actualizando grupoEtnico");
        }
    }
}