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
    public class PreclinicaController : ControllerBase
    {

        public IPreclinica PreclinicaRepo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public PreclinicaController(IPreclinica preclinicaRepository, IConfiguration config, IMapper mapper)
        {
            _config = config;
            PreclinicaRepo = preclinicaRepository;
            _mapper = mapper;
        }


        [Authorize(Roles = "2,3")]
        [HttpGet("page/{pageNo}/limit/{limit}/doctorId/{doctorId}", Name = "GetPreclinicasSinAtender")]
        public IActionResult GetPreclinicasSinAtender(int pageNo, int limit, int doctorId)
        {
            try
            {
                var _pageResponse = PreclinicaRepo.GetPreclinicasSinAtender(pageNo, limit, doctorId);
                return Ok(_pageResponse);
            }
            catch (Exception e)
            {

                var a = e.Message;
            }

            return BadRequest("no se han podido obtener registros");
        }



        [Authorize(Roles = "2,3")]
        [HttpPost("")]
        public IActionResult Add([FromBody] Preclinica preclinica)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (PreclinicaRepo.AddPreclinica(preclinica)) return Ok(preclinica);
            return BadRequest();
        }

        [Authorize(Roles = "2,3")]
        [HttpPut("")]
        public IActionResult Update([FromBody] PreclinicaViewModel preclinica)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var _pre = _mapper.Map<PreclinicaViewModel, Preclinica>(preclinica);
            if (PreclinicaRepo.UpdatePreclinica(_pre))
            {
                var preclinicaRetorno = PreclinicaRepo.GetInfoPreclinica(_pre.PreclinicaId);
                return Ok(preclinicaRetorno);
            }
            return BadRequest();
        }


    }
}