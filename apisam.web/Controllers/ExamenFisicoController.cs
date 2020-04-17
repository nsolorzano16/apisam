
using apisam.entities;
using apisam.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace apisam.web.Controllers
{
    [EnableCors("Todos")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ExamenFisicoController : ControllerBase
    {
        public IExamenFisico ExamenFisicoRepo;

        public ExamenFisicoController(
            IExamenFisico examenFisicoRepository)
        {
            ExamenFisicoRepo = examenFisicoRepository;
        }


        [Authorize(Roles = "2")]
        [HttpPost("")]
        public IActionResult Add([FromBody] ExamenFisico examenFisico)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = ExamenFisicoRepo.AddExamenFisico(examenFisico);
            if (_resp.Ok) return Ok(examenFisico);
            return BadRequest(_resp);

        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public IActionResult Update([FromBody] ExamenFisico examenFisico)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = ExamenFisicoRepo.UpdateExamenFisico(examenFisico);
            if (_resp.Ok) return Ok(examenFisico);
            return BadRequest(_resp);
        }
    }
}