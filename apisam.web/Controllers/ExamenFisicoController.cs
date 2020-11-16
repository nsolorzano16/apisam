
using System.Threading.Tasks;
using apisam.entities;
using apisam.interfaces;
using apisam.web.HandleErrors;
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
        public async Task<IActionResult> Add([FromBody] ExamenFisico examenFisico)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await ExamenFisicoRepo.AddExamenFisico(examenFisico);
            if (_resp.Ok) return Ok(examenFisico);
            return BadRequest(new BadRequestError(_resp.Mensaje));

        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] ExamenFisico examenFisico)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await ExamenFisicoRepo.UpdateExamenFisico(examenFisico);
            if (_resp.Ok) return Ok(examenFisico);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "2")]
        [HttpGet("pacienteid/{pacienteId}/doctorid/{doctorId}/preclinicaid/{preclinicaId}", Name = "GetExamenFisico")]
        public async Task<IActionResult> GetExamenFisico([FromRoute] int pacienteId, [FromRoute] string doctorId, [FromRoute] int preclinicaId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await ExamenFisicoRepo.GetExamenFisico(pacienteId, doctorId, preclinicaId));

        }
    }
}