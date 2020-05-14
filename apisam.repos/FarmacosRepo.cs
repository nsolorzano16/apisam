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
    public class FarmacosRepo : IFarmacosUsoActual
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        private static TimeZoneInfo hondurasTime;

        public FarmacosRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");

        }


        public async Task<RespuestaMetodos> AddFarmaco(FarmacosUsoActual farmaco)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                farmaco.CreadoFecha = dateTime_HN;
                farmaco.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<FarmacosUsoActual>(farmaco);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
        }
        public async Task<RespuestaMetodos> UpdateFarmaco(FarmacosUsoActual farmaco)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                farmaco.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<FarmacosUsoActual>(farmaco);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
        }
        public async Task<RespuestaMetodos> AddFarmacoLista(List<FarmacosUsoActual> farmacos)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                farmacos.ForEach(x =>
                {
                    x.CreadoFecha = dateTime_HN;
                    x.ModificadoFecha = dateTime_HN;
                });
                using var _db = dbFactory.Open();
                await _db.SaveAllAsync<FarmacosUsoActual>(farmacos);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }



            return _resp;
        }
        public async Task<RespuestaMetodos> UpdateFarmacoLista(List<FarmacosUsoActual> farmacos)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {

                farmacos.ForEach(f =>
                {
                    f.ModificadoFecha = dateTime_HN;
                });
                using var _db = dbFactory.Open();
                await _db.SaveAllAsync<FarmacosUsoActual>(farmacos);
                _resp.Ok = true;

            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;
        }
        public async Task<List<FarmacosUsoActual>> GetFarmacos(int pacienteId, int doctorId)
        {
            using var _db = dbFactory.Open();
            return await _db.SelectAsync<FarmacosUsoActual>(
                x => x.PacienteId == pacienteId
                && x.DoctorId == doctorId && x.Activo == true);
        }
    }
}
