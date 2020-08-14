using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Legacy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace apisam.repos
{
    public class PlanesRepo : IPlanes
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        private static TimeZoneInfo hondurasTime;

        public PlanesRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        }


        public async  Task<RespuestaMetodos> AddPlan(Planes plan)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                plan.CreadoFecha = dateTime_HN;
                plan.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<Planes>(plan);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
        }

        public async  Task<RespuestaMetodos> UpdatePlan(Planes plan)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                plan.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<Planes>(plan);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
        }

        public async Task<Planes> GetPlanById(int id)
        {
            using var _db = dbFactory.Open();
            return await _db.SingleAsync<Planes>(x=>x.PlanId == id);
        }

       public async  Task<List<Planes>> GetPlanes()
        {
            using var _db = dbFactory.Open();
            return await _db.SelectAsync<Planes>();
        }
    }
}
