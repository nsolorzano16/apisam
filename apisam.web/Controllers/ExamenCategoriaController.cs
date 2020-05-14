using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
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
    [Authorize(Roles = "2,3")]
    [ApiController]
    public class ExamenCategoriaController : ControllerBase
    {
        public IExamenCategoria CategoriasRepo;

        public ExamenCategoriaController(IExamenCategoria categoria)
        {
            CategoriasRepo = categoria;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetCategoriasExamenes()
        {
            return Ok(await CategoriasRepo.CategoriasExamenes());
        }

        [HttpGet("{id}", Name = "GetExamenCategoriaById")]
        public async Task<IActionResult> GetExamenCategoriaById(int id)
        {
            return Ok(await CategoriasRepo.GetExamenById(id));
        }
    }
}