using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace apisam.web.Data
{
    public class User : IdentityUser
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
        public string SegundoApellido { get; set; }
        [Required]
       [MaxLength(20)]
        public string Identificacion { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        [Required]
        [MaxLength(1)]
        public string Sexo { get; set; }
        public string Telefono2 { get; set; }
        [Required]
        public string ColegioNumero { get; set; }
        public string FotoUrl { get; set; }
        [Required]
        public bool Activo { get; set; }
        [Required]
        public string CreadoPor { get; set; }
        [Required]
        public DateTime CreadoFecha { get; set; }
        [Required]
        public string ModificadoPor { get; set; }
        [Required]
        public DateTime ModificadoFecha { get; set; }       
        public string Notas { get; set; }
    }


   
}


 