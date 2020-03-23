using System;
using System.Collections.Generic;
using System.Linq;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class GrupoEtnicoRepo : IGrupoEtnico
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        public GrupoEtnicoRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }



        public List<GrupoEtnico> GruposEtnicos
        {
            get
            {
                using var _db = dbFactory.Open();
                return _db.Select<GrupoEtnico>().ToList();
            }

        }



        public bool Add(GrupoEtnico grupoEtnico)
        {
            bool _flag = false;
            using var _db = dbFactory.Open();
            _db.Save<GrupoEtnico>(grupoEtnico);
            _flag = true;


            return _flag;

        }
        public bool Update(GrupoEtnico grupoEtnico)
        {
            bool _flag = false;
            using var _db = dbFactory.Open();
            _db.Save<GrupoEtnico>(grupoEtnico);
            _flag = true;


            return _flag;

        }


        public GrupoEtnico GrupoEtnicoById(int id)
        {
            using var _db = dbFactory.Open();
            return _db.Select<GrupoEtnico>().FirstOrDefault(x => x.GrupoEtnicoId == id);
        }
    }
}
