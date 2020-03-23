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
    public class GrupoSanguineoController : ControllerBase
    {

        public IGrupoSanguineo GrupoSanguineoRepo;

        public GrupoSanguineoController(IGrupoSanguineo grupoSanguineoRepository)
        {
            GrupoSanguineoRepo = grupoSanguineoRepository;
        }


        [HttpGet("")]
        public IEnumerable<GrupoSanguineo> Get()
        {
            return GrupoSanguineoRepo.GruposSanguineos;
        }

        [HttpGet("{id}", Name = "GetGrupoSanguineo")]
        public IActionResult GetGrupoSanguineo(int id)
        {
            return Ok(GrupoSanguineoRepo.GetGrupoSanguineoById(id));
        }
    }
}