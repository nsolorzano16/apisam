using System;
namespace apisam.entities.ViewModels
{
    public class PacientesViewModel : RegistroBase
    {
        public PacientesViewModel()
        {
        }

        public int PacienteId { get; set; }
        public int DoctorId { get; set; }
        public int PaisId { get; set; }
        public int ProfesionId { get; set; }
        public int EscolaridadId { get; set; }
        public int ReligionId { get; set; }
        public int GrupoSanguineoId { get; set; }
        public int GrupoEtnicoId { get; set; }
        public int DepartamentoId { get; set; }
        public int MunicipioId { get; set; }
        public int? DepartamentoResidenciaId { get; set; }
        public int? MunicipioResidenciaId { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Identificacion { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string EstadoCivil { get; set; }
        public int? Edad { get; set; }
        public string Direccion { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string NombreEmergencia { get; set; }
        public string TelefonoEmergencia { get; set; }
        public string Parentesco { get; set; }
        public bool MenorDeEdad { get; set; }
        public string NombreMadre { get; set; }
        public string IdentificacionMadre { get; set; }
        public string NombrePadre { get; set; }
        public string IdentificacionPadre { get; set; }
        public string CarneVacuna { get; set; }
        public string FotoUrl { get; set; }
        public string Pais { get; set; }
        public string Profesion { get; set; }
        public string Escolaridad { get; set; }
        public string Religion { get; set; }
        public string GrupoSanguineo { get; set; }
        public string GrupoEtnico { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }

    }
}
