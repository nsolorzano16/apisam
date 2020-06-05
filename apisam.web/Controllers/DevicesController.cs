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
    public class DevicesController : ControllerBase
    {

        public IDevices DevicesRepo;

        public DevicesController(
            IDevices devices)
        {
            DevicesRepo = devices;
        }

        [Authorize(Roles = "2,3")]
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] Devices device)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await DevicesRepo.AddDevice(device);
            if (_resp.Ok) return Ok(device);
            return BadRequest(new BadRequestError(_resp.Mensaje));

        }
    }
}