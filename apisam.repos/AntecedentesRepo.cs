using System;
using System.Linq;
using apisam.entities;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class AntecedentesRepo : IAntecedentesFamiliaresPersonales
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        public AntecedentesRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
        }



        public bool AddAntecedentes(AntecedentesFamiliaresPersonales antecedente)
        {
            var _flag = false;
            using (var _db = dbFactory.Open())
            {
                antecedente.CreadoFecha = DateTime.Now.ToLocalTime();
                antecedente.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<AntecedentesFamiliaresPersonales>(antecedente);
                _flag = true;
            }

            return _flag;
        }
        public bool UpdateAntecedentes(AntecedentesFamiliaresPersonales antecedente)
        {
            var _flag = false;
            using (var _db = dbFactory.Open())
            {
                antecedente.ModificadoFecha = DateTime.Now.ToLocalTime();
                _db.Save<AntecedentesFamiliaresPersonales>(antecedente);
                _flag = true;
            }

            return _flag;
        }
        public AntecedentesFamiliaresPersonales GetAntecedente(int pacienteId, int doctorId)
        {
            using var _db = dbFactory.Open();
            var antecedente = _db.Select<AntecedentesFamiliaresPersonales>
                ().FirstOrDefault(x => x.PacienteId == pacienteId
                && x.DoctorId == doctorId);
            return antecedente;
        }
    }
}
