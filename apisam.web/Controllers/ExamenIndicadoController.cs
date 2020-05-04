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
    public class ExamenIndicadoController : ControllerBase
    {
        public IExamenIndicado ExamenRepo;

        public ExamenIndicadoController(IExamenIndicado examenrepository)
        {
            ExamenRepo = examenrepository;

        }

        [Authorize(Roles = "2")]
        [HttpPost("")]
        public IActionResult Add([FromBody] ExamenIndicado examen)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = ExamenRepo.AddExamenIndicado(examen);
            if (_resp.Ok) return Ok(examen);
            return BadRequest(_resp);

        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public IActionResult Update([FromBody] ExamenIndicado examen)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = ExamenRepo.UpdateExamenIndicado(examen);
            if (_resp.Ok) return Ok(examen);
            return BadRequest(_resp);
        }

        [Authorize(Roles = "2")]
        [HttpGet("id/{examenId}", Name = "GetExamenIndicadoById")]
        public IActionResult GetExamenIndicadoById([FromRoute] int examenId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var _examenIndicado = ExamenRepo.GetExamenIndicadoById(examenId);
            if (_examenIndicado != null) return Ok(_examenIndicado);
            return Ok();

        }
    }
}