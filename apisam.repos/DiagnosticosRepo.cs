using System;
using System.Collections.Generic;
using System.Linq;
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
        public DiagnosticosRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }

        public RespuestaMetodos AddDiagnostico(Diagnosticos diagnostico)
        {
            var _resp = new RespuestaMetodos();
            try
            {
                using var _db = dbFactory.Open();
                diagnostico.CreadoFecha = DateTime.Now.ToLocalTime();
                diagnostico.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<Diagnosticos>(diagnostico);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }
            return _resp;

        }
        public RespuestaMetodos AddDiagnosticoLista(List<Diagnosticos> diagnosticos)
        {
            var _resp = new RespuestaMetodos();
            try
            {
                diagnosticos.ForEach(x =>
                {
                    x.CreadoFecha = DateTime.Now.ToLocalTime();
                    x.ModificadoFecha = DateTime.Now.ToLocalTime();
                });
                using var _db = dbFactory.Open();
                _db.SaveAll<Diagnosticos>(diagnosticos);
                _resp.Ok = true;

            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;

        }

        public RespuestaMetodos UpdateDiagnosticoLista(List<Diagnosticos> diagnosticos)
        {
            var _resp = new RespuestaMetodos();
            try
            {
                diagnosticos.ForEach(x =>
                {
                    x.ModificadoFecha = DateTime.Now.ToLocalTime();
                });
                using var _db = dbFactory.Open();
                _db.SaveAll<Diagnosticos>(diagnosticos);
                _resp.Ok = true;

            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;

        }

        public RespuestaMetodos UpdateDiagnostico(Diagnosticos diagnostico)
        {
            var _resp = new RespuestaMetodos();
            try
            {
                using var _db = dbFactory.Open();
                diagnostico.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<Diagnosticos>(diagnostico);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;
        }
        public List<Diagnosticos> GetDiagnosticos(int pacienteId, int doctorId)
        {
            using var _db = dbFactory.Open();
            return _db.Select<Diagnosticos>(
                x => x.PacienteId == pacienteId
                && x.DoctorId == doctorId).ToList();

        }
    }
}
