using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class Preclinica : RegistroBase
    {
        public Preclinica()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int PreclinicaId { get; set; }
        public int PacienteId { get; set; }
        public int DoctorId { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public int FrecuenciaRespiratoria { get; set; }
        public int RitmoCardiaco { get; set; }
        public int PresionSistolica { get; set; }
        public int PresionDiastolica { get; set; }
        public double IMC { get; set; }
        public bool Atendida { get; set; }
        public string PesoDescripcion { get; set; }





    }
}
