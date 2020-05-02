using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class HistorialGinecoObstetra : RegistroBase
    {
        public HistorialGinecoObstetra()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int HistorialId { get; set; }
        public int PacienteId { get; set; }
        public int DoctorId { get; set; }
        public int? PreclinicaId { get; set; }
        public DateTime? Menarquia { get; set; }
        public DateTime? Fur { get; set; }
        public string Sg { get; set; }
        public string G { get; set; }
        public string P { get; set; }
        public string C { get; set; }
        public string Hv { get; set; }
        public string Fpp { get; set; }
        public string Uc { get; set; }
        public DateTime? FechaMenopausia { get; set; }
        public string Anticonceptivo { get; set; }
        public string Hallazgo { get; set; }
        public string Vacunacion { get; set; }



    }
}
