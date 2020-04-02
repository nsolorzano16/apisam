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

        public bool AddNota(Notas nota)
        {
            var _flag = false;
            using (var _db = dbFactory.Open())
            {
                nota.CreadoFecha = DateTime.Now.ToLocalTime();
                nota.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<Notas>(nota);
                _flag = true;
            }

            return _flag;
        }
        public bool UpdateNota(Notas nota)
        {
            var _flag = false;
            using (var _db = dbFactory.Open())
            {
                nota.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<Notas>(nota);
                _flag = true;
            }

            return _flag;
        }
        public bool AddNotaLista(List<Notas> notas)
        {
            var _flag = false;
            notas.ForEach(x =>
            {
                x.CreadoFecha = DateTime.Now.ToLocalTime();
                x.ModificadoFecha = DateTime.Now.ToLocalTime();
            });
            using (var _db = dbFactory.Open())
            {
                _db.SaveAll<Notas>(notas);
                _flag = true;
            }

            return _flag;
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
