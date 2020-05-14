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
    public class PaisRepo : IPais
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();

        public PaisRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }



        public async Task<List<Pais>> Paises()
        {

            using var _db = dbFactory.Open();
            return await _db.SelectAsync<Pais>();


        }

        public async Task<Pais> GetPaisById(int id)
        {
            using var _db = dbFactory.Open();
            return await _db.SingleByIdAsync<Pais>(id);
        }


    }
}
