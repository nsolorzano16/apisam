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
    public class DepartamentosController : ControllerBase
    {

        public IDepartamento DepartamentosRepo;

        public DepartamentosController(IDepartamento departamentosRepository)
        {
            DepartamentosRepo = departamentosRepository;
        }


        [HttpGet("")]
        public async Task<IActionResult> GetDepartamentos()
        {

            return Ok(await DepartamentosRepo.Departamentos());
        }

        [HttpGet("{id}", Name = "GetDepartamento")]
        public async Task<IActionResult> GetDepartamento(int id)
        {
            return Ok(await DepartamentosRepo.GetDepartamentoById(id));
        }
    }
}