using System;
using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class Usuario : RegistroBase
    {
        public Usuario()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public int AsistenteId { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Identificacion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string ColegioNumero { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
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


    public class UserChangePassword
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string ModificadoPor { get; set; }


    }
}
