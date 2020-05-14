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
    public class PaisController : ControllerBase
    {
        public IPais PaisRepo;

        public PaisController(IPais paisRepository)
        {
            PaisRepo = paisRepository;
        }


        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await PaisRepo.Paises());
        }

        [HttpGet("{id}", Name = "GetPaisById")]
        public async Task<IActionResult> GetPaisById(int id)
        {
            return Ok(await PaisRepo.GetPaisById(id));
        }
    }
}