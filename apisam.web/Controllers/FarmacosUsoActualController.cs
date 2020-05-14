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
    public class FarmacosUsoActualController : ControllerBase
    {
        public IFarmacosUsoActual farmacosRepo;

        public FarmacosUsoActualController(
            IFarmacosUsoActual farmacosRepository)
        {
            farmacosRepo = farmacosRepository;
        }


        [Authorize(Roles = "2")]
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] List<FarmacosUsoActual> farmacos)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = await farmacosRepo.AddFarmacoLista(farmacos);
            if (_resp.Ok) return Ok(farmacos);
            return BadRequest(_resp);
        }

        [Authorize(Roles = "2")]
        [HttpPost("agregar", Name = "AddFarmaco")]
        public async Task<IActionResult> AddFarmaco([FromBody] FarmacosUsoActual farmaco)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = await farmacosRepo.AddFarmaco(farmaco);
            if (_resp.Ok) return Ok(farmaco);
            return BadRequest(_resp);
        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] List<FarmacosUsoActual> farmacos)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = await farmacosRepo.UpdateFarmacoLista(farmacos);
            if (_resp.Ok) return Ok(farmacos);
            return BadRequest(_resp);
        }

        [Authorize(Roles = "2")]
        [HttpPut("editar", Name = "UpdateFarmaco")]
        public async Task<IActionResult> UpdateFarmaco([FromBody] FarmacosUsoActual farmaco)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = await farmacosRepo.UpdateFarmaco(farmaco);
            if (_resp.Ok) return Ok(farmaco);
            return BadRequest(_resp);
        }

        [Authorize(Roles = "2")]
        [HttpPut("desactivar", Name = "Desactivar")]
        public async Task<IActionResult> Desactivar([FromBody] FarmacosUsoActual farmaco)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = await farmacosRepo.UpdateFarmaco(farmaco);
            if (_resp.Ok) return Ok(farmaco);
            return BadRequest(_resp);
        }


        [Authorize(Roles = "2")]
        [HttpGet("pacienteId/{pacienteId}/doctorId/{doctorId}", Name = "GetFarmacos")]
        public async Task<IActionResult> GetFarmacos([FromRoute] int pacienteId, int doctorId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var _farmacos = await farmacosRepo.GetFarmacos(pacienteId, doctorId);
            if (_farmacos != null) return Ok(_farmacos);
            return Ok();

        }
    }
}