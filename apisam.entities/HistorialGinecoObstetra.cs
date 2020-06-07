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
        public DateTime? Fum { get; set; }
        public string G { get; set; }
        public string P { get; set; }
        public string C { get; set; }
        public string Hv { get; set; }
        public string Hm { get; set; }
        public string Anticonceptivos { get; set; }
        public bool VacunaVph { get; set; }
        public bool Embarazo { get; set; }
        public string Fpp { get; set; }
        public string Afu { get; set; }
        public DateTime Presentacion { get; set; }
        public string MovimientosFetales { get; set; }
        public string Fcf { get; set; }
        public int? PreclinicaId { get; set; }




    }
}
