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
        public IActionResult Add([FromBody] List<FarmacosUsoActual> farmacos)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = farmacosRepo.AddFarmacoLista(farmacos);
            if (_resp.Ok) return Ok(farmacos);
            return BadRequest(_resp);
        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public IActionResult Update([FromBody] List<FarmacosUsoActual> farmacos)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = farmacosRepo.UpdateFarmacoLista(farmacos);
            if (_resp.Ok) return Ok(farmacos);
            return BadRequest(_resp);
        }

        [Authorize(Roles = "2")]
        [HttpPut("desactivar", Name = "Desactivar")]
        public IActionResult Desactivar([FromBody] FarmacosUsoActual farmaco)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = farmacosRepo.UpdateFarmaco(farmaco);
            if (_resp.Ok) return Ok(farmaco);
            return BadRequest(_resp);
        }
    }
}