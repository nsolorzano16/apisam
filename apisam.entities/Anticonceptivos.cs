using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class Anticonceptivos
    {
        [PrimaryKey, AutoIncrement]
        public int AnticonceptivoId { get; set; }
        public string Nombre { get; set; }
    }
}
