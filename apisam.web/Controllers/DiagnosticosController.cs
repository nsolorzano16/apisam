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
    public class DiagnosticosController : ControllerBase
    {
        public IDiagnosticos diagnosticosRepo;
        private readonly IMapper _mapper;

        public DiagnosticosController(
            IDiagnosticos diagnosticosRepository, IMapper mapper)
        {
            diagnosticosRepo = diagnosticosRepository;
            _mapper = mapper;
        }


        [Authorize(Roles = "2")]
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] List<Diagnosticos> diagnosticos)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await diagnosticosRepo.AddDiagnosticoLista(diagnosticos);
            if (_resp.Ok) return Ok(diagnosticos);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "2")]
        [HttpPost("agregar", Name = "AddDiagnostico")]
        public async Task<IActionResult> AddDiagnostico([FromBody] Diagnosticos diagnostico)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await diagnosticosRepo.AddDiagnostico(diagnostico);
            if (_resp.Ok) return Ok(diagnostico);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] List<DiagnosticosViewModel> diagnosticos)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            var _diagnosticosMap = _mapper.Map<List<DiagnosticosViewModel>, List<Diagnosticos>>(diagnosticos);
            RespuestaMetodos _resp = await diagnosticosRepo.UpdateDiagnosticoLista(_diagnosticosMap);

            if (_resp.Ok) return Ok(await diagnosticosRepo.GetDiagnosticos(_diagnosticosMap[0].PacienteId,_diagnosticosMap[0].DoctorId,_diagnosticosMap[0].PreclinicaId));
            return BadRequest(new BadRequestError(_resp.Mensaje));

        }

        [Authorize(Roles = "2")]
        [HttpPut("editar", Name = "UpdateDiagnostico")]
        public async Task<IActionResult> UpdateDiagnostico([FromBody] DiagnosticosViewModel diagnostico)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            var _diagnosticoMap = _mapper.Map<DiagnosticosViewModel, Diagnosticos>(diagnostico);
            RespuestaMetodos _resp = await diagnosticosRepo.UpdateDiagnostico(_diagnosticoMap);
            if (_resp.Ok) return Ok(await diagnosticosRepo.GetDiagnostico(diagnostico.DiagnosticoId));
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "2")]
        [HttpPut("desactivar", Name = "DesactivarDiagnostico")]
        public async Task<IActionResult> DesactivarDiagnostico([FromBody] DiagnosticosViewModel diagnostico)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            var _diagnosticoMap = _mapper.Map<DiagnosticosViewModel, Diagnosticos>(diagnostico);
            RespuestaMetodos _resp = await diagnosticosRepo.UpdateDiagnostico(_diagnosticoMap);
            if (_resp.Ok) return Ok(await diagnosticosRepo.GetDiagnostico(diagnostico.DiagnosticoId));
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }


        [Authorize(Roles = "2")]
        [HttpGet("pacienteid/{pacienteId}/doctorid/{doctorId}/preclinicaid/{preclinicaId}", Name = "GetDiagnosticos")]
        public async Task<IActionResult> GetDiagnosticos([FromRoute] int pacienteId, [FromRoute] string doctorId, [FromRoute] int preclinicaId)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            return Ok(await diagnosticosRepo.GetDiagnosticos(pacienteId, doctorId, preclinicaId));

        }
    }
}