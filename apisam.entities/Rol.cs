using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class Rol
    {
        public Rol()
        {
        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
