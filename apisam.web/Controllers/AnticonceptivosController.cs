using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisam.interfaces;
using apisam.repos;
using apisam.web.HandleErrors;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apisam.web.Controllers
{
    [EnableCors("Todos")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AnticonceptivosController : ControllerBase
    {

        public IAnticonceptivos AnticonceptivosRepo;

        public AnticonceptivosController(IAnticonceptivos anticonceptivosrep)
        {
            AnticonceptivosRepo = anticonceptivosrep;
        }



        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            return Ok(await AnticonceptivosRepo.GetAnticonceptivos());

        }


    }

}