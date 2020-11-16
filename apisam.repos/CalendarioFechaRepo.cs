namespace apisam.repos
{
    using apisam.entities;
    using apisam.entities.ViewModels;
    using apisam.interfaces;
    using apisam.repositories;
    using ServiceStack.OrmLite;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
                evento.FechaFiltro = evento.Inicio;
                await _db.SaveAsync<CalendarioFecha>(evento);
                //notificaciones web
                var noti = new NotificacionesRepo();
                var lista = await _db.SelectAsync<Devices>(x => x.UsuarioId == evento.DoctorId);


                if (lista.Count > 0)
                {
                    lista.ForEach(async item =>
                    {
                        await noti.SendNoti(item.TokenDevice, "Citas en lista de espera");
                    });

                }
                var _flag = await noti.Exists(evento.DoctorId, 0);
                if (_flag)
                {
                    //actualiza si es true
                    await noti.SumaNotificacion(evento.DoctorId, 0);

                }
                {
                    // crea si es false 
                    var model = new CrearNotificacionModel()
                    {
                        id = evento.DoctorId,
                        doctorId = evento.DoctorId,
                        total = 1
                    };
                    await noti.CrearNotificacion(model, 0);
                }

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
            var noti = new NotificacionesRepo();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                evento.ModificadoFecha = dateTime_HN;
                await _db.SaveAsync<CalendarioFecha>(evento);
                if (evento.Activo == false)
                {
                    await noti.RestaNotificacion(evento.DoctorId, 0);
                }
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }
            return _resp;
        }

        public async Task<List<CalendarioFecha>> GetEventos(string doctorId)
        {

            using var _db = dbFactory.Open();
            return await _db.SelectAsync<CalendarioFecha>(x => x.DoctorId == doctorId && x.Activo == true);
        }

        public async Task<List<CalendarioFecha>> GetEventosDashboard(string doctorId)
        {
            using var _db = dbFactory.Open();
            return await _db.SelectAsync<CalendarioFecha>(x => x.DoctorId == doctorId && x.Activo==true);
        }

        public List<CalendarioMovilViewModel> GetEventosMovil(string doctorId)
        {
            var listCalendario = new List<CalendarioMovilViewModel>();

            using var _db = dbFactory.Open();
            var _qry = $@"SELECT DISTINCT c.FechaFiltro FROM calendariofecha c WHERE c.DoctorId = '{doctorId}' AND c.Activo = 1 ";
            var listaFechas = _db.Select<CalendarioFecha>(_qry).ToList();

            listaFechas.ForEach(x =>
           {

               var listaEvents = _db.Select<CalendarioFecha>(y => y.DoctorId == doctorId && y.FechaFiltro == x.FechaFiltro && y.Activo == true);
               var itemTemp = new CalendarioMovilViewModel
               {
                   Date = x.FechaFiltro,
                   Events = listaEvents
               };
               listCalendario.Add(itemTemp);
           });

            return listCalendario;
        }
    }
}
