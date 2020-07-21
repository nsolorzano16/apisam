namespace apisam.web.Controllers
{
    using apisam.entities;
    using apisam.entities.ViewModels;
    using apisam.interfaces;
    using apisam.web.Events;
    //using apisam.web.Events;
    using apisam.web.HandleErrors;
    using iText.Forms.Xfdf;
    using iText.IO.Font.Constants;
    using iText.IO.Image;
    using iText.Kernel.Events;
    using iText.Kernel.Font;
    using iText.Kernel.Geom;
    using iText.Kernel.Pdf;
    using iText.Kernel.Pdf.Canvas.Draw;
    using iText.Layout;
    using iText.Layout.Element;
    using iText.Layout.Properties;
  
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using System.IO;
    using System.Threading.Tasks;

    [EnableCors("Todos")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        public IConsulta ConsultaRepo;

        private readonly IWebHostEnvironment _env;

        public ConsultaController(IConsulta consultaRepository, IWebHostEnvironment env)
        {
            ConsultaRepo = consultaRepository;
            _env = env;
        }

        [Authorize(Roles = "2")]
        [HttpGet("pacienteId/{pacienteId}/doctorId/{doctorId}/preclinicaId/{preclinicaId}", Name = "GetDetalleConsulta")]
        public async Task<IActionResult> GetDetalleConsulta([FromRoute] int pacienteId, [FromRoute] int doctorId, [FromRoute] int preclinicaId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var resp = await ConsultaRepo.GetDetalleConsulta(doctorId, pacienteId, preclinicaId);
            return Ok(resp);
        }

        [Authorize(Roles = "2")]
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] ConsultaGeneral consulta)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await ConsultaRepo.AddConsultaGeneral(consulta);
            if (_resp.Ok) return Ok(consulta);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "2")]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] ConsultaGeneral consulta)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            RespuestaMetodos _resp = await ConsultaRepo.UpdateConsultaGeneral(consulta);
            if (_resp.Ok) return Ok(consulta);
            return BadRequest(new BadRequestError(_resp.Mensaje));
        }

        [Authorize(Roles = "2")]
        [HttpGet("getconsultageneral/pacienteid/{pacienteId}/doctorid/{doctorId}/preclinicaid/{preclinicaId}", Name = "GetConsultaGeneral")]
        public async Task<IActionResult> GetConsultaGeneral([FromRoute] int pacienteId, [FromRoute] int doctorId, [FromRoute] int preclinicaId)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            return Ok(await ConsultaRepo.GetConsultaGeneral(pacienteId, doctorId, preclinicaId));
        }

        [Authorize(Roles = "2")]
        [HttpGet("expediente/pacienteid/{pacienteId}/doctorid/{doctorId}", Name = "GetExpediente")]
        public async Task<IActionResult> GetExpediente([FromRoute] int pacienteId, [FromRoute] int doctorId)
        {
            if (!ModelState.IsValid) return BadRequest(new BadRequestError("Modelo no valido"));
            return Ok(await ConsultaRepo.GetExpediente(pacienteId, doctorId));
        }

        [HttpGet("pdf/expediente/pacienteid/{pacienteId}/doctorid/{doctorId}", Name = "GetExpedientePdf")]
        [Produces(contentType: "application/pdf")]
        public IActionResult GetExpedientePdf([FromRoute] int pacienteId, [FromRoute] int doctorId)
        {
            string pathLogo = "https://storagedesam.blob.core.windows.net/assets/logosam.png?sv=2019-02-02&st=2020-07-21T02%3A39%3A42Z&se=2020-07-22T02%3A39%3A42Z&sr=b&sp=r&sig=ffO%2BoHqwf%2B9xzqdyCX638auUUTzw04f02A%2BfYy8fE8M%3D";

           
        
            Image img = new Image(ImageDataFactory.Create(pathLogo));
            var _expediente = ConsultaRepo.GetExpediente(pacienteId, doctorId).Result;

            string pass = "SuperSecret!";
            byte[] passUser = System.Text.Encoding.UTF8.GetBytes(pass);
            byte[] passOwner = System.Text.Encoding.UTF8.GetBytes(pass);

            MemoryStream _ms = new MemoryStream();
            PdfWriter writer = new PdfWriter(_ms, new WriterProperties()
                .AddUAXmpMetadata().SetPdfVersion(PdfVersion.PDF_1_7).SetStandardEncryption(
                null,null,
                EncryptionConstants.ALLOW_PRINTING |
                EncryptionConstants.ALLOW_ASSEMBLY 
                ,EncryptionConstants.ENCRYPTION_AES_256) ) ;
            PdfDocument pdf = new PdfDocument(writer);

            PdfDocumentInfo info = pdf.GetDocumentInfo();
            info.SetTitle("Expediente de Paciente");
            info.SetAuthor("Sistema de Administracion Medica");
            info.SetSubject("SAM");
            info.SetCreator("Sistema de Administracion Medica");




            Document document = new Document(pdf, PageSize.LETTER);
            document.SetMargins(75, 35, 70, 35);

            PdfFont helvetica = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            document.SetFont(helvetica);
            pdf.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderPdfEventHandler(img));
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new FooterPdfEventHandler());





            Paragraph header = new Paragraph("Expediente").SetTextAlignment(TextAlignment.CENTER).SetFontSize(20);
            document.Add(header);
            //line separator
            LineSeparator ls = new LineSeparator(new SolidLine());

            Paragraph informacionPersonal = new Paragraph("Información Personal").SetTextAlignment(TextAlignment.CENTER).SetFontSize(16).SetBold();
            Paragraph newline = new Paragraph(new Text("\n"));
            Paragraph nombreCompleto = new Paragraph(new Text($@"Nombre Completo: ").SetBold()).Add(new Paragraph($@"{_expediente.Paciente.Nombres} {_expediente.Paciente.PrimerApellido} {_expediente.Paciente.SegundoApellido}"));
            Paragraph identificacion = new Paragraph(new Text($@"Identificación: ").SetBold()).Add(new Paragraph($"{_expediente.Paciente.Identificacion}"));
            Paragraph email = new Paragraph(new Text($@"Email: ").SetBold()).Add(new Paragraph($" {_expediente.Paciente.Email}"));
            Paragraph fechaNacimiento = new Paragraph(new Text($@"Fecha de Nacimiento: ").SetBold()).Add(new Paragraph($"{_expediente.Paciente.FechaNacimiento.ToShortDateString()}"));
            Paragraph edad = new Paragraph(new Text($@"Edad: ").SetBold()).Add(new Paragraph($" {_expediente.Paciente.Edad}"));
            Paragraph estadoCivil = new Paragraph(new Text($@"Estado Civil: ").SetBold()).Add(new Paragraph($"{ConsultaRepo.PacienteEstadoCivil(_expediente.Paciente.EstadoCivil)}"));
            Paragraph sexo = new Paragraph(new Text($@"Sexo: ").SetBold()).Add(new Paragraph($"{(_expediente.Paciente.Sexo == "M" ? "Masculino" : "Femenino")}"));
            Paragraph telefono = new Paragraph(new Text($@"Telefono: ").SetBold()).Add(new Paragraph($"{_expediente.Paciente.Telefono1}"));
            Paragraph telefono2 = new Paragraph(new Text($@"Telefono Alternativo: ").SetBold()).Add(new Paragraph($" {_expediente.Paciente.Telefono2}"));
            Paragraph contactoEmergencia = new Paragraph(new Text($@"Contacto Emergencia: ").SetBold()).Add(new Paragraph($"{_expediente.Paciente.NombreEmergencia}"));
            Paragraph parentesco = new Paragraph(new Text($@"Parentesco: ").SetBold()).Add(new Paragraph($" {_expediente.Paciente.Parentesco}"));
            Paragraph pais = new Paragraph(new Text($@"Pais: ").SetBold()).Add(new Paragraph($" {_expediente.Paciente.Pais}"));
            Paragraph datosGenerales = new Paragraph("Datos Generales").SetTextAlignment(TextAlignment.CENTER).SetFontSize(16).SetBold();
            Paragraph escolaridad = new Paragraph(new Text($@"Escolaridad: ").SetBold()).Add(new Paragraph($"{_expediente.Paciente.Escolaridad}"));
            Paragraph religion = new Paragraph(new Text($@"Religion: ").SetBold()).Add(new Paragraph($" {_expediente.Paciente.Religion}"));
            Paragraph tipoSangre = new Paragraph(new Text($@"Tipo de Sangre: ").SetBold()).Add(new Paragraph($"{_expediente.Paciente.GrupoSanguineo}"));
            Paragraph grupoEtnico = new Paragraph(new Text($@"Grupo Etnico: ").SetBold()).Add(new Paragraph($"{_expediente.Paciente.GrupoEtnico}"));
            Paragraph profesion = new Paragraph(new Text($@"Profesión: ").SetBold()).Add(new Paragraph($"{_expediente.Paciente.Profesion}"));
            Paragraph datosReferenciales = new Paragraph("Datos Referenciales").SetTextAlignment(TextAlignment.CENTER).SetFontSize(16).SetBold();
            Paragraph deptoNacimiento = new Paragraph(new Text($@"Departamento de Nacimiento: ").SetBold()).Add(new Paragraph($"{_expediente.Paciente.Departamento}"));
            Paragraph muniNacimiento = new Paragraph(new Text($@"Municipio de Nacimiento: ").SetBold()).Add(new Paragraph($"{_expediente.Paciente.Municipio}"));
            Paragraph deptoResidencia = new Paragraph(new Text($@"Departamento de Residencia: ").SetBold()).Add(new Paragraph($"{_expediente.Paciente.DepartamentoResidencia}"));
            Paragraph muniResidencia = new Paragraph(new Text($@"Municipio de Residencia: ").SetBold()).Add(new Paragraph($"{_expediente.Paciente.MunicipioResidencia}"));
            Paragraph direccionTitle = new Paragraph(new Text($@"Dirección:").SetBold());
            Paragraph direccion = new Paragraph(new Text($@" { _expediente.Paciente.Direccion}")).SetTextAlignment(TextAlignment.JUSTIFIED);
            Paragraph datosFamiliares = new Paragraph("Datos Familiares").SetTextAlignment(TextAlignment.CENTER).SetFontSize(16).SetBold();
            Paragraph nombreMadre = new Paragraph(new Text($@"Nombre Madre: ").SetBold()).Add(new Paragraph($" {_expediente.Paciente.NombreMadre}"));
            Paragraph identificacionMadre = new Paragraph(new Text($@"Identificación Madre: ").SetBold()).Add(new Paragraph($"{_expediente.Paciente.IdentificacionMadre}"));
            Paragraph nombrePadre = new Paragraph(new Text($@"Nombre Padre: ").SetBold()).Add(new Paragraph($" {_expediente.Paciente.NombrePadre}"));
            Paragraph identificacionPadre = new Paragraph(new Text($@"Identificacion Padre: ").SetBold()).Add(new Paragraph($"{_expediente.Paciente.IdentificacionPadre}"));
            Paragraph otrainformacion = new Paragraph("Otra Información").SetTextAlignment(TextAlignment.CENTER).SetFontSize(16).SetBold();

            Paragraph notasTitle = new Paragraph(new Text($@"Notas Adicionales:").SetBold());
            Paragraph notas = new Paragraph(new Text($@" { _expediente.Paciente.Notas}")).SetTextAlignment(TextAlignment.JUSTIFIED);
            Paragraph antecedentesFamiliares = new Paragraph("Antecedentes Familiares").SetTextAlignment(TextAlignment.CENTER).SetFontSize(16).SetBold();

            Paragraph antecedentesPatologicosFamiliaresTitle = new Paragraph(new Text($@"Antecedentes Patologicos Familiares:").SetBold());
            Paragraph antecedentesPatFamiliares = new Paragraph(new Text($@" { _expediente.AntecedentesFamiliaresPersonales.AntecedentesPatologicosFamiliares}")).SetTextAlignment(TextAlignment.JUSTIFIED);

            Paragraph antecedentesPatologicosPersonalesTitle = new Paragraph(new Text($@"Antecedentes Patologicos Personales:").SetBold());
            Paragraph antecedentesPatologicosPersonales = new Paragraph(new Text($@" { _expediente.AntecedentesFamiliaresPersonales.AntecedentesPatologicosPersonales}")).SetTextAlignment(TextAlignment.JUSTIFIED);

            Paragraph antecedentesNoPatologicosFamiTitle = new Paragraph(new Text($@"Antecedentes No Patologicos Familiares:").SetBold());
            Paragraph antecedentesNoPatFamiliares = new Paragraph(new Text($@" { _expediente.AntecedentesFamiliaresPersonales.AntecedentesNoPatologicosFamiliares}")).SetTextAlignment(TextAlignment.JUSTIFIED);

            Paragraph antecedentesNoPatologicosPersonalesTitle = new Paragraph(new Text($@"Antecedentes No Patologicos Personales:").SetBold());
            Paragraph antecedentesNoPatologicosPersonales = new Paragraph(new Text($@" { _expediente.AntecedentesFamiliaresPersonales.AntecedentesNoPatologicosPersonales}")).SetTextAlignment(TextAlignment.JUSTIFIED);

            Paragraph antecedentesInmunoAlergicosTitle = new Paragraph(new Text($@"Antecedentes Inmuno Alergicos:").SetBold());
            Paragraph antecedentesInmunoAlergicos = new Paragraph(new Text($@" { _expediente.AntecedentesFamiliaresPersonales.AntecedentesInmunoAlergicosPersonales}")).SetTextAlignment(TextAlignment.JUSTIFIED);

            Paragraph habitos = new Paragraph("Habitos").SetTextAlignment(TextAlignment.CENTER).SetFontSize(16).SetBold();
            Paragraph cigarrillo = new Paragraph(new Text($@"Cigarrillo: ").SetBold()).Add(new Paragraph($" {((_expediente.Habitos.Cigarrillo) ? "Si" : "No")}"));
            Paragraph cafe = new Paragraph(new Text($@"Café: ").SetBold()).Add(new Paragraph($"{((_expediente.Habitos.Cafe) ? "Si" : "No")}"));
            Paragraph alcohol = new Paragraph(new Text($@"Alcohol: ").SetBold()).Add(new Paragraph($"{((_expediente.Habitos.Alcohol) ? "Si" : "No")}"));
            Paragraph drogas = new Paragraph(new Text($@"Drogas: ").SetBold()).Add(new Paragraph($"{((_expediente.Habitos.DrogasEstupefaciente) ? "Si" : "X")}"));
            Paragraph notasHabitos = new Paragraph(new Text($@"Notas Adicionales: ").SetBold()).Add(new Paragraph($" {_expediente.Habitos.Notas}"));

            Paragraph historialGinecologico = new Paragraph("Historial Ginecologico").SetTextAlignment(TextAlignment.CENTER).SetFontSize(16).SetBold();

            Paragraph menarquia = new Paragraph(new Text($@"Fecha Menarquia: ").SetBold()).Add(new Paragraph($"{(_expediente.HistorialGinecoObstetra.FechaMenarquia.Value != null ? _expediente.HistorialGinecoObstetra.FechaMenarquia.Value.ToShortDateString() : "")}"));

            Paragraph ultimaRegla = new Paragraph(new Text($@"Ultima Menstruación: ").SetBold()).Add(new Paragraph($"{(_expediente.HistorialGinecoObstetra.Fum.Value != null ? _expediente.HistorialGinecoObstetra.Fum.Value.ToShortDateString() : "")}"));

            Paragraph numeroGesta = new Paragraph(new Text($@"Numero de Gesta: ").SetBold()).Add(new Paragraph($"  {_expediente.HistorialGinecoObstetra.G}"));
            Paragraph cesareas = new Paragraph(new Text($@"Cesáreas: ").SetBold()).Add(new Paragraph($"{_expediente.HistorialGinecoObstetra.C}"));
            Paragraph partos = new Paragraph(new Text($@"Partos: ").SetBold()).Add(new Paragraph($" {_expediente.HistorialGinecoObstetra.P}"));
            Paragraph hv = new Paragraph(new Text($@"Hijos Vivos: ").SetBold()).Add(new Paragraph($"{_expediente.HistorialGinecoObstetra.Hv}"));
            Paragraph hm = new Paragraph(new Text($@"Hijos Muertos: ").SetBold()).Add(new Paragraph($"{_expediente.HistorialGinecoObstetra.Hm}"));
            Paragraph anticonceptivo = new Paragraph(new Text($@"Anticonceptivo: ").SetBold()).Add(new Paragraph($"{_expediente.HistorialGinecoObstetra.AnticonceptivoTexto}"));
            Paragraph anticonceptivoDescripcion = new Paragraph(new Text($@"Anticonceptivo Descripción: ").SetBold()).Add(new Paragraph($"{_expediente.HistorialGinecoObstetra.DescripcionAnticonceptivos}"));
            Paragraph fechamenopausia = new Paragraph(new Text($@"Fecha Menarquia: ").SetBold()).Add(new Paragraph($"{(_expediente.HistorialGinecoObstetra.FechaMenopausia.Value != null ? _expediente.HistorialGinecoObstetra.FechaMenopausia.Value.ToShortDateString() : "")}"));
            Paragraph notasGinecologias = new Paragraph(new Text($@"Notas Adicionales: ").SetBold()).Add(new Paragraph($" {_expediente.HistorialGinecoObstetra.Notas}"));

            Paragraph farmacos = new Paragraph("Farmacos").SetTextAlignment(TextAlignment.CENTER).SetFontSize(16).SetBold();

            document.Add(ls);
            document.Add(informacionPersonal);
            document.Add(ls);
            document.Add(nombreCompleto);
            document.Add(identificacion);
            document.Add(email);
            document.Add(fechaNacimiento);
            document.Add(edad);
            document.Add(estadoCivil);
            document.Add(sexo);
            document.Add(telefono);
            document.Add(telefono2);
            document.Add(contactoEmergencia);
            document.Add(parentesco);
            document.Add(pais);
            document.Add(ls);
            document.Add(datosGenerales);
            document.Add(ls);
            document.Add(escolaridad);
            document.Add(religion);
            document.Add(tipoSangre);
            document.Add(grupoEtnico);
            document.Add(profesion);
            document.Add(ls);
            document.Add(datosReferenciales);
            document.Add(ls);
            document.Add(deptoNacimiento);
            document.Add(muniNacimiento);
            document.Add(deptoResidencia);
            document.Add(muniResidencia);
            document.Add(direccionTitle);
            document.Add(direccion);
            document.Add(ls);
            document.Add(datosFamiliares);
            document.Add(ls);
            document.Add(nombreMadre);
            document.Add(identificacionMadre);
            document.Add(nombrePadre);
            document.Add(identificacionPadre);
            document.Add(ls);
            document.Add(otrainformacion);
            document.Add(ls);
            document.Add(notasTitle);
            document.Add(notas);
            document.Add(ls);

            if (_expediente.AntecedentesFamiliaresPersonales != null)
            {
                document.Add(antecedentesFamiliares);
                document.Add(ls);
                document.Add(antecedentesPatologicosFamiliaresTitle);
                document.Add(antecedentesPatFamiliares);
                document.Add(antecedentesPatologicosPersonalesTitle);
                document.Add(antecedentesPatologicosPersonales);
                document.Add(antecedentesNoPatologicosFamiTitle);
                document.Add(antecedentesNoPatFamiliares);
                document.Add(antecedentesNoPatologicosPersonalesTitle);
                document.Add(antecedentesNoPatologicosPersonales);
                document.Add(antecedentesInmunoAlergicosTitle);
                document.Add(antecedentesInmunoAlergicos);

            }

            if (_expediente.Habitos != null)
            {
                document.Add(ls);
                document.Add(habitos);
                document.Add(ls);
                document.Add(cigarrillo);
                document.Add(cafe);
                document.Add(alcohol);
                document.Add(drogas);
                document.Add(notasHabitos);
            }

            if (_expediente.HistorialGinecoObstetra != null)
            {
                document.Add(ls);
                document.Add(historialGinecologico);
                document.Add(ls);
                document.Add(menarquia);
                document.Add(ultimaRegla);
                document.Add(numeroGesta);
                document.Add(cesareas);
                document.Add(partos);
                document.Add(hv);
                document.Add(hm);
                document.Add(anticonceptivo);
                document.Add(anticonceptivoDescripcion);
                document.Add(fechamenopausia);
                document.Add(notasGinecologias);
            }


            if (_expediente.FarmacosUsoActual.Count > 0)
            {
                document.Add(ls);
                document.Add(farmacos);

                _expediente.FarmacosUsoActual.ForEach(f =>
                {
                    document.Add(ls);
                    Paragraph nombreTemp = new Paragraph(new Text($@"Nombre: ").SetBold()).Add(new Paragraph($"{f.Nombre}"));
                    Paragraph concentracionTemp = new Paragraph(new Text($@"Concentracion: ").SetBold()).Add(new Paragraph($" {f.Concentracion}"));
                    Paragraph dosisTemp = new Paragraph(new Text($@"Dosis: ").SetBold()).Add(new Paragraph($"{f.Dosis}"));
                    Paragraph tiempoTemp = new Paragraph(new Text($@"Tiempo: ").SetBold()).Add(new Paragraph($" {f.Tiempo}"));
                    document.Add(nombreTemp);
                    document.Add(concentracionTemp);
                    document.Add(dosisTemp);
                    document.Add(tiempoTemp);
                });

                document.Add(ls);
            }

            document.Add(newline);

            if (_expediente.Consultas.Count > 0)
            {
                Paragraph tituloConsulta = new Paragraph($"Consultas").SetTextAlignment(TextAlignment.CENTER).SetFontSize(16).SetBold();
                document.Add(tituloConsulta);
                _expediente.Consultas.ForEach(consulta =>
                {

                    Paragraph preclinicaTitle = new Paragraph($"Preclinica - {consulta.Preclinica.CreadoFecha}").SetTextAlignment(TextAlignment.CENTER).SetFontSize(16).SetBold();



                    Paragraph preclinica =
                    new Paragraph(new Text("Peso lbs: ").SetBold()).
                    Add(new Paragraph($"{consulta.Preclinica.Peso} ")).
                    Add(new Paragraph(new Text("Altura cm: ").SetBold())).
                    Add(new Paragraph($"{consulta.Preclinica.Altura} ")).
                    Add(new Paragraph(new Text("Temperatura C°: ").SetBold())).
                    Add(new Paragraph($"{consulta.Preclinica.Temperatura} ")).
                    Add(new Paragraph(new Text("Frecuencia Respiratoria/rpm: ").SetBold())).
                    Add(new Paragraph($"{consulta.Preclinica.FrecuenciaRespiratoria} ")).
                    Add(new Paragraph(new Text("Ritmo Cardiaco/ppm: ").SetBold())).
                    Add(new Paragraph($"{consulta.Preclinica.RitmoCardiaco} ")).
                    Add(new Paragraph(new Text("Presión Sistolica/mmHg :").SetBold())).
                    Add(new Paragraph($"{consulta.Preclinica.PresionSistolica} ")).
                    Add(new Paragraph(new Text("Presion Diastolica/mmHg: ").SetBold())).
                    Add(new Paragraph($"{consulta.Preclinica.PresionDiastolica} ")).
                    Add(new Paragraph(new Text("IMC: ").SetBold())).
                    Add(new Paragraph($"{consulta.Preclinica.IMC}"));

                    document.Add(ls);
                    document.Add(preclinicaTitle);
                    document.Add(ls);
                    document.Add(preclinica);
                    document.Add(ls);

                    if (consulta.ConsultaGeneral != null)
                    {
                        Paragraph ConsultaGeneralTitle = new Paragraph("Consulta General").SetTextAlignment(TextAlignment.CENTER).SetFontSize(15).SetBold();

                        //consulta general
                        Paragraph motivoConsultaTitle = new Paragraph("Motivo Consulta:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                        Paragraph motivoConsulta = new Paragraph($"{consulta.ConsultaGeneral.MotivoConsulta}").SetTextAlignment(TextAlignment.JUSTIFIED);

                        Paragraph heaTitle = new Paragraph("Historia de la enfermedad actual:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                        Paragraph hea = new Paragraph($"{consulta.ConsultaGeneral.Hea}").SetTextAlignment(TextAlignment.JUSTIFIED);

                        Paragraph fogTitle = new Paragraph("Funciones Orgánicas Generales:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                        Paragraph fog = new Paragraph($"{consulta.ConsultaGeneral.Hea}").SetTextAlignment(TextAlignment.JUSTIFIED);

                        Paragraph notasConsultaGTitle = new Paragraph("Notas Adicionales:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                        Paragraph notasConsultaG = new Paragraph($"{consulta.ConsultaGeneral.Notas}").SetTextAlignment(TextAlignment.JUSTIFIED);

                        document.Add(ConsultaGeneralTitle);
                        document.Add(ls);
                        document.Add(motivoConsultaTitle);
                        document.Add(motivoConsulta);
                        document.Add(heaTitle);
                        document.Add(hea);
                        document.Add(fogTitle);
                        document.Add(fog);
                        document.Add(notasConsultaGTitle);
                        document.Add(notasConsultaG);
                        document.Add(ls);
                    }

                    if (consulta.ExamenFisico != null)
                    {
                        Paragraph examenFisicoTitle = new Paragraph("Examen Fisico").SetTextAlignment(TextAlignment.CENTER).SetFontSize(15).SetBold();

                        Paragraph aspectoGeneralTitle = new Paragraph("Aspecto General:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                        Paragraph aspectoGeneral = new Paragraph($"{consulta.ExamenFisico.AspectoGeneral}").SetTextAlignment(TextAlignment.JUSTIFIED);

                        Paragraph pielFanerastitle = new Paragraph("Piel y Faneras:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                        Paragraph pielFaneras = new Paragraph($"{consulta.ExamenFisico.PielFaneras}").SetTextAlignment(TextAlignment.JUSTIFIED);

                        Paragraph cabezaTitle = new Paragraph("Cabeza:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                        Paragraph cabeza = new Paragraph($"{consulta.ExamenFisico.Cabeza}").SetTextAlignment(TextAlignment.JUSTIFIED);

                        Paragraph oidosTitle = new Paragraph("Oidos:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                        Paragraph oidos = new Paragraph($"{consulta.ExamenFisico.Oidos}").SetTextAlignment(TextAlignment.JUSTIFIED);

                        Paragraph ojosTitle = new Paragraph("Ojos:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                        Paragraph ojos = new Paragraph($"{consulta.ExamenFisico.Oidos}").SetTextAlignment(TextAlignment.JUSTIFIED);

                        Paragraph narizTitle = new Paragraph("Nariz:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                        Paragraph nariz = new Paragraph($"{consulta.ExamenFisico.Oidos}").SetTextAlignment(TextAlignment.JUSTIFIED);

                        Paragraph bocaTitle = new Paragraph("Boca:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                        Paragraph boca = new Paragraph($"{consulta.ExamenFisico.Boca}").SetTextAlignment(TextAlignment.JUSTIFIED);

                        Paragraph cuelloTitle = new Paragraph("Cuello:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                        Paragraph cuello = new Paragraph($"{consulta.ExamenFisico.Cuello}").SetTextAlignment(TextAlignment.JUSTIFIED);

                        Paragraph toraxTitle = new Paragraph("Torax:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                        Paragraph torax = new Paragraph($"{consulta.ExamenFisico.Torax}").SetTextAlignment(TextAlignment.JUSTIFIED);

                        Paragraph abdomenTitle = new Paragraph("Abdomen:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                        Paragraph abdomen = new Paragraph($"{consulta.ExamenFisico.Abdomen}").SetTextAlignment(TextAlignment.JUSTIFIED);

                        Paragraph columnaTitle = new Paragraph("Columna Vertebral Region Lumbar:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                        Paragraph columna = new Paragraph($"{consulta.ExamenFisico.ColumnaVertebralRegionLumbar}").SetTextAlignment(TextAlignment.JUSTIFIED);

                        Paragraph miembrosTitle = new Paragraph("Miembros Superiores e Inferiores:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                        Paragraph miembros = new Paragraph($"{consulta.ExamenFisico.MiembrosInferioresSuperiores}").SetTextAlignment(TextAlignment.JUSTIFIED);

                        Paragraph genitalesTitle = new Paragraph("Genitales:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                        Paragraph genitales = new Paragraph($"{consulta.ExamenFisico.Genitales}").SetTextAlignment(TextAlignment.JUSTIFIED);

                        Paragraph neurologicoTitle = new Paragraph("Neurológico:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                        Paragraph neurologico = new Paragraph($"{consulta.ExamenFisico.Neurologico}").SetTextAlignment(TextAlignment.JUSTIFIED);

                        Paragraph notasAdicionalesExamenTitle = new Paragraph("Notas Adicionales:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                        Paragraph notasFisico = new Paragraph($"{consulta.ExamenFisico.Neurologico}").SetTextAlignment(TextAlignment.JUSTIFIED);

                        document.Add(examenFisicoTitle);
                        document.Add(ls);
                        document.Add(aspectoGeneralTitle);
                        document.Add(aspectoGeneral);
                        document.Add(pielFanerastitle);
                        document.Add(pielFaneras);
                        document.Add(cabezaTitle);
                        document.Add(cabeza);
                        document.Add(oidosTitle);
                        document.Add(oidos);
                        document.Add(ojosTitle);
                        document.Add(ojos);
                        document.Add(narizTitle);
                        document.Add(nariz);
                        document.Add(bocaTitle);
                        document.Add(boca);
                        document.Add(cuelloTitle);
                        document.Add(cuello);
                        document.Add(toraxTitle);
                        document.Add(torax);
                        document.Add(abdomenTitle);
                        document.Add(abdomen);
                        document.Add(columnaTitle);
                        document.Add(columna);
                        document.Add(miembrosTitle);
                        document.Add(miembros);
                        document.Add(genitalesTitle);
                        document.Add(genitales);
                        document.Add(neurologicoTitle);
                        document.Add(neurologico);
                        document.Add(notasAdicionalesExamenTitle);
                        document.Add(notasFisico);
                        document.Add(ls);

                    }

                    if (consulta.PlanesTerapeuticos.Count > 0)
                    {
                        consulta.PlanesTerapeuticos.ForEach(plan =>
                        {
                            Paragraph planTerapeuticoTitle = new Paragraph("Plan Terapeutico").SetTextAlignment(TextAlignment.CENTER).SetFontSize(15).SetBold();

                            Paragraph nombreMedicamento = new Paragraph(new Text($@"Nombre Medicamento: ").SetBold()).Add(new Paragraph($"{plan.NombreMedicamento}"));

                            Paragraph dosis = new Paragraph(new Text($@"Dosis: ").SetBold()).Add(new Paragraph($"{plan.Dosis}"));

                            Paragraph horario = new Paragraph(new Text($@"Horario: ").SetBold()).Add(new Paragraph($"{plan.Horario}"));

                            Paragraph diasRequeridos = new Paragraph(new Text($@"Dias Requeridos: ").SetBold()).Add(new Paragraph($"{plan.DiasRequeridos}"));

                            Paragraph viaAdministracion = new Paragraph(new Text($@"Via de Administración: ").SetBold()).Add(new Paragraph($"{plan.NombreMedicamento}"));

                            Paragraph permanente = new Paragraph(new Text($@"Permanente: ").SetBold()).Add(new Paragraph($" {((plan.Permanente) ? "Si" : "No")}"));

                            Paragraph notasPlan = new Paragraph("Notas Adicionales:").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                            Paragraph notaDescPlan = new Paragraph($"{plan.Notas}").SetTextAlignment(TextAlignment.JUSTIFIED);

                            document.Add(planTerapeuticoTitle);
                            document.Add(ls);
                            document.Add(nombreMedicamento);
                            document.Add(dosis);
                            document.Add(horario);
                            document.Add(diasRequeridos);
                            document.Add(viaAdministracion);
                            document.Add(permanente);
                            document.Add(notasPlan);
                            document.Add(notaDescPlan);
                            document.Add(ls);

                        });
                    }
                    if (consulta.ExamenesIndicados.Count > 0)
                    {
                        Paragraph examenesIndicadosTitle = new Paragraph("Examenes Indicados").SetTextAlignment(TextAlignment.CENTER).SetFontSize(15).SetBold();

                        Table tablaExamenes = new Table(6).UseAllAvailableWidth();

                        Cell cell = new Cell().Add(new Paragraph("#").SetTextAlignment(TextAlignment.CENTER));
                        tablaExamenes.AddCell(cell);
                        cell = new Cell().Add(new Paragraph("Categoria").SetTextAlignment(TextAlignment.CENTER));
                        tablaExamenes.AddCell(cell);
                        cell = new Cell().Add(new Paragraph("Tipo").SetTextAlignment(TextAlignment.CENTER));
                        tablaExamenes.AddCell(cell);
                        cell = new Cell().Add(new Paragraph("Detalle").SetTextAlignment(TextAlignment.CENTER));
                        tablaExamenes.AddCell(cell);
                        cell = new Cell().Add(new Paragraph("Nombre").SetTextAlignment(TextAlignment.CENTER));
                        tablaExamenes.AddCell(cell);
                        cell = new Cell().Add(new Paragraph("Notas Adicionales").SetTextAlignment(TextAlignment.CENTER));
                        tablaExamenes.AddCell(cell);
                        document.Add(examenesIndicadosTitle);
                        document.Add(ls);
                        document.Add(newline);



                        int index = 0;
                        consulta.ExamenesIndicados.ForEach(examen =>
                        {
                            index++;

                            cell = new Cell().Add(new Paragraph(new Text($"{index}")));
                            tablaExamenes.AddCell(cell);
                            cell = new Cell().Add(new Paragraph(new Text($"{examen.ExamenCategoria}")));
                            tablaExamenes.AddCell(cell);
                            cell = new Cell().Add(new Paragraph(new Text($"{examen.ExamenTipo}")));
                            tablaExamenes.AddCell(cell);
                            cell = new Cell().Add(new Paragraph(new Text($"{examen.ExamenDetalle}")));
                            tablaExamenes.AddCell(cell);
                            cell = new Cell().Add(new Paragraph(new Text($"{examen.Nombre}")));
                            tablaExamenes.AddCell(cell);
                            cell = new Cell().Add(new Paragraph(new Text($"{examen.Notas}")));
                            tablaExamenes.AddCell(cell);


                        });
                        document.Add(tablaExamenes);
                        document.Add(newline);
                        document.Add(ls);
                    }

                    int indexDiagno = 0;
                    if (consulta.Diagnosticos.Count > 0)
                    {
                        Paragraph diagnosticosTitle = new Paragraph("Diagnosticos").SetTextAlignment(TextAlignment.CENTER).SetFontSize(15).SetBold();
                        document.Add(diagnosticosTitle);
                        document.Add(ls);
                        consulta.Diagnosticos.ForEach(d =>
                        {
                            indexDiagno++;
                            Paragraph diagnoTitle = new Paragraph($"# {indexDiagno}-Problema Clinico :").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                            Paragraph diagnosticoItem = new Paragraph($"{d.ProblemasClinicos}").SetTextAlignment(TextAlignment.JUSTIFIED);
                            document.Add(diagnoTitle);
                            document.Add(diagnosticoItem);
                        });
                        document.Add(ls);
                    }

                    int indexNotas = 0;

                    if (consulta.Notas.Count > 0)
                    {
                        Paragraph notasConsulTitle = new Paragraph("Notas").SetTextAlignment(TextAlignment.CENTER).SetFontSize(15).SetBold();
                        document.Add(notasConsulTitle);
                        document.Add(ls);
                        consulta.Notas.ForEach(n =>
                        {
                            indexNotas++;
                            Paragraph notaTitulo = new Paragraph($"# {indexNotas}-Nota :").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetBold();
                            Paragraph notadescrip = new Paragraph($"{n.Notas}").SetTextAlignment(TextAlignment.JUSTIFIED);
                            document.Add(notaTitulo);
                            document.Add(notadescrip);
                        });
                    }






                });

            }








            document.Close();
            byte[] byteStream = _ms.ToArray();
            _ms = new MemoryStream();
            _ms.Write(byteStream, 0, byteStream.Length);
            _ms.Position = 0;


            return new FileStreamResult(_ms, "application/pdf");



        }
    }
}
