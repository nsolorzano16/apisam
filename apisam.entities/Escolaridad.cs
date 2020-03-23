using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class Escolaridad
    {
        public Escolaridad()
        {
        }
        [PrimaryKey, AutoIncrement]
        public int EscolaridadId { get; set; }
        public string Nombre { get; set; }
    }


}
