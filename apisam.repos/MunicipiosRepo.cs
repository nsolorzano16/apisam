using System;
using System.Collections.Generic;
using System.Linq;
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



        public List<Municipio> Municipios
        {
            get
            {
                using var _db = dbFactory.Open();
                return _db.Select<Municipio>().ToList();
            }

        }

        public List<Municipio> GetMunicipiosByDepartamento(int id)
        {
            using var _db = dbFactory.Open();
            return _db.Select<Municipio>().Where(x => x.DepartamentoId == id).ToList();
        }
    }
}
