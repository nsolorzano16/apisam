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
    public class GrupoSanguineoRepo : IGrupoSanguineo
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        public GrupoSanguineoRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }



        public async Task<List<GrupoSanguineo>> GruposSanguineos()

        {

            using var _db = dbFactory.Open();
            return await _db.SelectAsync<GrupoSanguineo>();


        }
        public async Task<GrupoSanguineo> GetGrupoSanguineoById(int id)
        {
            using var _db = dbFactory.Open();
            return await _db.SingleByIdAsync<GrupoSanguineo>(id);
        }
    }
}
