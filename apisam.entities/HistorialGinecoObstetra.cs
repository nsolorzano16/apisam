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
        public int AnticonceptivoId { get; set; }
        public DateTime? FechaMenarquia { get; set; }
        public DateTime? Fum { get; set; }
        public int G { get; set; }
        public int P { get; set; }
        public int C { get; set; }
        public int Hv { get; set; }
        public int Hm { get; set; }
        public string DescripcionAnticonceptivos { get; set; }
        public bool VacunaVph { get; set; }
        public DateTime? FechaMenopausia { get; set; }
        public int? PreclinicaId { get; set; }
      
    }
}
