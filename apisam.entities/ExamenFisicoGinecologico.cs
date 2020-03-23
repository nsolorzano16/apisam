using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class ExamenFisicoGinecologico : RegistroBase
    {
        public ExamenFisicoGinecologico()
        {
        }
        [PrimaryKey, AutoIncrement]
        public int ExamenId { get; set; }
        public int PacienteId { get; set; }
        public int DoctorId { get; set; }
        public string Afu { get; set; }
        public string Pelvis { get; set; }
        public string Dorso { get; set; }
        public string Fcf { get; set; }
        public string Ap { get; set; }

    }

}
