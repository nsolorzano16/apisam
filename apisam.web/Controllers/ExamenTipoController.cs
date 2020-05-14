using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Authorize(Roles = "2,3")]
    [ApiController]
    public class ExamenTipoController : ControllerBase
    {
        public IExamenTipo ExamenTipoRepo;
        public ExamenTipoController(IExamenTipo tipo)
        {
            ExamenTipoRepo = tipo;
        }

        [HttpGet("categoriaid/{categoriaId}", Name = "GetTipoExamenes")]
        public async Task<IActionResult> GetTipoExamenes([FromRoute] int categoriaId)
        {
            return Ok(await ExamenTipoRepo.GetTipoExamenes(categoriaId));
        }

        [HttpGet("{id}", Name = "GetExamenTipoById")]
        public async Task<IActionResult> GetExamenTipoById([FromRoute] int id)
        {
            return Ok(await ExamenTipoRepo.GetExamenTipoById(id));
        }
    }
}