using System;
using System.ComponentModel.DataAnnotations;

namespace apisam.entities.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
        }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
