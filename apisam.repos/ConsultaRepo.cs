using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            // hondurasTime = TimeZoneInfo.Local;
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

            var _historialGinecoObstetra = await _db.SingleAsync<HistorialGinecoObstetra>(
                x => x.PacienteId == pacienteId
                && x.Activo == true);

            var _farmacos = await _db.SelectAsync<FarmacosUsoActual>
                (x => x.PacienteId ==
                pacienteId && x.Activo == true);

            var _examenFisico = await _db.SingleAsync<ExamenFisico>(
                x => x.PreclinicaId == preclinicaId && x.PacienteId == pacienteId
                && x.DoctorId == doctorId && x.Activo == true);

            //var _examenFisicoGinecologico = await _db.SingleAsync<ExamenFisicoGinecologico>(
            //    x => x.PreclinicaId == preclinicaId && x.PacienteId == pacienteId
            //    && x.DoctorId == doctorId && x.Activo == true);

            var _diagnosticos = await _db.SelectAsync<Diagnosticos>(x => x.PreclinicaId == preclinicaId
            && x.PacienteId == pacienteId
            && x.DoctorId == doctorId && x.Activo == true);

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


        public List<ConsultaViewModel> GetExpediente(int pacienteId, int doctorId)
        {
            var listaConsultas = new List<ConsultaViewModel>();
            using var _db = dbFactory.Open();
            var _qry = $@"
                                SELECT * from Preclinica 
                                WHERE PacienteId = {pacienteId} AND DoctorId = {doctorId} and Atendida = 1
                                ORDER BY CreadoFecha desc";

            var listaPreclinicas = _db.Select<Preclinica>(_qry);

            listaPreclinicas.ForEach(async x =>
            {

                var detail = await GetDetalleConsulta(doctorId, pacienteId, x.PreclinicaId);
                listaConsultas.Add(detail);
            });



            return listaConsultas;

        }

    }
}
