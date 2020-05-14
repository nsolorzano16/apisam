using System;
using System.Linq;
using System.Threading.Tasks;
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
        private static TimeZoneInfo hondurasTime;

        public HabitosRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        }

        public async Task<RespuestaMetodos> AddAHabito(Habitos habito)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                habito.CreadoFecha = dateTime_HN;
                habito.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<Habitos>(habito);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;

        }
        public async Task<RespuestaMetodos> UpdateAHabito(Habitos habito)
        {

            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                habito.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<Habitos>(habito);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
        }
        public async Task<Habitos> GetHabito(int pacienteId, int doctorId)
        {
            using var _db = dbFactory.Open();
            return await _db.SingleAsync<Habitos>
                 (x => x.PacienteId == pacienteId
                 && x.DoctorId == doctorId && x.Activo == true);

        }
    }
}
