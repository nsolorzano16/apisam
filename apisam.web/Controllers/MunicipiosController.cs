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
    public class MunicipiosController : ControllerBase
    {
        public IMunicipio MunicipiosRepo;

        public MunicipiosController(IMunicipio municipiosRepository)
        {
            MunicipiosRepo = municipiosRepository;
        }


        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await MunicipiosRepo.Municipios());
        }

        [HttpGet("{id}", Name = "GetMunicipiosByDepartamento")]
        public async Task<IActionResult> GetMunicipiosByDepartamento(int id)
        {
            return Ok(await MunicipiosRepo.GetMunicipiosByDepartamento(id));
        }
    }
}