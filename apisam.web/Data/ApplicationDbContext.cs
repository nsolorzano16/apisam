using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apisam.web.Data
{
    public class ApplicationDbContext : IdentityDbContext<User,UserRole,string> 
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Identificacion)
                .IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.ColegioNumero).IsUnique();

      

            base.OnModelCreating(modelBuilder);
        }
    }
}
