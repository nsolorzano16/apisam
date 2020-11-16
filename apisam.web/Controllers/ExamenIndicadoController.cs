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

namespace apisam.web.Controllers
{
    [EnableCors("Todos")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ExamenIndicadoController : ControllerBase
    {
        public IExamenIndicado ExamenRepo;
        private readonly IMapper _mapper;

        public ExamenIndicadoController(IExamenIndicado examenrepository, IMapper mapper)
        {
            ExamenRepo = examenrepository;
            _mapper = mapper;

        }

        [Authorize(Roles = "2")]
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] ExamenIndicado examen)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await ExamenRepo.AddExamenIndicado(examen);
            if (_resp.Ok) return Ok(examen);
            return BadRequest(new BadRequestError(_resp.Mensaje));

        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] ExamenIndicado examen)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await ExamenRepo.UpdateExamenIndicado(examen);
            if (_resp.Ok) return Ok(examen);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "2")]
        [HttpPut("edit")]
        public async Task<IActionResult> UpdateExamen([FromBody] ExamenesIndicadosViewModel examen)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            var ex = _mapper.Map<ExamenesIndicadosViewModel, ExamenIndicado>(examen);
            RespuestaMetodos _resp = await ExamenRepo.UpdateExamenIndicado(ex);
            if (_resp.Ok) return Ok(await ExamenRepo.GetInfoExamenIndicado(ex.ExamenIndicadoId));
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "2")]
        [HttpGet("id/{examenId}", Name = "GetExamenIndicadoById")]
        public async Task<IActionResult> GetExamenIndicadoById([FromRoute] int examenId)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            return Ok(await ExamenRepo.GetExamenIndicadoById(examenId));



        }

        [Authorize(Roles = "2")]
        [HttpGet("listar/pacienteid/{pacienteId}/doctorid/{doctorId}/preclinicaid/{preclinicaId}", Name = "GetExamenes")]
        public async Task<IActionResult> GetExamenes([FromRoute] int pacienteId, [FromRoute] string doctorId, [FromRoute] int preclinicaId)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            return Ok(await ExamenRepo.GetExamenes(pacienteId, doctorId, preclinicaId));
        }

        [Authorize(Roles = "2")]
        [HttpGet("detail/pacienteid/{pacienteId}/doctorid/{doctorId}/preclinicaid/{preclinicaId}", Name = "GetDetalleExamenesIndicados")]
        public async Task<IActionResult> GetDetalleExamenesIndicados([FromRoute] int pacienteId,
            [FromRoute] string doctorId, [FromRoute] int preclinicaId)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            return Ok(await ExamenRepo.GetDetalleExamenesIndicados(pacienteId, doctorId, preclinicaId));
        }
    }
}