using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class AnticonceptivosRepo : IAnticonceptivos
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        public AnticonceptivosRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }


        public async Task<List<Anticonceptivos>> GetAnticonceptivos()
        {
            using var _db = dbFactory.Open();
            return await _db.SelectAsync<Anticonceptivos>();
        }

    }
}
