using System;
using System.Collections.Generic;
using System.Linq;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class ReligionRepo : IReligion
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        public ReligionRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }



        public List<Religion> Religiones
        {
            get
            {
                using var _db = dbFactory.Open();
                return _db.Select<Religion>().ToList();
            }

        }



        public bool Add(Religion religion)
        {
            bool _flag = false;
            using var _db = dbFactory.Open();
            _db.Save<Religion>(religion);
            _flag = true;


            return _flag;

        }
        public bool Update(Religion religion)
        {
            bool _flag = false;
            using var _db = dbFactory.Open();
            _db.Save<Religion>(religion);
            _flag = true;


            return _flag;

        }
        public Religion GetReligionById(int id)
        {
            using var _db = dbFactory.Open();
            return _db.Select<Religion>().FirstOrDefault(x => x.ReligionId == id);
        }
    }
}
