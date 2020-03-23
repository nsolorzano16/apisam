using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class Habitos : RegistroBase
    {
        public Habitos()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int HabitoId { get; set; }
        public int PacienteId { get; set; }
        public int DoctorId { get; set; }
        public bool? Cafe { get; set; }
        public bool? Cigarrillo { get; set; }
        public int? TazasCafe { get; set; }
        public int? CantidadCigarrillo { get; set; }


    }
}
