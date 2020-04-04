using System;
namespace apisam.entities.ViewModels
{
    public class PreclinicaViewModel : RegistroBase
    {
        public PreclinicaViewModel()
        {
        }

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
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Identificacion { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string EstadoCivil { get; set; }
        public int? Edad { get; set; }
        public bool MenorDeEdad { get; set; }
        public string NombreMadre { get; set; }
        public string IdentificacionMadre { get; set; }
        public string NombrePadre { get; set; }
        public string IdentificacionPadre { get; set; }
        public string CarneVacuna { get; set; }
        public string FotoUrl { get; set; }
        public string NotasPaciente { get; set; }
    }
}
