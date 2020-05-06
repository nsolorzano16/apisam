using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<ExamenTipo> GetTipoExamenes(int examenCategoriaId)
        {

            using var _db = dbFactory.Open();
            return _db.Select<ExamenTipo>(x => x.ExamenCategoriaId == examenCategoriaId).ToList();
        }

        public ExamenTipo GetExamenTipoById(int examenId)
        {
            using var _db = dbFactory.Open();
            return _db.SingleById<ExamenTipo>(examenId);
        }



    }
}
