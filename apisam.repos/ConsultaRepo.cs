using System;
using System.Linq;
using apisam.entities;
using apisam.entities.ViewModels;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class ConsultaRepo : IConsulta
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        private static TimeZoneInfo hondurasTime;

        public ConsultaRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        }

        public ConsultaViewModel GetDetalleConsulta(int doctorId, int pacienteId, int preclinicaId)
        {
            ConsultaViewModel _resp = new ConsultaViewModel();
            using var _db = dbFactory.Open();
            var _preclinica = _db.Single<Preclinica>(x => x.PreclinicaId ==
            preclinicaId && x.PacienteId == pacienteId && x.DoctorId == doctorId
            && x.Activo == true && x.Atendida == true);

            var _antecedentesPersonales = _db.Single<AntecedentesFamiliaresPersonales>
                (x => x.PacienteId == pacienteId
                && x.DoctorId == doctorId && x.Activo == true);

            var _habitos = _db.Single<Habitos>(x => x.PacienteId
            == pacienteId && x.DoctorId == doctorId && x.Activo == true);

            var _historialGinecoObstetra = _db.Single<HistorialGinecoObstetra>(
                x => x.PacienteId == pacienteId
                && x.DoctorId == doctorId && x.Activo == true);

            var _farmacos = _db.Select<FarmacosUsoActual>
                (x => x.PacienteId ==
                pacienteId && x.DoctorId == doctorId && x.Activo == true).ToList();

            var _examenFisico = _db.Single<ExamenFisico>(
                x => x.PreclinicaId == preclinicaId && x.PacienteId == pacienteId
                && x.DoctorId == doctorId && x.Activo == true);

            var _examenFisicoGinecologico = _db.Single<ExamenFisicoGinecologico>(
                x => x.PreclinicaId == preclinicaId && x.PacienteId == pacienteId
                && x.DoctorId == doctorId && x.Activo == true);

            var _diagnosticos = _db.Select<Diagnosticos>(x => x.PreclinicaId == preclinicaId
            && x.PacienteId == pacienteId
            && x.DoctorId == doctorId && x.Activo == true).ToList();

            var _notas = _db.Select<Notas>(x => x.PreclinicaId == preclinicaId
            && x.PacienteId == pacienteId && x.DoctorId == doctorId && x.Activo == true).ToList();

            var _consultaGeneral = _db.Single<ConsultaGeneral>(x => x.PreclinicaId == preclinicaId
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
                                                INNER JOIN ExamenDetalle ed on ei.ExamenDetalleId = ed.ExamenDetalleId";

            var _listaExamenes = _db.Select<ExamenesIndicadosViewModel>(_qry
                ).ToList();

            var _planTerapeutico = _db.Single<PlanTerapeutico>(x => x.PreclinicaId == preclinicaId
             && x.PacienteId == pacienteId && x.DoctorId == doctorId && x.Activo == true);

            _resp.Preclinica = _preclinica;
            _resp.AntecedentesFamiliaresPersonales = _antecedentesPersonales;
            _resp.Habitos = _habitos;
            _resp.HistorialGinecoObstetra = _historialGinecoObstetra;
            _resp.FarmacosUsoActual = _farmacos;
            _resp.ExamenFisico = _examenFisico;
            _resp.ExamenFisicoGinecologico = _examenFisicoGinecologico;
            _resp.Diagnosticos = _diagnosticos;
            _resp.Notas = _notas;
            _resp.ConsultaGeneral = _consultaGeneral;
            _resp.ExamenesIndicados = _listaExamenes;
            _resp.PlanTerapeutico = _planTerapeutico;

            return _resp;
        }



        public RespuestaMetodos AddConsultaGeneral(ConsultaGeneral consulta)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                consulta.CreadoFecha = dateTime_HN;
                consulta.ModificadoFecha = dateTime_HN;
                _db.Save<ConsultaGeneral>(consulta);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;
        }

        public RespuestaMetodos UpdateConsultaGeneral(ConsultaGeneral consulta)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                consulta.ModificadoFecha = dateTime_HN;
                _db.Save<ConsultaGeneral>(consulta);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }
            return _resp;
        }

        public ConsultaGeneral GetConsultaGeneral(int pacienteId, int doctorId, int preclinicaId)
        {
            using var _db = dbFactory.Open();
            var consulta = _db.Single<ConsultaGeneral>(x => x.PacienteId == pacienteId
            && x.DoctorId == doctorId && x.PreclinicaId == preclinicaId && x.Activo == true);
            return consulta;
        }


    }
}
