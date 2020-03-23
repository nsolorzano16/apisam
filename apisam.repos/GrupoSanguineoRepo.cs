using System;
using System.Collections.Generic;
using System.Linq;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class GrupoSanguineoRepo : IGrupoSanguineo
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        public GrupoSanguineoRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }



        public List<GrupoSanguineo> GruposSanguineos

        {
            get
            {
                using var _db = dbFactory.Open();
                return _db.Select<GrupoSanguineo>().ToList();
            }

        }
        public GrupoSanguineo GetGrupoSanguineoById(int id)
        {
            using var _db = dbFactory.Open();
            return _db.Select<GrupoSanguineo>().FirstOrDefault(x => x.GrupoSanguineoId == id);
        }
    }
}
