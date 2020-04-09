using System;
using System.Collections.Generic;
using System.Linq;
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
        public FarmacosRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }

        public RespuestaMetodos AddFarmaco(FarmacosUsoActual farmaco)
        {
            var _resp = new RespuestaMetodos();
            try
            {
                using var _db = dbFactory.Open();
                farmaco.CreadoFecha = DateTime.Now.ToLocalTime();
                farmaco.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<FarmacosUsoActual>(farmaco);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
        }
        public RespuestaMetodos UpdateFarmaco(FarmacosUsoActual farmaco)
        {
            var _resp = new RespuestaMetodos();
            try
            {
                using var _db = dbFactory.Open();
                farmaco.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<FarmacosUsoActual>(farmaco);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;
        }
        public RespuestaMetodos AddFarmacoLista(List<FarmacosUsoActual> farmacos)
        {
            var _resp = new RespuestaMetodos();
            try
            {
                farmacos.ForEach(x =>
                {
                    x.CreadoFecha = DateTime.Now.ToLocalTime();
                    x.ModificadoFecha = DateTime.Now.ToLocalTime();
                });
                using var _db = dbFactory.Open();
                _db.SaveAll<FarmacosUsoActual>(farmacos);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }



            return _resp;
        }
        public RespuestaMetodos UpdateFarmacoLista(List<FarmacosUsoActual> farmacos)
        {
            var _resp = new RespuestaMetodos();
            try
            {

                farmacos.ForEach(f =>
                {
                    f.ModificadoFecha = DateTime.Now.ToLocalTime();
                });
                using var _db = dbFactory.Open();
                _db.SaveAll<FarmacosUsoActual>(farmacos);
                _resp.Ok = true;

            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;
        }
        public List<FarmacosUsoActual> GetFarmacos(int pacienteId, int doctorId)
        {
            using var _db = dbFactory.Open();
            return _db.Select<FarmacosUsoActual>(
                x => x.PacienteId == pacienteId
                && x.DoctorId == doctorId).ToList();
        }
    }
}
