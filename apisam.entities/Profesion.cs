using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class Profesion
    {
        public Profesion()
        {
        }
        [PrimaryKey, AutoIncrement]
        public int ProfesionId { get; set; }
        public string Nombre { get; set; }
    }
}
