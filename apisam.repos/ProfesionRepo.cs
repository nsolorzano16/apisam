using System;
using System.Collections.Generic;
using System.Linq;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class ProfesionRepo : IProfesion
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        public ProfesionRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }



        public List<Profesion> Profesiones
        {
            get
            {
                using var _db = dbFactory.Open();
                return _db.Select<Profesion>().ToList();
            }

        }



        public RespuestaMetodos Add(Profesion profesion)
        {
            var _resp = new RespuestaMetodos();
            try
            {

                using var _db = dbFactory.Open();
                _db.Save<Profesion>(profesion);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;

        }
        public RespuestaMetodos Update(Profesion profesion)
        {
            var _resp = new RespuestaMetodos();
            try
            {
                using var _db = dbFactory.Open();
                _db.Save<Profesion>(profesion);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }



            return _resp;

        }
        public Profesion GetProfesionById(int id)
        {
            using var _db = dbFactory.Open();
            return _db.Select<Profesion>().FirstOrDefault(x => x.ProfesionId == id);
        }
    }
}
