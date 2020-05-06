using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class PlanTerapeutico : RegistroBase
    {
        public PlanTerapeutico()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int PlanTerapeuticoId { get; set; }
        public int PacienteId { get; set; }
        public int DoctorId { get; set; }
        public int PreclinicaId { get; set; }
        public string NombreMedicamento { get; set; }
        public string Dosis { get; set; }
        public string ViaAdministracion { get; set; }
        public string Horario { get; set; }
        public bool Permanente { get; set; }
        public string DiasRequeridos { get; set; }


    }
}
