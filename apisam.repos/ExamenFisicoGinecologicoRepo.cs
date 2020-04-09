using System;
using System.Collections.Generic;
using System.Linq;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class ExamenFisicoGinecologicoRepo : IExamenFisicoGinecologico
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();

        public ExamenFisicoGinecologicoRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }

        public RespuestaMetodos AddExamenFisicoGinecologico(ExamenFisicoGinecologico examen)
        {
            var _resp = new RespuestaMetodos();
            try
            {
                using var _db = dbFactory.Open();
                examen.CreadoFecha = DateTime.Now.ToLocalTime();
                examen.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<ExamenFisicoGinecologico>(examen);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }
            return _resp;
        }
        public RespuestaMetodos UpdateExamenFisicoGinecologico(ExamenFisicoGinecologico examen)

        {
            var _resp = new RespuestaMetodos();
            try
            {

                using var _db = dbFactory.Open();
                examen.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<ExamenFisicoGinecologico>(examen);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;

            }

            return _resp;
        }
        public List<ExamenFisicoGinecologico> GetExamenesGinecologicos(int pacienteId, int doctorId)

        {
            using var _db = dbFactory.Open();
            return _db.Select<ExamenFisicoGinecologico>(
                x => x.PacienteId == pacienteId
                && x.DoctorId == doctorId).ToList();
        }
    }
}
