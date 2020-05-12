using System;
using System.Collections.Generic;
using apisam.entities;
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
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        }

        public RespuestaMetodos AddExamenIndicado(ExamenIndicado examen)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                examen.CreadoFecha = dateTime_HN;
                examen.ModificadoFecha = dateTime_HN;
                _db.Save<ExamenIndicado>(examen);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;
        }

        public RespuestaMetodos UpdateExamenIndicado(ExamenIndicado examen)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                examen.ModificadoFecha = dateTime_HN;
                _db.Save<ExamenIndicado>(examen);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }
            return _resp;
        }

        public ExamenIndicado GetExamenIndicadoById(int examenId)
        {
            using var _db = dbFactory.Open();
            var antecedente = _db.Single<ExamenIndicado>(x =>
            x.ExamenIndicadoId == examenId && x.Activo == true);
            return antecedente;
        }

        public List<ExamenIndicado> GetExamenes(int pacienteId, int doctorId, int preclinicaId)
        {
            using var _db = dbFactory.Open();
            return _db.Select<ExamenIndicado>(x => x.PacienteId == pacienteId &&
            x.DoctorId == doctorId && x.PreclinicaId == preclinicaId && x.Activo == true);
        }


    }
}
