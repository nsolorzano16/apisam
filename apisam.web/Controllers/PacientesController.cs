using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;
using apisam.interfaces;
using apisam.web.HandleErrors;
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
    [Authorize(Roles = "2,3")]
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
        public async Task<IActionResult> Add([FromBody] Paciente paciente)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await PacienteRepo.AddPaciente(paciente);
            if (_resp.Ok) return Ok(paciente);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "2,3")]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] PacientesViewModel paciente)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            var _pac = _mapper.Map<PacientesViewModel, Paciente>(paciente);
            RespuestaMetodos _resp = await PacienteRepo.UpdatePaciente(_pac);
            if (_resp.Ok)
            {
                var _pacienteRetorno = await PacienteRepo.GetInfoPaciente(paciente.PacienteId);
                return Ok(_pacienteRetorno);
            }

            return BadRequest(new BadRequestError(_resp.Mensaje));
        }


        [Authorize(Roles = "2,3")]
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await PacienteRepo.GetPacienteById(id));
        }

        [Authorize(Roles = "2,3")]
        [HttpGet("identificacion/{identificacion}", Name = "GetPacienteByIdentificacion")]
        public async Task<IActionResult> GetPacienteByIdentificacion([FromRoute] string identificacion)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await PacienteRepo.GetPacienteByIdentificacion(identificacion));
        }


        [Authorize(Roles = "2,3")]
        [HttpGet("page/{pageNo}/limit/{limit}", Name = "GetPacientes")]
        public async Task<IActionResult> GetPacientes([FromRoute] int pageNo, [FromRoute]
        int limit, [FromQuery] string filter)
        {
            try
            {
                var _pageResponse = await PacienteRepo.GetPacientes(pageNo, limit, filter);
                return Ok(_pageResponse);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);

            }

        }
    }
}