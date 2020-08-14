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
    [Authorize(Roles = "2")]
    public class CieController : ControllerBase
    {

        public ICie CieRepo;
       
        public CieController(ICie cierepository)
        {
            CieRepo = cierepository;
        }

   
        [HttpGet("page/{pageNo}/limit/{limit}", Name = "GetEnfermedades")]
        public async Task<IActionResult> GetEnfermedades([FromRoute] int pageNo, [FromRoute]
        int limit, [FromQuery] string filter)
        {
            return Ok(await CieRepo.GetEnfermedades(pageNo, limit, filter));
        }
    }
}
