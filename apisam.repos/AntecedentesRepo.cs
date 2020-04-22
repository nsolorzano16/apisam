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
        private static TimeZoneInfo hondurasTime;


        public AntecedentesRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        }



        public RespuestaMetodos AddAntecedentes(AntecedentesFamiliaresPersonales antecedente)
        {

            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                antecedente.CreadoFecha = dateTime_HN;
                antecedente.ModificadoFecha = dateTime_HN;
                _db.Save<AntecedentesFamiliaresPersonales>(antecedente);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;
        }

        public RespuestaMetodos UpdateAntecedentes(AntecedentesFamiliaresPersonales antecedente)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                antecedente.ModificadoFecha = dateTime_HN;
                _db.Save<AntecedentesFamiliaresPersonales>(antecedente);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }
            return _resp;
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
