using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int DoctorId { get; set; }
        public int PaisId { get; set; }
        public int ProfesionId { get; set; }
        public int EscolaridadId { get; set; }
        public int ReligionId { get; set; }
        public int GrupoSanguineoId { get; set; }
        public int GrupoEtnicoId { get; set; }
        public int? DepartamentoId { get; set; }
        public int? MunicipioId { get; set; }
        public int? DepartamentoResidenciaId { get; set; }
        public int? MunicipioResidenciaId { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        [Unique]
        public string Identificacion { get; set; }
        [Unique]
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


        //[Reference]
        //public AntecedentesFamiliaresPersonales
        //    AntecedentesFamiliaresPersonales
        //{ get; set; }

        //[Reference]
        //public List<Diagnosticos> Diagnosticos { get; set; }

        //[Reference]
        //public List<ExamenFisico> ExamenesFisicos { get; set; }

        //[Reference]
        //public List<ExamenFisicoGinecologico> ExamenesGinecologicos { get; set; }

        //[Reference]
        //public List<FarmacosUsoActual> Farmacos { get; set; }

        //[Reference]
        //public Habitos Habito { get; set; }


        //[Reference]
        //public HistorialGinecoObstetra HistorialGinecoObstetra { get; set; }


        //[Reference]
        //public List<Preclinica> Preclinicas { get; set; }

        //[Reference]
        //public List<Notas> NotasLista { get; set; }

    }
}
