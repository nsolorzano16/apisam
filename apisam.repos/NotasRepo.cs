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
        public NotasRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }

        public RespuestaMetodos AddNota(Notas nota)
        {
            var _resp = new RespuestaMetodos();
            try
            {
                using var _db = dbFactory.Open();
                nota.CreadoFecha = DateTime.Now.ToLocalTime();
                nota.ModificadoFecha = DateTime.Now.ToLocalTime();
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
            try
            {
                using var _db = dbFactory.Open();
                nota.ModificadoFecha = DateTime.Now.ToLocalTime();
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
            try
            {
                notas.ForEach(x =>
                {
                    x.CreadoFecha = DateTime.Now.ToLocalTime();
                    x.ModificadoFecha = DateTime.Now.ToLocalTime();
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
            try
            {
                notas.ForEach(x =>
                {
                    x.ModificadoFecha = DateTime.Now.ToLocalTime();
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

        public List<Notas> GetNotas(int pacienteId, int doctorId)
        {
            using var _db = dbFactory.Open();
            return _db.Select<Notas>(
                x => x.PacienteId == pacienteId
                && x.DoctorId == doctorId).ToList();
        }
    }
}
