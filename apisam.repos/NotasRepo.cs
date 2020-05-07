using System;
using System.Collections.Generic;
using System.Linq;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class NotasRepo : INotas
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        private static TimeZoneInfo hondurasTime;

        public NotasRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        }

        public RespuestaMetodos AddNota(Notas nota)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                nota.CreadoFecha = dateTime_HN;
                nota.ModificadoFecha = dateTime_HN;
                _db.Save<Notas>(nota);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
        }

        public RespuestaMetodos UpdateNota(Notas nota)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                nota.ModificadoFecha = dateTime_HN;
                _db.Save<Notas>(nota);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
        }
        public RespuestaMetodos AddNotaLista(List<Notas> notas)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                notas.ForEach(x =>
                {
                    x.CreadoFecha = dateTime_HN;
                    x.ModificadoFecha = dateTime_HN;
                });
                using var _db = dbFactory.Open();
                _db.SaveAll<Notas>(notas);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
        }

        public RespuestaMetodos UpdateNotaLista(List<Notas> notas)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                notas.ForEach(x =>
                {
                    x.ModificadoFecha = dateTime_HN;
                });
                using var _db = dbFactory.Open();
                _db.SaveAll<Notas>(notas);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
        }

        public List<Notas> GetNotas(int pacienteId, int doctorId, int preclinicaId)
        {
            using var _db = dbFactory.Open();
            return _db.Select<Notas>(
                x => x.PacienteId == pacienteId
                && x.DoctorId == doctorId && x.PreclinicaId == preclinicaId && x.Activo == true).ToList();
        }
    }
}
