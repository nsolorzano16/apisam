using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class Pais
    {
        public Pais()
        {
        }
        [PrimaryKey, AutoIncrement]
        public int PaisId { get; set; }
        public string Nombre { get; set; }
    }
}
