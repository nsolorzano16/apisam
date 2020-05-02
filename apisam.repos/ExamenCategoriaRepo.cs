﻿using System;
using System.Collections.Generic;
using System.Linq;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class ExamenCategoriaRepo : IExamenCategoria
    {

        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();


        public ExamenCategoriaRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }


        public List<ExamenCategoria> CategoriasExamenes
        {
            get
            {
                using var _db = dbFactory.Open();
                return _db.Select<ExamenCategoria>().ToList();
            }
        }

        public ExamenCategoria GetExamenById(int examenId)
        {

            using var _db = dbFactory.Open();
            return _db.SingleById<ExamenCategoria>(examenId);
        }



    }
}
