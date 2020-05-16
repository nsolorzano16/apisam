using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class ExamenIndicadoRepo : IExamenIndicado
    {

        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        private static TimeZoneInfo hondurasTime;


        public ExamenIndicadoRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            //hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
            hondurasTime = TimeZoneInfo.Local;
        }

        public async Task<RespuestaMetodos> AddExamenIndicado(ExamenIndicado examen)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                examen.CreadoFecha = dateTime_HN;
                examen.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<ExamenIndicado>(examen);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;
        }

        public async Task<RespuestaMetodos> UpdateExamenIndicado(ExamenIndicado examen)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                examen.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync(examen);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }
            return _resp;
        }

        public async Task<ExamenIndicado> GetExamenIndicadoById(int examenId)
        {
            using var _db = dbFactory.Open();
            var antecedente = await _db.SingleAsync<ExamenIndicado>(x =>
            x.ExamenIndicadoId == examenId && x.Activo == true);
            return antecedente;
        }

        public async Task<List<ExamenIndicado>> GetExamenes(int pacienteId, int doctorId, int preclinicaId)
        {
            using var _db = dbFactory.Open();
            return await _db.SelectAsync<ExamenIndicado>(x => x.PacienteId == pacienteId &&
            x.DoctorId == doctorId && x.PreclinicaId == preclinicaId && x.Activo == true);
        }

        public async Task<List<ExamenesIndicadosViewModel>> GetDetalleExamenesIndicados(int pacienteId, int doctorId, int preclinicaId)
        {
            using var _db = dbFactory.Open();
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

            return await _db.SelectAsync<ExamenesIndicadosViewModel>(_qry);

        }

        public async Task<ExamenesIndicadosViewModel> GetInfoExamenIndicado(int id)
        {
            using var _db = dbFactory.Open();
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
                                                WHERE ei.ExamenIndicadoId = {id}";

            return await _db.SingleAsync<ExamenesIndicadosViewModel>(_qry);

        }


    }
}
