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
    public class PreclinicaController : ControllerBase
    {

        public IPreclinica PreclinicaRepo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public PreclinicaController(IPreclinica preclinicaRepository,
            IConfiguration config, IMapper mapper)
        {
            _config = config;
            PreclinicaRepo = preclinicaRepository;
            _mapper = mapper;
        }



        [Authorize(Roles = "2,3")]
        [HttpGet("page/{pageNo}/limit/{limit}/doctorId/{doctorId}/atendida/{atendida}",
            Name = "GetPreclinicasSinAtender")]
        public async Task<IActionResult> GetPreclinicasSinAtender(int pageNo, int limit, string doctorId, int atendida)
        {
            string a;
            try
            {

                return Ok(await PreclinicaRepo.GetPreclinicasSinAtender(pageNo, limit, doctorId, atendida));
            }
            catch (Exception e)
            {

                a = e.Message;
            }

            return BadRequest(a);
        }



        [Authorize(Roles = "2,3")]
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] Preclinica preclinica)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await PreclinicaRepo.AddPreclinica(preclinica);
            if (_resp.Ok) return Ok(preclinica);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "2,3")]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] PreclinicaViewModel preclinica)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            var _pre = _mapper.Map<PreclinicaViewModel, Preclinica>(preclinica);
            RespuestaMetodos _resp = await PreclinicaRepo.UpdatePreclinica(_pre);
            if (_resp.Ok) return Ok(await PreclinicaRepo.GetInfoPreclinica(_pre.PreclinicaId));
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }


    }
}