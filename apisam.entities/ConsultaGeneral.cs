using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class ConsultaGeneral : RegistroBase
    {
        public ConsultaGeneral()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int ConsultaId { get; set; }
        public int PacienteId { get; set; }
        public string DoctorId { get; set; }
        public int PreclinicaId { get; set; }
        public string MotivoConsulta { get; set; }
        public string Fog { get; set; }
        public string Hea { get; set; }
    }
}


