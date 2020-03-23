using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class Religion
    {
        public Religion()
        {
        }
        [PrimaryKey, AutoIncrement]
        public int ReligionId { get; set; }
        public string Nombre { get; set; }
    }
}
