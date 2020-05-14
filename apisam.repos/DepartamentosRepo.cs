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
    public class DepartamentosRepo : IDepartamento
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();

        public DepartamentosRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }



        public async Task<List<Departamento>> Departamentos()
        {
            using var _db = dbFactory.Open();
            return await _db.SelectAsync<Departamento>();

        }



        public async Task<Departamento> GetDepartamentoById(int id)
        {
            using var _db = dbFactory.Open();
            return await _db.SingleAsync<Departamento>(x => x.DepartamentoId == id);
        }


    }
}
