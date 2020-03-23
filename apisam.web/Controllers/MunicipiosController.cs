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
        public IEnumerable<Municipio> Get()
        {
            return MunicipiosRepo.Municipios;
        }

        [HttpGet("{id}", Name = "GetMunicipiosByDepartamento")]
        public IActionResult GetMunicipiosByDepartamento(int id)
        {
            return Ok(MunicipiosRepo.GetMunicipiosByDepartamento(id));
        }
    }
}