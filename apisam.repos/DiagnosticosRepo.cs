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
        public async Task<List<DiagnosticosViewModel>> GetDiagnosticos(int pacienteId, string doctorId, int preclinicaId)
        {
            using var _db = dbFactory.Open();
            var _qry = $@"SELECT 
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
                                    WHERE d.PacienteId = {pacienteId} AND d.DoctorId = '{doctorId}' AND d.PreclinicaId = {preclinicaId} AND d.Activo = 1";

            return await _db.SelectAsync<DiagnosticosViewModel>(_qry);

        }

        public async Task<DiagnosticosViewModel> GetDiagnostico(int diagnosticoId)
        {
            using var _db = dbFactory.Open();
            var _qry = $@"SELECT 
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
                                    WHERE d.DiagnosticoId = {diagnosticoId}";

            return await _db.SingleAsync<DiagnosticosViewModel>(_qry);

        }
    }
}
