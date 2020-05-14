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
    public class ExamenDetalleRepo : IExamenDetalle
    {

        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();

        public ExamenDetalleRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }

        public async Task<List<ExamenDetalle>> GetDetalleExamenes(int examenTipoId, int examenCategoriaId)
        {
            using var _db = dbFactory.Open();
            return await _db.SelectAsync<ExamenDetalle>(x => x.ExamenTipoId == examenTipoId
            && x.ExamenCategoriaId == examenCategoriaId);

        }

        public async Task<ExamenDetalle> GetExamenDetalleById(int examenId)
        {
            using var _db = dbFactory.Open();
            return await _db.SingleByIdAsync<ExamenDetalle>(examenId);
        }

    }
}
