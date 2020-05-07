using System;
using System.Collections.Generic;
using System.Linq;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class ViaAdministracionRepo : IViaAdministracion
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        public ViaAdministracionRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }


        public List<ViaAdministracion> ListaViaAdministracion
        {
            get
            {
                using var _db = dbFactory.Open();
                return _db.Select<ViaAdministracion>().ToList();
            }

        }
    }
}
