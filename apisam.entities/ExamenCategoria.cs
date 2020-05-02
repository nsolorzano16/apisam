using System;
using ServiceStack.DataAnnotations;

namespace apisam.entities
{
    public class ExamenCategoria
    {
        public ExamenCategoria()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int ExamenCategoriaId { get; set; }
        public string Nombre { get; set; }
    }
}
