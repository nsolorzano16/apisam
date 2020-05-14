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
    public class GrupoEtnicoRepo : IGrupoEtnico
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        public GrupoEtnicoRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }



        public async Task<List<GrupoEtnico>> GruposEtnicos()
        {

            using var _db = dbFactory.Open();
            return await _db.SelectAsync<GrupoEtnico>();


        }



        public async Task<RespuestaMetodos> Add(GrupoEtnico grupoEtnico)
        {
            var _resp = new RespuestaMetodos();
            try
            {

                using var _db = dbFactory.Open();
                await _db.SaveAsync<GrupoEtnico>(grupoEtnico);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;

        }
        public async Task<RespuestaMetodos> Update(GrupoEtnico grupoEtnico)
        {
            var _resp = new RespuestaMetodos();
            try
            {

                using var _db = dbFactory.Open();
                await _db.SaveAsync<GrupoEtnico>(grupoEtnico);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }


            return _resp;

        }


        public async Task<GrupoEtnico> GetGrupoEtnicoById(int id)
        {
            using var _db = dbFactory.Open();
            return await _db.SingleByIdAsync<GrupoEtnico>(id);
        }
    }
}
