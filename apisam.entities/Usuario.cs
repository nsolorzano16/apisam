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
        [Unique]
        public string Identificacion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        [Unique]
        public string ColegioNumero { get; set; }
        [Unique]
        public string Email { get; set; }
        [Unique]
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string FotoUrl { get; set; }

    }


    public class UserChangePassword
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string ModificadoPor { get; set; }


    }
}
