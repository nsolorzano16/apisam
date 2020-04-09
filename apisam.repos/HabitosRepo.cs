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

        public RespuestaMetodos AddAHabito(Habitos habito)
        {
            var _resp = new RespuestaMetodos();
            try
            {
                using var _db = dbFactory.Open();
                habito.CreadoFecha = DateTime.Now.ToLocalTime();
                habito.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<Habitos>(habito);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;

        }
        public RespuestaMetodos UpdateAHabito(Habitos habito)
        {

            var _resp = new RespuestaMetodos();
            try
            {
                using var _db = dbFactory.Open();
                habito.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<Habitos>(habito);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
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
