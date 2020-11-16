using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class ExamenFisicoRepo : IExamenFisico
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        private static TimeZoneInfo hondurasTime;

        public ExamenFisicoRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        }

        public async Task<RespuestaMetodos> AddExamenFisico(ExamenFisico examen)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                examen.CreadoFecha = dateTime_HN;
                examen.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<ExamenFisico>(examen);
                _resp.Ok = true;

            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;

        }
        public async Task<RespuestaMetodos> UpdateExamenFisico(ExamenFisico examen)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                examen.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<ExamenFisico>(examen);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;

        }
        public async Task<List<ExamenFisico>> GetExamenes(int pacienteId, string doctorId)
        {
            using var _db = dbFactory.Open();
            return await _db.SelectAsync<ExamenFisico>(
                x => x.PacienteId == pacienteId
                && x.DoctorId == doctorId);
        }

        public async Task<ExamenFisico> GetExamenFisico(int pacienteId, string doctorId, int preclinicaId)
        {
            using var _db = dbFactory.Open();
            return await _db.SingleAsync<ExamenFisico>(x => x.PacienteId == pacienteId && x.DoctorId == doctorId
            && x.PreclinicaId == preclinicaId && x.Activo == true);
        }
    }
}
