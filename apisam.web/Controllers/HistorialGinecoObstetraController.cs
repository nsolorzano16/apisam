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
    public class HistorialGinecoObstetraController : ControllerBase
    {
        public IHistorialGinecoObstetra HistorialRepo;

        public HistorialGinecoObstetraController(
            IHistorialGinecoObstetra historalGinecoRepo)
        {
            HistorialRepo = historalGinecoRepo;
        }


        [Authorize(Roles = "2")]
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] HistorialGinecoObstetra historial)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await HistorialRepo.AddAHistorial(historial);
            if (_resp.Ok) return Ok(historial);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] HistorialGinecoObstetra historial)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await HistorialRepo.UpdateAHistorial(historial);
            if (_resp.Ok) return Ok(historial);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }



        [Authorize(Roles = "2")]
        [HttpGet("pacienteId/{pacienteId}", Name = "GetHistorialGinecoObstetra")]
        public async Task<IActionResult> GetHistorialGinecoObstetra([FromRoute] int pacienteId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await HistorialRepo.GetHistorial(pacienteId));


        }

        [Authorize(Roles = "2")]
        [HttpGet("detalle/pacienteId/{pacienteId}", Name = "GetDetalleHistorial")]
        public async Task<IActionResult> GetDetalleHistorial([FromRoute] int pacienteId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await HistorialRepo.GetDetalleHistorial(pacienteId));


        }


    }
}