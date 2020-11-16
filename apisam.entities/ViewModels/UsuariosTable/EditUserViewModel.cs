using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace apisam.entities.ViewModels.UsuariosTable
{
     public class EditUserViewModel : RegistroBase
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string  UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
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
 
        public string SegundoApellido { get; set; }
        [Required]
        public string Identificacion { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        public string Sexo { get; set; }

        public string Telefono2 { get; set; }
        [Required]
        public string ColegioNumero { get; set; }
        [Required]
        public string FotoUrl { get; set; }
  
        public DateTimeOffset LockoutEnd { get; set; }
        public int AccessFailedCount { get; set; }
        [NotMapped]
        public bool Locked { get; set; }

    }
}
