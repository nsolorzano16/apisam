using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class Paciente : RegistroBase
    {
        public Paciente()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int PacienteId { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Identificacion { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string EstadoCivil { get; set; }
        public string LugarNacimiento { get; set; }
        public int Edad { get; set; }
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



    }
}
