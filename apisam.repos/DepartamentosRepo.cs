using System;
using System.Collections.Generic;
using System.Linq;
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



        public List<Departamento> Departamentos
        {
            get
            {
                using var _db = dbFactory.Open();
                return _db.Select<Departamento>().ToList();
            }

        }

        public Departamento GetDepartamentoById(int id)
        {
            using var _db = dbFactory.Open();
            return _db.Select<Departamento>().FirstOrDefault(x => x.DepartamentoId == id);
        }


    }
}
