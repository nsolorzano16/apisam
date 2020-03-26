using System;
using System.Collections.Generic;
using System.Linq;
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



        public List<Pais> Paises

        {
            get
            {
                using var _db = dbFactory.Open();
                return _db.Select<Pais>().ToList();
            }

        }
        public Pais GetPaisById(int id)
        {
            using var _db = dbFactory.Open();
            return _db.Select<Pais>().FirstOrDefault(x => x.PaisId == id);
        }
    }
}

