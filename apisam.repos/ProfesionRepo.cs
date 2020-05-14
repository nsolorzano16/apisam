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
    public class ProfesionRepo : IProfesion
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        public ProfesionRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }



        public async Task<List<Profesion>> Profesiones()
        {

            using var _db = dbFactory.Open();
            return await _db.SelectAsync<Profesion>();


        }



        public async Task<RespuestaMetodos> Add(Profesion profesion)
        {
            var _resp = new RespuestaMetodos();
            try
            {

                using var _db = dbFactory.Open();
                await _db.SaveAsync<Profesion>(profesion);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;

        }
        public async Task<RespuestaMetodos> Update(Profesion profesion)
        {
            var _resp = new RespuestaMetodos();
            try
            {
                using var _db = dbFactory.Open();
                await _db.SaveAsync<Profesion>(profesion);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }



            return _resp;

        }
        public async Task<Profesion> GetProfesionById(int id)
        {
            using var _db = dbFactory.Open();
            return await _db.SingleByIdAsync<Profesion>(id);
        }
    }
}
