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
    public class ExamenTipoRepo : IExamenTipo
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();

        public ExamenTipoRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }

        public async Task<List<ExamenTipo>> GetTipoExamenes(int examenCategoriaId)
        {

            using var _db = dbFactory.Open();
            return await _db.SelectAsync<ExamenTipo>(x => x.ExamenCategoriaId == examenCategoriaId);
        }

        public async Task<ExamenTipo> GetExamenTipoById(int examenId)
        {
            using var _db = dbFactory.Open();
            return await _db.SingleByIdAsync<ExamenTipo>(examenId);
        }

        public async Task<List<ExamenTipo>> GetTipoExamenesAll()
        {
            using var _db = dbFactory.Open();
            return await _db.SelectAsync<ExamenTipo>();
        }


    }
}
