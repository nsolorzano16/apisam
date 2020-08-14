namespace apisam.repos
{
    using apisam.entities;
    using apisam.entities.ViewModels;
    using apisam.interfaces;
    using apisam.repositories;
    using ServiceStack;
    using ServiceStack.OrmLite;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ConsultaRepo : IConsulta
    {
        private readonly OrmLiteConnectionFactory dbFactory;

        private readonly Conexion con = new Conexion();

        private static TimeZoneInfo hondurasTime;

        private readonly PacientesRepo pacientesRepo = new PacientesRepo();

        public ConsultaRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        }

        public async Task<ConsultaViewModel> GetDetalleConsulta(int doctorId, int pacienteId, int preclinicaId)
        {
            ConsultaViewModel _resp = new ConsultaViewModel();
            using var _db = dbFactory.Open();
            var _preclinica = await _db.SingleAsync<Preclinica>(x => x.PreclinicaId ==
            preclinicaId && x.PacienteId == pacienteId && x.DoctorId == doctorId
            && x.Activo == true && x.Atendida == true);

            var _antecedentesPersonales = await _db.SingleAsync<AntecedentesFamiliaresPersonales>
                (x => x.PacienteId == pacienteId && x.Activo == true);

            var _habitos = await _db.SingleAsync<Habitos>(x => x.PacienteId
            == pacienteId && x.Activo == true);

            var _qryH = $@"SELECT
                                        h.HistorialId,
                                        h.PacienteId,
                                        h.AnticonceptivoId,
                                        h.FechaMenarquia,
                                        h.Fum,
                                        h.G,
                                        h.P,
                                        h.C,
                                        h.Hv,
                                        h.Hm,
                                        h.DescripcionAnticonceptivos,
                                        h.VacunaVph,
                                        h.FechaMenopausia,
                                        a.Nombre as 'AnticonceptivoTexto',
                                        h.Activo,
                                        h.CreadoPor,
                                        h.CreadoFecha,
                                        h.ModificadoPor,
                                        h.ModificadoFecha,
                                        h.Notas,
                                        h.PreclinicaId
                                        FROM HistorialGinecoObstetra h 
                                        INNER JOIN Anticonceptivos a ON h.AnticonceptivoId = a.AnticonceptivoId
                                        WHERE h.PacienteId = {pacienteId} AND h.Activo = 1";
            var _historialGinecoObstetra = await _db.SingleAsync<HistorialGinecoViewModel>(_qryH);

            var _farmacos = await _db.SelectAsync<FarmacosUsoActual>
                (x => x.PacienteId ==
                pacienteId && x.Activo == true);

            var _examenFisico = await _db.SingleAsync<ExamenFisico>(
                x => x.PreclinicaId == preclinicaId && x.PacienteId == pacienteId
                && x.DoctorId == doctorId && x.Activo == true);


            var _qryDiagnosticos = $@"SELECT 
                                    d.DiagnosticoId,
                                    d.PacienteId,
                                    d.DoctorId,
                                    d.CieId,
                                    d.ProblemasClinicos,
                                    d.Activo,
                                    d.CreadoPor,
                                    d.CreadoFecha,
                                    d.ModificadoPor,
                                    d.ModificadoFecha,
                                    d.Notas,
                                    d.PreclinicaId,
                                    c.Codigo as 'CodigoCie',
                                    c.Nombre as 'NombreCie'
                                    FROM 
                                    Diagnosticos d 
                                    INNER JOIN CIE c 
                                    ON d.CieId = c.CieId
                                    WHERE d.PacienteId = {pacienteId} AND d.DoctorId = {doctorId} AND d.PreclinicaId = {preclinicaId} AND d.Activo = 1";


            var _diagnosticos = await _db.SelectAsync<DiagnosticosViewModel>(_qryDiagnosticos);

            var _notas = await _db.SelectAsync<Notas>(x => x.PreclinicaId == preclinicaId
            && x.PacienteId == pacienteId && x.DoctorId == doctorId && x.Activo == true);

            var _consultaGeneral = await _db.SingleAsync<ConsultaGeneral>(x => x.PreclinicaId == preclinicaId
             && x.PacienteId == pacienteId && x.DoctorId == doctorId && x.Activo == true);

            var _qry = $@"SELECT
                                                ei.ExamenIndicadoId,
                                                ei.PacienteId,
                                                ei.DoctorId,
                                                ei.PreclinicaId,
                                                ei.ExamenCategoriaId,
                                                ei.ExamenTipoId,
                                                ei.ExamenDetalleId,
                                                ei.Nombre,
                                                ei.Activo,
                                                ei.CreadoPor,
                                                ei.CreadoFecha,
                                                ei.ModificadoPor,
                                                ei.ModificadoFecha,
                                                ei.Notas,
                                                ec.Nombre as 'ExamenCategoria',
                                                et.Nombre as 'ExamenTipo',
                                                ed.Nombre as 'ExamenDetalle'
                                            FROM ExamenIndicado ei
                                                INNER JOIN ExamenCategoria ec on ei.ExamenCategoriaId = ec.ExamenCategoriaId
                                                INNER JOIN ExamenTipo et on ei.ExamenTipoId = et.ExamenTipoId
                                                LEFT JOIN ExamenDetalle ed on ei.ExamenDetalleId = ed.ExamenDetalleId
                                                WHERE ei.PacienteId ={pacienteId} and ei.DoctorId = {doctorId}
                                                and ei.PreclinicaId = {preclinicaId} and ei.activo = 1";

            var _listaExamenes = await _db.SelectAsync<ExamenesIndicadosViewModel>(_qry);
            var _qryPlanes = $@"SELECT
                                        p.PlanTerapeuticoId,
                                        p.PacienteId,
                                        p.DoctorId,
                                        p.PreclinicaId,
                                        p.NombreMedicamento,
                                        p.Dosis,
                                        p.ViaAdministracionId,
                                        p.Horario,
                                        p.Permanente,
                                        p.DiasRequeridos,
                                        p.Activo,
                                        p.CreadoPor,
                                        p.CreadoFecha,
                                        p.ModificadoPor,
                                        p.ModificadoFecha,
                                        p.Notas,
                                        v.Nombre as 'ViaAdministracion'
                                    FROM PlanTerapeutico p
                                        INNER JOIN ViaAdministracion v on p.ViaAdministracionId = v.ViaAdministracionId
                                        WHERE p.PacienteId = {pacienteId} AND p.DoctorId = {doctorId} AND p.PreclinicaId = {preclinicaId}
                                          AND p.Activo = 1";
            var _planTerapeutico = await _db.SelectAsync<PlanTerapeuticoViewModel>(_qryPlanes);

            _resp.Preclinica = _preclinica;
            _resp.AntecedentesFamiliaresPersonales = _antecedentesPersonales;
            _resp.Habitos = _habitos;
            _resp.HistorialGinecoObstetra = _historialGinecoObstetra;
            _resp.FarmacosUsoActual = _farmacos;
            _resp.ExamenFisico = _examenFisico;
            // _resp.ExamenFisicoGinecologico = _examenFisicoGinecologico;
            _resp.Diagnosticos = _diagnosticos;
            _resp.Notas = _notas;
            _resp.ConsultaGeneral = _consultaGeneral;
            _resp.ExamenesIndicados = _listaExamenes;
            _resp.PlanesTerapeuticos = _planTerapeutico;

            return _resp;
        }

        public async Task<RespuestaMetodos> AddConsultaGeneral(ConsultaGeneral consulta)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                consulta.CreadoFecha = dateTime_HN;
                consulta.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<ConsultaGeneral>(consulta);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;
        }

        public async Task<RespuestaMetodos> UpdateConsultaGeneral(ConsultaGeneral consulta)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                consulta.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<ConsultaGeneral>(consulta);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }
            return _resp;
        }

        public async Task<ConsultaGeneral> GetConsultaGeneral(int pacienteId, int doctorId, int preclinicaId)
        {
            using var _db = dbFactory.Open();
            var consulta = await _db.SingleAsync<ConsultaGeneral>(x => x.PacienteId == pacienteId
            && x.DoctorId == doctorId && x.PreclinicaId == preclinicaId && x.Activo == true);
            return consulta;
        }

        public async Task<ExpedienteViewModel> GetExpediente(int pacienteId, int doctorId)
        {
            var _expediente = new ExpedienteViewModel();
            List<ConsultaExpedienteViewModel> listaConsultas = new List<ConsultaExpedienteViewModel>();
            using var _db = dbFactory.Open();

            var _antecedentesPersonales = await _db.SingleAsync<AntecedentesFamiliaresPersonales>
           (x => x.PacienteId == pacienteId && x.Activo == true);

            var _habitos = await _db.SingleAsync<Habitos>(x => x.PacienteId
            == pacienteId && x.Activo == true);

            var _qryH = $@"SELECT
                                        h.HistorialId,
                                        h.PacienteId,
                                        h.AnticonceptivoId,
                                        h.FechaMenarquia,
                                        h.Fum,
                                        h.G,
                                        h.P,
                                        h.C,
                                        h.Hv,
                                        h.Hm,
                                        h.DescripcionAnticonceptivos,
                                        h.VacunaVph,
                                        h.FechaMenopausia,
                                        a.Nombre as 'AnticonceptivoTexto',
                                        h.Activo,
                                        h.CreadoPor,
                                        h.CreadoFecha,
                                        h.ModificadoPor,
                                        h.ModificadoFecha,
                                        h.Notas,
                                        h.PreclinicaId
                                        FROM HistorialGinecoObstetra h 
                                        INNER JOIN Anticonceptivos a ON h.AnticonceptivoId = a.AnticonceptivoId
                                        WHERE h.PacienteId = {pacienteId} AND h.Activo = 1";
            var _historialGinecoObstetra = await _db.SingleAsync<HistorialGinecoViewModel>(_qryH);

            var _farmacos = await _db.SelectAsync<FarmacosUsoActual>
                (x => x.PacienteId ==
                pacienteId && x.Activo == true);

            // foreach de las preclinicas atendidas
            var _preclinicasAtendidas = await _db.SelectAsync<Preclinica>(x => x.PacienteId == pacienteId &&
            x.DoctorId == doctorId && x.Atendida == true && x.Activo == true);

            var _paciente = await pacientesRepo.GetInfoPaciente(pacienteId);

            _preclinicasAtendidas.OrderByDescending(x => x.PreclinicaId).ToList();
            foreach (var item in _preclinicasAtendidas)
            {
                var _examenFisico = await _db.SingleAsync<ExamenFisico>(x => x.PreclinicaId == item.PreclinicaId && x.PacienteId == item.PacienteId
                           && x.DoctorId == item.DoctorId && x.Activo == true);

                var _qryDiagnosticos = $@"SELECT 
                                    d.DiagnosticoId,
                                    d.PacienteId,
                                    d.DoctorId,
                                    d.CieId,
                                    d.ProblemasClinicos,
                                    d.Activo,
                                    d.CreadoPor,
                                    d.CreadoFecha,
                                    d.ModificadoPor,
                                    d.ModificadoFecha,
                                    d.Notas,
                                    d.PreclinicaId,
                                    c.Codigo as 'CodigoCie',
                                    c.Nombre as 'NombreCie'
                                    FROM 
                                    Diagnosticos d 
                                    INNER JOIN CIE c 
                                    ON d.CieId = c.CieId
                                    WHERE d.PacienteId = {item.PacienteId} AND d.DoctorId = {item.DoctorId} AND d.PreclinicaId = {item.PreclinicaId} AND d.Activo = 1";

                var _diagnosticos = await _db.SelectAsync<DiagnosticosViewModel>(_qryDiagnosticos);

                var _notas = await _db.SelectAsync<Notas>(x => x.PreclinicaId == item.PreclinicaId
                && x.PacienteId == item.PacienteId && x.DoctorId == item.DoctorId && x.Activo == true);

                var _consultaGeneral = await _db.SingleAsync<ConsultaGeneral>(x => x.PreclinicaId == item.PreclinicaId
                 && x.PacienteId == item.PacienteId && x.DoctorId == item.DoctorId && x.Activo == true);

                var _qry = $@"SELECT
                                                ei.ExamenIndicadoId,
                                                ei.PacienteId,
                                                ei.DoctorId,
                                                ei.PreclinicaId,
                                                ei.ExamenCategoriaId,
                                                ei.ExamenTipoId,
                                                ei.ExamenDetalleId,
                                                ei.Nombre,
                                                ei.Activo,
                                                ei.CreadoPor,
                                                ei.CreadoFecha,
                                                ei.ModificadoPor,
                                                ei.ModificadoFecha,
                                                ei.Notas,
                                                ec.Nombre as 'ExamenCategoria',
                                                et.Nombre as 'ExamenTipo',
                                                ed.Nombre as 'ExamenDetalle'
                                            FROM ExamenIndicado ei
                                                INNER JOIN ExamenCategoria ec on ei.ExamenCategoriaId = ec.ExamenCategoriaId
                                                INNER JOIN ExamenTipo et on ei.ExamenTipoId = et.ExamenTipoId
                                                LEFT JOIN ExamenDetalle ed on ei.ExamenDetalleId = ed.ExamenDetalleId
                                                WHERE ei.PacienteId ={item.PacienteId} and ei.DoctorId = {item.DoctorId}
                                                and ei.PreclinicaId = {item.PreclinicaId} and ei.activo = 1";

                var _listaExamenes = await _db.SelectAsync<ExamenesIndicadosViewModel>(_qry);
                var _qryPlanes = $@"SELECT
                                        p.PlanTerapeuticoId,
                                        p.PacienteId,
                                        p.DoctorId,
                                        p.PreclinicaId,
                                        p.NombreMedicamento,
                                        p.Dosis,
                                        p.ViaAdministracionId,
                                        p.Horario,
                                        p.Permanente,
                                        p.DiasRequeridos,
                                        p.Activo,
                                        p.CreadoPor,
                                        p.CreadoFecha,
                                        p.ModificadoPor,
                                        p.ModificadoFecha,
                                        p.Notas,
                                        v.Nombre as 'ViaAdministracion'
                                    FROM PlanTerapeutico p
                                        INNER JOIN ViaAdministracion v on p.ViaAdministracionId = v.ViaAdministracionId
                                        WHERE p.PacienteId = {item.PacienteId} AND p.DoctorId = {item.DoctorId} AND p.PreclinicaId = {item.PreclinicaId}
                                          AND p.Activo = 1";
                var _planTerapeutico = await _db.SelectAsync<PlanTerapeuticoViewModel>(_qryPlanes);
      var _consultaTemp = new ConsultaExpedienteViewModel
                {
                    Preclinica = item,
                    ExamenFisico = _examenFisico,
                    Diagnosticos = _diagnosticos,
                    Notas = _notas,
                    ConsultaGeneral = _consultaGeneral,
                    ExamenesIndicados = _listaExamenes,
                    PlanesTerapeuticos = _planTerapeutico,
                };
                listaConsultas.Add(_consultaTemp);
            }

            _expediente.AntecedentesFamiliaresPersonales = _antecedentesPersonales;
            _expediente.Habitos = _habitos;
            _expediente.HistorialGinecoObstetra = _historialGinecoObstetra;
            _expediente.FarmacosUsoActual = _farmacos;
            _expediente.Paciente = _paciente;
            _expediente.Consultas = listaConsultas.OrderByDescending(x => x.Preclinica.CreadoFecha).ToList();

            return _expediente;
        }

        public async Task<List<ConsultaExpedienteViewModel>> GetConsultasByDoctorId(int doctorId)
        {
            List<ConsultaExpedienteViewModel> listaConsultas = new List<ConsultaExpedienteViewModel>();
            using var _db = dbFactory.Open();
            var _preclinicasAtendidas = await _db.SelectAsync<Preclinica>(x => x.DoctorId == doctorId);


            foreach (var preclinica in _preclinicasAtendidas)
            {
                var _examenFisico = await _db.SingleAsync<ExamenFisico>(x => x.DoctorId == preclinica.DoctorId);

                var _qryDiagnosticos = $@"SELECT 
                                    d.DiagnosticoId,
                                    d.PacienteId,
                                    d.DoctorId,
                                    d.CieId,
                                    d.ProblemasClinicos,
                                    d.Activo,
                                    d.CreadoPor,
                                    d.CreadoFecha,
                                    d.ModificadoPor,
                                    d.ModificadoFecha,
                                    d.Notas,
                                    d.PreclinicaId,
                                    c.Codigo as 'CodigoCie',
                                    c.Nombre as 'NombreCie'
                                    FROM 
                                    Diagnosticos d 
                                    INNER JOIN CIE c 
                                    ON d.CieId = c.CieId
                                    WHERE d.DoctorId = {preclinica.DoctorId} ";

                var _diagnosticos = await _db.SelectAsync<DiagnosticosViewModel>(_qryDiagnosticos);

                var _notas = await _db.SelectAsync<Notas>(x => x.DoctorId == preclinica.DoctorId);

                var _consultaGeneral = await _db.SingleAsync<ConsultaGeneral>(x => x.DoctorId == preclinica.DoctorId);

                var _qry = $@"SELECT
                                                ei.ExamenIndicadoId,
                                                ei.PacienteId,
                                                ei.DoctorId,
                                                ei.PreclinicaId,
                                                ei.ExamenCategoriaId,
                                                ei.ExamenTipoId,
                                                ei.ExamenDetalleId,
                                                ei.Nombre,
                                                ei.Activo,
                                                ei.CreadoPor,
                                                ei.CreadoFecha,
                                                ei.ModificadoPor,
                                                ei.ModificadoFecha,
                                                ei.Notas,
                                                ec.Nombre as 'ExamenCategoria',
                                                et.Nombre as 'ExamenTipo',
                                                ed.Nombre as 'ExamenDetalle'
                                            FROM ExamenIndicado ei
                                                INNER JOIN ExamenCategoria ec on ei.ExamenCategoriaId = ec.ExamenCategoriaId
                                                INNER JOIN ExamenTipo et on ei.ExamenTipoId = et.ExamenTipoId
                                                LEFT JOIN ExamenDetalle ed on ei.ExamenDetalleId = ed.ExamenDetalleId
                                                WHERE ei.DoctorId = {preclinica.DoctorId}";

                var _listaExamenes = await _db.SelectAsync<ExamenesIndicadosViewModel>(_qry);
                var _qryPlanes = $@"SELECT
                                        p.PlanTerapeuticoId,
                                        p.PacienteId,
                                        p.DoctorId,
                                        p.PreclinicaId,
                                        p.NombreMedicamento,
                                        p.Dosis,
                                        p.ViaAdministracionId,
                                        p.Horario,
                                        p.Permanente,
                                        p.DiasRequeridos,
                                        p.Activo,
                                        p.CreadoPor,
                                        p.CreadoFecha,
                                        p.ModificadoPor,
                                        p.ModificadoFecha,
                                        p.Notas,
                                        v.Nombre as 'ViaAdministracion'
                                    FROM PlanTerapeutico p
                                        INNER JOIN ViaAdministracion v on p.ViaAdministracionId = v.ViaAdministracionId
                                        WHERE p.DoctorId = {preclinica.DoctorId}";

                var _planTerapeutico = await _db.SelectAsync<PlanTerapeuticoViewModel>(_qryPlanes);

                var _consultaTemp = new ConsultaExpedienteViewModel
                {
                    Preclinica = preclinica,
                    ExamenFisico = _examenFisico,
                    Diagnosticos = _diagnosticos,
                    Notas = _notas,
                    ConsultaGeneral = _consultaGeneral,
                    ExamenesIndicados = _listaExamenes,
                    PlanesTerapeuticos = _planTerapeutico
                };
                listaConsultas.Add(_consultaTemp);

            }


            return listaConsultas;
        }

        public string PacienteEstadoCivil(string estado)
        {
            if (estado.EqualsIgnoreCase("S")) return "Soltero(a)";
            if (estado.EqualsIgnoreCase("C")) return "Casado(a)";
            if (estado.EqualsIgnoreCase("D")) return "Divorciado(a)";
            if (estado.EqualsIgnoreCase("V")) return "Viudo(a)";
            if (estado.EqualsIgnoreCase("UL")) return "Union Libre";

            return "";
        }
    }
}
