using System;
using System.Collections.Generic;
using System.Linq;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class ReligionRepo : IReligion
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        public ReligionRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }



        public List<Religion> Religiones
        {
            get
            {
                using var _db = dbFactory.Open();
                return _db.Select<Religion>().ToList();
            }

        }



        public RespuestaMetodos Add(Religion religion)
        {
            var _resp = new RespuestaMetodos();
            try
            {
                using var _db = dbFactory.Open();
                _db.Save<Religion>(religion);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;

            }



            return _resp;

        }
        public RespuestaMetodos Update(Religion religion)
        {
            var _resp = new RespuestaMetodos();
            try
            {
                using var _db = dbFactory.Open();
                _db.Save<Religion>(religion);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }



            return _resp;

        }
        public Religion GetReligionById(int id)
        {
            using var _db = dbFactory.Open();
            return _db.Select<Religion>().FirstOrDefault(x => x.ReligionId == id);
        }
    }
}
