using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Authorize(Roles = "1")]
    public class AuditoriaController : ControllerBase
    {
        public IAuditoria AuditoriaRepo;

        public AuditoriaController(IAuditoria auditoriarepository)
        {
            AuditoriaRepo = auditoriarepository;
        }


        [HttpGet("page/{pageNo}/limit/{limit}", Name = "GetAuditorias")]
        public async Task<IActionResult> GetAuditorias([FromRoute] int pageNo, [FromRoute] int limit, [FromQuery] string filter)
        {
            try
            {
                var _pageResponse = await AuditoriaRepo.GetAuditorias(pageNo, limit, filter);
                return Ok(_pageResponse);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }
    }
}