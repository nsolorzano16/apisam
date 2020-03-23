using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class FarmacosUsoActual : RegistroBase
    {
        public FarmacosUsoActual()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int FarmacoId { get; set; }
        public int PacienteId { get; set; }
        public int DoctorId { get; set; }
        public string Nombre { get; set; }
        public string Concentracion { get; set; }
        public string Dosis { get; set; }
        public string Tiempo { get; set; }


    }
}
