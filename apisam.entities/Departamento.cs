using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class Departamento
    {
        public Departamento()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int DepartamentoId { get; set; }
        public string Nombre { get; set; }
    }
}
