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
        public IEnumerable<Pais> Get()
        {
            return PaisRepo.Paises;
        }

        [HttpGet("{id}", Name = "GetPaisById")]
        public IActionResult GetPaisById(int id)
        {
            return Ok(PaisRepo.GetPaisById(id));
        }
    }
}