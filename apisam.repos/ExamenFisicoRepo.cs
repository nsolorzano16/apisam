using System;
using System.Collections.Generic;
using System.Linq;
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
        public ExamenFisicoRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }

        public RespuestaMetodos AddExamenFisico(ExamenFisico examen)
        {
            var _resp = new RespuestaMetodos();
            try
            {
                using var _db = dbFactory.Open();
                examen.CreadoFecha = DateTime.Now.ToLocalTime();
                examen.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<ExamenFisico>(examen);
                _resp.Ok = true;

            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;

        }
        public RespuestaMetodos UpdateExamenFisico(ExamenFisico examen)
        {
            var _resp = new RespuestaMetodos();
            try
            {
                using var _db = dbFactory.Open();
                examen.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<ExamenFisico>(examen);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;

        }
        public List<ExamenFisico> GetExamenes(int pacienteId, int doctorId)
        {
            using var _db = dbFactory.Open();
            return _db.Select<ExamenFisico>(
                x => x.PacienteId == pacienteId
                && x.DoctorId == doctorId).ToList();
        }
    }
}
