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

        public bool AddExamenFisicoGinecologico(ExamenFisicoGinecologico examen)
        {
            var _flag = false;
            using (var _db = dbFactory.Open())
            {
                examen.CreadoFecha = DateTime.Now;
                examen.ModificadoFecha = DateTime.Now;
                _db.Save<ExamenFisicoGinecologico>(examen);
                _flag = true;
            }

            return _flag;
        }
        public bool UpdateExamenFisicoGinecologico(ExamenFisicoGinecologico examen)

        {
            var _flag = false;
            using (var _db = dbFactory.Open())
            {
                examen.ModificadoFecha = DateTime.Now;
                _db.Save<ExamenFisicoGinecologico>(examen);
                _flag = true;
            }

            return _flag;
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
