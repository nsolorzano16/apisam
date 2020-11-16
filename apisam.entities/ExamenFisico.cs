using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class ExamenFisico : RegistroBase
    {
        public ExamenFisico()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int ExamenFisicoId { get; set; }
        public int PacienteId { get; set; }
        public string DoctorId { get; set; }
        public string AspectoGeneral { get; set; }
        public string PielFaneras { get; set; }
        public string Cabeza { get; set; }
        public string Oidos { get; set; }
        public string Ojos { get; set; }
        public string Nariz { get; set; }
        public string Boca { get; set; }
        public string Cuello { get; set; }
        public string Torax { get; set; }
        public string Abdomen { get; set; }
        public string ColumnaVertebralRegionLumbar { get; set; }
        public string MiembrosInferioresSuperiores { get; set; }
        public string Genitales { get; set; }
        public string Neurologico { get; set; }
        public int PreclinicaId { get; set; }




    }
}
