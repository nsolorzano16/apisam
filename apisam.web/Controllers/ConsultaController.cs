//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using apisam.entities.ViewModels;
//using apisam.interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;

//namespace apisam.web.Controllers
//{
//    [EnableCors("Todos")]
//    [Produces("application/json")]
//    //[Route("api/[controller]")]
//    [ApiController]
//    public class ConsultaController : ControllerBase
//    {
//        public IAntecedentesFamiliaresPersonales AntecedentesRepo;
//        public IDiagnosticos DiagnosticosRepo;
//        public IExamenFisico ExamenFisicoRepo;
//        public IExamenFisicoGinecologico ExamenFisicoGinecologicoRepo;
//        public IFarmacosUsoActual FarmacosRepo;
//        public IHabitos HabitosRepo;
//        public IHistorialGinecoObstetra HistorialRepo;
//        public IPreclinica PreclinicaRepo;
//        public INotas NotasRepo;
//        private readonly IConfiguration _config;

//        public ConsultaController(IAntecedentesFamiliaresPersonales antecedentesRepository,
//            IDiagnosticos diagnosticosRepository,
//            IExamenFisico examenFisicoRepository,
//            IExamenFisicoGinecologico examenFisicoGinecologicoRepository,
//            IFarmacosUsoActual farmacosUsoActualRepository,
//            IHabitos habitosRepository,
//            IHistorialGinecoObstetra historialRepository,
//            IPreclinica preclinicaRepository,
//            INotas notasRepository,
//            IConfiguration config)
//        {
//            AntecedentesRepo = antecedentesRepository;
//            DiagnosticosRepo = diagnosticosRepository;
//            ExamenFisicoRepo = examenFisicoRepository;
//            ExamenFisicoGinecologicoRepo = examenFisicoGinecologicoRepository;
//            FarmacosRepo = farmacosUsoActualRepository;
//            HabitosRepo = habitosRepository;
//            HistorialRepo = historialRepository;
//            PreclinicaRepo = preclinicaRepository;
//            NotasRepo = notasRepository;
//            _config = config;


//        }

//        [Authorize(Roles = "2,3")]
//        [HttpPost("")]
//        public IActionResult Add([FromBody] ConsultaViewModel consulta)
//        {

//            if (!ModelState.IsValid) return BadRequest(ModelState);
//            bool Preclinica = PreclinicaRepo.AddPreclinica(consulta.Preclinica);

//            if (Preclinica)
//            {
//                if (consulta.AntecedentesFamiliaresPersonales != null)
//                    _ = AntecedentesRepo.
//                        AddAntecedentes(consulta.AntecedentesFamiliaresPersonales);

//                if (consulta.Diagnosticos != null)
//                    _ = DiagnosticosRepo.AddDiagnosticoLista(consulta.Diagnosticos);


//                if (consulta.ExamenFisico != null)
//                {
//                    _ = ExamenFisicoRepo.AddExamenFisico(consulta.ExamenFisico);
//                }
//                if (consulta.ExamenFisicoGinecologico != null)
//                    _
//                        = ExamenFisicoGinecologicoRepo
//                        .AddExamenFisicoGinecologico(consulta.ExamenFisicoGinecologico);

//                if (consulta.FarmacosUsoActual != null)
//                    _ = FarmacosRepo.AddFarmacoLista(consulta.FarmacosUsoActual);


//                if (consulta.Habitos != null)
//                    _ = HabitosRepo.AddAHabito(consulta.Habitos);

//                if (consulta.HistorialGinecoObstetra != null)
//                    _ = HistorialRepo.AddAHistorial(consulta.HistorialGinecoObstetra);


//                if (consulta.Notas != null)
//                    _ = NotasRepo.AddNotaLista(consulta.Notas);


//            }
//            else { return BadRequest("ha ocurrido un error"); }


//            return Ok("Registros creados");
//        }

//    }
//}