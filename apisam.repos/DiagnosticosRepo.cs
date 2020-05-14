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
    public class DiagnosticosRepo : IDiagnosticos
    {

        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        private static TimeZoneInfo hondurasTime;

        public DiagnosticosRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        }

        public async Task<RespuestaMetodos> AddDiagnostico(Diagnosticos diagnostico)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                diagnostico.CreadoFecha = dateTime_HN;
                diagnostico.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<Diagnosticos>(diagnostico);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }
            return _resp;

        }
        public async Task<RespuestaMetodos> AddDiagnosticoLista(List<Diagnosticos> diagnosticos)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                diagnosticos.ForEach(x =>
                {
                    x.CreadoFecha = dateTime_HN;
                    x.ModificadoFecha = dateTime_HN;
                });
                using var _db = dbFactory.Open();
                await _db.SaveAllAsync<Diagnosticos>(diagnosticos);
                _resp.Ok = true;

            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;

        }

        public async Task<RespuestaMetodos> UpdateDiagnosticoLista(List<Diagnosticos> diagnosticos)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                diagnosticos.ForEach(x =>
                {
                    x.ModificadoFecha = dateTime_HN;
                });
                using var _db = dbFactory.Open();
                await _db.SaveAllAsync<Diagnosticos>(diagnosticos);
                _resp.Ok = true;

            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;

        }

        public async Task<RespuestaMetodos> UpdateDiagnostico(Diagnosticos diagnostico)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                diagnostico.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<Diagnosticos>(diagnostico);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;
        }
        public async Task<List<Diagnosticos>> GetDiagnosticos(int pacienteId, int doctorId, int preclinicaId)
        {
            using var _db = dbFactory.Open();
            return await _db.SelectAsync<Diagnosticos>(
                x => x.PacienteId == pacienteId
                && x.DoctorId == doctorId && x.PreclinicaId == preclinicaId && x.Activo == true);

        }
    }
}
