using System;
using System.Linq;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class HistorialGinecoObstetraRepo : IHistorialGinecoObstetra
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        public HistorialGinecoObstetraRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }

        public bool AddAHistorial(HistorialGinecoObstetra historial)
        {
            var _flag = false;
            using (var _db = dbFactory.Open())
            {
                historial.CreadoFecha = DateTime.Now.ToLocalTime();
                historial.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<HistorialGinecoObstetra>(historial);
                _flag = true;
            }

            return _flag;

        }
        public bool UpdateAHistorial(HistorialGinecoObstetra historial)
        {
            var _flag = false;
            using (var _db = dbFactory.Open())
            {

                historial.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<HistorialGinecoObstetra>(historial);
                _flag = true;
            }

            return _flag;
        }
        public HistorialGinecoObstetra GetHistorial(int pacienteId, int doctorId)
        {
            using var _db = dbFactory.Open();
            var historial = _db.Select<HistorialGinecoObstetra>
                ().FirstOrDefault(x => x.PacienteId == pacienteId
                && x.DoctorId == doctorId);
            return historial;
        }
    }
}
