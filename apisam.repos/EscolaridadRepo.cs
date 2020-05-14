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
    public class EscolaridadRepo : IEscolaridad
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        public EscolaridadRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }



        public async Task<List<Escolaridad>> Escolaridades()
        {

            using var _db = dbFactory.Open();
            return await _db.SelectAsync<Escolaridad>();

        }



        public async Task<RespuestaMetodos> Add(Escolaridad escolaridad)
        {
            var _resp = new RespuestaMetodos();
            try
            {
                using var _db = dbFactory.Open();

                await _db.SaveAsync<Escolaridad>(escolaridad);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }
            return _resp;

        }
        public async Task<RespuestaMetodos> Update(Escolaridad escolaridad)
        {
            var _resp = new RespuestaMetodos();
            try
            {
                using var _db = dbFactory.Open();
                await _db.SaveAsync<Escolaridad>(escolaridad);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }
            return _resp;

        }
        public async Task<Escolaridad> GetEscolaridadById(int id)
        {
            using var _db = dbFactory.Open();
            return await _db.SingleAsync<Escolaridad>(x => x.EscolaridadId == id);
        }
    }
}
