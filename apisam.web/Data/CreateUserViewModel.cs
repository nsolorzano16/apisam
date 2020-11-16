using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apisam.web.Data
{
    public class  CreateUserViewModel
    {
       
        [Required]
        public int RolId { get; set; }
        [Required]
        public string AsistenteId { get; set; }
        [Required]
        public int PlanId { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string PrimerApellido { get; set; }
        [Required]
        public string SegundoApellido { get; set; }
        [Required]
        public string Identificacion { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [Required]      
        public string Sexo { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
       
        public string Telefono2 { get; set; }
        [Required]
        public string ColegioNumero { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string CreadoPor { get; set; }           
        public string Notas { get; set; }
       
    }
}
