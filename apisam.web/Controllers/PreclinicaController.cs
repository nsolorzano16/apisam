using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisam.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace apisam.web.Controllers
{
    [EnableCors("Todos")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PreclinicaController : ControllerBase
    {

        public IPreclinica PreclinicaRepo;
        private readonly IConfiguration _config;

        public PreclinicaController(IPreclinica preclinicaRepository, IConfiguration config)
        {
            _config = config;
            PreclinicaRepo = preclinicaRepository;
        }


        [Authorize(Roles = "2,3")]
        [HttpGet("page/{pageNo}/limit/{limit}/doctorId/{doctorId}", Name = "GetPreclinicasSinAtender")]
        public IActionResult GetPreclinicasSinAtender(int pageNo, int limit, int doctorId)
        {
            try
            {
                var _pageResponse = PreclinicaRepo.GetPreclinicasSinAtender(pageNo, limit, doctorId);
                return Ok(_pageResponse);
            }
            catch (Exception e)
            {

                var a = e.Message;
            }

            return BadRequest("no se han podido obtener registros");
        }
    }
}