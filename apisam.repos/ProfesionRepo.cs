using System;
using System.Collections.Generic;
using System.Linq;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class ProfesionRepo : IProfesion
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        public ProfesionRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }



        public List<Profesion> Profesiones
        {
            get
            {
                using var _db = dbFactory.Open();
                return _db.Select<Profesion>().ToList();
            }

        }



        public bool Add(Profesion profesion)
        {
            bool _flag = false;
            using var _db = dbFactory.Open();
            _db.Save<Profesion>(profesion);
            _flag = true;


            return _flag;

        }
        public bool Update(Profesion profesion)
        {
            bool _flag = false;
            using var _db = dbFactory.Open();
            _db.Save<Profesion>(profesion);
            _flag = true;


            return _flag;

        }
        public Profesion GetProfesionById(int id)
        {
            using var _db = dbFactory.Open();
            return _db.Select<Profesion>().FirstOrDefault(x => x.ProfesionId == id);
        }
    }
}
