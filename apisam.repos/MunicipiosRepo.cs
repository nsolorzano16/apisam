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
    public class MunicipioRepo : IMunicipio
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        public MunicipioRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }



        public async Task<List<Municipio>> Municipios()
        {

            using var _db = dbFactory.Open();
            return await _db.SelectAsync<Municipio>();


        }

        public async Task<List<Municipio>> GetMunicipiosByDepartamento(int id)
        {
            using var _db = dbFactory.Open();
            return await _db.SelectAsync<Municipio>(x => x.DepartamentoId == id);
        }
    }
}
