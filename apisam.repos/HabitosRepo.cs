using System;
using System.Linq;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class HabitosRepo : IHabitos
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        public HabitosRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }

        public bool AddAHabito(Habitos habito)
        {
            var _flag = false;
            using (var _db = dbFactory.Open())
            {
                habito.CreadoFecha = DateTime.Now.ToLocalTime();
                habito.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<Habitos>(habito);
                _flag = true;
            }

            return _flag;

        }
        public bool UpdateAHabito(Habitos habito)
        {

            var _flag = false;
            using (var _db = dbFactory.Open())
            {

                habito.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<Habitos>(habito);
                _flag = true;
            }

            return _flag;
        }
        public Habitos GetHabito(int pacienteId, int doctorId)
        {
            using var _db = dbFactory.Open();
            var habito = _db.Select<Habitos>
                ().FirstOrDefault(x => x.PacienteId == pacienteId
                && x.DoctorId == doctorId);
            return habito;
        }
    }
}
