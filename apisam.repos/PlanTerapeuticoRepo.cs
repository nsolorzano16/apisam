using System;
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

        public RespuestaMetodos AddPlanTerapeutico(PlanTerapeutico planTerapeutico)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                planTerapeutico.CreadoFecha = dateTime_HN;
                planTerapeutico.ModificadoFecha = dateTime_HN;
                _db.Save<PlanTerapeutico>(planTerapeutico);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;
        }

        public RespuestaMetodos UpdatePlanTerapeutico(PlanTerapeutico planTerapeutico)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                planTerapeutico.ModificadoFecha = dateTime_HN;
                _db.Save<PlanTerapeutico>(planTerapeutico);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }
            return _resp;
        }

        public PlanTerapeutico GetPlanTerapeuticoById(int planTerapeuticoId)
        {
            using var _db = dbFactory.Open();
            var antecedente = _db.Single<PlanTerapeutico>(x =>
            x.PlanTerapeuticoId == planTerapeuticoId && x.Activo == true);
            return antecedente;
        }


    }
}
