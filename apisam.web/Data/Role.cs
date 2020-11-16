using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apisam.web.Data
{
    public class UserRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
