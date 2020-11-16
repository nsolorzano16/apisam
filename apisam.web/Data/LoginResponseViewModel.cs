using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apisam.web.Data
{
    public class LoginResponseViewModel
    {
        public SignInResult  Resultado { get; set; }
        public string Token { get; set; }
        public int Intentos { get; set; }
        public DateTime HoraDesbloqueo { get; set; }

    }
}
