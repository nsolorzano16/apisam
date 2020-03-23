using System;
using System.Collections.Generic;
using System.Linq;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class EscolaridadRepo : IEscolaridad
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        public EscolaridadRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }



        public List<Escolaridad> Escolaridades
        {
            get
            {
                using var _db = dbFactory.Open();
                return _db.Select<Escolaridad>().ToList();
            }

        }



        public bool Add(Escolaridad escolaridad)
        {
            bool _flag = false;
            using var _db = dbFactory.Open();
            _db.Save<Escolaridad>(escolaridad);
            _flag = true;


            return _flag;

        }
        public bool Update(Escolaridad escolaridad)
        {
            bool _flag = false;
            using var _db = dbFactory.Open();
            _db.Save<Escolaridad>(escolaridad);
            _flag = true;


            return _flag;

        }
        public Escolaridad GetEscolaridadById(int id)
        {
            using var _db = dbFactory.Open();
            return _db.Select<Escolaridad>().FirstOrDefault(x => x.EscolaridadId == id);
        }
    }
}
