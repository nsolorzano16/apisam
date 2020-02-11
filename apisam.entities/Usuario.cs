using System;
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




    }
}
