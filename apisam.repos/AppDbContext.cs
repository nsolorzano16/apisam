using System;
using apisam.entities;
using Microsoft.EntityFrameworkCore;

namespace apisam.repos
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Paciente> Paciente { get; set; }
    }
}
