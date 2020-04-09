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
        public IEnumerable<Religion> Get()
        {
            return ReligionRepo.Religiones;
        }

        [HttpGet("{id}", Name = "GetReligion")]
        public IActionResult GetReligion(int id)
        {
            return Ok(ReligionRepo.GetReligionById(id));
        }

        [HttpPost("")]
        public IActionResult Add([FromBody] Religion religion)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = ReligionRepo.Add(religion);
            if (_resp.Ok) return Ok(religion);
            return BadRequest(_resp);

        }

        [HttpPut("")]
        public IActionResult Update([FromBody] Religion religion)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RespuestaMetodos _resp = ReligionRepo.Update(religion);
            if (_resp.Ok) return Ok(religion);
            return BadRequest(_resp);
        }
    }
}