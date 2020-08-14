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
    public class DashboardController : ControllerBase
    {
        public IDashboard dashboardRepo;

        public DashboardController(
            IDashboard dashboardRepository)
        {
            dashboardRepo = dashboardRepository;
        }

        [Authorize(Roles = "1,2,3")]
        [HttpGet("totalpacientesaniomes/username/{username}", Name = "GetPacientesTotalAnioMes")]
        public async Task<IActionResult> GetPacientesTotalAnioMes([FromRoute] string username)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await dashboardRepo.GetPacientesTotalAnioMes(username));

        }
    }
}
