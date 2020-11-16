using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace apisam.entities.ViewModels
{
   public class ConfirmEmailViewModel
    {
        [Required]
        public string Token{ get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
