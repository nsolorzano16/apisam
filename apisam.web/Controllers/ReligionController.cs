using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
using apisam.interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apisam.web.Controllers
{
    [EnableCors("Todos")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReligionController : ControllerBase
    {

        public IReligion ReligionRepo;

        public ReligionController(IReligion religionRepository)
        {
            ReligionRepo = religionRepository;
        }


        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await ReligionRepo.Religiones());
        }

        [HttpGet("{id}", Name = "GetReligion")]
        public async Task<IActionResult> GetReligion(int id)
        {
            return Ok(await ReligionRepo.GetReligionById(id));
        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] Religion religion)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = await ReligionRepo.Add(religion);
            if (_resp.Ok) return Ok(religion);
            return BadRequest(_resp);

        }

        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] Religion religion)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = await ReligionRepo.Update(religion);
            if (_resp.Ok) return Ok(religion);
            return BadRequest(_resp);
        }
    }
}