using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apisam.entities;
using apisam.entities.ViewModels;

namespace apisam.interfaces
{
    public interface ICalendarioFecha
    {

        Task<RespuestaMetodos> AddCalendarioFecha(CalendarioFecha evento);
        Task<RespuestaMetodos> UpdateCalendarioFecha(CalendarioFecha evento);
        Task<List<CalendarioFecha>> GetEventos(string doctorId);
        List<CalendarioMovilViewModel> GetEventosMovil(string doctorId);

        Task<List<CalendarioFecha>> GetEventosDashboard(string doctorId);


    }
}
