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

        public bool AddDiagnostico(Diagnosticos diagnostico)
        {
            var _flag = false;
            using (var _db = dbFactory.Open())
            {
                diagnostico.CreadoFecha = DateTime.Now;
                diagnostico.ModificadoFecha = DateTime.Now;
                _db.Save<Diagnosticos>(diagnostico);
                _flag = true;
            }

            return _flag;

        }
        public bool AddDiagnosticoLista(List<Diagnosticos> diagnosticos)
        {
            var _flag = false;
            diagnosticos.ForEach(x =>
            {
                x.CreadoFecha = DateTime.Now;
                x.ModificadoFecha = DateTime.Now;
            });
            using (var _db = dbFactory.Open())
            {
                _db.SaveAll<Diagnosticos>(diagnosticos);
                _flag = true;
            }

            return _flag;

        }

        public bool UpdateDiagnostico(Diagnosticos diagnostico)
        {
            var _flag = false;
            using (var _db = dbFactory.Open())
            {
                diagnostico.ModificadoFecha = DateTime.Now;
                _db.Save<Diagnosticos>(diagnostico);
                _flag = true;
            }

            return _flag;
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
