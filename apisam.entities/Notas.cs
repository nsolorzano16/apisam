using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class Notas : RegistroBase
    {
        public Notas()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int NotaId { get; set; }
        public int PacienteId { get; set; }
        public int? PreclinicaId { get; set; }
        public int DoctorId { get; set; }


    }
}
