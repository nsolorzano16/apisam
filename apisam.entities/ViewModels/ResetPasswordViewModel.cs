﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace apisam.entities.ViewModels
{
  public  class ResetPasswordViewModel
    {
        [Required]
        public string Email { get; set; }
      
    }
}
