using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;
using apisam.interfaces;
using AutoMapper;
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
    public class PacientesController : ControllerBase
    {
        public IPaciente PacienteRepo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public PacientesController(IPaciente pacienterepository, IConfiguration config, IMapper mapper)
        {
            _config = config;
            PacienteRepo = pacienterepository;
            _mapper = mapper;
        }

        [Authorize(Roles = "2,3")]
        [HttpPost("")]
        public IActionResult Add([FromBody] Paciente paciente)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (PacienteRepo.AddPaciente(paciente)) return Ok(paciente);
            return BadRequest("error salvando paciente o el paciente ya existe");
        }

        [Authorize(Roles = "2,3")]
        [HttpPut("")]
        public IActionResult Update([FromBody] PacientesViewModel paciente)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var _pac = _mapper.Map<PacientesViewModel, Paciente>(paciente);
            if (PacienteRepo.UpdatePaciente(_pac)) return Ok(paciente);
            return BadRequest("error editando paciente");
        }


        [Authorize(Roles = "2,3")]
        [HttpGet("{id}", Name = "GetUserById")]
        public IActionResult GetUserById([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(PacienteRepo.GetPacienteById(id));
        }

        [Authorize(Roles = "2,3")]
        [HttpGet("identificacion/{identificacion}", Name = "GetPacienteByIdentificacion")]
        public IActionResult GetPacienteByIdentificacion([FromRoute]string identificacion)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(PacienteRepo.GetPacienteByIdentificacion(identificacion));
        }


        [Authorize(Roles = "2,3")]
        [HttpGet("page/{pageNo}/limit/{limit}/doctor/{doctorId}", Name = "GetPacientes")]
        public IActionResult GetPacientes([FromRoute] int pageNo, [FromRoute] int limit, [FromQuery] string filter, [FromRoute] int doctorId)
        {
            try
            {
                var _pageResponse = PacienteRepo.GetPacientes(pageNo, limit, filter, doctorId);
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