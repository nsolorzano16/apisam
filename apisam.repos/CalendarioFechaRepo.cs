using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class CalendarioFechaRepo : ICalendarioFecha
    {

        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        private static TimeZoneInfo hondurasTime;


        public CalendarioFechaRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        }

        public async Task<RespuestaMetodos> AddCalendarioFecha(CalendarioFecha evento)
        {

            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                evento.CreadoFecha = dateTime_HN;
                evento.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<CalendarioFecha>(evento);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;
        }

        public async Task<RespuestaMetodos> UpdateCalendarioFecha(CalendarioFecha evento)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                evento.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<CalendarioFecha>(evento);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }
            return _resp;
        }



        public async Task<List<CalendarioFecha>> GetEventos(int doctorId)
        {

            using var _db = dbFactory.Open();
            return await _db.SelectAsync<CalendarioFecha>(x => x.DoctorId == doctorId);

        }


        public List<CalendarioMovilViewModel> GetEventosMovil(int doctorId)
        {

            using var _db = dbFactory.Open();
            var _qry = $@"SELECT DISTINCT c.Inicio from CalendarioFecha c WHERE c.DoctorId = {doctorId}";
            var listCalendario = new List<CalendarioMovilViewModel>();
            var listaFechas = _db.Select<CalendarioFecha>(_qry).ToList();

            listaFechas.ForEach(x =>
           {

               var listaEvents = _db.Select<CalendarioFecha>(y => y.DoctorId == doctorId && y.Inicio == x.Inicio);
               var itemTemp = new CalendarioMovilViewModel
               {
                   Date = x.Inicio,
                   Events = listaEvents
               };
               listCalendario.Add(itemTemp);
           });

            return listCalendario;



        }



    }
}
