using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class Municipio
    {
        public Municipio()
        {
        }
        [PrimaryKey, AutoIncrement]
        public int MunicipioId { get; set; }
        public int DepartamentoId { get; set; }
        public string Nombre { get; set; }
    }
}
