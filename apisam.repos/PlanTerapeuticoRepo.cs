using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class PlanTerapeuticoRepo : IPlanTerapeutico
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        private static TimeZoneInfo hondurasTime;

        public PlanTerapeuticoRepo()
        {

            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        }

        public async Task<RespuestaMetodos> AddPlanTerapeutico(PlanTerapeutico planTerapeutico)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                planTerapeutico.CreadoFecha = dateTime_HN;
                planTerapeutico.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<PlanTerapeutico>(planTerapeutico);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;
        }

        public async Task<RespuestaMetodos> UpdatePlanTerapeutico(PlanTerapeutico planTerapeutico)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                planTerapeutico.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<PlanTerapeutico>(planTerapeutico);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }
            return _resp;
        }

        public async Task<PlanTerapeutico> GetPlanTerapeutico(int pacienteId, int doctorId, int preclinicaId)
        {
            using var _db = dbFactory.Open();
            return await _db.SingleAsync<PlanTerapeutico>(x =>
                  x.PacienteId == pacienteId && x.DoctorId == doctorId && x.PreclinicaId == preclinicaId && x.Activo == true);

        }


        public async Task<List<PlanTerapeutico>> GetPlanes(int pacienteId, int doctorId, int preclinicaId)
        {
            using var _db = dbFactory.Open();
            return await _db.SelectAsync<PlanTerapeutico>(x => x.PacienteId == pacienteId && x.DoctorId == doctorId
            && x.PreclinicaId == preclinicaId && x.Activo == true);
        }

    }
}
