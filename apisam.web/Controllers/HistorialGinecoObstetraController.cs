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
        public IActionResult Add([FromBody] HistorialGinecoObstetra historial)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = HistorialRepo.AddAHistorial(historial);
            if (_resp.Ok) return Ok(historial);
            return BadRequest(_resp);
        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public IActionResult Update([FromBody] HistorialGinecoObstetra historial)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = HistorialRepo.UpdateAHistorial(historial);
            if (_resp.Ok) return Ok(historial);
            return BadRequest(_resp);
        }


    }
}