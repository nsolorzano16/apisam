using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class CalendarioFecha : RegistroBase
    {
        public CalendarioFecha()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int CalendarioFechaId { get; set; }
        public string DoctorId { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public bool TodoElDia { get; set; }
        public string ColorPrimario { get; set; }
        public string ColorSecundario { get; set; }
        public DateTime FechaFiltro { get; set; }



    }
}
