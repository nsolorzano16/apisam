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
    public class ExamenDetalleController : ControllerBase
    {

        public IExamenDetalle DetalleRepo;

        public ExamenDetalleController(IExamenDetalle detallerepository)
        {
            DetalleRepo = detallerepository;
        }

        [HttpGet("examentipoid/{examenTipoId}/examencategoriaid/{examenCategoriaId}", Name = "GetDetalleExamenes")]
        public IActionResult GetDetalleExamenes([FromRoute] int examenTipoId, [FromRoute] int examenCategoriaId)
        {
            return Ok(DetalleRepo.GetDetalleExamenes(examenTipoId, examenCategoriaId));
        }

        [HttpGet("{id}", Name = "GetExamenDetalleById")]
        public IActionResult GetExamenDetalleById([FromRoute] int id)
        {
            return Ok(DetalleRepo.GetExamenDetalleById(id));
        }
    }
}