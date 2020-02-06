using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class Clinica : RegistroBase
    {
        public Clinica()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CentroMedico { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Email { get; set; }
    }
}
