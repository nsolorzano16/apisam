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
    public class ViaAdministracionController : ControllerBase
    {
        public IViaAdministracion ViaRepo;

        public ViaAdministracionController(IViaAdministracion viarepository)
        {
            ViaRepo = viarepository;
        }


        [HttpGet("")]
        public IEnumerable<ViaAdministracion> Get()
        {
            return ViaRepo.ListaViaAdministracion;
        }
    }
}